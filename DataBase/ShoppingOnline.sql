go
use master 
if exists (select name from sys.databases where name = 'COVID')
drop database COVID


go
create database COVID

on (name = 'COVID_DATA', filename='D:\COVID.MDF')
log on (name = 'COVID_LOG', filename='D:\COVID.LDF')

go 
use COVID

CREATE TABLE PRODUCTTYPE
(
	PRODUCTTYPEID int Identity PRIMARY KEY,
	PRODUCTTYPENAME NVARCHAR(50),
	STATUSPRODUCTTYPE BIT DEFAULT 1
)

go
CREATE TABLE BRAND 
(
	BRANDID INT IDENTITY PRIMARY KEY,
	BRANDNAME NVARCHAR(50),
	STATUSBRAND BIT DEFAULT 1
)

go
CREATE TABLE PRODUCT
(
	PRODUCTID INT IDENTITY PRIMARY KEY,
	PRODUCTNAME NVARCHAR(50),
	BRANDID INT,
	PRODUCTTYPEID INT,
	MAINPIC NVARCHAR(MAX),
	PIC1 NVARCHAR(MAX),
	PIC2 NVARCHAR(MAX),
	PIC3 NVARCHAR(MAX),
	PIC4 NVARCHAR(MAX),
	STATUSPRODUCT BIT DEFAULT 1,
	PRODUCTPRICE INT,
	PRODUCTVIEW INT DEFAULT 0,
	PRODUCTVIEWLIKE INT DEFAULT 0,
	PRODUCTAMOUNT INT,
	DECRIPTION NVARCHAR(MAX),
	[DATEADD] DATE,
	foreign key (BRANDID) references Brand(BRANDID) on update cascade,
	foreign key (PRODUCTTYPEID) references PRODUCTTYPE(PRODUCTTYPEID) on update cascade,
)

go
CREATE TABLE CONFIG
(
	CONFIGNAME NVARCHAR(50) PRIMARY KEY,
	DECRIPTIONCONFIGNAME NVARCHAR(MAX),
)

go
CREATE TABLE CONFIGDETAIL
(
	PRODUCTID INT ,
	CONFIGNAME NVARCHAR(50),
	INFORMATION NVARCHAR(MAX),
	foreign key (PRODUCTID) references PRODUCT(PRODUCTID) on update cascade,
	foreign key (CONFIGNAME) references CONFIG(CONFIGNAME) on update cascade,
)

go
CREATE TABLE ACCOUNT
(
	[USER] VARCHAR(30) PRIMARY KEY,
	[PASSWORD] VARCHAR(30) CHECK (LEN([PASSWORD])> 7),
	FULLNAME NVARCHAR(100),
	STATUSACCOUNT BIT DEFAULT 1 ,
	PHONENUMBER VARCHAR(10) CHECK (LEN(PHONENUMBER) = 10)

)

go
CREATE TABLE ACCOUNT_ADMIN
(
	[USER] VARCHAR(30) PRIMARY KEY,
	[PASSWORD] VARCHAR(30) ,
	FULLNAME NVARCHAR(100),
	STATUSACCOUNT BIT DEFAULT 1 ,
)

go
CREATE TABLE COMMENT
(
	COMMENTID INT IDENTITY PRIMARY KEY,
	PRODUCTID INT ,
	[USER] VARCHAR(30),
	COMMENTTEXT NVARCHAR(MAX),
	foreign key ([USER]) references ACCOUNT([USER]) on update cascade ,
	foreign key (PRODUCTID) references PRODUCT(PRODUCTID) on update cascade ,
)

go
CREATE TABLE ACCOUNTLIKE
(
	[USER] VARCHAR(30),
	PRODUCTID INT,
	PRIMARY KEY ([USER] , PRODUCTID),
	foreign key ([USER]) references ACCOUNT([USER]) on update cascade ,
	foreign key (PRODUCTID) references PRODUCT(PRODUCTID) on update cascade ,
)

go
CREATE TABLE CART 
(
	[USER] VARCHAR(30),
	PRODUCTID INT,
	AMOUNT INT,
	
	PRIMARY KEY([USER], PRODUCTID),

	foreign key ([USER]) references ACCOUNT([USER]) on update cascade ,
	foreign key (PRODUCTID) references PRODUCT(PRODUCTID) on update cascade ,
)


go
CREATE TABLE VOUCHER
(
	VOUCHERID INT IDENTITY PRIMARY KEY,
	DECRIPTIONVOUCHER NVARCHAR(MAX),
	DATEENDTIRE DATE,
	STATUSVOUCHER BIT DEFAULT 1
)

go

CREATE TABLE BILL
(
	BILLID INT IDENTITY PRIMARY KEY,
	[USER] VARCHAR(30),
	[ADDRESS] NVARCHAR(MAX),
	[PHONENUMBERRECIVE] VARCHAR(10) CHECK (LEN([PHONENUMBERRECIVE]) = 10),
	DATECREATE DATE DEFAULT GETDATE(),
	VOUCHERID INT,

	foreign key (VOUCHERID) references VOUCHER(VOUCHERID) on update cascade ,
)




CREATE TABLE VOCHERDETAIL
(
	VOUCHERID INT,
	[USER] VARCHAR(30),
	PRIMARY KEY (VOUCHERID,[USER]),
	foreign key (VOUCHERID) references VOUCHER(VOUCHERID) on update cascade ,
	foreign key ([USER]) references ACCOUNT([USER]) on update cascade ,

)

go
CREATE TABLE BILLDETAIL
(
	BILLID INT,
	PRODUCTID INT,

	PRIMARY KEY (PRODUCTID,BILLID),
	foreign key (BILLID) references BILL(BILLID) on update cascade ,
	foreign key (PRODUCTID) references PRODUCT(PRODUCTID) on update cascade ,

)

----------------------------------------------------TRIGER PASSWORD----------------------------------------------------

go
CREATE trigger [dbo].[MaHoaPassword] on [dbo].[Account] after insert, update as
begin        
		if not exists (select * from deleted)
			update Account
			set [password] = convert( VARCHAR(30), hashbytes('MD5', B.[password]), 2)
			from Account A
			join inserted B on A.[user] = B.[user]			
		else
			update Account
			set [password] = convert( VARCHAR(30), hashbytes('MD5', B.[password]), 2)	
			from deleted A
			join Account B on A.[User] = B.[User] 
			where A.[Password] != B.[Password]
end 

----------------------------------------------------QUERY INSERT----------------------------------------------------

GO

SET DATEFORMAT DMY 

go --(brandname, STATUSBRAND)
insert into Brand values(N'SamSung','1')  --1 
insert into Brand values(N'iPhone','1')	  --2
insert into Brand values(N'Redmi','1')	  --3
Insert into Brand values (N'Oppo','1')    --4
Insert into Brand values (N'Apple','1')   --5

go --(PRODUCTTYPENAME, STATUSPRODUCTTYPE)
insert into ProductType values(N'Laptop','1')     --1
insert into ProductType values(N'SmartPhone','1') --2
insert into ProductType values(N'Tablet','1')	  --3
insert into ProductType values(N'SmartWatch','1') --4
insert into ProductType values(N'HeadPhone','1')  --5

go --(PRODUCTNAME, BRANDID, PRODUCTTYPEID, MAINPIC, PIC1, PIC2, PIC3, PIC4, STATUSPRODUCT, PRODUCTPRICE, PRODUCTVIEW, PRODUCTVIEWLIKE, PRODUCTAMOUNT, DECRIPTION, [DATEADD])
--insert into PRODUCT values (N'Vsmart Joy 4',4,2,'Vsmart/Joy4/Main.jpg',null,null,null,null,1,'3590000','0','0','100',N'Điện thoại này từ VIệt Nam Chẩt lượng cao')
insert into PRODUCT values (N'Apple watch series 6',5,4,'Asset/images/SmartWatch/AppleWatch/apple-watch-series-6-1.jpg','Asset/images/SmartWatch/AppleWatch/apple-watch-series-6-2.jpg','Asset/images/SmartWatch/AppleWatch/apple-watch-series-6-3.jpg','Asset/images/SmartWatch/AppleWatch/apple-watch-series-6-4.jpg','Asset/images/SmartWatch/AppleWatch/apple-watch-series-6-5.jpg',1,'18530000','0','0','100',N'Đồng hồ xịn nè lo mà mô tả nó đi',GETDATE())

go --(CONFIGNAME, DECRIPTIONCONFIGNAME)
INSERT INTO CONFIG VALUES ('RAM',N'BỘ nhớ đệm')                 --1
INSERT INTO CONFIG VALUES ('ROM',N'Ổ cứng')						--2
INSERT INTO CONFIG VALUES ('SCREEN',N'Công nghệ màn hình')		--3
INSERT INTO CONFIG VALUES ('RESOLUTION',N'Độ phân giải')		--4
INSERT INTO CONFIG VALUES ('FONTCAM',N'Cammera trước')			--5
INSERT INTO CONFIG VALUES ('BACKCAM',N'Cammera sau')			--6
INSERT INTO CONFIG VALUES ('CPU',N'Chip xử lý')					--7
INSERT INTO CONFIG VALUES ('GPU',N'Chip đồ họa')				--8
INSERT INTO CONFIG VALUES ('OS',N'Hệ Điều hành')				--9
INSERT INTO CONFIG VALUES ('Sim',N'')							--10
INSERT INTO CONFIG VALUES ('DESIGN',N'Thiết kế')				--11
INSERT INTO CONFIG VALUES ('MATERIAL',N'Chất Liệu')				--12
INSERT INTO CONFIG VALUES ('SIZE',N'Kich thước')				--13
INSERT INTO CONFIG VALUES ('RELEASEDAY',N'Thời điểm ra mắt')	--14
INSERT INTO CONFIG VALUES ('SCREENSIZE',N'Kích thước màn hình')	--15
INSERT INTO CONFIG VALUES ('STRAPABLE',N'Có thể thay dây')	    --16
INSERT INTO CONFIG VALUES ('WATERPROOF',N'Chống nước')			--17
INSERT INTO CONFIG VALUES ('SENSOR',N'Cảm biến')				--18
INSERT INTO CONFIG VALUES ('WEIGHT',N'Trọng lượng')				--19
INSERT INTO CONFIG VALUES ('CALLABLE',N'Có thể gọi')			--20
INSERT INTO CONFIG VALUES ('ORTHERFEATURE',N'Tính năng khác')   --21
INSERT INTO CONFIG VALUES ('Pin',N'Thời lượng pin')   --21






go --(productid, cofigname, inf)
/*INSERT INTO CONFIGDETAIL VALUES ('1',N'RAM',N'6 GB')
INSERT INTO CONFIGDETAIL VALUES ('1',N'ROM',N'64 GB')
INSERT INTO CONFIGDETAIL VALUES ('1',N'SCREEN',N'LTPS IPS LCD')
INSERT INTO CONFIGDETAIL VALUES ('1',N'RESOLUTION',N'Full HD+ (1080 x 2340 Pixels)')
INSERT INTO CONFIGDETAIL VALUES ('1',N'FONTCAM',N'13 MP')
INSERT INTO CONFIGDETAIL VALUES ('1',N'BACKCAM',N'Chính 16 MP & Phụ 8 MP, 2 MP, 2 MP')
INSERT INTO CONFIGDETAIL VALUES ('1',N'CPU',N'6 Snapdragon 665')
INSERT INTO CONFIGDETAIL VALUES ('1',N'OS',N'Android 10')
INSERT INTO CONFIGDETAIL VALUES ('1',N'GPU',N'Adreno 610')
INSERT INTO CONFIGDETAIL VALUES ('1',N'Sim',N'2 Nano Sim')
INSERT INTO CONFIGDETAIL VALUES ('1',N'DESIGN',N'Nguyên Khối')
INSERT INTO CONFIGDETAIL VALUES ('1',N'MATERIAL',N'Khung & Mặt lưng nhựa')
INSERT INTO CONFIGDETAIL VALUES ('1',N'SIZE',N'Dài 163.65 mm - Ngang 77.65 mm - Dày 9.15 mm - Nặng 216.4 g')
INSERT INTO CONFIGDETAIL VALUES ('1',N'RELEASEDAY',N'12/2020')*/

--Apple watch series 6
INSERT INTO CONFIGDETAIL VALUES ('1',N'SCREENSIZE',N'1.78 inch')
INSERT INTO CONFIGDETAIL VALUES ('1',N'ROM',N'32 GB')
INSERT INTO CONFIGDETAIL VALUES ('1',N'SCREEN',N'Retina LTPO OLED, 16 triệu màu,mặt lưng kính Sapphire, 3D Touch, độ sáng 1000 nits')
INSERT INTO CONFIGDETAIL VALUES ('1',N'RESOLUTION',N'448 x 368 pixels')
INSERT INTO CONFIGDETAIL VALUES ('1',N'OS',N'WatchOS')
INSERT INTO CONFIGDETAIL VALUES ('1',N'STRAPABLE',N'Có')
INSERT INTO CONFIGDETAIL VALUES ('1',N'Pin',N'- Thời lượng pin 18 giờ - Sạc đầy trong 1.5 giờ')
INSERT INTO CONFIGDETAIL VALUES ('1',N'WATERPROOF',N'Chống nước 5 ATM (50m)')
INSERT INTO CONFIGDETAIL VALUES ('1',N'ORTHERFEATURE',N'- Đo điện tâm đồ ECG- Nghe nhạc trên Apple Music- Các tính năng luyện tập thể dục- Thay đổi mặt đồng hồ')
INSERT INTO CONFIGDETAIL VALUES ('1',N'CPU',N'Apple S6')
INSERT INTO CONFIGDETAIL VALUES ('1',N'SENSOR',N'Cảm biến tiệm cận, cảm biến gia tốc, con quay hồi chuyển, gia tốc kế, la bàn, cảm biến nhịp tim')
INSERT INTO CONFIGDETAIL VALUES ('1',N'SIZE',N'40 x 34 x 10.4 mm')
INSERT INTO CONFIGDETAIL VALUES ('1',N'WEIGHT',N'39.7g')




go --([USER], [PASSWORD], FULLNAME, STATUSACCOUNT, PHONENUMBER)
Insert into ACCOUNT values ('Phus','123456789',N'Trương Gia Phú','1','0123456789') 
Insert into ACCOUNT values ('Syx','123456789',N'Nguyễn Quang Sỹ','1','0987654321') 

go --([USER], [PASSWORD], FULLNAME, STATUSACCOUNT)
Insert into ACCOUNT_ADMIN values (N'admin','admin',N'Trí Đẹp Trai','1') 

go --(COMMENTID, PRODUCTID, [USER], COMMENTTEXT)
Insert into COMMENT values (1,'Phus','Hàng này cùi vl')
Insert into COMMENT values (1,'Syx','Hàng này xịn đấy')

go --([USER], PRODUCTID)
Insert into ACCOUNTLIKE values ('Syx',1)

go --([USER], PRODUCTID, AMOUNT)
Insert into CART values ('Phus',1,20)


go --(VOUCHERID, DECRIPTIONVOUCHER, DATEENTIRE, STATUSVOUCHER)
INSERT INTO VOUCHER VALUES ('giảm giá 100%','30/12/2021',1)

go --(VOUCHERID, [USER])
INSERT INTO  VOCHERDETAIL VALUES ('1','syx')

go --(BILLID, [USER], [ADDRESS], [PHONENUMBERRECIVE], DATECREATE, VOUCHERID)
Insert into BILL values ('Phus','7 núi','0123456789',GETDATE(),1)

go --(BILLID, PRODUCTID)
Insert into BILLDETAIL values (1,1)






	/*PRODUCTID INT IDENTITY PRIMARY KEY,
	PRODUCTNAME NVARCHAR(50),
	BRANDID INT,
	PRODUCTTYPEID INT,
	MAINPIC NVARCHAR(MAX),
	PIC1 NVARCHAR(MAX),
	PIC2 NVARCHAR(MAX),
	PIC3 NVARCHAR(MAX),
	PIC4 NVARCHAR(MAX),
	STATUSPRODUCT BIT DEFAULT 1,
	PRODUCTPRICE INT,
	PRODUCTVIEW INT,
	PRODUCTVIEWLIKE INT,
	PRODUCTAMOUNT INT,
	DECRIPTION NVARCHAR(MAX),
	foreign key (BRANDID) references Brand(BRANDID) on update cascade ,
	foreign key (PRODUCTTYPEID) references PRODUCTTYPE(PRODUCTTYPEID) on update cascade,*/

/*
CREATE TABLE RELATIVEPRODUCT
(
	PRODUCTID INT,
	PRODUCTREL INT,
	PRIMARY KEY (PRODUCTID,PRODUCTREL),
	foreign key (PRODUCTID) references PRODUCT(PRODUCTID) on update cascade ,
)*/














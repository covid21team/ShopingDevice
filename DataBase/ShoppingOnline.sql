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
create table TEMPPRODUCT
(
	PRODUCTTYPEID int,
	BRANDID INT,
	TEMPPRODUCTSTATUS bit,
	primary key(PRODUCTTYPEID, BRANDID),
	foreign key (BRANDID) references Brand(BRANDID) on update cascade,
	foreign key (PRODUCTTYPEID) references PRODUCTTYPE(PRODUCTTYPEID) on update cascade,
)

go
CREATE TABLE PRODUCT
(
	PRODUCTID INT IDENTITY PRIMARY KEY,
	PRODUCTNAME NVARCHAR(50),
	BRANDID INT,
	PRODUCTTYPEID INT,
	MAINPIC int,
	STATUSPRODUCT BIT DEFAULT 1,
	PRODUCTPRICE INT,
	--PRODUCTVIEW INT DEFAULT 0,
	PRODUCTAMOUNT INT,
	DECRIPTION NVARCHAR(MAX),
	[DATEADD] DATETIME, -- Ngày nhập hàng vào
	foreign key (BRANDID) references Brand(BRANDID) on update cascade,
	foreign key (PRODUCTTYPEID) references PRODUCTTYPE(PRODUCTTYPEID) on update cascade,
)

go
create table PICTURE
(
	PictureId INT IDENTITY PRIMARY KEY,
	[Path] Varchar(max), -- dùng vào việc xóa ảnh 
	Link Varchar(max), -- dùng cho moblie lấy ảnh
)

go
create table PicProduct
(
	productId int,
	pictureId int,
	[dateAdd] datetime,
	primary key(productId, pictureId),

	foreign key (productId) references PRODUCT(PRODUCTID) on update cascade,
	foreign key (pictureId) references PICTURE(PictureId) on update cascade,
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
	PRIMARY KEY (PRODUCTID,CONFIGNAME),
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
	SEX BIT,					 -- 1 LÀ NAM 0 LÀ NỮ
	DATAOFBIRTH DATE,
	STATUSACCOUNT BIT DEFAULT 1 ,
	PHONENUMBER VARCHAR(10) CHECK (LEN(PHONENUMBER) = 10),
	EMAIL Varchar(100),
)

go
CREATE TABLE ADDRESS_SHIP
(
	ADDRESSID INT IDENTITY PRIMARY KEY,
	[USER] VARCHAR(30),
	FULLNAME NVARCHAR(50),
	PHONE VARCHAR(10) CHECK (LEN(PHONE) = 10),
	CITY NVARCHAR(MAX),
	DISTRICT NVARCHAR(MAX),
	WARDS NVARCHAR(MAX),
	[ADDRESS] NVARCHAR(MAX),
	[DEFAULT] BIT,
	ADDRESS_STATUS BIT,

	foreign key ([USER]) references ACCOUNT([USER]) on update cascade,
)

CREATE TABLE VIEWNUMBER
(
	[USER] VARCHAR(30),
	PRODUCTID INT,
	DATESEEN DATETIME, -- Đây là ngày khách hàng xem sản phẩm Tris cũng không muốn để đâu này là ép buộc đó
	primary key([USER], PRODUCTID),
	foreign key ([USER]) references ACCOUNT([USER]) on update cascade,
	foreign key (PRODUCTID) references PRODUCT(PRODUCTID) on update cascade,
)


CREATE TABLE RATINGPRODUCT
(
	[USER] VARCHAR(30),
	PRODUCTID INT,
	RATE INT, -- XẾP TỪ 1 TỚI 5 SAO 
	primary key([USER], PRODUCTID),
	foreign key ([USER]) references ACCOUNT([USER]) on update cascade,
	foreign key (PRODUCTID) references PRODUCT(PRODUCTID) on update cascade,
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
	DATECOMMENT DATETIME,
	foreign key ([USER]) references ACCOUNT([USER]) on update cascade ,
	foreign key (PRODUCTID) references PRODUCT(PRODUCTID) on update cascade ,
)

go
CREATE TABLE ACCOUNTLIKE
(
	[USER] VARCHAR(30),
	PRODUCTID INT,
	DATELIKE DATETIME, -- lí do tồn tại thì có gì liên hệ Sỹ để biết câu trả lời
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
	PRODUCTSTATUS bit, -- status này giúp trong việc check box được tích hay không, và lúc chuyển qua checkout cần load những sản phẩm muốn mua trong háo đơn đó
	PRIMARY KEY([USER], PRODUCTID),

	foreign key ([USER]) references ACCOUNT([USER]) on update cascade ,
	foreign key (PRODUCTID) references PRODUCT(PRODUCTID) on update cascade ,
)

go
CREATE TABLE VOUCHER
(
	VOUCHERID INT IDENTITY PRIMARY KEY,
	DECRIPTIONVOUCHER NVARCHAR(MAX),
	DATEENDTIRE DATETIME,
	STATUSVOUCHER BIT DEFAULT 1
)

-- Lí do bảng bill lại có nhưng thuộc tính của địa chỉ mà không đơn giản gọi cái id thì sẽ có hết thông tin
-- Vì khi tạo bill thì địa chỉ đã xác định rõ ràng
-- Nếu kết nối với bảng địa chỉ thì khi thay đổi value của bảng địa chỉ thì đồng nghĩa bill bị thay đổi địa chỉ
-- Dẫn đến việc xác định nơi nhận cái hóa đơn có thể bị thay đổi mọi lúc, khi xuất bill cho khách hàng sẽ không đúng chỉ mong muốn của khách hàng
go
CREATE TABLE BILL
(
	BILLID INT IDENTITY PRIMARY KEY,
	[USER] VARCHAR(30),
	DATECREATE DATETIME DEFAULT GETDATE(),
	VOUCHERID INT,
	TOTALBILL bigint,
	BIllSTATUS int, --1: chờ xét duyệt 2: đang giao 3: đã giao 4: hủy đơn
	NOTE nvarchar(max),
	FULLNAME NVARCHAR(50),
	PHONE VARCHAR(10) CHECK (LEN(PHONE) = 10),
	CITY NVARCHAR(MAX),
	DISTRICT NVARCHAR(MAX),
	WARDS NVARCHAR(MAX),
	[ADDRESS] NVARCHAR(MAX),

	foreign key ([USER]) references ACCOUNT([USER]) on update cascade,
	foreign key (VOUCHERID) references VOUCHER(VOUCHERID) on update cascade ,
)

CREATE TABLE VOCHERDETAIL
(
	VOUCHERID INT,
	[USER] VARCHAR(30),
	DATEENDTIRE DATETIME, -- ngày hết hạn sở hữu của khách hàng về voucher, do sẽ có một số voucher tồn tại vĩnh viễn
	PRIMARY KEY (VOUCHERID,[USER]),
	foreign key (VOUCHERID) references VOUCHER(VOUCHERID) on update cascade ,
	foreign key ([USER]) references ACCOUNT([USER]) on update cascade ,

)

go
CREATE TABLE BILLDETAIL
(
	BILLID INT,
	PRODUCTID INT,
	AMOUNT INT,
	PRIMARY KEY (PRODUCTID,BILLID),
	foreign key (BILLID) references BILL(BILLID) on update cascade ,
	foreign key (PRODUCTID) references PRODUCT(PRODUCTID) on update cascade ,

)

SET DATEFORMAT DMY

----------------------------------------------------TRIGER PASSWORD----------------------------------------------------

go
CREATE trigger [TSPTeam].[MaHoaPassword] on [TSPTeam].[Account] after insert, update as
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

go
CREATE trigger [TSPTeam].[MaHoaPasswordAdmin] on [TSPTeam].[ACCOUNT_ADMIN] after insert, update as
begin        
		if not exists (select * from deleted)
			update ACCOUNT_ADMIN
			set [password] = convert( VARCHAR(30), hashbytes('MD5', B.[password]), 2)
			from ACCOUNT_ADMIN A
			join inserted B on A.[user] = B.[user]			
		else
			update ACCOUNT_ADMIN
			set [password] = convert( VARCHAR(30), hashbytes('MD5', B.[password]), 2)	
			from deleted A
			join ACCOUNT_ADMIN B on A.[User] = B.[User] 
			where A.[Password] != B.[Password]
end 

----------------------------------------------------FUNCTION PASSWORD----------------------------------------------------

go

create procedure CheckLogin(@username varchar(10), @password varchar(50))
as
begin
	--declare @roleName nvarchar(50)
	set @password = convert(varchar(30), hashbytes('MD5', @password), 2) 
	select [USER]
	from ACCOUNT
	where @username = [USER] and @password = [PASSWORD]
end

go
create procedure CheckLoginAdmin(@username varchar(10), @password varchar(50))
as
begin
	--declare @roleName nvarchar(50)
	set @password = convert(varchar(30), hashbytes('MD5', @password), 2) 
	select [USER]
	from ACCOUNT_ADMIN
	where @username = [USER] and @password = [PASSWORD]
end

----------------------------------------------------QUERY INSERT----------------------------------------------------

GO

SET DATEFORMAT DMY 

go --(brandname, STATUSBRAND)
insert into Brand values(N'SamSung','1')  --1 
insert into Brand values(N'Apple','1')	  --2
insert into Brand values(N'Xiaomi','1')	  --3
Insert into Brand values (N'Oppo','1')    --4
Insert into Brand values (N'Acer','1')   --5
Insert into Brand values (N'Msi','1')   --5
Insert into Brand values (N'Sony','1')   --5
Insert into Brand values (N'Lenovo','1')   --5
Insert into Brand values (N'IPad','1')   --5
Insert into Brand values (N'Asus','1')   --5

go --(PRODUCTTYPENAME, STATUSPRODUCTTYPE)
insert into ProductType values(N'Laptop','1')     --1
insert into ProductType values(N'Điện thoại','1') --2
insert into ProductType values(N'Tablet','1')	  --3
insert into ProductType values(N'Đồng hồ','1') --4
insert into ProductType values(N'Tai nghe','1')  --5

go --(PRODUCTTYPEID, BRANDID, Status)
insert into TEMPPRODUCT values(1,2,1)
insert into TEMPPRODUCT values(1,5,1)
insert into TEMPPRODUCT values(2,1,1)
insert into TEMPPRODUCT values(2,2,1)
insert into TEMPPRODUCT values(2,3,1)
insert into TEMPPRODUCT values(2,4,1)
insert into TEMPPRODUCT values(3,1,1)
insert into TEMPPRODUCT values(3,2,1)
insert into TEMPPRODUCT values(4,1,1)
insert into TEMPPRODUCT values(4,2,1)
insert into TEMPPRODUCT values(4,3,1)
insert into TEMPPRODUCT values(5,2,1)
insert into TEMPPRODUCT values(5,3,1)
insert into TEMPPRODUCT values(1,6,1)
insert into TEMPPRODUCT values(5,7,1)
insert into TEMPPRODUCT values(3,8,1)
insert into TEMPPRODUCT values(3,9,1)
insert into TEMPPRODUCT values(1,10,1)


go --(CONFIGNAME, DECRIPTIONCONFIGNAME)
INSERT INTO CONFIG VALUES ('RAM',N'Bộ nhớ đệm')                 --1
INSERT INTO CONFIG VALUES ('ROM',N'Ổ cứng')						--2
INSERT INTO CONFIG VALUES ('SCREEN',N'Công nghệ màn hình')		--3
INSERT INTO CONFIG VALUES ('RESOLUTION',N'Độ phân giải')		--4
INSERT INTO CONFIG VALUES ('FONTCAM',N'Cammera trước')			--5
INSERT INTO CONFIG VALUES ('BACKCAM',N'Cammera sau')			--6
INSERT INTO CONFIG VALUES ('CPU',N'Chip xử lý')					--7
INSERT INTO CONFIG VALUES ('GPU',N'Chip đồ họa')				--8
INSERT INTO CONFIG VALUES ('OS',N'Hệ Điều hành')				--9
INSERT INTO CONFIG VALUES ('Sim',N'Sim')					    --10
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
INSERT INTO CONFIG VALUES ('Pin',N'Thời lượng pin')				--22
INSERT INTO CONFIG VALUES ('BlueTooth',N'Loại BlueTooth')		--23
INSERT INTO CONFIG VALUES ('Wifi',N'Loại Wifi')					--24

go --([USER], [PASSWORD], FULLNAME, STATUSACCOUNT)
Insert into ACCOUNT_ADMIN values (N'admin','admin',N'Trang Chủ','1') 

go --(VOUCHERID, DECRIPTIONVOUCHER, DATEENTIRE, STATUSVOUCHER)
INSERT INTO VOUCHER VALUES ('giảm giá 100%','30/12/2021',1)
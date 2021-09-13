using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Database
{
    public partial class COVIDContext : DbContext
    {

        public COVIDContext(DbContextOptions<COVIDContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountAdmin> AccountAdmin { get; set; }
        public virtual DbSet<Accountlike> Accountlike { get; set; }
        public virtual DbSet<AddressShip> AddressShip { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Billdetail> Billdetail { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<Configdetail> Configdetail { get; set; }
        public virtual DbSet<PicProduct> PicProduct { get; set; }
        public virtual DbSet<Picture> Picture { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Producttype> Producttype { get; set; }
        public virtual DbSet<Ratingproduct> Ratingproduct { get; set; }
        public virtual DbSet<Tempproduct> Tempproduct { get; set; }
        public virtual DbSet<Viewnumber> Viewnumber { get; set; }
        public virtual DbSet<Vocherdetail> Vocherdetail { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }
        public virtual DbSet<WwBaucuaAccount> WwBaucuaAccount { get; set; }
        public virtual DbSet<WwBaucuaAnimal> WwBaucuaAnimal { get; set; }
        public virtual DbSet<WwBaucuaChat> WwBaucuaChat { get; set; }
        public virtual DbSet<WwBaucuaColor> WwBaucuaColor { get; set; }
        public virtual DbSet<WwBaucuaPlayer> WwBaucuaPlayer { get; set; }
        public virtual DbSet<WwBaucuaPlayerdetail> WwBaucuaPlayerdetail { get; set; }
        public virtual DbSet<WwBaucuaRoom> WwBaucuaRoom { get; set; }
        public virtual DbSet<WwBill> WwBill { get; set; }
        public virtual DbSet<WwBillDetail> WwBillDetail { get; set; }
        public virtual DbSet<WwCustomer> WwCustomer { get; set; }
        public virtual DbSet<WwProduct> WwProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "TSPTeam");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.User);

                entity.ToTable("ACCOUNT");

                entity.Property(e => e.User)
                    .HasColumnName("USER")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Dataofbirth)
                    .HasColumnName("DATAOFBIRTH")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname)
                    .HasColumnName("FULLNAME")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .HasColumnName("PHONENUMBER")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Sex).HasColumnName("SEX");

                entity.Property(e => e.Statusaccount)
                    .HasColumnName("STATUSACCOUNT")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AccountAdmin>(entity =>
            {
                entity.HasKey(e => e.User);

                entity.ToTable("ACCOUNT_ADMIN");

                entity.Property(e => e.User)
                    .HasColumnName("USER")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Fullname)
                    .HasColumnName("FULLNAME")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Statusaccount)
                    .HasColumnName("STATUSACCOUNT")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Accountlike>(entity =>
            {
                entity.HasKey(e => new { e.User, e.Productid });

                entity.ToTable("ACCOUNTLIKE");

                entity.Property(e => e.User)
                    .HasColumnName("USER")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

                entity.Property(e => e.Datelike)
                    .HasColumnName("DATELIKE")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Accountlike)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ACCOUNTLI__PRODU__4D2A7347");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Accountlike)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ACCOUNTLIK__USER__4C364F0E");
            });

            modelBuilder.Entity<AddressShip>(entity =>
            {
                entity.HasKey(e => e.Addressid);

                entity.ToTable("ADDRESS_SHIP");

                entity.Property(e => e.Addressid).HasColumnName("ADDRESSID");

                entity.Property(e => e.Address).HasColumnName("ADDRESS");

                entity.Property(e => e.AddressStatus).HasColumnName("ADDRESS_STATUS");

                entity.Property(e => e.City).HasColumnName("CITY");

                entity.Property(e => e.Default).HasColumnName("DEFAULT");

                entity.Property(e => e.District).HasColumnName("DISTRICT");

                entity.Property(e => e.Fullname)
                    .HasColumnName("FULLNAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("PHONE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.User)
                    .HasColumnName("USER")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Wards).HasColumnName("WARDS");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.AddressShip)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("FK__ADDRESS_SH__USER__3B0BC30C");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("BILL");

                entity.Property(e => e.Billid).HasColumnName("BILLID");

                entity.Property(e => e.Address).HasColumnName("ADDRESS");

                entity.Property(e => e.BillStatus).HasColumnName("BIllSTATUS");

                entity.Property(e => e.City).HasColumnName("CITY");

                entity.Property(e => e.Datecreate)
                    .HasColumnName("DATECREATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.District).HasColumnName("DISTRICT");

                entity.Property(e => e.Fullname)
                    .HasColumnName("FULLNAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Note).HasColumnName("NOTE");

                entity.Property(e => e.Phone)
                    .HasColumnName("PHONE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Totalbill).HasColumnName("TOTALBILL");

                entity.Property(e => e.User)
                    .HasColumnName("USER")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Voucherid).HasColumnName("VOUCHERID");

                entity.Property(e => e.Wards).HasColumnName("WARDS");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("FK__BILL__USER__589C25F3");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.Voucherid)
                    .HasConstraintName("FK__BILL__VOUCHERID__59904A2C");
            });

            modelBuilder.Entity<Billdetail>(entity =>
            {
                entity.HasKey(e => new { e.Productid, e.Billid });

                entity.ToTable("BILLDETAIL");

                entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

                entity.Property(e => e.Billid).HasColumnName("BILLID");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.Billdetail)
                    .HasForeignKey(d => d.Billid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BILLDETAI__BILLI__603D47BB");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Billdetail)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BILLDETAI__PRODU__61316BF4");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("BRAND");

                entity.Property(e => e.Brandid).HasColumnName("BRANDID");

                entity.Property(e => e.Brandname)
                    .HasColumnName("BRANDNAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Statusbrand)
                    .HasColumnName("STATUSBRAND")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => new { e.User, e.Productid });

                entity.ToTable("CART");

                entity.Property(e => e.User)
                    .HasColumnName("USER")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.Productstatus).HasColumnName("PRODUCTSTATUS");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CART__PRODUCTID__50FB042B");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CART__USER__5006DFF2");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("COMMENT");

                entity.Property(e => e.Commentid).HasColumnName("COMMENTID");

                entity.Property(e => e.Commenttext).HasColumnName("COMMENTTEXT");

                entity.Property(e => e.Datecomment)
                    .HasColumnName("DATECOMMENT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

                entity.Property(e => e.User)
                    .HasColumnName("USER")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("FK__COMMENT__PRODUCT__4959E263");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("FK__COMMENT__USER__4865BE2A");
            });

            modelBuilder.Entity<Config>(entity =>
            {
                entity.HasKey(e => e.Configname);

                entity.ToTable("CONFIG");

                entity.Property(e => e.Configname)
                    .HasColumnName("CONFIGNAME")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Decriptionconfigname).HasColumnName("DECRIPTIONCONFIGNAME");
            });

            modelBuilder.Entity<Configdetail>(entity =>
            {
                entity.HasKey(e => new { e.Productid, e.Configname });

                entity.ToTable("CONFIGDETAIL");

                entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

                entity.Property(e => e.Configname)
                    .HasColumnName("CONFIGNAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Information).HasColumnName("INFORMATION");

                entity.HasOne(d => d.Config)
                    .WithMany(p => p.Configdetail)
                    .HasForeignKey(d => d.Configname)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CONFIGDET__CONFI__32767D0B");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Configdetail)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CONFIGDET__PRODU__318258D2");
            });

            modelBuilder.Entity<PicProduct>(entity =>
            {
                entity.HasKey(e => new { e.Productid, e.Pictureid });

                entity.ToTable("PIC_PRODUCT");

                entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

                entity.Property(e => e.Pictureid).HasColumnName("PICTUREID");

                entity.Property(e => e.Mainpic).HasColumnName("MAINPIC");

                entity.HasOne(d => d.Picture)
                    .WithMany(p => p.PicProduct)
                    .HasForeignKey(d => d.Pictureid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PIC_PRODU__PICTU__762C88DA");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PicProduct)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PIC_PRODU__PRODU__753864A1");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.ToTable("PICTURE");

                entity.Property(e => e.Pictureid).HasColumnName("PICTUREID");

                entity.Property(e => e.Link)
                    .HasColumnName("LINK")
                    .IsUnicode(false);

                entity.Property(e => e.Path)
                    .HasColumnName("PATH")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

                entity.Property(e => e.Brandid).HasColumnName("BRANDID");

                entity.Property(e => e.Dateadd)
                    .HasColumnName("DATEADD")
                    .HasColumnType("datetime");

                entity.Property(e => e.Decription).HasColumnName("DECRIPTION");

                entity.Property(e => e.Productamount).HasColumnName("PRODUCTAMOUNT");

                entity.Property(e => e.Productname)
                    .HasColumnName("PRODUCTNAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Productprice).HasColumnName("PRODUCTPRICE");

                entity.Property(e => e.Producttypeid).HasColumnName("PRODUCTTYPEID");

                entity.Property(e => e.Statusproduct)
                    .HasColumnName("STATUSPRODUCT")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.Brandid)
                    .HasConstraintName("FK__PRODUCT__BRANDID__2610A626");

                entity.HasOne(d => d.Producttype)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.Producttypeid)
                    .HasConstraintName("FK__PRODUCT__PRODUCT__2704CA5F");
            });

            modelBuilder.Entity<Producttype>(entity =>
            {
                entity.ToTable("PRODUCTTYPE");

                entity.Property(e => e.Producttypeid).HasColumnName("PRODUCTTYPEID");

                entity.Property(e => e.Producttypename)
                    .HasColumnName("PRODUCTTYPENAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Statusproducttype)
                    .HasColumnName("STATUSPRODUCTTYPE")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Ratingproduct>(entity =>
            {
                entity.HasKey(e => new { e.User, e.Productid });

                entity.ToTable("RATINGPRODUCT");

                entity.Property(e => e.User)
                    .HasColumnName("USER")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

                entity.Property(e => e.Rate).HasColumnName("RATE");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Ratingproduct)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RATINGPRO__PRODU__42ACE4D4");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Ratingproduct)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RATINGPROD__USER__41B8C09B");
            });

            modelBuilder.Entity<Tempproduct>(entity =>
            {
                entity.HasKey(e => new { e.Producttypeid, e.Brandid });

                entity.ToTable("TEMPPRODUCT");

                entity.Property(e => e.Producttypeid).HasColumnName("PRODUCTTYPEID");

                entity.Property(e => e.Brandid).HasColumnName("BRANDID");

                entity.Property(e => e.Tempproductstatus).HasColumnName("TEMPPRODUCTSTATUS");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Tempproduct)
                    .HasForeignKey(d => d.Brandid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TEMPPRODU__BRAND__214BF109");

                entity.HasOne(d => d.Producttype)
                    .WithMany(p => p.Tempproduct)
                    .HasForeignKey(d => d.Producttypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TEMPPRODU__PRODU__22401542");
            });

            modelBuilder.Entity<Viewnumber>(entity =>
            {
                entity.HasKey(e => new { e.User, e.Productid });

                entity.ToTable("VIEWNUMBER");

                entity.Property(e => e.User)
                    .HasColumnName("USER")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

                entity.Property(e => e.Dateseen)
                    .HasColumnName("DATESEEN")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Viewnumber)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VIEWNUMBE__PRODU__3EDC53F0");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Viewnumber)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VIEWNUMBER__USER__3DE82FB7");
            });

            modelBuilder.Entity<Vocherdetail>(entity =>
            {
                entity.HasKey(e => new { e.Voucherid, e.User });

                entity.ToTable("VOCHERDETAIL");

                entity.Property(e => e.Voucherid).HasColumnName("VOUCHERID");

                entity.Property(e => e.User)
                    .HasColumnName("USER")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Dateendtire)
                    .HasColumnName("DATEENDTIRE")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Vocherdetail)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VOCHERDETA__USER__5D60DB10");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.Vocherdetail)
                    .HasForeignKey(d => d.Voucherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VOCHERDET__VOUCH__5C6CB6D7");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("VOUCHER");

                entity.Property(e => e.Voucherid).HasColumnName("VOUCHERID");

                entity.Property(e => e.Dateendtire)
                    .HasColumnName("DATEENDTIRE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Decriptionvoucher).HasColumnName("DECRIPTIONVOUCHER");

                entity.Property(e => e.Statusvoucher)
                    .HasColumnName("STATUSVOUCHER")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<WwBaucuaAccount>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("ww_BAUCUA_ACCOUNT");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AcStatus).HasColumnName("AC_STATUS");

                entity.Property(e => e.Accountname)
                    .HasColumnName("ACCOUNTNAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Money).HasColumnName("MONEY");

                entity.Property(e => e.Pass)
                    .HasColumnName("PASS")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WwBaucuaAnimal>(entity =>
            {
                entity.HasKey(e => e.Animalid);

                entity.ToTable("ww_BAUCUA_ANIMAL");

                entity.Property(e => e.Animalid).HasColumnName("ANIMALID");

                entity.Property(e => e.Animalname)
                    .HasColumnName("ANIMALNAME")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WwBaucuaChat>(entity =>
            {
                entity.HasKey(e => e.Chatid);

                entity.ToTable("ww_BAUCUA_CHAT");

                entity.Property(e => e.Chatid).HasColumnName("CHATID");

                entity.Property(e => e.Numberviewers).HasColumnName("NUMBERVIEWERS");

                entity.Property(e => e.Playerid).HasColumnName("PLAYERID");

                entity.Property(e => e.Textchat)
                    .IsRequired()
                    .HasColumnName("TEXTCHAT");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.WwBaucuaChat)
                    .HasForeignKey(d => d.Playerid)
                    .HasConstraintName("FK__ww_BAUCUA__PLAYE__253C7D7E");
            });

            modelBuilder.Entity<WwBaucuaColor>(entity =>
            {
                entity.HasKey(e => e.Colorid);

                entity.ToTable("ww_BAUCUA_COLOR");

                entity.Property(e => e.Colorid).HasColumnName("COLORID");

                entity.Property(e => e.Colorname)
                    .HasColumnName("COLORNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WwBaucuaPlayer>(entity =>
            {
                entity.HasKey(e => e.Playerid);

                entity.ToTable("ww_BAUCUA_PLAYER");

                entity.Property(e => e.Playerid).HasColumnName("PLAYERID");

                entity.Property(e => e.Colorid).HasColumnName("COLORID");

                entity.Property(e => e.Dice).HasColumnName("DICE");

                entity.Property(e => e.Ready).HasColumnName("READY");

                entity.Property(e => e.Roomid).HasColumnName("ROOMID");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.WwBaucuaPlayer)
                    .HasForeignKey(d => d.Colorid)
                    .HasConstraintName("FK__ww_BAUCUA__COLOR__1D9B5BB6");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.WwBaucuaPlayer)
                    .HasForeignKey(d => d.Roomid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ww_BAUCUA__ROOMI__1CA7377D");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.WwBaucuaPlayer)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__ww_BAUCUA__USERN__1E8F7FEF");
            });

            modelBuilder.Entity<WwBaucuaPlayerdetail>(entity =>
            {
                entity.HasKey(e => new { e.Playerid, e.Animalid });

                entity.ToTable("ww_BAUCUA_PLAYERDETAIL");

                entity.Property(e => e.Playerid).HasColumnName("PLAYERID");

                entity.Property(e => e.Animalid).HasColumnName("ANIMALID");

                entity.Property(e => e.Bets).HasColumnName("BETS");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.WwBaucuaPlayerdetail)
                    .HasForeignKey(d => d.Animalid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ww_BAUCUA__ANIMA__226010D3");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.WwBaucuaPlayerdetail)
                    .HasForeignKey(d => d.Playerid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ww_BAUCUA__PLAYE__216BEC9A");
            });

            modelBuilder.Entity<WwBaucuaRoom>(entity =>
            {
                entity.HasKey(e => e.Roomid);

                entity.ToTable("ww_BAUCUA_ROOM");

                entity.Property(e => e.Roomid).HasColumnName("ROOMID");

                entity.Property(e => e.Amountperson).HasColumnName("AMOUNTPERSON");

                entity.Property(e => e.Dice1).HasColumnName("DICE1");

                entity.Property(e => e.Dice2).HasColumnName("DICE2");

                entity.Property(e => e.Dice3).HasColumnName("DICE3");

                entity.Property(e => e.Minbets).HasColumnName("MINBETS");

                entity.Property(e => e.Owner)
                    .HasColumnName("OWNER")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .HasColumnName("PASS")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Roomstatus).HasColumnName("ROOMSTATUS");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.WwBaucuaRoom)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("FK__ww_BAUCUA__OWNER__19CACAD2");
            });

            modelBuilder.Entity<WwBill>(entity =>
            {
                entity.HasKey(e => e.BillId);

                entity.ToTable("ww_Bill");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.PhoneNavigation)
                    .WithMany(p => p.WwBill)
                    .HasForeignKey(d => d.Phone)
                    .HasConstraintName("FK__ww_Bill__Phone__1387E197");
            });

            modelBuilder.Entity<WwBillDetail>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.BillId });

                entity.ToTable("ww_BillDetail");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.WwBillDetail)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ww_BillDe__BillI__1758727B");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WwBillDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ww_BillDe__Produ__16644E42");
            });

            modelBuilder.Entity<WwCustomer>(entity =>
            {
                entity.HasKey(e => e.Phone);

                entity.ToTable("ww_Customer");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tag).HasMaxLength(100);
            });

            modelBuilder.Entity<WwProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("ww_Product");

                entity.Property(e => e.Pic)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ProdutName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }*/

       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }*/
    }
}

using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TSP_Covid21.Models.ShopEntity;
using TSP_Covid21.Models.ViewModel;

namespace TSP_Covid21.Models.BUS
{
    public class Product_BUS
    {
        private COVIDEntities db;

        public Product_BUS()
        {
            db = new COVIDEntities();
        }

        #region methods

        // Load product new
        public IEnumerable<PRODUCT> loadProductNew()
        {
            var date = DateTime.Now;
            var result = db.PRODUCTs.Where(p => p.STATUSPRODUCT == true).OrderByDescending(t => t.DATEADD).Take(5);
            return result;
        }

        // Load product top sale
        public IEnumerable<PRODUCT> loadProductTop()
        {
            var result = db.PRODUCTs.Where(p => p.STATUSPRODUCT == true).OrderByDescending(t => t.PRODUCTID).Take(5);
            return result;
        }

        // Load product top sale at page product
        public IEnumerable<PRODUCT> loadProductTop_Product(int page, int pagesize)
        {
            var result = db.PRODUCTs.Where(p => p.STATUSPRODUCT == true).OrderByDescending(t => t.PRODUCTID).Take(18);
            return result.ToPagedList(page, pagesize);
        }

        // Load product of SmartPhone
        public IEnumerable<PRODUCT> loadSmartPhoneTop()
        {
            var result = from c in db.PRODUCTs
                         orderby (c.PRODUCTTYPE.PRODUCTTYPENAME)
                         where c.PRODUCTTYPE.PRODUCTTYPENAME == "SmartPhone" & c.STATUSPRODUCT == true
                         select c;

            return result;
        }

        // Load product of SmartWatch
        public IEnumerable<PRODUCT> loadSmartWatchTop()
        {
            var result = from c in db.PRODUCTs
                         orderby (c.PRODUCTTYPE.PRODUCTTYPENAME)
                         where c.PRODUCTTYPE.PRODUCTTYPENAME == "SmartWatch" & c.STATUSPRODUCT == true
                         select c;

            return result;
        }

        // Load product of LapTop
        public IEnumerable<PRODUCT> loadLapTopTop()
        {
            var result = from c in db.PRODUCTs
                         orderby (c.PRODUCTTYPE.PRODUCTTYPENAME)
                         where c.PRODUCTTYPE.PRODUCTTYPENAME == "Laptop" & c.STATUSPRODUCT == true
                         select c;

            return result;
        }

        // Load product for product page
        public IEnumerable<PRODUCT> loadProduct(string productTypeName)
        {           
            var result = db.PRODUCTs.Where(p => p.PRODUCTTYPE.PRODUCTTYPENAME == productTypeName & p.STATUSPRODUCT == true);  

            return result;
        }

        // Load amount product of user like
        public string amountProductLike(string user)
        {
            var list = db.ACCOUNTLIKEs.Where(c => c.USER == user);

            var result = list.Count();
            return result.ToString();
        }

        // Load product of user like for product page
        public IEnumerable<ProductViewModel> loadProductLike(int page, int pagesize, string user)
        {
            var list = from a in db.PRODUCTs
                       join b in db.ACCOUNTLIKEs
                       on a.PRODUCTID equals b.PRODUCTID
                       where b.USER == user & a.STATUSPRODUCT == true
                       select new ProductViewModel
                       {
                           ProductId = a.PRODUCTID,
                           ProductName = a.PRODUCTNAME,
                           BrandName = a.BRAND.BRANDNAME,
                           ProductTypeName = a.PRODUCTTYPE.PRODUCTTYPENAME,
                           MainPic = a.MAINPIC,
                           Pic1 = a.PIC1,
                           Pic2 = a.PIC2,
                           Pic3 = a.PIC3,
                           Pic4 = a.PIC4,
                           ProductPrice = a.PRODUCTPRICE,
                           ProductAmount = a.PRODUCTAMOUNT,
                           Decription = a.DECRIPTION,
                           DateAdd = a.DATEADD,
                           ProductConfig = a.CONFIGDETAILs
                       };
            var result = list.OrderByDescending(x => x.ProductId).Skip((page - 1) * pagesize).Take(20);

            return result.ToPagedList(page, pagesize);
        }

        // Load product of user cart for cart page
        public IEnumerable<ProductOfCartViewModel> loadProductCart(string user)
        {
            var list = from a in db.PRODUCTs
                       join b in db.CARTs
                        on a.PRODUCTID equals b.PRODUCTID
                       where b.USER == user & a.STATUSPRODUCT == true
                       select new ProductOfCartViewModel
                       {
                           ProductId = a.PRODUCTID,
                           ProductName = a.PRODUCTNAME,
                           MainPic = a.MAINPIC,
                           ProductPrice = a.PRODUCTPRICE,
                           ProductAmount = b.AMOUNT,
                           Status = b.PRODUCTSTATUS,
                       };

            return list;
        }

        // Load product of user checkout for checkout page
        public IEnumerable<ProductOfCartViewModel> loadProductCheckout(string user)
        {
            var list = from a in db.PRODUCTs
                       join b in db.CARTs
                        on a.PRODUCTID equals b.PRODUCTID
                       where b.USER == user & b.PRODUCTSTATUS == true
                       select new ProductOfCartViewModel
                       {
                           ProductId = a.PRODUCTID,
                           ProductName = a.PRODUCTNAME,
                           MainPic = a.MAINPIC,
                           ProductPrice = a.PRODUCTPRICE,
                           ProductAmount = b.AMOUNT,
                           Status = b.PRODUCTSTATUS,
                       };

            return list;
        }

        // Load list product in cart
        public IEnumerable<CART> loadCart(string user)
        {
            var list = db.CARTs.Where(c => c.USER == user & c.PRODUCT.STATUSPRODUCT == true);
            return list;
        }

        //load brand of producttype
        public IEnumerable<BrandViewModel> loadBrand(string ProductTypeName)
        {
            var result = from a in db.TEMPPRODUCTs
                         join b in db.PRODUCTTYPEs
                         on a.PRODUCTTYPEID equals b.PRODUCTTYPEID
                         where b.PRODUCTTYPENAME == ProductTypeName 
                         select new BrandViewModel
                         {
                             BrandId = a.BRANDID,
                             BrandName = a.BRAND.BRANDNAME,
                             Product = a.BRAND.PRODUCTs,
                             TempProduct = b.TEMPPRODUCTs
                         };
            return result;

        }

        //load all brand
        public IEnumerable<BrandViewModel> loadAllBrand()
        {
            var result = from a in db.BRANDs
                         select new BrandViewModel
                         {
                             BrandId = a.BRANDID,
                             BrandName = a.BRANDNAME,
                             Product = a.PRODUCTs,
                             TempProduct = a.TEMPPRODUCTs
                         };
            return result;
        }

        // Xóa sản phẩm ở giỏ hang                                                                                                                                                                                                                                                             
        public void delProduct_Cart(int productId, string user)
        {
            CART c = db.CARTs.Where(p => p.PRODUCTID == productId && p.USER == user).SingleOrDefault();
            db.CARTs.Remove(c);
            db.SaveChanges();
        }

        // Lấy sản thông tin sản phẩm
        public PRODUCT productDetail(int productId)
        {
            var result = db.PRODUCTs.Where(p => p.PRODUCTID == productId).SingleOrDefault();
            return result;
        }

        // Lấy danh sách đánh giá của sản phẩm
        public IEnumerable<RATINGPRODUCT> loadRatingProduct(int productId)
        {
            var result = db.RATINGPRODUCTs.Where(p => p.PRODUCTID == productId).ToList();
            return result;
        }

        // Lấy cấu hình của sản phẩm
        public IEnumerable<CONFIGDETAIL> loadConfigProduct(int productId)
        {
            var result = db.CONFIGDETAILs.Where(p => p.PRODUCTID == productId);
            return result;
        }

        // Lấy danh sách bình luận về sản phẩm
        public IEnumerable<COMMENT> loadCommentProduct(int productId)
        {

            var list = db.COMMENTs.Where(p => p.PRODUCTID == productId);

            var result = list.OrderByDescending(x => x.PRODUCTID).Skip(0);
            return result;
        }

        // Lấy đánh giá của 1 người dùng về sản phẩm 
        public int ReviewRatingOfUser(string user, int productId)
        {
            var resuft = db.RATINGPRODUCTs.Where(p => p.USER == user && p.PRODUCTID == productId).Select(p => p.RATE).SingleOrDefault();
            return Convert.ToInt32(resuft);
        }

        // Lấy danh sách sản phẩm liên quan
        public IEnumerable<PRODUCT> loadRelatedProduct(int productTypeId, int brandId)
        {
            var result = db.PRODUCTs.Where(p => p.PRODUCTTYPEID == productTypeId && p.BRANDID == brandId).Take(4);
            return result;

        }

        // Thêm bình luận về sản phẩm
        public void insertComment(int productId, string user, string comment)
        {
            DateTime time = DateTime.Now;

            COMMENT c = new COMMENT
            {
                PRODUCTID = productId,
                USER = user,
                COMMENTTEXT = comment,
                DATECOMMENT = time,
            };
            db.COMMENTs.Add(c);
            db.SaveChanges();
        }

        // Thêm đánh giá hoặc sữa lại đánh giá về sản phẩm
        public void insertRating(int productId, string user, int rate)
        {
            RATINGPRODUCT r = new RATINGPRODUCT
            {
                PRODUCTID = productId,
                USER = user,
                RATE = rate,
            };
            db.RATINGPRODUCTs.AddOrUpdate(r);
            db.SaveChanges();
        }

        // Thêm sản phẩm vào giỏ hàng
        public void insertCart(int productId, string user)
        {
            CART c = new CART
            {
                USER = user,
                PRODUCTID = productId,
                AMOUNT = 1,
                PRODUCTSTATUS = true,
            };
            db.CARTs.AddOrUpdate(c);
            db.SaveChanges();
        }

        // Thêm sản phẩm vào giỏ hàng với số lượng mong muốn
        public void insertCartWithAmount(int productId, string user, int amount)
        {
            CART c = new CART
            {
                USER = user,
                PRODUCTID = productId,
                AMOUNT = amount,
                PRODUCTSTATUS = true,
            };
            db.CARTs.AddOrUpdate(c);
            db.SaveChanges();
        }

        // Thay đổi trạng thái muốn mua trong hóa đơn hiện tại hay không
        public void change_CheckBox(int productId, bool status, string user, int quantity)
        {
            CART c = new CART
            {
                USER = user,
                PRODUCTID = productId,
                AMOUNT = quantity,
                PRODUCTSTATUS = status,
            };
            db.CARTs.AddOrUpdate(c);
            db.SaveChanges();
        }

        //Dùng vào lúc chọn lọc sản phẩm theo mong muoonscuar khách hàng
        public IEnumerable<PRODUCT> listProduct(string productTypeName, int startPrice, int endPrice, int sort, List<string> listbrand)
        {
            var list = from p in db.PRODUCTs
                       where p.PRODUCTPRICE > startPrice &&
                       p.PRODUCTPRICE < endPrice
                       select p;

            /*
             Kiểm tra người dùng đang bấm chọn loại sản phẩm hay tìm kiếm
             All: có nghĩa là người dùng đang tìm kiếm sản phẩm
             */
            if (!productTypeName.Equals("All"))
            {
                list = list.Where(p => p.PRODUCTTYPE.PRODUCTTYPENAME == productTypeName);
            }

            // Kiểm tra loại sản phẩm có nằm trong sản phẩm được chọn không
            var list1 = list;
            if (listbrand.Count() > 0)
            {
                list1 = list.Where(p => listbrand.Where(t => t == p.BRAND.BRANDNAME).Any());
            }

            if (sort == 0) // sắp xếp theo từ thấp đến cao bằng giá sản phẩm
            {
                var result = list1.OrderBy(p => p.PRODUCTPRICE).ToList();
                return result;
            }

            var result1 = list1.OrderByDescending(p => p.PRODUCTPRICE).ToList();

            return result1;

        }
        
        public  IEnumerable<PRODUCT> search(string key)
        {
            var result = db.PRODUCTs.ToList();

            if(key != null)
            {
                result = result.Where(p => p.PRODUCTNAME.ToUpper().Contains(key.ToUpper())).ToList();
            }

            return result;
        }

        #endregion

    }
}
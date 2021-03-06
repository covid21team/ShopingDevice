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
            var result = db.PRODUCT.OrderByDescending(t => t.DATEADD).Where(p => p.STATUSPRODUCT == true).Take(5);
            return result;
        }

        // Load product top sale
        public IEnumerable<PRODUCT> loadProductTop()
        {
            var result = db.PRODUCT.OrderByDescending(t => t.PRODUCTID).Where(p => p.STATUSPRODUCT == true).Take(5);
            return result;
        }

        // Load product top sale at page product
        public IEnumerable<PRODUCT> loadProductTop_Product(int page, int pagesize)
        {
            var result = db.PRODUCT.OrderByDescending(t => t.PRODUCTID).Where(p => p.STATUSPRODUCT == true).Take(18);
            return result.ToPagedList(page, pagesize);
        }

        // Load product of SmartPhone
        public IEnumerable<PRODUCT> loadSmartPhoneTop()
        {
            var result = from c in db.PRODUCT
                         orderby (c.PRODUCTTYPE.PRODUCTTYPENAME)
                         where c.PRODUCTTYPE.PRODUCTTYPEID == 2 & c.STATUSPRODUCT == true
                         select c;

            return result.Take(3);
        }

        // Load product of SmartWatch
        public IEnumerable<PRODUCT> loadSmartWatchTop()
        {
            var result = from c in db.PRODUCT
                         orderby (c.PRODUCTTYPE.PRODUCTTYPENAME)
                         where c.PRODUCTTYPE.PRODUCTTYPEID == 4 & c.STATUSPRODUCT == true
                         select c;

            return result.Take(3);
        }

        // Load product of LapTop
        public IEnumerable<PRODUCT> loadLapTopTop()
        {
            var result = from c in db.PRODUCT
                         orderby (c.PRODUCTTYPE.PRODUCTTYPENAME)
                         where c.PRODUCTTYPE.PRODUCTTYPEID == 1 & c.STATUSPRODUCT == true
                         select c;

            return result.Take(3);
        }

        // Load product for product page
        public IEnumerable<PRODUCT> loadProduct(int productTypeId)
        {           
            var result = db.PRODUCT.Where(p => p.PRODUCTTYPE.PRODUCTTYPEID == productTypeId);  

            return result;
        }

        // Load amount product of user like
        public string amountProductLike(string user)
        {
            var list = db.ACCOUNTLIKE.Where(c => c.USER == user);

            var result = list.Count();
            return result.ToString();
        }

        // Load product of user like for product page
        public IEnumerable<ProductViewModel> loadProductLike(string user)
        {
            var list = from a in db.PRODUCT
                       join b in db.ACCOUNTLIKE
                       on a.PRODUCTID equals b.PRODUCTID
                       where b.USER == user
                       select new ProductViewModel
                       {
                           ProductId = a.PRODUCTID,
                           ProductName = a.PRODUCTNAME,
                           BrandName = a.BRAND.BRANDNAME,
                           ProductTypeName = a.PRODUCTTYPE.PRODUCTTYPENAME,
                           ProductPrice = a.PRODUCTPRICE,
                           ProductAmount = a.PRODUCTAMOUNT,
                           Decription = a.DECRIPTION,
                           Status = a.STATUSPRODUCT,
                           DateAdd = a.DATEADD,
                           ProductConfig = a.CONFIGDETAIL,
                           MainPic = a.PIC_PRODUCT.Where(t => t.MAINPIC == true).Select(c => c.PICTURE.PATH).FirstOrDefault(),
                       };

            return list.ToList();
        }

        // Load product of user cart for cart page
        public IEnumerable<ProductOfCartViewModel> loadProductCart(string user)
        {
            var list = from a in db.PRODUCT
                       join b in db.CART
                        on a.PRODUCTID equals b.PRODUCTID
                       where b.USER == user
                       select new ProductOfCartViewModel
                       {
                           ProductId = a.PRODUCTID,
                           ProductName = a.PRODUCTNAME,
                           ProductPrice = a.PRODUCTPRICE,
                           ProductAmount = b.AMOUNT,
                           MainPic = a.PIC_PRODUCT.Where(t => t.MAINPIC == true).Select(c => c.PICTURE.PATH).FirstOrDefault(),
                           Status = b.PRODUCTSTATUS,
                       };

            return list;
        }

        // Load product of user checkout for checkout page
        public IEnumerable<ProductOfCartViewModel> loadProductCheckout(string user)
        {
            var list = from a in db.PRODUCT
                       join b in db.CART
                        on a.PRODUCTID equals b.PRODUCTID
                       where b.USER == user
                       select new ProductOfCartViewModel
                       {
                           ProductId = a.PRODUCTID,
                           ProductName = a.PRODUCTNAME,
                           MainPic = a.PIC_PRODUCT.Where(t => t.MAINPIC == true && t.PRODUCTID == a.PRODUCTID).Select(c => c.PICTURE.PATH).FirstOrDefault(),
                           ProductPrice = a.PRODUCTPRICE,
                           ProductAmount = b.AMOUNT,
                           Status = b.PRODUCTSTATUS,
                       };

            return list;
        }

        // Load list product in cart
        public IEnumerable<CART> loadCart(string user)
        {
            var list = db.CART.Where(c => c.USER == user);
            return list;
        }

        //load brand of producttype
        public IEnumerable<BrandViewModel> loadBrand(int ProductTypeId)
        {
            var result = from a in db.TEMPPRODUCT
                         join b in db.PRODUCTTYPE
                         on a.PRODUCTTYPEID equals b.PRODUCTTYPEID
                         where b.PRODUCTTYPEID == ProductTypeId 
                         select new BrandViewModel
                         {
                             BrandId = a.BRANDID,
                             BrandName = a.BRAND.BRANDNAME,
                             Product = a.BRAND.PRODUCT,
                         };
            return result;

        }

        public IEnumerable<BRAND> listBrand()
        {
            return db.BRAND;
        }

        //load all brand
        public IEnumerable<BrandViewModel> loadAllBrand()
        {
            var result = from a in db.BRAND
                         select new BrandViewModel
                         {
                             BrandId = a.BRANDID,
                             BrandName = a.BRANDNAME,
                             Product = a.PRODUCT,
                         };
            return result;
        }

        // Xóa sản phẩm ở giỏ hang                                                                                                                                                                                                                                                             
        public void delProduct_Cart(int productId, string user)
        {
            CART c = db.CART.Where(p => p.PRODUCTID == productId && p.USER == user).SingleOrDefault();
            db.CART.Remove(c);
            db.SaveChanges();
        }

        public void delProduct(int productId, string user)
        {
            var result = db.CART.Where(p => p.PRODUCTID == productId & p.USER == user).SingleOrDefault();
            db.CART.Remove(result);
            db.SaveChanges();
        }

        // Lấy sản thông tin sản phẩm
        public PRODUCT productDetail(int productId)
        {
            var result = db.PRODUCT.Where(p => p.PRODUCTID == productId).SingleOrDefault();
            return result;
        }

        //Khi xem sản phẩm
        public void insertView(int productId, string user)
        {
            DateTime time = DateTime.Now;
            VIEWNUMBER v = new VIEWNUMBER
            {
                USER = user,
                PRODUCTID = productId,
                DATESEEN = time,
            };
            db.VIEWNUMBER.AddOrUpdate(v);
            db.SaveChanges();
        }

        // Lấy danh sách đánh giá của sản phẩm
        public IEnumerable<RATINGPRODUCT> loadRatingProduct(int productId)
        {
            var result = db.RATINGPRODUCT.Where(p => p.PRODUCTID == productId).ToList();
            return result;
        }

        // Lấy cấu hình của sản phẩm
        public IEnumerable<CONFIGDETAIL> loadConfigProduct(int productId)
        {
            var result = db.CONFIGDETAIL.Where(p => p.PRODUCTID == productId);
            return result;
        }

        // Lấy danh sách bình luận về sản phẩm
        public IEnumerable<COMMENT> loadCommentProduct(int productId)
        {

            var list = db.COMMENT.Where(p => p.PRODUCTID == productId);

            var result = list.OrderByDescending(x => x.PRODUCTID).Skip(0);
            return result;
        }

        // Lấy đánh giá của 1 người dùng về sản phẩm 
        public int ReviewRatingOfUser(string user, int productId)
        {
            var resuft = db.RATINGPRODUCT.Where(p => p.USER == user && p.PRODUCTID == productId).Select(p => p.RATE).SingleOrDefault();
            return Convert.ToInt32(resuft);
        }

        // Lấy danh sách sản phẩm liên quan
        public IEnumerable<PRODUCT> loadRelatedProduct(int productTypeId, int brandId)
        {
            var result = db.PRODUCT.Where(p => p.PRODUCTTYPEID == productTypeId && p.BRANDID == brandId).Take(4);
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
            db.COMMENT.Add(c);
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
            db.RATINGPRODUCT.AddOrUpdate(r);
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
            db.CART.AddOrUpdate(c);
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
            db.CART.AddOrUpdate(c);
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
            db.CART.AddOrUpdate(c);
            db.SaveChanges();
        }

        //Dùng vào lúc chọn lọc sản phẩm theo mong muốn của khách hàng
        public IEnumerable<PRODUCT> listProduct(int ProductTypeId, int startPrice, int endPrice, int sort, List<string> listbrand, string key)
        {
            var list = from p in db.PRODUCT
                       where p.PRODUCTPRICE > startPrice &&
                       p.PRODUCTPRICE < endPrice
                       select p;

            /*
             Kiểm tra người dùng đang bấm chọn loại sản phẩm hay tìm kiếm
             0: có nghĩa là người dùng đang tìm kiếm sản phẩm
             */
            if (ProductTypeId != 0)
            {
                list = list.Where(p => p.PRODUCTTYPE.PRODUCTTYPEID == ProductTypeId);
            }
            else
            {
                list = list.Where(t => t.PRODUCTNAME.ToUpper().Contains(key.ToUpper()));
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
            var result = db.PRODUCT.ToList();

            if(key != null)
            {
                result = result.Where(p => p.PRODUCTNAME.ToUpper().Contains(key.ToUpper())).ToList();
            }

            return result;
        }

        public void insertBill(string user, string note, int total)
        {
            var a = db.ADDRESS_SHIP.Where(p => p.USER == user & p.DEFAULT == true).FirstOrDefault();
            DateTime time = DateTime.Now;

            BILL b = new BILL
            {
                USER = user,
                DATECREATE = time,
                TOTALBILL = total,
                BIllSTATUS = 1,
                NOTE = note,
                FULLNAME = a.FULLNAME,
                PHONE = a.PHONE,
                CITY = a.CITY,
                DISTRICT = a.DISTRICT,
                WARDS = a.WARDS,
                ADDRESS = a.ADDRESS,
            };
            db.BILL.Add(b);
            db.SaveChanges();

            int id = db.BILL.Where(p => p.USER == user).OrderByDescending(y => y.DATECREATE).Select(t => t.BILLID).FirstOrDefault();
            IEnumerable<CART> c = db.CART.Where(p => p.USER == user & p.PRODUCTSTATUS == true);

            foreach(var item in c)
            {
                BILLDETAIL bd = new BILLDETAIL
                {
                    BILLID = id,
                    PRODUCTID = item.PRODUCTID,
                    AMOUNT = item.AMOUNT,
                };
                db.BILLDETAIL.Add(bd);
                PRODUCT p = db.PRODUCT.Where(t => t.PRODUCTID == item.PRODUCTID).FirstOrDefault();
                p.PRODUCTAMOUNT = p.PRODUCTAMOUNT - item.AMOUNT;
                db.CART.Remove(item);
            }
            db.SaveChanges();

        }

        public void insertFavourite(int productId, string user)
        {
            ACCOUNTLIKE c = new ACCOUNTLIKE
            {
                USER = user,
                PRODUCTID = productId,
                DATELIKE = DateTime.Now,
            };
            db.ACCOUNTLIKE.AddOrUpdate(c);
            db.SaveChanges();
        }

        public BILL takeBill(string user)
        {
            return db.BILL.Where(p => p.USER == user).OrderByDescending(c => c.BILLID).FirstOrDefault();
        }

        #endregion

    }
}
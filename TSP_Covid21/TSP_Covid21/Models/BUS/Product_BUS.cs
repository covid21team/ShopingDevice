using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = db.PRODUCTs.OrderByDescending(t => t.DATEADD).Take(5);
            return result;
        }

        // Load product top sale
        public IEnumerable<PRODUCT> loadProductTop()
        {
            var result = db.PRODUCTs.OrderByDescending(t => t.PRODUCTVIEW).Take(5);
            return result;
        }

        // Load product top sale at page product
        public IEnumerable<PRODUCT> loadProductTop_Product(int page, int pagesize)
        {
            var result = db.PRODUCTs.OrderByDescending(t => t.PRODUCTVIEW).Take(3);
            return result.ToPagedList(page, pagesize);
        }

        // Load product of SmartPhone
        public IEnumerable<PRODUCT> loadSmartPhoneTop(int page, int pagesize)
        {
            var result = from c in db.PRODUCTs
                         orderby(c.PRODUCTTYPE.PRODUCTTYPENAME)
                         where c.PRODUCTTYPE.PRODUCTTYPENAME == "SmartPhone"
                         select c;

            return result.ToPagedList(page, pagesize); 
        }

        // Load product of SmartWatch
        public IEnumerable<PRODUCT> loadSmartWatchTop(int page, int pagesize)
        {
            var result = from c in db.PRODUCTs
                         orderby (c.PRODUCTTYPE.PRODUCTTYPENAME)
                         where c.PRODUCTTYPE.PRODUCTTYPENAME == "SmartWatch"
                         select c;

            return result.ToPagedList(page, pagesize);
        }

        // Load product of LapTop
        public IEnumerable<PRODUCT> loadLapTopTop(int page, int pagesize)
        {
            var result = from c in db.PRODUCTs
                         orderby (c.PRODUCTTYPE.PRODUCTTYPENAME)
                         where c.PRODUCTTYPE.PRODUCTTYPENAME == "Laptop"
                         select c;

            return result.ToPagedList(page, pagesize);
        }

        // Load product for product page
        public IEnumerable<ProductViewModel> loadProduct(int page, int pagesize,string productTypeName)
        {
            var list = from a in db.PRODUCTs
                         orderby (a.PRODUCTTYPE.PRODUCTTYPENAME)
                         where a.PRODUCTTYPE.PRODUCTTYPENAME == productTypeName
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
                             ProductView = a.PRODUCTVIEW,
                             ProductAmount = a.PRODUCTAMOUNT,
                             Decription = a.DECRIPTION,
                             DateAdd = a.DATEADD,
                             ProductConfig = a.CONFIGDETAILs
                         };

            var result = list.OrderByDescending(x => x.ProductId).Skip((page - 1) * pagesize).Take(20);

            return result.ToPagedList(page, pagesize);
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
            var list = from a in db.PRODUCTs join b in db.ACCOUNTLIKEs
                          on a.PRODUCTID equals b.PRODUCTID
                          where b.USER == user
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
                              ProductView = a.PRODUCTVIEW,
                              ProductAmount = a.PRODUCTAMOUNT,
                              Decription = a.DECRIPTION,
                              DateAdd = a.DATEADD,
                              ProductConfig = a.CONFIGDETAILs
                          };
            var result = list.OrderByDescending(x => x.ProductId).Skip((page - 1) * pagesize).Take(20);

            return result.ToPagedList(page, pagesize);
        }

        // Load product of user cart for cart page
        public IEnumerable<ProductViewModel> loadProductCart(string user)
        {
            var list = from a in db.PRODUCTs
                       join b in db.CARTs
                        on a.PRODUCTID equals b.PRODUCTID
                       where b.USER == user
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
                           ProductView = a.PRODUCTVIEW,
                           ProductAmount = b.AMOUNT,
                           Decription = a.DECRIPTION,
                           DateAdd = a.DATEADD,
                           ProductConfig = a.CONFIGDETAILs
                       };

            return list;
        }

        // Load list product in cart
        public IEnumerable<CART> loadCart(string user)
        {
            var list = db.CARTs.Where(c => c.USER == user);
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

        #endregion  
    }
}
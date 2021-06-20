using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

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

        // Load product top view
        public IEnumerable<PRODUCT> loadProductTop()
        {
            var result = db.PRODUCTs.OrderByDescending(t => t.PRODUCTVIEW).Take(5);
            return result;
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

        // Load product 
        public IEnumerable<PRODUCT> loadProduct(int page, int pagesize,string productTypeName)
        {
            var result = from c in db.PRODUCTs
                         orderby (c.PRODUCTTYPE.PRODUCTTYPENAME)
                         where c.PRODUCTTYPE.PRODUCTTYPENAME == productTypeName
                         select c;

            return result.ToPagedList(page, pagesize);
        }

        // Load amount product of user like
        public string amountProductLike(string user)
        {
            var list = db.ACCOUNTLIKEs.Where(c => c.USER == user);

            var result = list.Count();
            return result.ToString();
        }

        // Load list product in cart
        public IEnumerable<CART> loadCart(string user)
        {
            var list = db.CARTs.Where(c => c.USER == user);
            return list;
        }

        //load brand of producttype
        public IEnumerable<TEMPPRODUCT> loadBrand(string producttype)  
        {
            var result = db.TEMPPRODUCTs.Where(p => p.PRODUCTTYPE.PRODUCTTYPENAME == producttype);
            return result;
        }

        #endregion  
    }
}
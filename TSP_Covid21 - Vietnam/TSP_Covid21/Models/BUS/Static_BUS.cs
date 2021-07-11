using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;
using TSP_Covid21.Models.ViewModel;

namespace TSP_Covid21.Models.BUS
{
    public class Static_BUS
    {
        private COVIDEntities db;

        public Static_BUS()
        {
            db = new COVIDEntities();
        }

        public string Revenue()
        {
            var result = db.BILL.Where(p => p.BIllSTATUS != 4).Sum(p => p.TOTALBILL);

            return result.ToString();
        }

        public string totalProduct()
        {
            var result = db.BILLDETAIL.Where(p => p.BILL.BIllSTATUS != 4).Sum(p => p.AMOUNT);

            return result.ToString();
        }

        public string totalBill()
        {
            var result = db.BILL.Where(p => p.BIllSTATUS != 4).Count();

            return result.ToString();
        }

        public IEnumerable<BILL> loadBill()
        {
            var result = db.BILL;

            return result;
        }

        public IEnumerable<StaticProductType> StaticProductType()
        {
            var result = from c in db.PRODUCTTYPE 
                         select new StaticProductType()
                         {
                             PRODUCTTYPEID = c.PRODUCTTYPEID,
                             PRODUCTYPENAME = c.PRODUCTTYPENAME,
                             QUANTITY = c.PRODUCT.Sum(a => a.BILLDETAIL.Where(t => t.BILL.BIllSTATUS != 4).Sum(b => b.AMOUNT))
                         };

            return result.OrderBy(p => p.PRODUCTTYPEID).ToList();
        }
     
        public IEnumerable<StaticBrand> StaticBrand(int productTypeId)
        {
            var result = from c in db.BRAND
                         join a in db.TEMPPRODUCT
                         on c.BRANDID equals a.BRANDID
                         where a.PRODUCTTYPEID == productTypeId
                         select new StaticBrand()
                         {
                             BRANDID = a.BRANDID,
                             BRANDNAME = a.BRAND.BRANDNAME,
                             QUANTITY = c.PRODUCT.Sum(a => a.BILLDETAIL.Where(t => t.PRODUCT.PRODUCTTYPEID == productTypeId & t.BILL.BIllSTATUS != 4).Sum(b => b.AMOUNT))
                         };

            return result;
        }

        public IEnumerable<PRODUCT> listProduct()
        {
            var result = db.PRODUCT;

            return result.ToList();
        }
    }
}
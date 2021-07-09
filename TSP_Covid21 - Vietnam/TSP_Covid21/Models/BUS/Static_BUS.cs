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
            var result = db.BILL.Sum(p => p.TOTALBILL);

            return result.ToString();
        }

        public string totalProduct()
        {
            var result = db.BILLDETAIL.Sum(p => p.AMOUNT);

            return result.ToString();
        }

        public string totalBill()
        {
            var result = db.BILL.Count();

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
                             QUANTITY = c.PRODUCT.Sum(a => a.BILLDETAIL.Sum(b => b.AMOUNT))
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
                             QUANTITY = c.PRODUCT.Sum(a => a.BILLDETAIL.Where(t => t.PRODUCT.PRODUCTTYPEID == productTypeId).Sum(b => b.AMOUNT))
                         };

            return result;
        }
    }
}
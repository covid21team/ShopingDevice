using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Models.BUS
{
    public class Bill_BUS
    {
        private COVIDEntities db;

        public Bill_BUS()
        {
            db = new COVIDEntities();
        }

        public IEnumerable<BILL> listBill()
        {
            return db.BILL.OrderBy(p => p.BIllSTATUS).ToList();
        }

        public BILL billDetail(int billId)
        {
            return db.BILL.Find(billId);
        }

        public void submitBill(int billId)
        {
            var bill = db.BILL.Find(billId);
            bill.BIllSTATUS = 2;
            db.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.BUS;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Areas.Admin.Controllers
{
    public class BillManagerController : Controller
    {
        private Bill_BUS BB;

        public BillManagerController()
        {
            BB = new Bill_BUS();
        }

        public IEnumerable<BILL> listBill()
        {
            return BB.listBill();
        }

        public ActionResult billDetail(int billId)
        {
            var result = BB.billDetail(billId);

            return PartialView(result);
        }

        public void submitBill(int billId)
        {
            BB.submitBill(billId);
        }
    }
}
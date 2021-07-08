using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.BUS;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Areas.Admin.Controllers
{
    public class StaticController : Controller
    {
        Static_BUS SB;

        public StaticController()
        {
            SB = new Static_BUS();
        }

        // GET: Admin/Static
        public ActionResult All()
        {
            return View();
        }

        // Doanh thu trong 1 năm
        public string Revenue()
        {
            return SB.Revenue();
        }

        // Số sản phẩm được bán ra trong 1 năm
        public string totalProduct()
        {
            return SB.totalProduct();
        }

        // Số hóa đơn thành công bán ra trong 1 năm
        public string totalBill()
        {
            return SB.totalBill();
        }

        public IEnumerable<BILL> loadBill()
        {
            return SB.loadBill();
        }

       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api
        public ActionResult Index()
        {
            return View();
        }

        public class product
        {
            public int id { get; set; }
            public string name { get; set; }
            public string pic { get; set; }
            public int ? price { get; set; }
        }

        public JsonResult json()
        {
            COVIDEntities db = new COVIDEntities();
            var result = from a in db.PRODUCT
                         select new product()
                         {
                             id = a.PRODUCTID,
                             name = a.PRODUCTNAME,
                             pic = a.MAINPIC,
                             price = a.PRODUCTPRICE,
                         };
            return Json(new
            {
                data = result,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
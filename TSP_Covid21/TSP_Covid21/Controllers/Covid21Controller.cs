using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSP_Covid21.Controllers
{
    public class Covid21Controller : Controller
    {
        // GET: Covid21
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Product(string ProductTypeName)
        {
            var db = new Models.BUS.Product_BUS();

            Session["ProductTypeName"] = ProductTypeName;
            var result = db.loadProduct(1, 9, ProductTypeName);
                

            return View(result);
        }
    }
}
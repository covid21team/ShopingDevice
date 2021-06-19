using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testAJAX.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AXAJ()
        {
            return View();
        }

        //handle ajax action link
        public ActionResult LoadAjaxActionLink()
        {
            return Content("sy");
        }

        //handle ajax by beginform
        public ActionResult LoadAjaxBeginForm(String text)
        {
            return Content(text);
        }

        // Handle ajax by jQuery
        public ActionResult LoadAjaxJQuery(int a, int b)
        {
            //System.Threading.Thread.Sleep(3000);
            return Content((a + b).ToString());
        }

        public ActionResult load()
        {
            return PartialView("hihi");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testAJAX.Controllers
{
    public class AJAXController : Controller
    {
        // GET: AJAX
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult loadDataAjax()
        {
            return Content("phus");
        }
        
        public ActionResult loadDataAjaxBeginForm(string text)
        {
            return Content(text); 
        }

        public ActionResult loadAjaxJQuery(int a, int b)
        {
            return Content((a+b).ToString());
        }

        public ActionResult loadIndex()
        {
            System.Threading.Thread.Sleep(2000);
            return PartialView("hihi");
        }
    }
}
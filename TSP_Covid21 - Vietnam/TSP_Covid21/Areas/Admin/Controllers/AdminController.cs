using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSP_Covid21.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult ProductManager()
        {
            return View();
        }

        public ActionResult Static()
        {
            return View();
        }

        public ActionResult Personal()
        {
            return View();
        }

        public ActionResult CommentManager()
        {
            return View();
        }

        public ActionResult AccountManager()
        {
            return View();
        }

        public ActionResult PictureManager()
        {
            return View();
        }

        public ActionResult BillManager()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSP_Covid21.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(String user, String pass)
        {
            Models.BUS.Account_BUS AB = new Models.BUS.Account_BUS();

            try
            {
                string checkLogin = AB.checkLogin(user, pass);
                if (checkLogin.Equals(user))
                {
                    Session["user"] = checkLogin;
                    Session["fullname"] = AB.takeFullName(user);
                }
            }
            catch
            {
                
            }
            return RedirectToAction("Home", "Covid21");
        }

        public ActionResult checkUserCtrl(string user)
        {
            Models.BUS.Account_BUS AB = new Models.BUS.Account_BUS();

            try
            {
                string checkLogin = AB.checkUserModel(user);
                Session["checkUser"] = checkLogin;
            }
            catch
            {

            }
            return null;
        }
    }
}
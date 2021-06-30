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

        [HttpPost]
        public bool Signin(string user, string pass)
        {
            //System.Threading.Thread.Sleep(2000);
            Models.BUS.Account_BUS AB = new Models.BUS.Account_BUS();

            string checkLogin = AB.checkLogin(user, pass);
            if (checkLogin == user)
            {
                Session["user"] = checkLogin;
                Session["fullname"] = AB.takeFullName(user);
                return true;
            }
            return false;
        }

        [HttpPost]
        public void Signup()
        {
            Session["user"] = null;
            Session["fullname"] = null;
        }
    }
}
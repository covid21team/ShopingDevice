using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.BUS;

namespace TSP_Covid21.Areas.Admin.Controllers
{
    public class AccountManagerController : Controller
    {
        private Account_BUS AB;

        public AccountManagerController()
        {
            AB = new Account_BUS();
        }
        // GET: Admin/AccountManager
        public ActionResult Acount()
        {
            return View();
        }

        public bool LoginAdmin(string user, string pass)
        {
            AB = new Account_BUS();
            string checkLogin = AB.checkUserAdmin(user, pass);
            if (checkLogin == user)
            {
                Session["userAdmin"] = checkLogin;
                return true;
            }
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.BUS;
using TSP_Covid21.Models.ShopEntity;
using TSP_Covid21.Models.ViewModel;

namespace TSP_Covid21.Areas.Admin.Controllers
{
    public class AccountManagerController : Controller
    {
        private Account_BUS AB;

        public AccountManagerController()
        {
            AB = new Account_BUS();
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

        public ACCOUNT_ADMIN account(string user)
        {
            return AB.acountAdmin(user);
        }

        public IEnumerable<ListAccount> listAccount()
        {
            return AB.listAccount();
        }
    }
}
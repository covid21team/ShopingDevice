using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.ShopEntity;
using TSP_Covid21.Models.BUS;

namespace TSP_Covid21.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
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
                Session["gmail"] = AB.takeGmail(user);
                return true;
            }
            return false;
        }

        public bool checkPass(string user, string pass)
        {
            Account_BUS AB = new Account_BUS();

            string checkLogin = AB.checkLogin(user, pass);
            if (checkLogin == user)
            {
                return true;
            }
            return false;
        }

        public void changePass(string user, string pass_new)
        {
            Account_BUS AB = new Account_BUS();
            AB.changePass(user, pass_new);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["fullname"] = null;

            return RedirectToAction("Home", "Covid21");
        }

        public bool checkUser(string user)
        {
            Models.BUS.Account_BUS AB = new Models.BUS.Account_BUS();
            var result = AB.checkUser(user);

            return result;
        }

        public bool checkPhone(string phone)
        {
            Models.BUS.Account_BUS AB = new Models.BUS.Account_BUS();
            var result = AB.checkPhone(phone);

            return result;
        }

        [HttpPost]
        public bool Signup(string user, string pass, string fullname, string phone)
        {
            if (checkUser(user))
                return false;
            if (checkPhone(phone))
                return false;

            Account_BUS AB = new Account_BUS();
            AB.Signup(user, pass, fullname, phone);

            return true;
        }

        public IEnumerable<BILL> loadBill(string user)
        {
            Account_BUS AB = new Account_BUS();
            var result = AB.loadBill(user);

            return result;
        }

        public IEnumerable<ADDRESS_SHIP> loadAddress(string user)
        {
            Account_BUS AB = new Account_BUS();
            var result = AB.loadAddress(user);

            return result;

        }

        public IEnumerable<ADDRESS_SHIP> addressDefault(string user)
        {
            Account_BUS AB = new Account_BUS();
            var result = AB.addressDefault(user);

            return result;
        }

        public void changeInf(string user, string fullname, bool sex, string birth, string email, string phone)
        {
            DateTime date = DateTime.Parse(birth);

            Account_BUS AB = new Account_BUS();

            AB.changeInf(user, fullname, sex, date, email, phone);

            Session["fullname"] = fullname;
            Session["gmail"] = email;
        }

        public void insertAddress(string user, string fullname, string phone, string city, string district, string ward, string address, bool addDefault)
        {
            Account_BUS AB = new Account_BUS();
            AB.insertAddress(user, fullname, phone, city, district, ward, address, addDefault);
        }
    }
}
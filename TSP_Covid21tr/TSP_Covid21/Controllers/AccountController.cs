﻿using System;
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
        public void Logout()
        {
            Session["user"] = null;
            Session["fullname"] = null;
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

            Models.BUS.Account_BUS AB = new Models.BUS.Account_BUS();
            AB.Signup(user, pass, fullname, phone);

            return true;
        }
    }
}
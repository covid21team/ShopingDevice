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

        public ActionResult Login(String user, String pass)
        {
            Models.BUS.Account_BUS AB = new Models.BUS.Account_BUS();

            try
            {
                Models.ShopEntity.ACCOUNT checkLogin = AB.checkLogin(user);
                if (checkLogin.USER.Equals(user))
                {
                    Session["user"] = checkLogin.FULLNAME;
                }
            }
            catch
            {
                
            }
            return RedirectToAction("Home", "Covid21");
        }
    }
}
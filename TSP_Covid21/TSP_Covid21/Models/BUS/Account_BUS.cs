﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Models.BUS
{
    public class Account_BUS
    {
        private COVIDEntities db;

        public Account_BUS()
        {
            db = new COVIDEntities();
        }

        public string checkLogin(string user,string pass)
        {
            var result = db.CheckLogin(user, pass).SingleOrDefault();
            return result;
        }

        public string takeFullName(string user)
        {
            var result = db.ACCOUNTs.Where(c => c.USER == user).Select(p => p.FULLNAME).SingleOrDefault();
            return result;
        }

        public bool checkUser(string user)
        {
            var result = db.ACCOUNTs.Where(p => p.USER == user).SingleOrDefault();

            if(result == null)
            {
                return false;
            }
            return true;
        }

        public bool checkPhone(string phone)
        {
            var result = db.ACCOUNTs.Where(p => p.PHONENUMBER == phone).SingleOrDefault();

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public void Signup(string user, string pass, string fullname, string phone)
        {
            ACCOUNT a = new ACCOUNT
            {
                USER = user,
                PASSWORD = pass,
                FULLNAME = fullname,
                SEX = null,
                DATAOFBIRTH = null,
                STATUSACCOUNT = true,
                PHONENUMBER = phone,
            };
            db.ACCOUNTs.AddOrUpdate(a);
            db.SaveChanges();
        }
    }
}
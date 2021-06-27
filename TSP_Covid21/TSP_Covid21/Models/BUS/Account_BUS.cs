using System;
using System.Collections.Generic;
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

        public string checkUserModel(string user)
        {
            var result = db.ACCOUNTs.Where(p => p.USER == user).SingleOrDefault();
            if(result == null)
            {
                return "true";
            }
            return "false";
        }
    }
}
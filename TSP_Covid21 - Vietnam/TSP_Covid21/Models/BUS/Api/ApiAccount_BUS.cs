using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Models.BUS.Api
{
    public class ApiAccount_BUS
    {
        private COVIDEntities db;

        public ApiAccount_BUS()
        {
            db = new COVIDEntities();
        }

        public string checkLogin(string user, string pass)
        {
            var result = db.CheckLogin(user, pass).SingleOrDefault();
            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Models.BUS
{
    public class Account_BUS
    {
        private COVIDEntities1 db;

        public Account_BUS()
        {
            db = new COVIDEntities1();
        }

        public ACCOUNT checkLogin(string user)
        {
            ACCOUNT result = db.ACCOUNTs.Where(c => c.USER == user).SingleOrDefault();
            return result;
        }
    }
}
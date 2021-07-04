using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Models.BUS
{
    public class PersonalInformation_BUS
    {
        private COVIDEntities db;

        public PersonalInformation_BUS()
        {
            db = new COVIDEntities();
        }

        public ACCOUNT loadPI(string user)
        {
            var result = db.ACCOUNTs.Where(c => c.USER == user).SingleOrDefault();
            return result;
        }

        public IEnumerable<BILL> loadbill(string user)
        {
            var list = db.BILLs.Where(p => p.USER == user);
            return list.ToList();
        }
    }
}
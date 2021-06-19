using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Models.BUS
{
    public class Product_BUS
    {
        private COVIDEntities db;

        public Product_BUS()
        {
            db = new COVIDEntities();
        }
        #region methods

        public IEnumerable<PRODUCT> loadProductNew()
        {
            var result = db.PRODUCTs.ToList();
            return result;
        }

        #endregion  
    }
}
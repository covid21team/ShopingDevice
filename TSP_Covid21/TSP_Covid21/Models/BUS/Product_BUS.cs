﻿using PagedList;
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
            var date = DateTime.Now;
            var result = db.PRODUCTs.OrderByDescending(t => t.DATEADD).Take(5);
            return result;
        }

        public IEnumerable<PRODUCT> loadProductTop()
        {
            var result = db.PRODUCTs.OrderByDescending(t => t.PRODUCTVIEW).Take(5);
            return result;
        }

        public IEnumerable<PRODUCT> loadSmartPhoneTop(int page, int pagesize)
        {
            var result = from c in db.PRODUCTs
                         where c.BRANDID == 4
                         select c;

            IPagedList<PRODUCT> p = null;

            p = result.ToPagedList(page, pagesize);

            return p;
        }

        #endregion  
    }
}
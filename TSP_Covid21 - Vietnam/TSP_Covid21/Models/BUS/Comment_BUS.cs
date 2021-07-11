using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;
using TSP_Covid21.Models.ViewModel;

namespace TSP_Covid21.Models.BUS
{
    public class Comment_BUS
    {
        private COVIDEntities db;

        public Comment_BUS()
        {
            db = new COVIDEntities();
        }

        public IEnumerable<ListComment> listComment()
        {
            var result = from a in db.COMMENT
                         select new ListComment()
                         {
                             USER = a.USER,
                             NAME = a.ACCOUNT.FULLNAME,
                             PRODUCTID = a.PRODUCTID,
                             PRODUCTNAME = a.PRODUCT.PRODUCTNAME,
                             COMMENTTEXT = a.COMMENTTEXT,
                             COMMENTDATE = a.DATECOMMENT,
                         };

            return result;
        }
    }
}
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

        public IEnumerable<COMMENT> listComment()
        {
            var result = from a in db.COMMENT
                         select a;

            return result;
        }

        public void delComment(int commentId)
        {
            var result = db.COMMENT.Find(commentId);
            db.COMMENT.Remove(result);
            db.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Models.BUS
{
    public class Picture_BUS
    {
        private COVIDEntities db;

        public Picture_BUS()
        {
            db = new COVIDEntities();
        }
        public IEnumerable<PICTURE> listPic()
        {
            return db.PICTURE.ToList();
        }

        public void InsertPic(string link, string path)
        {
            PICTURE pic = new PICTURE
            {
                PATH = path,
                LINK = link,
            };
            db.PICTURE.AddOrUpdate(pic);
            db.SaveChanges();
        }

        public PICTURE pic(int id)
        {
            var result = db.PICTURE.Find(id);
            return result;
        }

        public void del(int id)
        {
            PICTURE pic = db.PICTURE.Find(id);
            db.PICTURE.Remove(pic);
            db.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
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
        public IEnumerable<string> listPic()
        {
            var result0 = db.PRODUCT.Select(p => p.MAINPIC).ToList();
            var result1 = db.PRODUCT.Select(t => t.PIC1).ToList();
            var result2 = db.PRODUCT.Select(t => t.PIC2).ToList();
            var result3 = db.PRODUCT.Select(t => t.PIC3).ToList();
            var result4 = db.PRODUCT.Select(t => t.PIC4).ToList();

            List<string> result = new List<string>();

            for(int i  = 0; i < result0.Count(); i ++)
            {
                result.Add(result0[i]);
                result.Add(result1[i]);
                result.Add(result2[i]);
                result.Add(result3[i]);
                result.Add(result4[i]);
            }

            return result;
        }


        /*public void test(string user)
        {
            var t = db.ACCOUNT; //lấy ds
            var h = db.ACCOUNT.Find(<id>) // lấy 1 hàng
            var d = db.ACCOUNT.Where(p => p.USER == user).SingleOrDefault();
        }*/
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Areas.Admin.Models
{
    public class ProductManager_BUS
    {
        private COVIDEntities db;

        public ProductManager_BUS()
        {
            db = new COVIDEntities();
        }

        public IEnumerable<PRODUCT> loadProduct()
        {
            var result = db.PRODUCT;

            return result;
        }

        public PRODUCT productDetail(int id)
        {
            var result = db.PRODUCT.Where(p => p.PRODUCTID == id).FirstOrDefault();

            return result;
        }

        public void delProduct(int id)
        {
            var product = db.PRODUCT.Where(t => t.PRODUCTID == id).FirstOrDefault();

            PRODUCT p = new PRODUCT
            {
                PRODUCTID = product.PRODUCTID,
                PRODUCTNAME = product.PRODUCTNAME,
                BRANDID = product.BRANDID,
                PRODUCTTYPEID = product.PRODUCTTYPEID,
                MAINPIC = product.MAINPIC,
                PIC1 = product.PIC1,
                PIC2 = product.PIC2,
                PIC3 = product.PIC3,
                PIC4 = product.PIC4,
                DECRIPTION = product.DECRIPTION,
                STATUSPRODUCT = false,
                PRODUCTPRICE = product.PRODUCTPRICE,
                PRODUCTAMOUNT = product.PRODUCTAMOUNT,
                DATEADD = product.DATEADD,
            };
            db.PRODUCT.AddOrUpdate(p);
            db.SaveChanges();
        }


        public IEnumerable<CONFIG> loadConfig()
        {
            return db.CONFIG;
        }

        public IEnumerable<BRAND> loadBrand()
        {
            return db.BRAND;
        }

        public IEnumerable<PRODUCTTYPE> loadType()
        {
            return db.PRODUCTTYPE;
        }

        public void editProduct(int id, string productName, int productType, int brand, int amount, string description, bool status, int price, string pic1, string pic2, string pic3, string pic4, string pic5, List<string> name, List<string> inf)
        {
            var dateadd = db.PRODUCT.Where(t => t.PRODUCTID == id).Select(c => c.DATEADD).FirstOrDefault();

            PRODUCT p = new PRODUCT
            {
                PRODUCTID = id,
                PRODUCTNAME = productName,
                BRANDID = brand,
                PRODUCTTYPEID = productType,
                MAINPIC = pic1,
                PIC1 = pic2,
                PIC2 = pic3,
                PIC3 = pic4,
                PIC4 = pic5,
                DECRIPTION = description,
                STATUSPRODUCT = status,
                PRODUCTPRICE = price,
                PRODUCTAMOUNT = amount,
                DATEADD = dateadd,
            };
            db.PRODUCT.AddOrUpdate(p);
            db.SaveChanges();

            var temp = db.CONFIGDETAIL.Where(h => h.PRODUCTID == id).ToList();
            foreach(var item in temp)
            {
                db.CONFIGDETAIL.Remove(item);
            }
            db.SaveChanges();

            for(int i = 0; i < name.Count(); i++)
            {
                CONFIGDETAIL c = new CONFIGDETAIL
                {
                    PRODUCTID = id,
                    CONFIGNAME = name[i],
                    INFORMATION = inf[i],
                };
                db.CONFIGDETAIL.AddOrUpdate(c);
                db.SaveChanges();
            }
        }

        public void addSP(string productName, int productType, int brand, int amount, string description, int price, string pic1, string pic2, string pic3, string pic4, string pic5)
        {
            DateTime time = DateTime.Now;

            PRODUCT p = new PRODUCT
            {
                PRODUCTNAME = productName,
                BRANDID = brand,
                PRODUCTTYPEID = productType,
                MAINPIC = pic1,
                PIC1 = pic2,
                PIC2 = pic3,
                PIC3 = pic4,
                PIC4 = pic5,
                DECRIPTION = description,
                STATUSPRODUCT = true,
                PRODUCTPRICE = price,
                PRODUCTAMOUNT = amount,
                DATEADD = time,
            };
            db.PRODUCT.Add(p);
            db.SaveChanges();
        }
    }
}
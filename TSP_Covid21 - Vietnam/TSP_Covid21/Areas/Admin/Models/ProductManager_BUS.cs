using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using TSP_Covid21.Areas.Admin.Models.ViewModel;
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

            PRODUCT p = db.PRODUCT.Where(t => t.PRODUCTID == id).FirstOrDefault();
            p.STATUSPRODUCT = false;
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

        public void editProduct(int id, string productName, int productType, int brand, int amount, string description, bool status, int price, string pic1, string[] pic2, string[] name, string[] inf)
        {
            PRODUCT p = db.PRODUCT.Find(id);
            p.PRODUCTNAME = productName;
            p.BRANDID = brand;
            p.PRODUCTTYPEID = productType;
            p.DECRIPTION = description;
            p.STATUSPRODUCT = status;
            p.PRODUCTPRICE = price;
            p.PRODUCTAMOUNT = amount;
            db.PRODUCT.AddOrUpdate(p);
            db.SaveChanges();

            del_Old(id);
            if (name.Length > 0)
            {
                insert_Config(id, name, inf);
            }
            insert_PicDetail(id, pic1, pic2);
        }

        public void addSP(string productName, int productType, int brand, int amount, string description, int price, string pic1, string[] pic2)
        {
            DateTime time = DateTime.Now;

            PRODUCT p = new PRODUCT
            {
                PRODUCTNAME = productName,
                BRANDID = brand,
                PRODUCTTYPEID = productType,
                DECRIPTION = description,
                STATUSPRODUCT = true,
                PRODUCTPRICE = price,
                PRODUCTAMOUNT = amount,
                DATEADD = time,
            };
            db.PRODUCT.Add(p);
            db.SaveChanges();

            int id = db.PRODUCT.OrderByDescending(y => y.PRODUCTID).Select(t => t.PRODUCTID).FirstOrDefault();

            insert_PicDetail(id, pic1, pic2);
        }

        public IEnumerable<ListPic_Edit> listPic(int productId)
        {
            var result = from t in db.PICTURE
                         select new ListPic_Edit
                         { 
                             pictureId = t.PICTUREID,
                             mainPic = t.PIC_PRODUCT.Where(c => c.PRODUCTID == productId).Select(y => y.MAINPIC).FirstOrDefault(),
                             path = t.PATH,
                             Pic = (t.PIC_PRODUCT.Where(c => c.PRODUCTID == productId).Count() == 0 ? false : true),
                         };

            return result.ToList();
        }

        private void del_Old(int id)
        {
            var temp = db.CONFIGDETAIL.Where(h => h.PRODUCTID == id).ToList();
            foreach (var item in temp)
            {
                db.CONFIGDETAIL.Remove(item);
                db.SaveChanges();
            }

            var delOld_Pic = db.PIC_PRODUCT.Where(l => l.PRODUCTID == id).ToList();
            foreach (var item in delOld_Pic)
            {
                db.PIC_PRODUCT.Remove(item);
                db.SaveChanges();
            }
        }

        private void insert_Config(int id, string[] name, string[] inf)
        {
            for (int i = 0; i < name.Length; i ++)
            {
                CONFIGDETAIL c = new CONFIGDETAIL
                {
                    PRODUCTID = id,
                    CONFIGNAME = name[i],
                    INFORMATION = inf[i],
                };
                db.CONFIGDETAIL.Add(c);
            }
            db.SaveChanges();
        }

        private void insert_PicDetail(int id, string pic1, string[] pic2)
        {
            PIC_PRODUCT pp = new PIC_PRODUCT
            {
                PRODUCTID = id,
                PICTUREID = int.Parse(pic1),
                MAINPIC = true,
            };
            db.PIC_PRODUCT.AddOrUpdate(pp);
            db.SaveChanges();

            foreach (string item in pic2)
            {
                PIC_PRODUCT pp1 = new PIC_PRODUCT
                {
                    PRODUCTID = id,
                    PICTUREID = int.Parse(item),
                    MAINPIC = false,
                };
                db.PIC_PRODUCT.AddOrUpdate(pp1);
            }
            db.SaveChanges();
        }
    }
}
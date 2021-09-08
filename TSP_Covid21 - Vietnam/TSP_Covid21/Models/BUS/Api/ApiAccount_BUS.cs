using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ApiObject;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Models.BUS.Api
{
    public class ApiAccount_BUS
    {
        private COVIDEntities db;

        public ApiAccount_BUS()
        {
            db = new COVIDEntities();
        }

        public string checkLogin(string user, string pass)
        {
            var result = db.CheckLogin(user, pass).SingleOrDefault();
            return result;
        }

        public string getFullName(string user)
        {
            var result = db.ACCOUNT.Find(user).FULLNAME;
            return result;
        }

        public IEnumerable<Account> getAccount(string user)
        {
            var result = from a in db.ACCOUNT
                         where a.USER == user
                         select new Account()
                         {
                             user = a.USER,
                             fullName = a.FULLNAME,
                             phoneNumber = a.PHONENUMBER,
                             email = a.EMAIL,
                             date = a.DATAOFBIRTH.ToString(),
                             sex = a.SEX
                         };
            return result;
        }

        public ACCOUNT checkUser(string user)
        {
            return db.ACCOUNT.Find(user);
        }

        public ACCOUNT checkPhone(string phone)
        {
            return db.ACCOUNT.Where(t => t.PHONENUMBER == phone).FirstOrDefault();
        }

        public ACCOUNT checkEmail(string email)
        {
            return db.ACCOUNT.Where(t => t.EMAIL == email).FirstOrDefault();
        }

        public IEnumerable<Product> getAllSeenProduct(string user)
        {
            var result = from a in db.VIEWNUMBER
                         where a.USER == user
                         select new Product()
                         {
                             id = a.PRODUCT.PRODUCTID,
                             name = a.PRODUCT.PRODUCTNAME,
                             picMain = a.PRODUCT.PIC_PRODUCT.Where(t => t.MAINPIC == true && t.PRODUCTID == a.PRODUCTID).Select(c => c.PICTURE.LINK).FirstOrDefault(),
                             price = a.PRODUCT.PRODUCTPRICE,
                             listPic = a.PRODUCT.PIC_PRODUCT.Where(t => t.MAINPIC == false && t.PRODUCTID == a.PRODUCTID).Select(c => c.PICTURE.LINK),
                         };
            return result;
        }

        
    }
}
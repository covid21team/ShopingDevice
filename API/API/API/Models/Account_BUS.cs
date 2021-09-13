using API.Database;
using API.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Account_BUS
    {
        private readonly COVIDContext db;

        public Account_BUS(COVIDContext context)
        {
            db = context;
        }

        public string checkLogin(string user, string pass)
        {
            var result = db.Account.Where(t => t.User == user && t.Password == pass).Select(c => c.User).FirstOrDefault();
            return result;
        }

        public string getFullName(string user)
        {
            var result = db.Account.Find(user).Fullname;
            return result;
        }

        public IEnumerable<Object.Account> getAccount(string user)
        {
            var result = from a in db.Account
                         where a.User == user
                         select new Object.Account()
                         {
                             user = a.User,
                             fullName = a.Fullname,
                             phoneNumber = a.Phonenumber,
                             email = a.Email,
                             date = a.Dataofbirth.ToString(),
                             sex = a.Sex
                         };
            return result;
        }

        public Database.Account checkUser(string user)
        {
            return db.Account.Find(user);
        }

        public Database.Account checkPhone(string phone)
        {
            return db.Account.Where(t => t.Phonenumber == phone).FirstOrDefault();
        }

        public Database.Account checkEmail(string email)
        {
            return db.Account.Where(t => t.Email == email).FirstOrDefault();
        }

        public void SignUp(Object.AccountSignUp acc)
        {
            Database.Account a = new Database.Account
            {
                User = acc.user,
                Password = acc.pass,
                Fullname = acc.fullname,
                Sex = null,
                Dataofbirth = null,
                Statusaccount = true,
                Phonenumber = acc.phonenumber,
                Email = acc.email,
            };
            db.Account.Add(a);
            db.SaveChanges();
        }

        public IEnumerable<Object.Product> getAllSeenProduct(string user)
        {
            var result = from a in db.Viewnumber
                         where a.User == user
                         select new Object.Product()
                         {
                             id = a.Product.Productid,
                             name = a.Product.Productname,
                             picMain = a.Product.PicProduct.Where(t => t.Mainpic == true && t.Productid == a.Productid).Select(c => c.Picture.Link).FirstOrDefault(),
                             price = a.Product.Productprice,
                             listPic = a.Product.PicProduct.Where(t => t.Mainpic == false && t.Productid == a.Productid).Select(c => c.Picture.Link),
                         };
            return result;
        }

        public IEnumerable<Object.ProductOfCart> getCart(string user)
        {
            var result = from a in db.Cart
                         where a.User == user
                         select new Object.ProductOfCart()
                         {
                             productId = a.Productid,
                             productName = a.Product.Productname,
                             productStatus = a.Productstatus,
                             price = a.Product.Productprice,
                             pic = a.Product.PicProduct.Where(t => t.Mainpic == true && t.Productid == a.Productid).Select(c => c.Picture.Link).FirstOrDefault(),
                             amount = a.Amount,
                         };

            return result;
        }

        public IEnumerable<Object.Bill> getBills(string user)
        {
            var result = from a in db.Bill
                         where a.User == user
                         select new Object.Bill()
                         {
                             billId = a.Billid,
                             totalBill = a.Totalbill,
                             date = a.Datecreate,
                             billStatus = a.BillStatus,
                             products = a.Billdetail.Where(t => t.Billid == a.Billid).Select(c => c.Product.Productname),
                         };

            return result;
        }

        public Object.BillDetail getBillDetail(int billId)
        {
            var result = from a in db.Bill
                         where a.Billid == billId
                         select new Object.BillDetail()
                         {
                             city = a.City,
                             district = a.District,
                             ward = a.Wards,
                             address = a.Address,
                             phone = a.Phone,
                             billStatus = a.BillStatus,
                             productOfBills = from b in db.Billdetail
                                              where b.Billid == a.Billid
                                              select new Object.ProductOfBill()
                                              {
                                                  productId = b.Productid,
                                                  productName = b.Product.Productname,
                                                  amount = b.Amount,
                                                  price = b.Product.Productprice,
                                                  pic = b.Product.PicProduct.Where(t => t.Mainpic == true && t.Productid == b.Productid).Select(c => c.Picture.Link).FirstOrDefault(),
                                              }
                         };

            return result.FirstOrDefault();
        }
    }
}

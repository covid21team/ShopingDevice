using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;
using TSP_Covid21.Models.ViewModel;

namespace TSP_Covid21.Models.BUS
{
    public class Account_BUS
    {
        private COVIDEntities db;

        public Account_BUS()
        {
            db = new COVIDEntities();
        }

        public string checkLogin(string user,string pass)
        {
            var result = db.CheckLogin(user, pass).SingleOrDefault();
            return result;
        }

        public string takeFullName(string user)
        {
            var result = db.ACCOUNT.Where(c => c.USER == user).Select(p => p.FULLNAME).SingleOrDefault();
            return result;
        }

        public string takeGmail(string user)
        {
            var result = db.ACCOUNT.Where(c => c.USER == user).Select(p => p.EMAIL).SingleOrDefault();
            return result;
        }

        public bool checkUser(string user)
        {
            var result = db.ACCOUNT.Where(p => p.USER == user).SingleOrDefault();

            if(result == null)
            {
                return false;
            }
            return true;
        }

        public bool checkPhone(string phone)
        {
            var result = db.ACCOUNT.Where(p => p.PHONENUMBER == phone).SingleOrDefault();

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public bool checkEmail(string email)
        {
            var result = db.ACCOUNT.Where(p => p.EMAIL == email).SingleOrDefault();

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public void Signup(string user, string pass, string fullname, string phone)
        {
            ACCOUNT a = new ACCOUNT
            {
                USER = user,
                PASSWORD = pass,
                FULLNAME = fullname,
                SEX = null,
                DATAOFBIRTH = null,
                STATUSACCOUNT = true,
                PHONENUMBER = phone,
            };
            db.ACCOUNT.AddOrUpdate(a);
            db.SaveChanges();
        }

        public AccountViewModel account(string user,int code)
        {
            var result = from a in db.ACCOUNT
                         where a.USER == user
                         select new AccountViewModel()
                         {
                             account = a,
                             code = code,
                         };
            return result.FirstOrDefault();
        }

        public IEnumerable<VIEWNUMBER> view(string user)
        {
            return db.VIEWNUMBER.Where(p => p.USER == user);
        }

        public IEnumerable<RATINGPRODUCT> rating(string user)
        {
            return db.RATINGPRODUCT.Where(p => p.USER == user);
        }

        public IEnumerable<BILL> loadBill(string user)
        {
            return db.BILL.Where(p => p.USER == user);
        }

        public IEnumerable<ADDRESS_SHIP> loadAddress(string user)
        {
            return db.ADDRESS_SHIP.Where(p => p.USER == user & p.ADDRESS_STATUS == true);
        }

        public IEnumerable<ADDRESS_SHIP> addressDefault(string user)
        {
            return db.ADDRESS_SHIP.Where(p => p.USER == user & p.DEFAULT == true);
        }

        public void changeInf(string user, string fullname, bool sex, string birth, string email, string phone)
        {
            ACCOUNT a = db.ACCOUNT.Where(p => p.USER == user).SingleOrDefault();
            a.FULLNAME = fullname;
            a.SEX = sex;
            if(birth != "NaN/NaN/NaN")
            {
                DateTime date = DateTime.Parse(birth);
                a.DATAOFBIRTH = date;
            }
            a.PHONENUMBER = phone;
            a.EMAIL = email;
            db.SaveChanges();
        }

        public void insertAddress(string user, string fullname, string phone, string city, string district, string ward, string address, bool addDefault)
        {
            if (addDefault == true)
            {
                ADDRESS_SHIP c = db.ADDRESS_SHIP.Where(p => p.USER == user & p.DEFAULT == true).FirstOrDefault();
                if(c != null)
                {
                    c.DEFAULT = false;
                    db.SaveChanges();
                }
                
            }
            else
            {
                IEnumerable<ADDRESS_SHIP> list = db.ADDRESS_SHIP.Where(p => p.USER == user);
                if (list.Count() == 0)
                {
                    addDefault = true;
                }
            }

            ADDRESS_SHIP a = new ADDRESS_SHIP
            {
                USER = user,
                FULLNAME = fullname,
                PHONE = phone,
                CITY = city,
                DISTRICT = district,
                WARDS = ward,
                ADDRESS = address,
                DEFAULT = addDefault,
                ADDRESS_STATUS = true,
            };
            db.ADDRESS_SHIP.Add(a);
            db.SaveChanges();
        }

        public void changePass(string user, string pass_new)
        {
            ACCOUNT a = db.ACCOUNT.Where(p => p.USER == user).FirstOrDefault();
            a.PASSWORD = pass_new;
            db.SaveChanges();
        }

        public ADDRESS_SHIP loadadd(int addressId)
        {
            return db.ADDRESS_SHIP.Where(p => p.ADDRESSID == addressId).FirstOrDefault();
        }

        public void editAddress_ship(int addressId,string user, string fullname, string phone, string city, string district, string ward, string address, bool addDefault)
        {
            if (addDefault == true)
            {
                ADDRESS_SHIP c = db.ADDRESS_SHIP.Where(p => p.DEFAULT == true).FirstOrDefault();
                c.DEFAULT = false;
                db.SaveChanges();
            }

            ADDRESS_SHIP a = new ADDRESS_SHIP
            {
                ADDRESSID = addressId,
                USER = user,
                FULLNAME = fullname,
                PHONE = phone,
                CITY = city,
                DISTRICT = district,
                WARDS = ward,
                ADDRESS = address,
                DEFAULT = addDefault,
                ADDRESS_STATUS = true,
            };
            db.ADDRESS_SHIP.AddOrUpdate(a);
            db.SaveChanges();

            if(addDefault == false)
            {
                IEnumerable<ADDRESS_SHIP> list = db.ADDRESS_SHIP.Where(p => p.DEFAULT == true);
                if (list.Count() == 0)
                {
                    ADDRESS_SHIP s = db.ADDRESS_SHIP.Where(p => p.ADDRESSID == addressId).FirstOrDefault();
                    s.DEFAULT = true;
                    db.SaveChanges();
                }
            }
        }

        public string checkUserAdmin(string user, string pass)
        {
           var result = db.CheckLoginAdmin(user, pass).SingleOrDefault();
           return result;
        }

        public ACCOUNT_ADMIN acountAdmin(string user)
        {
            return db.ACCOUNT_ADMIN.Where(p => p.USER == user).SingleOrDefault();
        }

        public BILL BillDetail(int id)
        {
            return db.BILL.Find(id);
        }

        public void cancelBill(int id)
        {
            var Bill = db.BILL.Find(id);
            foreach(var item in Bill.BILLDETAIL)
            {
                var product = db.PRODUCT.Find(item.PRODUCTID);
                product.PRODUCTAMOUNT += item.AMOUNT;
            }
            Bill.BIllSTATUS = 4;
            db.SaveChanges();
        }

        public void editAdress_Bill(int id, string fullname, string phone, string city, string district, string ward, string address)
        {
            var bill = db.BILL.Find(id);
            bill.FULLNAME = fullname;
            bill.PHONE = phone;
            bill.CITY = city;
            bill.DISTRICT = district;
            bill.WARDS = ward;
            bill.ADDRESS = address;
            db.SaveChanges();
        }

        public IEnumerable<ListAccount> listAccount()
        {
            var result = from a in db.ACCOUNT
                         select new ListAccount()
                         {
                             USER = a.USER,
                             FULLNAME = a.FULLNAME,
                             PHONE = a.PHONENUMBER,
                             EMAIL = a.EMAIL,
                             QUANTITYPRODUCT = a.BILL.Sum(p => p.BILLDETAIL.Sum(t => t.AMOUNT)),
                             TOTAL = a.BILL.Sum(p => p.BILLDETAIL.Sum(t => t.AMOUNT * t.PRODUCT.PRODUCTPRICE)),
                         };

            return result;
        }
    }
}
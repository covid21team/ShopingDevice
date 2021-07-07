﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

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

        public ACCOUNT account(string user)
        {
            return db.ACCOUNT.Where(p => p.USER == user).FirstOrDefault();
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

        public void changeInf(string user, string fullname, bool sex, DateTime birth, string email, string phone)
        {
            
        }

        public void insertAddress(string user, string fullname, string phone, string city, string district, string ward, string address, bool addDefault)
        {
            if (addDefault == true)
            {
                ADDRESS_SHIP c = db.ADDRESS_SHIP.Where(p => p.USER == user & p.DEFAULT == true).FirstOrDefault();
                c.DEFAULT = false;
                db.SaveChanges();
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
    }
}
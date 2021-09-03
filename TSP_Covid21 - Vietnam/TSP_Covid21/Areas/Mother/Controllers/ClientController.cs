using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Areas.Mother.ViewModel;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Areas.Mother.Controllers
{
    public class ClientController : Controller
    {

        private COVIDEntities db;

        public ClientController()
        {
            db = new COVIDEntities();
    }
        // GET: Mother/Client
        public ActionResult Home()
        {
            return View();
        }

        public IEnumerable<ww_Product> ListProduct()
        {
            return db.ww_Product;
        }

        public void CreateBill(string name, string phone)
        {
            ww_Customer customer = new ww_Customer()
            {
                Name = name,
                Phone = phone,
            };
            db.ww_Customer.AddOrUpdate(customer);
            db.SaveChanges();

            var billState = db.ww_Bill.Where(t => t.Phone == phone).OrderByDescending(c => c.BillId).Select(c => c.BillState).FirstOrDefault();

            if (billState == true || billState == null)
            {
                ww_Bill bill = new ww_Bill()
                {
                    Phone = phone,
                    BillState = false,
                };
                db.ww_Bill.AddOrUpdate(bill);
                db.SaveChanges();
            }
    
        }

        public void AddBillDetal(int productId, float amount, string phone)
        {
            var myBill = db.ww_Bill.Where(t => t.Phone == phone).OrderByDescending(c => c.BillId).FirstOrDefault();

            ww_BillDetail b = db.ww_BillDetail.Where(t => t.BillId == myBill.BillId && t.ProductId == productId).SingleOrDefault();

            ww_Product p = db.ww_Product.Find(productId);
            if(b != null)
            {
                p.Quantity = p.Quantity + b.Amount - amount;
            }
            else
            {
                p.Quantity -= amount;
            }
            db.ww_Product.AddOrUpdate(p);
            db.SaveChanges();

            ww_BillDetail billDetail = new ww_BillDetail()
            {
                BillId = myBill.BillId,
                ProductId = productId,
                Amount = amount,
            };
            db.ww_BillDetail.AddOrUpdate(billDetail);
            db.SaveChanges();
        }

        public ActionResult billDetail(string phone)
        {
            BillDetail_Cus b = new BillDetail_Cus()
            {
                BillDetail = db.ww_Bill.Where(t => t.Phone == phone && t.BillState == false).Select(c => c.ww_BillDetail).FirstOrDefault(),
                Customer = db.ww_Customer.Find(phone),
            };

            return PartialView(b);
        }

        [HttpPost]
        public float checkAmount(int id, float amount)
        {
            var p = db.ww_Product.Find(id);
            if (amount > (float)p.Quantity)
            {
                return (float)p.Quantity;
            }
            else
            {
                return amount;
            }
        }
    }
}
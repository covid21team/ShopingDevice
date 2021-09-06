using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.ShopEntity;
using TSP_Covid21.Areas.Admin.Models;
using TSP_Covid21.Areas.Admin.Models.ViewModel;

namespace TSP_Covid21.Areas.Admin.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductManager_BUS PS;

        public ProductManagerController()
        {
            PS = new ProductManager_BUS();
        }
        // GET: Admin/Home
        public ActionResult ListProduct()
        {
            return View();
        }

        public IEnumerable<PRODUCT> loadProduct()
        {
            var result = PS.loadProduct();

            return result;
        }

        public ActionResult productDetail(int id)
        {
            var result = PS.productDetail(id);

            return PartialView(result);
        }

        public void delProduct(int id)
        {
            PS.delProduct(id);
        }

        public ActionResult editProduct(int id)
        {
            var result = PS.productDetail(id);

            return PartialView(result);
        }

        public IEnumerable<CONFIG> loadConfig()
        {
            return PS.loadConfig();
        }

        public IEnumerable<BRAND> loadBrand()
        {
            return PS.loadBrand();  
        }

        public IEnumerable<PRODUCTTYPE> loadType()
        {
            return PS.loadType();
        }

        public void upProduct(int id, string productName, int productType, int brand, int amount, String description, bool status, int price, string pic1, string pic2, string name, string inf)
        {
            string[] configname = { };
            string[] configinf = { };
            if (name.Length > 0)
            {
                name = name.Remove(name.Length - 1, 1);
                inf = inf.Remove(inf.Length - 1, 1);
                configname = name.Split('&');
                configinf = inf.Split('&');
            }
            pic2 = pic2.Remove(pic2.Length - 1, 1);
            string[] pic = pic2.Split(',');

            PS.editProduct(id, productName, productType, brand, amount, description, status, price, pic1, pic, configname, configinf);
        }

        public ActionResult addProduct()
        {
            return PartialView();
        }

        public void addSP(string productName, int productType, int brand, int amount, string description, int price, string pic1, string pic2)
        {
            pic2 = pic2.Remove(pic2.Length - 1, 1);
            string[] pic = pic2.Split(',');
            PS.addSP(productName, productType, brand, amount, description, price, pic1, pic);
        }

        public IEnumerable<ListPic_Edit> listPic(int productId)
        {
            return PS.listPic(productId);
        }
    }
}
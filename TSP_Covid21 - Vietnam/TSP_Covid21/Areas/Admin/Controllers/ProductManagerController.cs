using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.ShopEntity;
using TSP_Covid21.Areas.Admin.Models;

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

        public void upProduct(int id, string productName, int productType, int brand, int amount, string description, bool status, int price, string pic1, string pic2, string pic3, string pic4, string pic5, string name, string inf)
        {
            List<string> configname = new List<string>();
            List<string> configinf = new List<string>();

            int dem = 0;
            string temp_backup = "";
            // Phân tích tên cấu hình
            for(int i = 0; i < name.Length; i++)
            {
                if (name[i].ToString().Equals("&"))
                {
                    configname.Add(temp_backup);
                    temp_backup = "";
                    dem++;
                }
                else
                {
                    temp_backup += name[i].ToString();
                }
            }
            dem = 0;
            temp_backup = "";
            // Phân tích thông tin cấu hình
            for (int i = 0; i < inf.Length; i++)
            {
                if (inf[i].ToString().Equals("&"))
                {
                    configinf.Add(temp_backup);
                    temp_backup = "";
                    dem++;
                }
                else
                {
                    temp_backup += inf[i].ToString();
                }
            }

            PS.editProduct(id, productName, productType, brand, amount, description, status, price, pic1, pic2, pic3, pic4, pic5);
        }

        public ActionResult addProduct()
        {
            return PartialView();
        }

        public void addSP(string productName, int productType, int brand, int amount, string description, int price, string pic1, string pic2, string pic3, string pic4, string pic5)
        {
            PS.addSP(productName, productType, brand, amount, description, price, pic1, pic2, pic3, pic4, pic5);
        }
    }
}
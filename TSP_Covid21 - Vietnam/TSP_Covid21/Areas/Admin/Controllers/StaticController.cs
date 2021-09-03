using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.BUS;
using TSP_Covid21.Models.ShopEntity;
using TSP_Covid21.Models.ViewModel;

namespace TSP_Covid21.Areas.Admin.Controllers
{
    public class StaticController : Controller
    {
        Static_BUS SB;

        public StaticController()
        {
            SB = new Static_BUS();
        }

        // GET: Admin/Static
        public ActionResult All()
        {
            return View();
        }

        // Doanh thu trong 1 năm
        public string Revenue()
        { 
            var result = SB.Revenue();
            if(result == "")
            {
                result = "0";
            }

            return result;
        }

        // Số sản phẩm được bán ra trong 1 năm
        public string totalProduct()
        {
            return SB.totalProduct();
        }

        // Số hóa đơn thành công bán ra trong 1 năm
        public string totalBill()
        {
            return SB.totalBill();
        }

        public IEnumerable<BILL> loadBill()
        {
            return SB.loadBill();
        }

        public string StaticProductType()
        {
            string a = "";
            IEnumerable<StaticProductType> data = SB.StaticProductType();
            foreach(var item in data)
            {
                if (item.QUANTITY == null)
                {
                    item.QUANTITY = 0;
                }
                a += item.PRODUCTYPENAME + ":" + item.QUANTITY + ",";
            }

            return a;
        }

        public string StaticBrand(int productTypeId)
        {
            if(productTypeId == 0)
            {
                return StaticProductType();
            }
            string a = "";
            IEnumerable<StaticBrand> data = SB.StaticBrand(productTypeId);
            foreach (var item in data)
            {
                if(item.QUANTITY == null)
                {
                    item.QUANTITY = 0;
                }
                a += item.BRANDNAME + ":" + item.QUANTITY + ",";
            }

            return a;
        }

        public IEnumerable<PRODUCT> listProduct()
        {
            return SB.listProduct();
        }
    }
}
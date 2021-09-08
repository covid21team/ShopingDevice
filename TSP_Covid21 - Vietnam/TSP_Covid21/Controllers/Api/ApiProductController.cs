using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.BUS;
using TSP_Covid21.Models.BUS.Api;

namespace TSP_Covid21.Controllers.Api
{
    public class ApiProductController : Controller
    {
        private ApiProduct_BUS api_BUS;

        public JsonResult listProduct()
        {
            api_BUS = new ApiProduct_BUS();
            var result = api_BUS.listProduct();
            return Json(new
            {
                data = result,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult productDetail(int productId)
        {
            api_BUS = new ApiProduct_BUS();
            var result = api_BUS.productDetail(productId);
            return Json(new
            {
                data = result.FirstOrDefault(),
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult productConfigs(int productId)
        {
            api_BUS = new ApiProduct_BUS();
            var result = api_BUS.productConfigs(productId);
            return Json(new
            {
                data = result,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult productRatings(int productId)
        {
            api_BUS = new ApiProduct_BUS();
            var result = api_BUS.productRatings(productId);
            return Json(new
            {
                data = result,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.ApiObject;
using TSP_Covid21.Models.BUS.Api;

namespace TSP_Covid21.Controllers.Api
{
    public class ApiAccountController : Controller
    {
        private ApiAccount_BUS api_BUS;

        [HttpPost]
        public JsonResult Login(AccountLogin account)
        {
            if(account.User == null || account.Pass == null)
            {
                return Json(new
                {
                    status = 2,
                    notifi = "Tài khoản và mật khẩu không được để trống"
                }, JsonRequestBehavior.AllowGet);
            }

            api_BUS = new ApiAccount_BUS();
            string checkLogin = api_BUS.checkLogin(account.User, account.Pass);

            if(checkLogin == account.User)
            {
                return Json(new
                {
                    status = 1,
                    notifi = "Đã đăng nhập thành công"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = 0,
                notifi = "Tài khoản hoặc mật khẩu bị sai"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
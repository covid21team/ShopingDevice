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
        public JsonResult login(AccountLogin account)
        {
            if(account.User == null || account.Pass == null)
            {
                return Json(new
                {
                    message = new Message(2, "Tài khoản và mật khẩu không được để trống"),
                    fullName = ""
                }, JsonRequestBehavior.AllowGet);
            }

            api_BUS = new ApiAccount_BUS();
            string checkLogin = api_BUS.checkLogin(account.User, account.Pass);

            if(checkLogin == account.User)
            {
                string name = api_BUS.getFullName(account.User);

                return Json(new
                {
                    message = new Message(1, "Đã đăng nhập thành công"),
                    fullName = name
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                message = new Message(0, "Tài khoản hoặc mật khẩu bị sai"),
                fullName = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAccount(string user)
        {
            if (user == null)
            {
                return Json(new
                {
                    data = "",
                    message = new Message(2, "Tài khoản không được để trống")
                }, JsonRequestBehavior.AllowGet);
            }

            api_BUS = new ApiAccount_BUS();
            var result = api_BUS.getAccount(user).FirstOrDefault();

            if(result == null)
            {
                return Json(new
                {
                    data = "",
                    message = new Message(0, "Tài khoản không tồn tại")
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                data = result,
                message = new Message(1, "Lấy dữ liệu thành công")
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult checkUser(string user)
        {
            if(user == null)
            {
                return Json(new
                {
                    message = new Message(2, "Tài khoản không được để trống")
                }, JsonRequestBehavior.AllowGet);
            }

            api_BUS = new ApiAccount_BUS();
            var result = api_BUS.checkUser(user);

            if (result == null)
            {
                return Json(new
                {
                    message = new Message(0, "Tài khoản chưa tồn tại")
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                message = new Message(1, "Tài khoản đã tồn tại")
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult checkPhone(string phone)
        {
            if (phone == null)
            {
                return Json(new
                {
                    message = new Message(3, "Số điện thoại không được để trống")
                }, JsonRequestBehavior.AllowGet);
            }

            if (phone.Length != 10)
            {
                return Json(new
                {
                    message = new Message(2, "Vui lòng nhập đúng số điện thoại")
                }, JsonRequestBehavior.AllowGet);
            }

            api_BUS = new ApiAccount_BUS();
            var result = api_BUS.checkPhone(phone);

            if (result == null)
            {
                return Json(new
                {
                    message = new Message(0, "Số điện thoại chưa đăng ký")
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                message = new Message(1, "Số điện thoại này đã được đăng ký")
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult checkEmail(string email)
        {
            if (email == null)
            {
                return Json(new
                {
                    message = new Message(3, "Email không được để trống")
                }, JsonRequestBehavior.AllowGet);
            }

            if (email.IndexOf("@") == -1 || email.IndexOf(".com") == -1)
            {
                return Json(new
                {
                    message = new Message(2, "Vui lòng nhập đúng email")
                }, JsonRequestBehavior.AllowGet);
            }

            api_BUS = new ApiAccount_BUS();
            var result = api_BUS.checkUser(email);

            if (result == null)
            {
                return Json(new
                {
                    message = new Message(0, "Email chưa đăng ký")
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                message = new Message(1, "Email này đã đăng ký")
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAllSeenProduct(string user)
        {
            if (user == null)
            {
                return Json(new
                {
                    data = "",
                    message = new Message(2, "Tài khoản không được để trống")
                }, JsonRequestBehavior.AllowGet);
            }

            api_BUS = new ApiAccount_BUS();
            var result = api_BUS.getAllSeenProduct(user);

            return Json(new
            {
                data = result,
                message = new Message(1, "Lấy dữ liệu thành công")
            }, JsonRequestBehavior.AllowGet);
        }

       
    }
}
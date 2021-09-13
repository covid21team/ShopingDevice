using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/api/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private Account_BUS Model;
        private TokenController Token;

        public LoginController(IConfiguration config, COVIDContext context)
        {
            Model = new Account_BUS(context);
            Token = new TokenController(config, context);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login(Object.AccountLogin account)
        {
            if (account.user == null || account.pass == null)
            {
                return Ok(new Object.MessageLogin(2, "Tài khoản và mật khẩu không được để trống", "",""));
            }

            var pass = Object.Orther.CreateMD5(account.pass);
            pass = pass.Substring(0, pass.Length - 2);

            string checkLogin = Model.checkLogin(account.user, pass);

            if (checkLogin == account.user)
            {
                string name = Model.getFullName(account.user);
                string token = Token.GetToken(checkLogin);
                return Ok(new Object.MessageLogin(1, "Đã đăng nhập thành công", name, token));
            }

            return Ok(new Object.MessageLogin(0, "Tài khoản hoặc mật khẩu bị sai", "",""));
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult signup(Object.AccountSignUp account)
        {
            if(account.user == null || account.pass == null || account.fullname == null || account.phonenumber == null || account.email == null)
            {
                return Ok(new Object.Message(0, "Vui lòng nhập đầy đủ các mục"));
            }
            if(account.phonenumber.Length != 10)
            {
                return Ok(new Object.Message(6, "Vui lòng nhập đúng số điện thoại"));
            }
            //Xem hàm check email có đúng format
            if(account.email.IndexOf("@") == -1 || account.email.IndexOf(".com") == -1)
            {
                return Ok(new Object.Message(5, "Vui lòng nhập đúng email"));
            }
            if(Model.checkUser(account.user) != null)
            {
                return Ok(new Object.Message(4, "Tài khoản đã tồn tại"));
            }
            if(Model.checkPhone(account.phonenumber) != null)
            {
                return Ok(new Object.Message(3, "Số điện thoại này đã có tài khoản"));
            }
            if(Model.checkEmail(account.email) != null)
            {
                return Ok(new Object.Message(2, "Email này đã có tài khoản"));
            }
            Model.SignUp(account);

            return Ok(new Object.Message(1, "Đăng ký thành công"));
        }

        [HttpGet]
        [Route("checkUser")]
        public IActionResult checkUser(string user)
        {
            if (user == null)
            {
                return Ok(new Object.Message(2, "Tài khoản không được để trống"));
            }

            var result = Model.checkUser(user);

            if (result == null)
            {
                return Ok(new Object.Message(0, "Tài khoản chưa tồn tại"));
            }
            return Ok(new Object.Message(1, "Tài khoản đã tồn tại"));
        }

        [HttpGet]
        [Route("checkPhone")]
        public IActionResult checkPhone(string phone)
        {
            if (phone == null)
            {
                return Ok(new Object.Message(3, "Số điện thoại không được để trống"));
            }

            if (phone.Length != 10)
            {
                return Ok(new Object.Message(2, "Vui lòng nhập đúng số điện thoại"));
            }

            var result = Model.checkPhone(phone);

            if (result == null)
            {
                return Ok(new Object.Message(0, "Số điện thoại chưa đăng ký"));
            }
            return Ok(new Object.Message(1, "Số điện thoại này đã được đăng ký"));
        }

        [HttpGet]
        [Route("checkEmail")]
        public IActionResult checkEmail(string email)
        {
            if (email == null)
            {
                return Ok(new Object.Message(3, "Email không được để trống"));
            }

            if (email.IndexOf("@") == -1 || email.IndexOf(".com") == -1)
            {
                return Ok(new Object.Message(2, "Vui lòng nhập đúng email"));
            }

            var result = Model.checkUser(email);

            if (result == null)
            {
                return Ok(new Object.Message(0, "Email chưa đăng ký"));
            }
            return Ok(new Object.Message(1, "Email này đã đăng ký"));
        }
    }
}
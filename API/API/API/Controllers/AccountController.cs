using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Database;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Authorize]
    [Route("/api/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private Account_BUS Model;
        private TokenController Token;

        public AccountController(IConfiguration config, COVIDContext context)
        {
            Model = new Account_BUS(context);
            Token = new TokenController(config, context);
        }

        /*[HttpGet]
        [Route("allAccount")]
        public IActionResult GetAllAccount()
        {
            Dictionary<string, string> requestHeaders =
            new Dictionary<string, string>();
            foreach (var header in Request.Headers)
            {
                requestHeaders.Add(header.Key, header.Value);
            }
            var stream = requestHeaders["Authorization"].Substring(7);

            var token = new JwtSecurityToken(jwtEncodedString: stream);
            string t = token.Claims.First(c => c.Type == "User").Value;

            return Ok(new Object.MessageGetAccount(null, 2, "Tài khoản không được để trống"));
        }*/

        [HttpGet]
        [Route("getAccount")]
        public IActionResult getAccount(string user)
        {
            if (user == null)
            {
                return Ok(new Object.MessageGetAccount(null, 2, "Tài khoản không được để trống"));
            }

            Dictionary<string, string> requestHeaders =
            new Dictionary<string, string>();
            foreach (var header in Request.Headers)
            {
                requestHeaders.Add(header.Key, header.Value);
            }

            if (Token.GetUserWithToken(requestHeaders) != user)
            {
                return Ok(new Object.MessageGetAccount(null, 3, "Kiểm tra lại token tý nào"));
            }

            var data = Model.getAccount(user).FirstOrDefault();

            if (data == null)
            {
                return Ok(new Object.MessageGetAccount(null, 0, "Tài khoản không tồn tại"));
            }
            return Ok(new Object.MessageGetAccount(data, 1, "Lấy dữ liệu thành công"));
        }

        [HttpGet]
        [Route("getAllSeenProduct")]
        public IActionResult getAllSeenProduct(string user)
        {
            if (user == null)
            {
                return Ok(new Object.MessageGetAllSeenProduct(null, 2, "Tài khoản không được để trống"));
            }

            Dictionary<string, string> requestHeaders =
               new Dictionary<string, string>();
            foreach (var header in Request.Headers)
            {
                requestHeaders.Add(header.Key, header.Value);
            }

            if (Token.GetUserWithToken(requestHeaders) != user)
            {
                return Ok(new Object.MessageGetAllSeenProduct(null, 3, "Kiểm tra lại token tý nào"));
            }

            var data = Model.getAllSeenProduct(user);

            return Ok(new Object.MessageGetAllSeenProduct(data, 1, "Lấy dữ liệu thành công"));
        }

        [HttpGet]
        [Route("getCart")]
        public IActionResult getCart(string user)
        {
            if (user == null)
            {
                return Ok(new Object.MessageGetCart(null, 2, "Tài khoản không được để trống"));
            }

            Dictionary<string, string> requestHeaders =
               new Dictionary<string, string>();
            foreach (var header in Request.Headers)
            {
                requestHeaders.Add(header.Key, header.Value);
            }

            if (Token.GetUserWithToken(requestHeaders) != user)
            {
                return Ok(new Object.MessageGetCart(null, 3, "Kiểm tra lại token tý nào"));
            }

            var data = Model.getCart(user);
            return Ok(new Object.MessageGetCart(data, 1, "Lấy dữ liệu thành công"));
        }

        [HttpGet]
        [Route("getBills")]
        public IActionResult getBills(string user)
        {
            if (user == null)
            {
                return Ok(new Object.MessageOfBills(null, 2, "Tài khoản không được để trống"));
            }

            Dictionary<string, string> requestHeaders =
               new Dictionary<string, string>();
            foreach (var header in Request.Headers)
            {
                requestHeaders.Add(header.Key, header.Value);
            }

            if (Token.GetUserWithToken(requestHeaders) != user)
            {
                return Ok(new Object.MessageOfBills(null, 3, "Kiểm tra lại token tý nào"));
            }

            var data = Model.getBills(user);
            return Ok(new Object.MessageOfBills(data, 1, "Lấy dữ liệu thành công"));
        }

        [HttpGet]
        [Route("getBillDetail")]
        public IActionResult getBillDetail(string user, int billId)
        {
            if (user == null)
            {
                return Ok(new Object.MessageOfBillDetail(null, 2, "Tài khoản không được để trống"));
            }

            Dictionary<string, string> requestHeaders =
               new Dictionary<string, string>();
            foreach (var header in Request.Headers)
            {
                requestHeaders.Add(header.Key, header.Value);
            }

            if (Token.GetUserWithToken(requestHeaders) != user)
            {
                return Ok(new Object.MessageOfBillDetail(null, 3, "Kiểm tra lại token tý nào"));
            }

            var data = Model.getBillDetail(billId);
            return Ok(new Object.MessageOfBillDetail(data, 1, "Lấy dữ liệu thành công"));
        }
    }
}
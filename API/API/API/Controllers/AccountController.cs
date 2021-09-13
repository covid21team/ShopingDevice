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

            var result = Model.getAccount(user).FirstOrDefault();

            if (result == null)
            {
                return Ok(new Object.MessageGetAccount(result, 0, "Tài khoản không tồn tại"));
            }
            return Ok(new Object.MessageGetAccount(result, 1, "Lấy dữ liệu thành công"));
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
                return Ok(new Object.MessageGetAllSeenProduct(null, 0, "Kiểm tra lại token tý nào"));
            }

            var result = Model.getAllSeenProduct(user);

            return Ok(new Object.MessageGetAllSeenProduct(result, 1, "Lấy dữ liệu thành công"));
        }
    }
}
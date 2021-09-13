using API.Database;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("/api/")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly COVIDContext db;

        public TokenController(IConfiguration config, COVIDContext context)
        {
            _configuration = config;
            db = context;
        }

        //kiểm tra token gửi về có giống với user đã đăng ký không
        [HttpGet]
        [Route("getToken")]
        public string GetToken(string user)
        {
            //create claims details based on the user information
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                    new Claim("Group", "TSPTeam"),
                    new Claim("Email", "covid21tsp@gmail.com"),
                    new Claim("Hosting", "covid21tsp.space"),
                    new Claim("User", user)
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddDays(1), signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetUserWithToken(Dictionary<string, string> request)
        {
            var stream = request["Authorization"].Substring(7);

            var token = new JwtSecurityToken(jwtEncodedString: stream);
            string t = token.Claims.First(c => c.Type == "User").Value;

            return t;
        }
    }
}
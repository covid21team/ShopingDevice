using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/api/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private Product_BUS Model;

        public ProductController(COVIDContext context)
        {
           Model = new Product_BUS(context);
        }

        [HttpGet]
        [Route("allProduct")]
        public IActionResult GetAllProduct()
        {
            return Ok(Model.listProduct());
        }

        [HttpGet]
        [Route("productDetail")]
        public IActionResult productDetail(int productId)
        {
            return Ok(Model.productDetail(productId));
        }

        [HttpGet]
        [Route("productConfigs")]
        public IActionResult productConfigs(int productId)
        {
            return Ok(Model.productConfigs(productId));
        }

        [HttpGet]
        [Route("productRatings")]
        public IActionResult productRatingts(int productId)
        {
            return Ok(Model.productRatings(productId));
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.BUS;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Controllers
{
    public class Covid21Controller : Controller
    {
        // GET: Covid21

        Product_BUS PB;

        public Covid21Controller()
        {
            PB = new Product_BUS();
        }

        public ActionResult Home()
        {
            return View();
        }

        /*  - Tại sao lại không khai báo biến result ở ngoài.
            Vì khi khai báo ở ngoài thì cần gắn giá trí trước.
            Nhưng khi gắn giá trị trước thì lại không thay đổi đc giá trị sao khi xử lí.
            
            - Theo kiến thức thì với 'var' có thể thay đổi giá trị.
            Nhưng ở đây không biết lí do tại sao không thay đổi giá trị.
            Nên thấy trong if sẽ có return.
            
            - Nhược điểm của việc này:
                + Sẽ không lấy được giá trị các biến bên trong câu điều kiện ra bên ngoài để xử lí tiếp tục
                
            - Do ở đây là trong if đã đi thằng vào lớp Model nên không bị ảnh hưởng*/
        public ActionResult Categories(string ProductTypeName) 
        {
            var db = new Models.BUS.Product_BUS();

            Session["ProductTypeName"] = ProductTypeName;

            var result = db.loadProduct(ProductTypeName);

            return View(result);
        }

        public ActionResult Cart(string user)
        {
            var db = new Product_BUS();

            var result = db.loadProductCart(user);

            return View(result);
        }

        // xóa sản phẩm trong giỏ hàng của khách hàng khi bấm remove trong bảng giỏ hàng
        public ActionResult delProduct_Cart(int productId, string user)
        {
            PB.delProduct_Cart(productId, user);

            return RedirectToAction("Cart","Covid21", new { user = user});
        }

        public ActionResult Product(int productId)
        {
            var db = new Product_BUS();

            var result = db.productDetail(productId);
            return View(result);
        }

        public ActionResult Favourite(string user)
        {
            var db = new Product_BUS();

            var result = db.loadProductLike(1, 9, user);

            return View(result);
        }

        public ActionResult CheckOut(string user)
        {
            var db = new Product_BUS();
            var result = db.loadProductCheckout(user);

            return View(result);
        }
    }
}
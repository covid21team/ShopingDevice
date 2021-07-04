﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.BUS;
using TSP_Covid21.Models.ViewModel;
using TSP_Covid21.Models.ShopEntity;
using System.Web.Services;
using TSP_Covid21.Models;

namespace TSP_Covid21.Controllers
{
    public class ProductController : Controller
    {
        Product_BUS PB;

        public ProductController()
        {
            PB = new Product_BUS();
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
        public IEnumerable<BrandViewModel> loadBrandCtrl(string ProductTypeName)
        {
            var result = PB.loadBrand(ProductTypeName);
            return result;
        }

        // Tải đánh giá của sản phẩm
        public IEnumerable<RATINGPRODUCT> loadRatingProduct(int productId)
        {
            return PB.loadRatingProduct(productId);
        }

        // Tải cấu hình của sản phẩm
        public IEnumerable<CONFIGDETAIL> loadConfigProduct(int productId)
        {
            return PB.loadConfigProduct(productId);
        }

        // Tải danh sách bình luận về sản phẩm
        public IEnumerable<COMMENT> loadCommentProduct(int productId)
        {
            return PB.loadCommentProduct(productId);
        }

        // Tải đánh giá của 1 khách hàng dành cho sản phẩm
        public float ReviewRatingOfUser(string user, int productId)
        {
            return PB.ReviewRatingOfUser(user, productId);
        }

        // Tải 4 sản phẩm liên quan đến sản phẩm đang xem 
        public IEnumerable<PRODUCT> loadRelatedProduct(int productTypeId, int brandId)
        {
            return PB.loadRelatedProduct(productTypeId, brandId);
        }

        // Thêm bình luận của khách hàng
        [HttpPost]
        public void insert_RatingAndComment(int productId, string user, int rate, string comment)
        {
            if (comment != "")
                PB.insertComment(productId, user, comment);

            PB.insertRating(productId, user, rate);
        }

        // Thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public void insertCart(int productId, string user)
        {
            PB.insertCart(productId, user);
        }

        // Thêm sản phẩm vào giỏ hàng với số lượng mong muốn
        [HttpPost]
        public void insertCartWithAmount(int productId, string user, int amount)
        {
            PB.insertCartWithAmount(productId, user, amount);
        }

        [HttpPost]
        public void change_CheckBox(int productId, bool status, int quantity)
        {
            //System.Threading.Thread.Sleep(2000);
            string user = Session["user"].ToString();

            PB.change_CheckBox(productId, status, user, quantity);
        }

        [HttpPost]
        public ActionResult listComment(int productId)
        {
            var result = PB.loadCommentProduct(productId);

            return PartialView(result);
        }

        [HttpPost]
        public ActionResult listRating(int productId)
        {
            var result = PB.loadRatingProduct(productId);

            return PartialView(result);
        }

        [HttpPost]
        //Dùng vào lúc chọn lọc sản phẩm theo mong muoonscuar khách hàng
        public ActionResult listProduct(string productTypeName, int valuePrice, int sort, string listbrand)
        {
            int startPrice = 0;
            int endPrice = 100000000;
            switch (valuePrice)
            {
                case 1:
                    {
                        startPrice = 0;
                        endPrice = 100000000;
                    }
                    break;
                case 2:
                    {
                        startPrice = 0;
                        endPrice = 1000000;
                    }
                    break;
                case 3:
                    {
                        startPrice = 1000000;
                        endPrice = 5000000;
                    }
                    break;
                case 4:
                    {
                        startPrice = 5000000;
                        endPrice = 10000000;
                    }
                    break;
                case 5:
                    {
                        startPrice = 10000000;
                        endPrice = 20000000;
                    }
                    break;
                case 6:
                    {
                        startPrice = 20000000;
                        endPrice = 100000000;
                    }
                    break;
            }

            List<string> BS = new List<string>();
            string temp = "";
            for (int i = 0; i < listbrand.Length; i++)
            {
                if (listbrand[i].ToString().Equals(","))
                {
                    BS.Add(temp);
                    temp = "";
                }
                else
                {
                    temp += listbrand[i];
                }
            }

            var result = PB.listProduct(productTypeName, startPrice, endPrice, sort, BS);

            return PartialView(result);
        }

        //productTypeName ở đây 
        [HttpPost]
        public ActionResult search(string key)
        {
            Session["ProductTypeName"] = "All";
            var result = PB.search(key);

            return View(result);
        }
    }
}
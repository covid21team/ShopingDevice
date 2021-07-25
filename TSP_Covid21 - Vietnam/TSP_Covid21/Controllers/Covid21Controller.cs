using System;
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
        Account_BUS AB;

        public Covid21Controller()
        {
            PB = new Product_BUS();
            AB = new Account_BUS();
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
        public ActionResult Categories(int ProductTypeId) 
        {
            var db = new Product_BUS();

            Session["ProductTypeName"] = ProductTypeId.ToString();

            var result = db.loadProduct(ProductTypeId);

            return View(result);
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult Cart(string user)
        {
            var db = new Product_BUS();

            var result = db.loadProductCart(user);

            return View(result);
        }


        public ActionResult Product(int productId)
        {
            var db = new Product_BUS();

            var result = db.productDetail(productId);
            if(Session["user"] != null)
            {
                db.insertView(productId, Session["user"].ToString());
            }
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
        // 1: thư mục chính 2: thư mục địa chỉ 3: thư mục hóa đơn
        public ActionResult Personal(string user, int code)
        {
            var db = new Account_BUS();
            var result = db.account(user, code);

            return View(result);
        }

        public ActionResult Seen(string user)
        {
            var db = new Account_BUS();
            var result = db.view(user);

            return View(result);
        }

        public ActionResult Rating(string user)
        {
            var db = new Account_BUS();
            var result = db.rating(user);

            return View(result);
        }

        public ActionResult AboutUs()
        {
            return View();
        }
    }
}
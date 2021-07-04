using System;
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

        /*List<Employee> listEmployee = new List<Employee>()
        {
            new Employee()
            {
                ID = 1,
                Name = "hlhl",
                Salary = 2000,
                Status = true
            },
            new Employee()
            {
                ID = 2,
                Name = "hihi",
                Salary = 2000,
                Status = true
            },
            new Employee()
            {
                ID = 3,
                Name = "bgbg",
                Salary = 2000,
                Status = true
            },
            new Employee()
            {
                ID = 4,
                Name = "hkhk",
                Salary = 2000,
                Status = true
            },
            new Employee()
            {
                ID = 5,
                Name = "haha",
                Salary = 2000,
                Status = true
            },
            new Employee()
            {
                ID = 6,
                Name = "huhu",
                Salary = 2000,
                Status = true
            },
            new Employee()
            {
                ID = 7,
                Name = "ssww",
                Salary = 2000,
                Status = true
            },
            new Employee()
            {
                ID = 8,
                Name = "hyhy",
                Salary = 2000,
                Status = true
            },
        };*/

       /* [HttpGet]
        public JsonResult testJson()
        {
            List<COMMENT> test = PB.testJson();
            return Json(new {
                data = test,
            },JsonRequestBehavior.AllowGet);
        }*/

        // Tải 4 sản phẩm liên quan đến sản phẩm đang xem 
        public IEnumerable<PRODUCT> loadRelatedProduct(int productTypeId, int brandId)
        {
            return PB.loadRelatedProduct(productTypeId, brandId);
        }

        // Thêm bình luận của khách hàng
        [HttpPost]
        public void insert_RatingAndComment(int productId, string user, int rate, string comment)
        {
            if(comment != "")
                PB.insertComment(productId,user,comment);

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
    }
}
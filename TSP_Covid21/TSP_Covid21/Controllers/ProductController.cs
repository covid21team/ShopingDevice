using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.BUS;
using TSP_Covid21.Models.ViewModel;
using TSP_Covid21.Models.ShopEntity;
using System.Web.Services;

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
            if(!ProductTypeName.Equals("SmartPhone") && !ProductTypeName.Equals("Laptop") && !ProductTypeName.Equals("Tablet") && !ProductTypeName.Equals("SmartWatch") && !ProductTypeName.Equals("HeadPhone"))
            {
                var result_user = PB.loadAllBrand();
                return result_user;
            }
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
        public PagedList.IPagedList<COMMENT> loadCommentProduct(int productId)
        {
            return PB.loadCommentProduct(1,3,productId);
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
    }
}
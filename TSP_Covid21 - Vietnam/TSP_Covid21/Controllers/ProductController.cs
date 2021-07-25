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
using System.Net.Mail;
using System.Net;
using System.Text;

namespace TSP_Covid21.Controllers
{
    public class ProductController : Controller
    {
        Product_BUS PB;
        private object smtp;

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
        public IEnumerable<BrandViewModel> loadBrandCtrl(int ProductTypeId)
        {
            var result = PB.loadBrand(ProductTypeId);
            return result;
        }

        public IEnumerable<BRAND> listBrand()
        {
            return PB.listBrand();
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
        public ActionResult insertCart(int productId, string user)
        {
            PB.insertCart(productId, user);

            return PartialView();
        }

        // Thêm sản phẩm vào giỏ hàng với số lượng mong muốn
        [HttpPost]
        public ActionResult insertCartWithAmount(int productId, string user, int amount)
        {
            PB.insertCartWithAmount(productId, user, amount);

            return PartialView();
        }

        // xóa sản phẩm trong giỏ hàng của khách hàng khi bấm remove trong bảng giỏ hàng
        public ActionResult delProduct_Cart(int productId)
        {
            string user = Session["user"].ToString();
            PB.delProduct_Cart(productId, user);

            return PartialView();
        }

        // xóa sản phẩm ở downdrop phần header
        public void delProduct(int productId)
        {
            string user = Session["user"].ToString();
            PB.delProduct(productId, user);
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

        //Dùng vào lúc chọn lọc sản phẩm theo mong muoonscuar khách hàng
        [HttpPost]
        public ActionResult listProduct(int ProductTypeId, int valuePrice, int sort, string listbrand, string key)
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

            var result = PB.listProduct(ProductTypeId, startPrice, endPrice, sort, BS, key);

            return PartialView(result);
        }

        //productTypeName ở đây 
        [HttpPost]
        public ActionResult search(string key)
        {
            Session["KeySearch"] = key;
            Session["ProductTypeName"] = "0";
            var result = PB.search(key);

            return View(result);
        }

        public string insertBill(string note, int total)
        {
            string user = Session["user"].ToString();
            PB.insertBill(user, note, total);

            BILL b = PB.takeBill(user);
            string fullName = Session["fullname"].ToString();
            string email = Session["gmail"].ToString();
            string price = String.Format("{0:#,##0.##}", b.BILLDETAIL.Sum(t => t.PRODUCT.PRODUCTPRICE * t.AMOUNT));
            string totalprice = String.Format("{0:#,##0.##}", b.TOTALBILL);
            price = price.Replace(".", ",");
            totalprice = totalprice.Replace(".", ",");

            string gmailshop = "covid21tsp@gmail.com";
            string passshop = "123456@a";
            string gmail = Session["gmail"].ToString() ;
            string title = "Đơn đặt hàng của bạn từ Covid21Shop";
            string str = "";
            try
            {
                SmtpClient mailclient = new SmtpClient("smtp.gmail.com", 587);
                mailclient.EnableSsl = true;
                mailclient.Credentials = new NetworkCredential(gmailshop, passshop);

                MailMessage message = new MailMessage(gmailshop, gmail);
                
                string htmlText = System.IO.File.ReadAllText(Server.MapPath("~/Asset/SendMail.html"));
                foreach (var item in b.BILLDETAIL)
                {
                    var temp = String.Format("{0:#,##0.##}", item.AMOUNT * item.PRODUCT.PRODUCTPRICE);
                    temp = temp.Replace(".", ",");
                    str += "<tr style='text - align: left'>"+
                                "<td class='tablechinh'>"+
                                    "<p>"+item.PRODUCT.PRODUCTNAME+"</p>"+
                                "</td>"+
                                "<td class='tablechinh'>"+
                                    "<p>X"+item.AMOUNT+"</p>"+
                                "</td>"+
                                "<td class='tablechinh'>"+
                                    "<p>"+temp+"đ</p>"+
                                "</td>"+
                            "</tr>";
                }
                htmlText = htmlText.Replace("{{FullName}}", fullName);
                htmlText = htmlText.Replace("{{BillId}}", b.BILLID.ToString());
                htmlText = htmlText.Replace("{{DateCreate}}", b.DATECREATE.ToString());
                htmlText = htmlText.Replace("{{ListProduct}}", str);
                htmlText = htmlText.Replace("{{Price}}", price + "đ");
                htmlText = htmlText.Replace("{{TotalPrice}}", totalprice + "đ");
                htmlText = htmlText.Replace("{{Address}}", b.ADDRESS+", "+b.WARDS+", "+b.DISTRICT+", "+b.CITY);
                htmlText = htmlText.Replace("{{Phone}}", b.PHONE);
                htmlText = htmlText.Replace("{{Gmail}}", email);
                message.Subject = title;
                message.Body = htmlText;

                message.IsBodyHtml = true;
                mailclient.Send(message);

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "True";
        }

        [HttpPost]
        public void insertFavourite(int productId, string user)
        {
            PB.insertFavourite(productId, user);
        }
    } 
}
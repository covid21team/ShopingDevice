using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.ShopEntity;
using TSP_Covid21.Models.BUS;
using System.Text;
using System.Xml.Linq;
using System.Globalization;
using System.Net.Mail;
using System.Net;

namespace TSP_Covid21.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool Signin(string user, string pass)
        {
            //System.Threading.Thread.Sleep(2000);
            Models.BUS.Account_BUS AB = new Models.BUS.Account_BUS();

            string checkLogin = AB.checkLogin(user, pass);
            if (checkLogin == user)
            {
                Session["user"] = checkLogin;
                Session["fullname"] = AB.takeFullName(user);
                Session["gmail"] = AB.takeGmail(user);
                return true;
            }
            return false;
        }

        public bool checkPass(string user, string pass)
        {
            Account_BUS AB = new Account_BUS();

            string checkLogin = AB.checkLogin(user, pass);
            if (checkLogin == user)
            {
                return true;
            }
            return false;
        }

        // Đổi mật khẩu khi đã đăng nhập, dùng user để đổi
        public void changePass(string user, string pass_new)
        {
            Account_BUS AB = new Account_BUS();
            AB.changePass(user, pass_new);
        }

        // Đổi mật khẩu khi chưa đăng nhập, dùng email để đổi
        public void changePassWithEmail(string pass_new)
        {
            string email = Session["gmail"].ToString();
            Account_BUS AB = new Account_BUS();
            AB.changePassWithEmail(email, pass_new);
        }

        public void Logout()
        {
            Session["user"] = null;
            Session["fullname"] = null;
        }

        public bool checkUser(string user)
        {
            Models.BUS.Account_BUS AB = new Models.BUS.Account_BUS();
            var result = AB.checkUser(user);

            return result;
        }

        public bool checkPhone(string phone)
        {
            Models.BUS.Account_BUS AB = new Models.BUS.Account_BUS();
            var result = AB.checkPhone(phone);

            return result;
        }

        public bool checkEmail(string email)
        {
            Account_BUS AB = new Account_BUS();
            var result = AB.checkEmail(email);

            return result;
        }

        // kiểm tra email ở view personal
        public bool checkMailPersonal(string email)
        {
            string user = Session["user"].ToString();
            Account_BUS AB = new Account_BUS();
            var result = AB.checkMailPersonal(email, user);

            return result;
        }

        public void SendCode(string email)
        {
            string gmailshop = "covid21tsp@gmail.com";
            string passshop = "123456@a";
            string title = "Mã xác nhận Email từ Covid21Shop";

            Random TenBienRanDom = new Random();
            var script = TenBienRanDom.Next(123456, 987654);//Trả về giá trị kiểu int
            try
            {
                SmtpClient mailclient = new SmtpClient("smtp.gmail.com", 587);
                mailclient.EnableSsl = true;
                mailclient.Credentials = new NetworkCredential(gmailshop, passshop);

                MailMessage message = new MailMessage(gmailshop, email);

                string htmlText = System.IO.File.ReadAllText(Server.MapPath("~/Asset/SendCode.html"));
                htmlText = htmlText.Replace("{{script}}", script.ToString());
                message.Subject = title;
                message.Body = htmlText;

                message.IsBodyHtml = true;
                mailclient.Send(message);
                Session["script"] = script.ToString();
                Session["gmail"] = email;

            }
            catch (Exception ex)
            {

            }
        }

        public bool ConfirmCode(string code)
        {
            if(code == Session["script"].ToString())
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        public string Signup(string user, string pass, string fullname, string phone, string email, string code)
        {
            string script = Session["script"].ToString();
            if (checkUser(user))
                return "Flase";
            if (checkPhone(phone))
                return "Flase";
            if (!code.Equals(script))
                return "CodeFail";

            Account_BUS AB = new Account_BUS();
            AB.Signup(user, pass, fullname, phone, email);

            return "True";
        }

        public IEnumerable<BILL> loadBill(string user)
        {
            Account_BUS AB = new Account_BUS();
            var result = AB.loadBill(user);

            return result;
        }

        public IEnumerable<ADDRESS_SHIP> loadAddress(string user)
        {
            Account_BUS AB = new Account_BUS();
            var result = AB.loadAddress(user);

            return result;

        }

        // Chỉ lấy ra 1 địa chỉ, tại sao ở đây lại dùng list là do khi không địa chỉ thì bằng null dẫn đến web lỗi nên phải dùng list
        public IEnumerable<ADDRESS_SHIP> addressDefault(string user)
        {
            Account_BUS AB = new Account_BUS();
            var result = AB.addressDefault(user);

            return result;
        }

        public void changeInf(string user, string fullname, bool sex, string birth, string email, string phone)
        {
            Account_BUS AB = new Account_BUS();

            AB.changeInf(user, fullname, sex, birth, email, phone);

            Session["fullname"] = fullname;
            Session["gmail"] = email;
        }

        public void insertAddress(string user, string fullname, string phone, string city, string district, string ward, string address, bool addDefault)
        {
            Account_BUS AB = new Account_BUS();
            AB.insertAddress(user, fullname, phone, city, district, ward, address, addDefault);
        }

        // dùng thêm địa chỉ 
        public ActionResult formAddAddress()
        {
            return PartialView();
        }

        // dùng lúc muốn chỉnh sửa lại nhưng địa chỉ có sẳn
        public ActionResult loadAddressInf(int addressId)
        {
            Account_BUS AB = new Account_BUS();
            ADDRESS_SHIP result = AB.loadadd(addressId);

            return PartialView(result);
        }

        public void editAddress_ship(int addressId, string user, string fullname, string phone, string city, string district, string ward, string address, bool addDefault)
        {
            Account_BUS AB = new Account_BUS();
            AB.editAddress_ship(addressId, user, fullname, phone, city, district, ward, address, addDefault);
        }

        public ActionResult BillDetail(int id)
        {
            Account_BUS AB = new Account_BUS();
            var result = AB.BillDetail(id);

            return PartialView(result);
        }

        public void cancelBill(int id)
        {
            Account_BUS AB = new Account_BUS();
            AB.cancelBill(id);
        }

        public void editAdress_Bill(int id, string fullname, string phone, string city, string district, string ward, string address)
        {
            Account_BUS AB = new Account_BUS();
            AB.editAdress_Bill(id, fullname, phone, city, district, ward, address);
        }



        public class SitemapNode
        {
            public string loc { get; set; }
            public DateTime? lastmod { get; set; }
            public double? priority { get; set; }
        }

        public string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");
            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.loc)),
                    sitemapNode.lastmod == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.lastmod.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }
            XDocument document = new XDocument(root);
            return document.ToString();
        }
        public IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();

            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/",
                    priority = 1.0
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Categories?ProductTypeId=2",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Categories?ProductTypeId=1",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Categories?ProductTypeId=3",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Categories?ProductTypeId=4",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Categories?ProductTypeId=5",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=5",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=1",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=2",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=3",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=4",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=6",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=7",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=8",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=9",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=10",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=11",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=12",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=13",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=14",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=15",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=16",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=17",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/Product?productId=18",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/ContactUs",
                    priority = 0.8
                });
            nodes.Add(
                new SitemapNode()
                {
                    loc = "https://covid21tsp.space/Covid21/PrivacyPolicy",
                    priority = 0.8
                });
            return nodes;
        }

        [Route("sitemap.xml")]
        public ActionResult testSiteMap()
        {
            var sitemapNodes = GetSitemapNodes();
            string xml = GetSitemapDocument(sitemapNodes);
            return Content(xml, "text/xml", Encoding.UTF8);
        }
    }
}
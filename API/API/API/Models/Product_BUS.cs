using API.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Product_BUS
    {
        private readonly COVIDContext db;

        public Product_BUS(COVIDContext context)
        {
            db = context;
        }

        public IEnumerable<Object.Product> listProduct()
        {
            var result = from a in db.Product
                         select new Object.Product()
                         {
                             id = a.Productid,
                             name = a.Productname,
                             picMain = a.PicProduct.Where(t => t.Mainpic == true && t.Productid == a.Productid).Select(c => c.Picture.Link).FirstOrDefault(),
                             price = a.Productprice,
                             listPic = a.PicProduct.Where(t => t.Mainpic == false && t.Productid == a.Productid).Select(c => c.Picture.Link),
                         };

            return result;
        }

        public IEnumerable<Object.Product> productDetail(int productId)
        {
            var result = from a in db.Product
                         where a.Productid == productId
                         select new Object.Product()
                         {
                             id = a.Productid,
                             name = a.Productname,
                             picMain = a.PicProduct.Where(t => t.Mainpic == true && t.Productid == a.Productid).Select(c => c.Picture.Link).FirstOrDefault(),
                             price = a.Productprice,
                             listPic = a.PicProduct.Where(t => t.Mainpic == false && t.Productid == a.Productid).Select(c => c.Picture.Link),
                         };

            return result;
        }

        public IEnumerable<Object.ProductConfig> productConfigs(int productId)
        {
            var result = from a in db.Configdetail
                         where a.Productid == productId
                         select new Object.ProductConfig()
                         {
                             configName = a.Config.Decriptionconfigname,
                             configContent = a.Information,
                         };

            return result;
        }

        //Khi đã comment thì phải cập nhật bảng rating, khách hàng không làm thì tự động cập nhật
        public IEnumerable<Object.ProductRating> productRatings(int productId)
        {
            var result = from a in db.Ratingproduct
                         where a.Productid == productId
                         select new Object.ProductRating()
                         {
                             userName = a.Account.Fullname,
                             rate = a.Rate,
                             productComments = from b in db.Comment
                                               where b.User == a.User && b.Productid == productId
                                               select new Object.ProductComment()
                                               {
                                                   commentText = b.Commenttext,
                                                   datecomment = b.Datecomment.ToString(),
                                               },
                         };

            return result;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ApiObject;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Models.BUS.Api
{
    public class ApiProduct_BUS
    {
        private COVIDEntities db;

        public ApiProduct_BUS()
        {
            db = new COVIDEntities();
        }

        public IEnumerable<Product> listProduct()
        {
            var result = from a in db.PRODUCT
                         select new Product()
                         {
                             id = a.PRODUCTID,
                             name = a.PRODUCTNAME,
                             picMain = a.PIC_PRODUCT.Where(t => t.MAINPIC == true && t.PRODUCTID == a.PRODUCTID).Select(c => c.PICTURE.LINK).FirstOrDefault(),
                             price = a.PRODUCTPRICE,
                             listPic = a.PIC_PRODUCT.Where(t => t.MAINPIC == false && t.PRODUCTID == a.PRODUCTID).Select(c => c.PICTURE.LINK),
                         };

            return result;
        }

        public IEnumerable<Product> productDetail(int productId)
        {
            var result = from a in db.PRODUCT
                         where a.PRODUCTID == productId
                         select new Product()
                         {
                             id = a.PRODUCTID,
                             name = a.PRODUCTNAME,
                             picMain = a.PIC_PRODUCT.Where(t => t.MAINPIC == true && t.PRODUCTID == a.PRODUCTID).Select(c => c.PICTURE.LINK).FirstOrDefault(),
                             price = a.PRODUCTPRICE,
                             listPic = a.PIC_PRODUCT.Where(t => t.MAINPIC == false && t.PRODUCTID == a.PRODUCTID).Select(c => c.PICTURE.LINK),
                         };

            return result;
        }

        public IEnumerable<ProductConfig> productConfigs(int productId)
        {
            var result = from a in db.CONFIGDETAIL
                         where a.PRODUCTID == productId
                         select new ProductConfig()
                         {
                             configName = a.CONFIG.DECRIPTIONCONFIGNAME,
                             configContent = a.INFORMATION,
                         };

            return result;
        }

        //Khi đã comment thì phải cập nhật bảng rating, khách hàng không làm thì tự động cập nhật
        public IEnumerable<ProductRating> productRatings(int productId)
        {
            var result = from a in db.RATINGPRODUCT
                         where a.PRODUCTID == productId
                         select new ProductRating()
                         {
                             userName = a.ACCOUNT.FULLNAME,
                             rate = a.RATE,
                             productComments = from b in db.COMMENT
                                               where b.USER == a.USER && b.PRODUCTID == productId
                                               select new ProductComment()
                                               {
                                                   commentText = b.COMMENTTEXT,
                                                   datecomment = b.DATECOMMENT.ToString(),
                                               },
                         };

            return result;
        }
    }
}
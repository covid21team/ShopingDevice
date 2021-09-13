using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Object
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string picMain { get; set; }
        public int? price { get; set; }
        public IEnumerable<string> listPic { get; set; }
    }

    public class ProductConfig
    {
        public string configName { get; set; }
        public string configContent { get; set; }
    }

    public class ProductRating
    {
        public string userName { get; set; }
        public int? rate { get; set; }
        public IEnumerable<ProductComment> productComments { get; set; }
    }

    public class ProductComment
    {
        public string commentText { get; set; }
        public string datecomment { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class PicProduct
    {
        public int Productid { get; set; }
        public int Pictureid { get; set; }
        public bool? Mainpic { get; set; }

        public Picture Picture { get; set; }
        public Product Product { get; set; }
    }
}

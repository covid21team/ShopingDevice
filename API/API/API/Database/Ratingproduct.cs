using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Ratingproduct
    {
        public string User { get; set; }
        public int Productid { get; set; }
        public int? Rate { get; set; }

        public Product Product { get; set; }
        public Account Account { get; set; }
    }
}

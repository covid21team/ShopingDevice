using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Configdetail
    {
        public int Productid { get; set; }
        public string Configname { get; set; }
        public string Information { get; set; }

        public Config Config { get; set; }
        public Product Product { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Billdetail
    {
        public int Billid { get; set; }
        public int Productid { get; set; }
        public int? Amount { get; set; }

        public Bill Bill { get; set; }
        public Product Product { get; set; }
    }
}

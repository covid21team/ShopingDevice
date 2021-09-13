using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Cart
    {
        public string User { get; set; }
        public int Productid { get; set; }
        public int? Amount { get; set; }
        public bool? Productstatus { get; set; }

        public Product Product { get; set; }
        public Account UserNavigation { get; set; }
    }
}

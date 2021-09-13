using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Accountlike
    {
        public string User { get; set; }
        public int Productid { get; set; }
        public DateTime? Datelike { get; set; }

        public Product Product { get; set; }
        public Account UserNavigation { get; set; }
    }
}

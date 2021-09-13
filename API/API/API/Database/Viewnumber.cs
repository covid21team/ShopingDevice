using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Viewnumber
    {
        public string User { get; set; }
        public int Productid { get; set; }
        public DateTime? Dateseen { get; set; }

        public Product Product { get; set; }
        public Account UserNavigation { get; set; }
    }
}

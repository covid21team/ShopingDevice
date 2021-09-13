using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class AddressShip
    {
        public int Addressid { get; set; }
        public string User { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Wards { get; set; }
        public string Address { get; set; }
        public bool? Default { get; set; }
        public bool? AddressStatus { get; set; }

        public Account UserNavigation { get; set; }
    }
}

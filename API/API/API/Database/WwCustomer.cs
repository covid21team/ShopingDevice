using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class WwCustomer
    {
        public WwCustomer()
        {
            WwBill = new HashSet<WwBill>();
        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Tag { get; set; }

        public ICollection<WwBill> WwBill { get; set; }
    }
}

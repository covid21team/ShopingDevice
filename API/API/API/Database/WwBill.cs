using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class WwBill
    {
        public WwBill()
        {
            WwBillDetail = new HashSet<WwBillDetail>();
        }

        public int BillId { get; set; }
        public string Phone { get; set; }
        public bool? BillState { get; set; }

        public WwCustomer PhoneNavigation { get; set; }
        public ICollection<WwBillDetail> WwBillDetail { get; set; }
    }
}

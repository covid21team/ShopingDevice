using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class WwBillDetail
    {
        public int BillId { get; set; }
        public int ProductId { get; set; }
        public double Amount { get; set; }

        public WwBill Bill { get; set; }
        public WwProduct Product { get; set; }
    }
}

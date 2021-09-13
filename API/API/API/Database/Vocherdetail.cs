using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Vocherdetail
    {
        public int Voucherid { get; set; }
        public string User { get; set; }
        public DateTime? Dateendtire { get; set; }

        public Account UserNavigation { get; set; }
        public Voucher Voucher { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Voucher
    {
        public Voucher()
        {
            Bill = new HashSet<Bill>();
            Vocherdetail = new HashSet<Vocherdetail>();
        }

        public int Voucherid { get; set; }
        public string Decriptionvoucher { get; set; }
        public DateTime? Dateendtire { get; set; }
        public bool? Statusvoucher { get; set; }

        public ICollection<Bill> Bill { get; set; }
        public ICollection<Vocherdetail> Vocherdetail { get; set; }
    }
}

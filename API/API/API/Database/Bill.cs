using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Bill
    {
        public Bill()
        {
            Billdetail = new HashSet<Billdetail>();
        }

        public int Billid { get; set; }
        public string User { get; set; }
        public DateTime? Datecreate { get; set; }
        public int? Voucherid { get; set; }
        public long? Totalbill { get; set; }
        public int? BillStatus { get; set; }
        public string Note { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Wards { get; set; }
        public string Address { get; set; }

        public Account UserNavigation { get; set; }
        public Voucher Voucher { get; set; }
        public ICollection<Billdetail> Billdetail { get; set; }
    }
}

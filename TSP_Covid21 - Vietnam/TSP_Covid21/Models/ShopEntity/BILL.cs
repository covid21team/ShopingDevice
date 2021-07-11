//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TSP_Covid21.Models.ShopEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class BILL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BILL()
        {
            this.BILLDETAIL = new HashSet<BILLDETAIL>();
        }
    
        public int BILLID { get; set; }
        public string USER { get; set; }
        public Nullable<System.DateTime> DATECREATE { get; set; }
        public Nullable<int> VOUCHERID { get; set; }
        public Nullable<long> TOTALBILL { get; set; }
        public Nullable<int> BIllSTATUS { get; set; }
        public string NOTE { get; set; }
        public string FULLNAME { get; set; }
        public string PHONE { get; set; }
        public string CITY { get; set; }
        public string DISTRICT { get; set; }
        public string WARDS { get; set; }
        public string ADDRESS { get; set; }
    
        public virtual ACCOUNT ACCOUNT { get; set; }
        public virtual VOUCHER VOUCHER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BILLDETAIL> BILLDETAIL { get; set; }
    }
}

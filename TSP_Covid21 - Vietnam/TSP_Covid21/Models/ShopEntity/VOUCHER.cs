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
    
    public partial class VOUCHER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VOUCHER()
        {
            this.VOCHERDETAIL = new HashSet<VOCHERDETAIL>();
            this.BILL = new HashSet<BILL>();
        }
    
        public int VOUCHERID { get; set; }
        public string DECRIPTIONVOUCHER { get; set; }
        public Nullable<System.DateTime> DATEENDTIRE { get; set; }
        public Nullable<bool> STATUSVOUCHER { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VOCHERDETAIL> VOCHERDETAIL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BILL> BILL { get; set; }
    }
}

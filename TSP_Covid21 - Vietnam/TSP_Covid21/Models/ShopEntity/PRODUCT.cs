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
    
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            this.ACCOUNTLIKE = new HashSet<ACCOUNTLIKE>();
            this.BILLDETAIL = new HashSet<BILLDETAIL>();
            this.CART = new HashSet<CART>();
            this.COMMENT = new HashSet<COMMENT>();
            this.CONFIGDETAIL = new HashSet<CONFIGDETAIL>();
            this.RATINGPRODUCT = new HashSet<RATINGPRODUCT>();
            this.VIEWNUMBER = new HashSet<VIEWNUMBER>();
        }
    
        public int PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public Nullable<int> BRANDID { get; set; }
        public Nullable<int> PRODUCTTYPEID { get; set; }
        public string MAINPIC { get; set; }
        public string PIC1 { get; set; }
        public string PIC2 { get; set; }
        public string PIC3 { get; set; }
        public string PIC4 { get; set; }
        public Nullable<bool> STATUSPRODUCT { get; set; }
        public Nullable<int> PRODUCTPRICE { get; set; }
        public Nullable<int> PRODUCTAMOUNT { get; set; }
        public string DECRIPTION { get; set; }
        public Nullable<System.DateTime> DATEADD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNTLIKE> ACCOUNTLIKE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BILLDETAIL> BILLDETAIL { get; set; }
        public virtual BRAND BRAND { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CART> CART { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONFIGDETAIL> CONFIGDETAIL { get; set; }
        public virtual PRODUCTTYPE PRODUCTTYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RATINGPRODUCT> RATINGPRODUCT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIEWNUMBER> VIEWNUMBER { get; set; }
    }
}

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
    
    public partial class CONFIG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CONFIG()
        {
            this.CONFIGDETAILs = new HashSet<CONFIGDETAIL>();
        }
    
        public string CONFIGNAME { get; set; }
        public string DECRIPTIONCONFIGNAME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONFIGDETAIL> CONFIGDETAILs { get; set; }
    }
}

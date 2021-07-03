//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanXe.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            this.IMG_PRODUCT = new HashSet<IMG_PRODUCT>();
            this.ORDER_DETAIL = new HashSet<ORDER_DETAIL>();
            this.RENTs = new HashSet<RENT>();
        }
    
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string IdBrand { get; set; }
        public int IdType { get; set; }
    
        public virtual BRAND BRAND { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMG_PRODUCT> IMG_PRODUCT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER_DETAIL> ORDER_DETAIL { get; set; }
        public virtual TYPECAR TYPECAR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RENT> RENTs { get; set; }
    }
}

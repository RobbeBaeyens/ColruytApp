//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Colruyt_DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lijst
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lijst()
        {
            this.Lijst_Product = new HashSet<Lijst_Product>();
        }
    
        public int id { get; set; }
        public string naam { get; set; }
        public Nullable<System.DateTime> datumAangemaakt { get; set; }
        public int loginId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lijst_Product> Lijst_Product { get; set; }
        public virtual Login Login { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PosetiMe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblLocal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblLocal()
        {
            this.tblComments = new HashSet<tblComment>();
            this.tblRatings = new HashSet<tblRating>();
            this.tblVisits = new HashSet<tblVisit>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_City { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public byte[] Imege { get; set; }

        
    
        public virtual tblCity tblCity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblComment> tblComments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRating> tblRatings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblVisit> tblVisits { get; set; }
    }
}

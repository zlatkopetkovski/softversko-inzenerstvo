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
    
    public partial class tblVisit
    {
        public int ID { get; set; }
        public string ID_User { get; set; }
        public int ID_Local { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual tblLocal tblLocal { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}

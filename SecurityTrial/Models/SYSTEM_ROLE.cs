//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SecurityTrial.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SYSTEM_ROLE
    {
        public SYSTEM_ROLE()
        {
            this.SYSTEM_USER_ROLE = new HashSet<SYSTEM_USER_ROLE>();
        }
    
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Discriminator { get; set; }
    
        public virtual ICollection<SYSTEM_USER_ROLE> SYSTEM_USER_ROLE { get; set; }
    }
}

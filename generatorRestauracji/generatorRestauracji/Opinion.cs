//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace generatorRestauracji
{
    using System;
    using System.Collections.Generic;
    
    public partial class Opinion
    {
        public int OpinionId { get; set; }
        public System.Guid RestaurantId { get; set; }
        public short Rating { get; set; }
        public string Comment { get; set; }
        public System.Guid UserId { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual Restaurant Restaurant { get; set; }
        public virtual User User { get; set; }
    }
}
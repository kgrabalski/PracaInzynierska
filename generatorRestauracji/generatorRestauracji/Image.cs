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
    
    public partial class Image
    {
        public Image()
        {
            this.Restaurants = new HashSet<Restaurant>();
        }
    
        public int ImageId { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
    
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}

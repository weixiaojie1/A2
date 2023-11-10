
namespace BookShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Photo
    {
        public int PhotoID { get; set; }
        public int ProductID { get; set; }
        public string PhotoUrl { get; set; }
    
        public virtual Product Product { get; set; }
    }
}

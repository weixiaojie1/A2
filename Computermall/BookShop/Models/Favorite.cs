
namespace BookShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Favorite
    {
        public int FavoriteID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}

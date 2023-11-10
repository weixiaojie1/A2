
namespace BookShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class News
    {
        public int NewsID { get; set; }
        public string Title { get; set; }
        public string NTypes { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public Nullable<System.DateTime> PushTime { get; set; }
        public Nullable<int> States { get; set; }
    }
}

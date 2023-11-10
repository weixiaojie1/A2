
namespace BookShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appraise
    {
        public int AppraiseID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string Content { get; set; }
        public int Grade { get; set; }
        public Nullable<System.DateTime> RateTime { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}


namespace BookShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrdersDetail
    {
        public int DetailID { get; set; }
        public int OrdersID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> States { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}

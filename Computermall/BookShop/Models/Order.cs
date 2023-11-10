
namespace BookShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrdersDetails = new HashSet<OrdersDetail>();
        }
    
        public int OrdersID { get; set; }
        public System.DateTime Orderdate { get; set; }
        public int UserID { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> DeliveryID { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public int States { get; set; }
        public string Remark { get; set; }
    
        public virtual Delivery Delivery { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}

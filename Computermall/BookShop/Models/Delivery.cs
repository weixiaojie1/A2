
namespace BookShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Delivery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Delivery()
        {
            this.Orders = new HashSet<Order>();
            this.Users = new HashSet<User>();
        }
    
        public int DeliveryID { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage = "Recipient cannot be empty")]
        public string Consignee { get; set; }
        [Required(ErrorMessage = "The shipping address cannot be empty")]
        public string Complete { get; set; }
        [Required(ErrorMessage = "Contact number cannot be empty")]
        public string Phone { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}

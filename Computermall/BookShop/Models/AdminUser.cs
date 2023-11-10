
namespace BookShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdminUser
    {
        public int SuID { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public int Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models
{
    public class ShopCart
    {
        public int Number { get; set; }
        public double TotalPrice { get; set; }
        public Product Product { get; set; }
        public bool IsCheck { get; set; }
    }
}
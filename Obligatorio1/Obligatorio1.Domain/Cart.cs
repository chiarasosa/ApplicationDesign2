using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Domain
{
    public class Cart
    {
        public List<Product> Products { get; set; }
        public double TotalPrice { get; set; }
        public int CartID { get; set; }

        public string? PromotionApplied { get; set; }
        public int UserID { get; set; }
       // public User User { get; set; }

        public Cart()
        {
            this.Products = new List<Product>();
            this.TotalPrice = 0;
            this.PromotionApplied = null;
        }
    }
}

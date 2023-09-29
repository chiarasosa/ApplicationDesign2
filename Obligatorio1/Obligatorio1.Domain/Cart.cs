using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Domain
{
    public class Cart
    {
        public List<Product> Products;

        public double TotalPrice;
        public int CartID { get; set; }

        public string? PromotionApplied;
        public Cart()
        {
            this.Products = new List<Product>();
            this.TotalPrice = 0;
            this.PromotionApplied = null;
        }
    }
}

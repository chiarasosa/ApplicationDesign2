using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Domain
{
    public class Cart
    {
        public int CartID { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        
        [NotMapped]
        public List<Product> Products { get; set; }
        public double TotalPrice { get; set; }
        public string PromotionApplied { get; set; }
        public int UserID { get; set; }
        public Cart()
        {
            this.CartProducts = new List<CartProduct>();
            this.TotalPrice = 0;
            this.PromotionApplied = "none";
        }
    }
}

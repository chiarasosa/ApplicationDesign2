using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Domain
{
    internal class Purchase
    {
        public int PurchaseID { get; set; }
        public User User { get; set; }

        public List<Product> PurchasedProducts { get; set; }
        
        public Promo PromoIDApplied { get; set; }

        public DateTime DateOfPurchase { get; set; }
    }
}

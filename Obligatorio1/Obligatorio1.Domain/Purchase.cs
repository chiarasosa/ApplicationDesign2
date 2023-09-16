using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Domain
{
    public class Purchase
    {
        public int PurchaseID { get; set; }
       
        public User? User { get; set; }

        public List<Product> PurchasedProducts { get; set; }
        
        public string PromoApplied { get; set; }

        public DateTime DateOfPurchase { get; set; }

        public Purchase()
        {
            this.PurchaseID = 1;
            User = null;
            this.PurchasedProducts = new List<Product>();
            PromoApplied = String.Empty;
            this.DateOfPurchase = DateTime.Now;
        }

        /*
        public Purchase(int PurchaseID, User user, List<Product> PuchsedProducts, IPromoLogic PromoApplied, DateTime DateOfPurchase)
        {
            this.PurchaseID = PurchaseID;
            this.User = user;
            this.PurchasedProducts = PurchasedProducts;
            this.PromoIDApplied = PromoIDApplied;
            this.DateOfPurchase = DateOfPurchase;
        }
        */
    }
}

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
        //public User User { get; set; }

        public List<Product> PurchasedProducts { get; set; }
        
        //public Promo PromoIDApplied { get; set; }

        public DateTime DateOfPurchase { get; set; }

        public Purchase()
        {
            this.PurchaseID = 0;
            //User = null;
            this.PurchasedProducts = new List<Product>();
            //PromoIDApplied = 0;
            this.DateOfPurchase = DateTime.Now;
        }

        /*
        public Purchase(int PurchaseID, User user, List<Product> PuchsedProducts, Promo PromoIDApplied, DateTime DateOfPurchase)
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

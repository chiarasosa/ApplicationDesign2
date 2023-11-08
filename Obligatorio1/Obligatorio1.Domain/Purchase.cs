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
        public int UserID { get; set; }
        public User User { get; set; }
        public List<PurchaseProduct> PurchasedProducts { get; set; }

        public string PromoApplied { get; set; }
        public DateTime? DateOfPurchase { get; set; }

        public string PaymentMethod { get; set; }

        public Purchase()
        {
            this.PurchasedProducts = new List<PurchaseProduct>();
            this.PromoApplied = String.Empty;
            this.DateOfPurchase = null;
            this.PaymentMethod = String.Empty;
        }

        public Purchase(int PurchaseID, int userID, List<PurchaseProduct> PurchasedProducts, string NamePromoApplied, DateTime DateOfPurchase)
        {
            this.PurchaseID = PurchaseID;
            this.UserID = userID;
            this.PurchasedProducts = PurchasedProducts;
            this.PromoApplied = NamePromoApplied;
            this.DateOfPurchase = DateOfPurchase;
            this.PaymentMethod = String.Empty;
        }
    }
}

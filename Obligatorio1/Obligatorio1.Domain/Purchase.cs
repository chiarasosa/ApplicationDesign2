﻿using System;
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

        public List<Product> PurchasedProducts { get; set; }

        public string PromoApplied { get; set; }

        public DateTime DateOfPurchase { get; set; }

        public Purchase()
        {
            this.PurchaseID = 0;
            this.UserID = 0;
            this.PurchasedProducts = new List<Product>();
            this.PromoApplied = String.Empty;
            this.DateOfPurchase = DateTime.Now;
        }

        public Purchase(int PurchaseID, int userID, List<Product> PurchasedProducts, string NamePromoApplied, DateTime DateOfPurchase)
        {
            this.PurchaseID = PurchaseID;
            this.UserID = userID;
            this.PurchasedProducts = PurchasedProducts;
            this.PromoApplied = NamePromoApplied;
            this.DateOfPurchase = DateOfPurchase;
        }
    }
}

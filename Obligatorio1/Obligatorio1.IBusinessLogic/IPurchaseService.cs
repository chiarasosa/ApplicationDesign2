﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;

namespace Obligatorio1.IBusinessLogic
{
    public interface IPurchaseService
    {
        void CreatePurchase(Guid authToken, Purchase p);
        IEnumerable<Purchase> GetAllPurchases();
        public Purchase GetPurchaseByID(int purchaseID);
        IEnumerable<Purchase> GetPurchasesByUserID(int userID);
    }
}

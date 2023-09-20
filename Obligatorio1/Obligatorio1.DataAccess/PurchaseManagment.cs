using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IDataAccess;
using Obligatorio1.Domain;

namespace Obligatorio1.DataAccess
{
    public class PurchaseManagment : IPurchaseManagment
    {
        private List<Product> cart;
        private Purchase purchase;
        

        public int ValidateMoreThan1Item(Purchase purchase)
        {
            
            return purchase.PurchasedProducts.Count();
        }
    }
}

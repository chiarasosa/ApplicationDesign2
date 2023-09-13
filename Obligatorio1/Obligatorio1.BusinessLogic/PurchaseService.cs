using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.BusinessLogic
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseManagment purchaseManagment;
        //private User? loggedInUser;

        
        public PurchaseService(IPurchaseManagment purchaseManagment)
        {
            this.purchaseManagment = purchaseManagment;
            //this.loggedInUser = null;
        }

        public bool ValidateMoreThan1Item(List<Product> cart)
        {
            if (cart.Count() < 1)
            {
                throw new Obligatorio1.Exceptions.ExceptionPurchase("El carrito debe tener mas de un elemento para poder realizar la compra.");
            }
            return true;
        }
    }
}

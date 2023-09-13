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
       
        public PurchaseManagment(IPurchaseManagment userManagment)
        {
           
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

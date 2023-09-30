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
        
        public PurchaseService(IPurchaseManagment purchaseManagment)
        {
            this.purchaseManagment = purchaseManagment;
        }

        public Purchase ExecutePurchase(Cart cart)
        {
            if(cart.Products.Count() == 0){
                throw new Obligatorio1.Exceptions.ExceptionPurchase("El carrito debe tener mas de un elemento para poder realizar la compra.");
            }
            return new Purchase
            {
                PurchasedProducts = cart.Products,
                PromoApplied = cart.PromotionApplied,
                DateOfPurchase = DateTime.Today,
            };
        }

        public List<Purchase> GetPurchases()
        {
            return new List<Purchase>();
        }
    }
}

using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.BusinessLogic
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseManagment _purchaseManagment;
        private readonly ICartManagment _cartManagment;
        private readonly IUserManagment _userManagment;

        public PurchaseService(IPurchaseManagment purchaseManagment, IUserManagment userManagment, ICartManagment cartManagment)
        {
            this._purchaseManagment = purchaseManagment;
            this._userManagment = userManagment;
            this._cartManagment = cartManagment;
        }

        public void CreatePurchase(Guid authToken)
        {
            try { _purchaseManagment.CreatePurchase(authToken); }
            catch { throw new ExceptionPurchase("Error inesperado al realizar la compra."); }
        }
        public List<Purchase> GetPurchases()
        {
            return new List<Purchase>();
        }
    }
}


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
 

        public PurchaseService(IPurchaseManagment purchaseManagment, IUserManagment userManagment, ICartManagment cartManagment, 
                        IGenericRepository<PurchaseProduct> purchaseProductRepository)
        {
            this._purchaseManagment = purchaseManagment;
            this._userManagment = userManagment;
            this._cartManagment = cartManagment;
        }

        public void CreatePurchase(Guid authToken)
        {
            try { _purchaseManagment.CreatePurchase(authToken); }
            catch { throw new PurchaseException("Error inesperado al realizar la compra."); }
        }
        public IEnumerable<Purchase> GetAllPurchases()
        {
            try 
            { 
                var purchases= _purchaseManagment.GetAllPurchases();
                return purchases;
            }
            catch 
            { 
                throw new PurchaseException("Error inesperado al obtener todas las compras."); 
            }
        }

        public Purchase GetPurchaseByID(int purchaseID) 
        {
            try
            {
                var purchase = _purchaseManagment.GetPurchaseByID(purchaseID);
                return purchase;
            }
            catch
            {
                throw new PurchaseException("Error inesperado al obtener la compra.");
            }
        }
        public IEnumerable<Purchase> GetPurchasesByUserID(int userID)
        {
            try
            {
                var purchase = _purchaseManagment.GetPurchasesByUserID(userID);
                return purchase;
            }
            catch
            {
                throw new PurchaseException("Error inesperado al obtener la compra.");
            }
        }
    }
}
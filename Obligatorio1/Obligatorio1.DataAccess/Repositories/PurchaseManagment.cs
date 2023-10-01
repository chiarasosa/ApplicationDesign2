using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IDataAccess;
using Obligatorio1.Domain;

namespace Obligatorio1.DataAccess.Repositories
{
    public class PurchaseManagment : IPurchaseManagment
    {
        private readonly IGenericRepository<Purchase> _repository;
        public PurchaseManagment(IGenericRepository<Purchase> purchaseRepositoy)
        {
            _repository = purchaseRepositoy;
        }
        public void CreatePurchase(Purchase purchase)
        {
            try
            {
                _repository.Insert(purchase);
                _repository.Save();
            }
            catch (Exception ex)
            {
                // Log the inner exception details
                Console.WriteLine("Inner Exception Details: " + ex.InnerException?.Message);
                throw;
            }
        }


    }
}

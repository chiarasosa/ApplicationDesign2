using Obligatorio1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.IDataAccess
{
    public interface IPurchaseProductManagment
    {
        public List<Product> GetProductsByPurchaseID(int purchaseID);
    }
}

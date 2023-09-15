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
    public class ThreeForOnePromoLogic : IPromoService
    {
        private readonly IPromoManagment promoManagment;

        public ThreeForOnePromoLogic(IPromoManagment purchaseManagment)
        {
            this.promoManagment = purchaseManagment;
        }

        public double CalculateDiscount(List<Product> cart)
        {
            return 0;
        }
    }
}

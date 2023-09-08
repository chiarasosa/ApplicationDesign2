using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;

namespace Obligatorio1.BusinessLogic
{
    public class PromoManager : IPromoManager
    {
        //cambiar decimal por IPromoLogic
        public decimal GetBestPromo(List<Product> cart)
        {
            return 0;
        }

        public decimal ApplyBestPromo(List<Product> cart)
        {
            return 0;
        }
    }
}

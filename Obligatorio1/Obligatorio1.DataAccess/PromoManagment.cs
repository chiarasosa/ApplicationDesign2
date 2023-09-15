using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IDataAccess;
using Obligatorio1.Domain;

namespace Obligatorio1.DataAccess
{
    public class PromoManagment : IPromoManagment
    {
        public double CalculateDiscount(List<Product> cart)
        {
            return 0;
        }
    }
}

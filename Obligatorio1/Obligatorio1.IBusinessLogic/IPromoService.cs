using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;

namespace Obligatorio1.IBusinessLogic
{
    public interface IPromoService
    {
        string GetName();
        double CalculateNewPriceWithDiscount(Cart cart);
    }
}

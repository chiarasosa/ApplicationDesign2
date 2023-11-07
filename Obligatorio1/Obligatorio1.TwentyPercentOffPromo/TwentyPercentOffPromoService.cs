using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;
using Obligatorio1.PromoInterface;

namespace Obligatorio1.BusinessLogic
{
    public class TwentyPercentOffPromoService : IPromoService
    {
        public TwentyPercentOffPromoService()
        {
 
        }

        public string GetName()
        {
            return "20% Off Promo";
        }
        public double CalculateNewPriceWithDiscount(Cart cart)
        {
            if (!(cart.Products.Count >= 2))
            {
                return cart.TotalPrice;
            }
            cart.TotalPrice -= cart.Products.Max(p => p.Price) * 0.2;

            return cart.TotalPrice;
        }

    }
}

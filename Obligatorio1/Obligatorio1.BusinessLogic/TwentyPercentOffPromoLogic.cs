using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;

namespace Obligatorio1.BusinessLogic
{
    public class TwentyPercentOffPromoLogic : IPromoService
    {
        public string Name;
        public TwentyPercentOffPromoLogic()
        {
            this.Name = "20% Off Promo";
        }

        public double CalculateNewPriceWithDiscount(Cart cart)
        {
            if (!CartHas2OrMoreItems(cart))
            {
                return cart.TotalPrice;
            }
            cart.TotalPrice -= cart.Products.Max(p => p.Price) * 0.2;

            return cart.TotalPrice;
        }

        public bool CartHas2OrMoreItems(Cart cart)
        {
            if (cart.Products != null)
            {
                int counter = 0;
                foreach (Product item in cart.Products)
                {
                    counter++;
                    if (counter == 2)
                        return true;
                }
            }
            return false;
        }

    }
}

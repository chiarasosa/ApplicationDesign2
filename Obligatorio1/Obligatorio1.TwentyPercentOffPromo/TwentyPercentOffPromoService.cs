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
            if (!(PromoUtility.ProductsWithPromotions(cart).Count >= 2))
            {
                return cart.TotalPrice;
            }
            else
            {
                cart.TotalPrice -= PromoUtility.ProductsWithPromotions(cart).Max(p => p.Price) * 0.2;

            }
            cart.TotalPrice -= cart.Products.Max(p => p.Price) * 0.2;

            return cart.TotalPrice;
        }
    }
}

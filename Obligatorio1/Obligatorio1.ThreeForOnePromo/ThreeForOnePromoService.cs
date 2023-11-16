using Obligatorio1.Domain;
using Obligatorio1.PromoInterface;

namespace Obligatorio1.BusinessLogic
{
    public class ThreeForOnePromoService : IPromoService
    {
        public ThreeForOnePromoService()
        {
        }
        public string GetName()
        {
            return "3x1 Promo";
        }
        public double CalculateNewPriceWithDiscount(Cart cart)
        {
            if (!(PromoUtility.ProductsWithPromotions(cart).Count >= 3))
            {
                return cart.TotalPrice;
            }

            Dictionary<string, List<Product>> productsByBrand = PromoUtility.GroupProductsBy(PromoUtility.ProductsWithPromotions(cart),
                product => product.Brand);

            string brandWithDiscount = FindBrandWithMaxDiscount(productsByBrand);

            if (brandWithDiscount != null && brandWithDiscount != "")
            {
                cart.TotalPrice = ApplyDiscountToCart(cart, productsByBrand[brandWithDiscount]);
            }
            return cart.TotalPrice;
        }

        public string FindBrandWithMaxDiscount(Dictionary<string, List<Product>> productsByBrand)
        {
            int maxDiscount = 0;
            string brandWithMaxDiscount = "";

            foreach (KeyValuePair<string, List<Product>> brandProducts in productsByBrand)
            {
                if (brandProducts.Value.Count() >= 3)
                {
                    int totalDiscount = brandProducts.Value.OrderBy(p => p.Price).Take(2).Sum(p => p.Price);
                    if (totalDiscount >= maxDiscount)
                    {
                        maxDiscount = totalDiscount;
                        brandWithMaxDiscount = brandProducts.Key;
                    }
                }
            }
            return brandWithMaxDiscount;
        }

        public double ApplyDiscountToCart(Cart cart, List<Product> productsToDiscount)
        {
            int totalDiscount = productsToDiscount.OrderBy(p => p.Price).Take(2).Sum(p => p.Price);
            cart.TotalPrice -= totalDiscount;
            return cart.TotalPrice;
        }

    }
}

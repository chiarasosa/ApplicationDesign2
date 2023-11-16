using Obligatorio1.Domain;
using Obligatorio1.PromoInterface;

namespace Obligatorio1.BusinessLogic
{
    public class ThreeForTwoPromoService : IPromoService
    {
        public ThreeForTwoPromoService()
        {
        }
        public string GetName()
        {
            return "3x2 Promo";
        }
        public double CalculateNewPriceWithDiscount(Cart cart)
        {
            if (!(PromoUtility.ProductsWithPromotions(cart).Count >= 3))
            {
                return cart.TotalPrice;
            }

            Dictionary<int, List<Product>> productsByCategory = PromoUtility.GroupProductsBy(PromoUtility.ProductsWithPromotions(cart),
                product => product.Category);

            int categoryWithDiscount = FindCategoryWithMaxDiscount(productsByCategory);

            if (categoryWithDiscount != 0)
            {
                cart.TotalPrice = ApplyDiscountToCart(cart, productsByCategory[categoryWithDiscount]);
            }

            return cart.TotalPrice;
        }
        public int FindCategoryWithMaxDiscount(Dictionary<int, List<Product>> productsByCategory)
        {
            int maxDiscount = 0;
            int categoryWithMaxDiscount = 0;

            foreach (KeyValuePair<int, List<Product>> categoryProducts in productsByCategory)
            {
                if (categoryProducts.Value.Count() >= 3)
                {
                    int totalDiscount = categoryProducts.Value.Min(p => p.Price);

                    if (totalDiscount >= maxDiscount)
                    {
                        maxDiscount = totalDiscount;
                        categoryWithMaxDiscount = categoryProducts.Key;
                    }
                }
            }

            return categoryWithMaxDiscount;
        }
        public double ApplyDiscountToCart(Cart cart, List<Product> productsToDiscount)
        {
            int totalDiscount = productsToDiscount.Min(p => p.Price);
            cart.TotalPrice -= totalDiscount;
            return cart.TotalPrice;
        }
    }
}

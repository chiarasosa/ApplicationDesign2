using Obligatorio1.Domain;
using Obligatorio1.PromoInterface;

namespace Obligatorio1.BusinessLogic
{
    public class TotalLookPromoService : IPromoService
    {
        public TotalLookPromoService()
        {

        }

        public string GetName()
        {
            return "Total Look Promo";
        }

        public double CalculateNewPriceWithDiscount(Cart cart)
        {
            if (!(PromoUtility.ProductsWithPromotions(cart).Count >= 3))
            {
                return cart.TotalPrice;
            }

            //solo ordeno los productos que estan incluidos en promociones por su color, los demas los ignoro
            Dictionary<string, List<Product>> productsByColor = PromoUtility.GroupProductsBy(PromoUtility.ProductsWithPromotions(cart),
                product => product.Color);

            string colorWithDiscount = FindColorWithMaxDiscount(productsByColor);

            if (colorWithDiscount != null)
            {
                cart.TotalPrice = ApplyDiscountToCart(cart, productsByColor[colorWithDiscount]);
            }

            return cart.TotalPrice;
        }

        public List<Product> ProductsWithPromotions(Cart cart)
        {
            List<Product> products = new List<Product>();
            foreach (Product product in cart.Products)
            {
                if (product.AvailableToPromotions == true)
                {
                    products.Add(product);
                }
            }
            return products;
        }

        public string FindColorWithMaxDiscount(Dictionary<string, List<Product>> productsByColor)
        {
            double maxDiscount = 0;
            string colorWithMaxDiscount = null;

            foreach (KeyValuePair<string, List<Product>> colorProducts in productsByColor)
            {
                if (colorProducts.Value.Count() >= 3)
                {
                    double totalDiscount = colorProducts.Value.Max(p => p.Price) * 0.5;

                    if (totalDiscount >= maxDiscount)
                    {
                        maxDiscount = totalDiscount;
                        colorWithMaxDiscount = colorProducts.Key;
                    }
                }
            }

            return colorWithMaxDiscount;
        }

        public double ApplyDiscountToCart(Cart cart, List<Product> productsToDiscount)
        {
            double totalDiscount = (productsToDiscount.Max(p => p.Price)) * 0.5;
            cart.TotalPrice -= totalDiscount;
            return cart.TotalPrice;
        }
    }
}

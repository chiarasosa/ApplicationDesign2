using Obligatorio1.Domain;

namespace Obligatorio1.PromoInterface
{
    public static class PromoUtility
    {

        public static Dictionary<TKey, List<Product>> GroupProductsBy<TKey>(List<Product> products, Func<Product, TKey> groupByFunc)
        {
            Dictionary<TKey, List<Product>> productsByGroup = products
                .GroupBy(groupByFunc)
                .ToDictionary(group => group.Key, group => group.ToList());

            return productsByGroup;
        }

        public static List<Product> ProductsWithPromotions(Cart cart)
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
    }

}

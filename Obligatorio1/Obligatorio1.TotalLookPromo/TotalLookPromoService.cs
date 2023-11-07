using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (!(cart.Products.Count >= 3))
            {
                return cart.TotalPrice;
            }

            Dictionary<string, List<Product>> productsByColor = PromoUtility.GroupProductsBy(cart, product => product.Color);

            string colorWithDiscount = FindColorWithMaxDiscount(productsByColor);

            if (colorWithDiscount != null)
            {
                cart.TotalPrice = ApplyDiscountToCart(cart, productsByColor[colorWithDiscount]);
            }

            return cart.TotalPrice;
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

        /*
        public Dictionary<string, List<Product>> GroupProductsByColor(Cart cart)
        {
            Dictionary<string, List<Product>> productsByColor = new Dictionary<string, List<Product>>();

            foreach (Product product in cart.Products)
            {

                if (!productsByColor.ContainsKey(product.Color))
                {
                    productsByColor[product.Color] = new List<Product>();
                }
                productsByColor[product.Color].Add(product);

            }

            return productsByColor;
        }
        */

        public double ApplyDiscountToCart(Cart cart, List<Product> productsToDiscount)
        {
            double totalDiscount = (productsToDiscount.Max(p => p.Price)) * 0.5;
            cart.TotalPrice -= totalDiscount;
            return cart.TotalPrice;
        }

    }
}

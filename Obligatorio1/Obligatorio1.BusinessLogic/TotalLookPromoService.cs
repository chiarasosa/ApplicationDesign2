using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;

namespace Obligatorio1.BusinessLogic
{
    public class TotalLookPromoService : IPromoService
    {
        public string Name;
        public TotalLookPromoService()
        {
            this.Name = "Total Look Promo";
        }

        public double CalculateNewPriceWithDiscount(Cart cart)
        {
            if (!CartHas3OrMoreItems(cart))
            {
                return cart.TotalPrice;
            }

            Dictionary<string, List<Product>> productsByColor = GroupProductsByColor(cart);

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

        public bool CartHas3OrMoreItems(Cart cart)
        {
            if (cart.Products != null)
            {
                int counter = 0;
                foreach (Product item in cart.Products)
                {
                    counter++;
                    if (counter == 3)
                        return true;
                }
            }
            return false;
        }

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


        public double ApplyDiscountToCart(Cart cart, List<Product> productsToDiscount)
        {
            double totalDiscount = (productsToDiscount.Max(p => p.Price)) * 0.5;
            cart.TotalPrice -= totalDiscount;
            return cart.TotalPrice;
        }

    }
}

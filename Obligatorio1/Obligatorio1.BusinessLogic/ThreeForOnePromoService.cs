using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;

namespace Obligatorio1.BusinessLogic
{
    public class ThreeForOnePromoService : IPromoService
    {
        public string Name;
        public ThreeForOnePromoService()
        {
            this.Name = "3x1 Promo";
        }
        public string GetName()
        {
            return this.Name;
        }
        public double CalculateNewPriceWithDiscount(Cart cart)
        {
            if (!CartHas3OrMoreItems(cart))
            {
                return cart.TotalPrice;
            }
            Dictionary<int, List<Product>> productsByBrand = GroupProductsByBrand(cart);
            int brandWithDiscount = FindBrandWithMaxDiscount(productsByBrand);
            if (brandWithDiscount != 0)
            {
                cart.TotalPrice = ApplyDiscountToCart(cart, productsByBrand[brandWithDiscount]);
            }
            return cart.TotalPrice;
        }

        public int FindBrandWithMaxDiscount(Dictionary<int, List<Product>> productsByBrand)
        {
            int maxDiscount = 0;
            int brandWithMaxDiscount = 0;

            foreach (KeyValuePair<int, List<Product>> brandProducts in productsByBrand)
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

        public bool CartHas3OrMoreItems(Cart cart)
        {
            if(cart.Products != null)
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

        public Dictionary<int, List<Product>> GroupProductsByBrand(Cart cart)
        {
            Dictionary<int, List<Product>> productsByBrand = new Dictionary<int, List<Product>>();
            foreach (Product product in cart.Products)
            {
                if (!productsByBrand.ContainsKey(product.Brand))
                {
                    productsByBrand[product.Brand] = new List<Product>();
                }
                productsByBrand[product.Brand].Add(product);
            }
            return productsByBrand;
        }

        public double ApplyDiscountToCart(Cart cart, List<Product> productsToDiscount)
        {
            int totalDiscount = productsToDiscount.OrderBy(p => p.Price).Take(2).Sum(p => p.Price);
            cart.TotalPrice -= totalDiscount;
            return cart.TotalPrice;
        }

    }
}

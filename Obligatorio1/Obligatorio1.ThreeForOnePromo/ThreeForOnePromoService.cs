﻿using Obligatorio1.Domain;
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

            Dictionary<int, List<Product>> productsByBrand = PromoUtility.GroupProductsBy(PromoUtility.ProductsWithPromotions(cart),
                product => product.Brand.ToList());

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

        public double ApplyDiscountToCart(Cart cart, List<Product> productsToDiscount)
        {
            int totalDiscount = productsToDiscount.OrderBy(p => p.Price).Take(2).Sum(p => p.Price);
            cart.TotalPrice -= totalDiscount;
            return cart.TotalPrice;
        }

    }
}

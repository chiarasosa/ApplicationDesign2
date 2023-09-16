using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.BusinessLogic
{
    public class ThreeForTwoPromoLogic : IPromoService
    {
        public ThreeForTwoPromoLogic()
        {

        }

        public double CalculateNewPriceWithDiscount(Cart cart)
        {
            if (!CartHas3OrMoreItems(cart))
            {
                return cart.TotalPrice;
            }

            Dictionary<int, List<Product>> productsByCategory = GroupProductsByCategory(cart);

            int categoryWithDiscount = FindCategoryWithMaxDiscount(productsByCategory);

            if (categoryWithDiscount != 0)
            {
                ApplyDiscountToCart(cart, productsByCategory[categoryWithDiscount]);
            }

            return cart.TotalPrice;
        }

        private int FindCategoryWithMaxDiscount(Dictionary<int, List<Product>> productsByCategory)
        {
            int maxDiscount = 0;
            int categoryWithMaxDiscount = 0;

            foreach (var categoryProducts in productsByCategory)
            {
                if (categoryProducts.Value.Count >= 3)
                {
                    int totalDiscount = categoryProducts.Value.Min(p => p.Price);

                    if (totalDiscount > maxDiscount)
                    {
                        maxDiscount = totalDiscount;
                        categoryWithMaxDiscount = categoryProducts.Key;
                    }
                }
            }

            return categoryWithMaxDiscount;
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

        public Dictionary<int, List<Product>> GroupProductsByCategory(Cart cart)
        {
            Dictionary<int, List<Product>> productsByCategory = new Dictionary<int, List<Product>>();

            foreach (Product product in cart.Products)
            {
                if (!productsByCategory.ContainsKey(product.Category))
                {
                    productsByCategory[product.Category] = new List<Product>();
                }
                productsByCategory[product.Category].Add(product);
            }

            return productsByCategory;
        }

        public void ApplyDiscountToCart(Cart cart, List<Product> productsToDiscount)
        {
            double discount = productsToDiscount.Min(p => p.Price);
            cart.TotalPrice -= discount;
        }
    }
}

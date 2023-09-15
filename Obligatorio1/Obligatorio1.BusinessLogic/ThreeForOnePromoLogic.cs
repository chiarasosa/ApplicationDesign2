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
    public class ThreeForOnePromoLogic : IPromoService
    {
        private readonly IPromoManagment promoManagment;

        public ThreeForOnePromoLogic(IPromoManagment purchaseManagment)
        {
            this.promoManagment = purchaseManagment;
        }

        public ThreeForOnePromoLogic()
        {

        }

      
        public double CalculateNewPriceWithDiscount(Cart cart)
        {
            if (!CartHas3OrMoreItems(cart))
            {
                return cart.TotalPrice;
            }

            Dictionary<int, List<Product>> productsByBrand = GroupProductsByBrand(cart);

            /*
            string brandWithDiscount = FindBrandWithDiscount(productsByBrand);

            if (brandWithDiscount != null)
            {
                ApplyDiscountToCart(cart, productsByBrand[brandWithDiscount]);
            }
            */
            return cart.TotalPrice;
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


    }
}

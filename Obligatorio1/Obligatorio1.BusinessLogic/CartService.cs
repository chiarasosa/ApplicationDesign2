using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.BusinessLogic
{
    public class CartService : ICartService
    {
        private readonly ICartManagment _cartManagment;
        private readonly IPromoManagerManagment promoManagerManagment;

        public CartService(ICartManagment cartManagment, IPromoManagerManagment promoManagerManagment)
        {
            this._cartManagment = cartManagment;
            this.promoManagerManagment = promoManagerManagment;
        }

        public void AddProductToCart(Product product, Guid authToken)
        {
            _cartManagment.AddProductToCart(product, authToken);
            //ApplyBestPromotion(authToken);
        }

        public void DeleteProductFromCart(Product product, Guid authToken)
        {
            _cartManagment.DeleteProductFromCart(product, authToken);
           // ApplyBestPromotion(authToken);
        }

        public Cart ApplyBestPromotion(Guid authToken)
        {
            Cart cart = _cartManagment.GetCart();
            if (cart.Products != null)
            {
                List<IPromoService> availablePromotions = promoManagerManagment.GetAvailablePromotions();
                if (availablePromotions.Count() > 0)
                {
                    double bestDiscount = cart.TotalPrice;
                    foreach (IPromoService promotion in availablePromotions)
                    {
                        double price = promotion.CalculateNewPriceWithDiscount(cart);
                        if (price < bestDiscount)
                        {
                            bestDiscount = price;
                            cart.PromotionApplied = promotion.GetName();
                        }
                        bestDiscount = Math.Min(bestDiscount, price);

                    }

                    cart.TotalPrice = bestDiscount;
                }
            }
            _cartManagment.UpdateCartWithDiscount(cart, authToken);
            return cart;
        }

        public Cart GetLoggedInCart()
        {
            return _cartManagment.GetCart();
        }
    }
}

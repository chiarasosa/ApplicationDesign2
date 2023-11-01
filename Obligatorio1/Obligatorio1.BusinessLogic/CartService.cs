using Microsoft.IdentityModel.Tokens;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;
using System.Xml.XPath;

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
            Cart cart = _cartManagment.GetCart(authToken);
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

        public IEnumerable<Product> GetAllProductsFromCart(Guid authToken)
        {
            IEnumerable<Product> products = _cartManagment.GetAllProductsFromCart(authToken);
            if (products != null)
            {
                return products;
            }
            throw new CartException("No existen productos asociados al carrito."); // Si no hay productos en el carrito, se devuelve una colección vacía.
        }

        public String GetPromottionApplied(Guid authToken) {

            string result = "";
            result= _cartManagment.GetPromottionApplied(authToken);

            if (result == null || result == "")
                throw new CartException("El carrito no tiene una promocion aplicada");

            return result;
        }
    }
}
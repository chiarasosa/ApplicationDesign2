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
        private readonly IPromotionsService promotionsService;

        public CartService(ICartManagment cartManagment, IPromoManagerManagment promoManagerManagment, IPromotionsService promotionsService)
        {
            this._cartManagment = cartManagment;
            this.promoManagerManagment = promoManagerManagment;
            this.promotionsService = promotionsService;
        }

        public void AddProductToCart(Product product, Guid authToken)
        {
            _cartManagment.AddProductToCart(product, authToken);
            ApplyBestPromotionCart(authToken);
        }

        public void DeleteProductFromCart(Product product, Guid authToken)
        {
            _cartManagment.DeleteProductFromCart(product, authToken);
            ApplyBestPromotionCart(authToken);
        }

        public Cart ApplyBestPromotionCart(Guid authToken)
        {
            Cart cart = promotionsService.ApplyBestPromotionToCart(authToken);
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
            throw new CartException("No existen productos asociados al carrito.");
        }

        public String GetPromottionAppliedCart(Guid authToken) {

            string result = "";
            result= _cartManagment.GetPromottionAppliedCart(authToken);

            if (result == null || result == "")
                throw new CartException("El carrito no tiene una promocion aplicada");

            return result;
        }

        public double GetTotalPriceCart(Guid authToken) {

            double totalPrice = 0;
            totalPrice = _cartManagment.GetTotalPriceCart(authToken);

            return totalPrice;
        }
    }
}
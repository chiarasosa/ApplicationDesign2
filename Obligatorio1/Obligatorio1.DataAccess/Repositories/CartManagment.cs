using Obligatorio1.Domain;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.DataAccess
{
    public class CartManagment : ICartManagment
    {
        private readonly IGenericRepository<Session> _sessionRepository;
        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IGenericRepository<CartProduct> _cartProductRepository;

        public CartManagment(IGenericRepository<Session> sessionRepository, IGenericRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
            _sessionRepository = sessionRepository;
        }

        public void AddProductToCart(Product product, Guid authToken)
        {
            var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User.Cart" });
            if (session != null)
            {
                var cart = session.User.Cart;
                if (cart.Products == null)
                    cart.Products = new List<Product>();

                if (cart.CartProducts == null)
                    cart.CartProducts = new List<CartProduct>();

                var cartProduct = new CartProduct();
                cartProduct.Product = product;
                cartProduct.ProductID = product.ProductID;
                cartProduct.CartID = cart.CartID;
                cartProduct.Cart = cart;

                cart.Products.Add(product);
                cart.CartProducts.Add(cartProduct);
                cart.TotalPrice = cart.TotalPrice + product.Price;
                _cartRepository.Update(cart);
                _cartRepository.Save();
            }
        }

        public void DeleteProductFromCart(Product product, Guid authToken)
        {
            var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User.Cart" });
            if (session != null)
            {
                var cart = session.User.Cart;
                cart.Products.Remove(product);
                _cartRepository.Update(cart);
                _cartRepository.Save();
            }
        }

        public Cart GetCart()
        {
            return new Cart();
        }

        public void UpdateCartWithDiscount(Cart cart, Guid authToken)
        {
            var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User.Cart" });
            if (session != null)
            {
                session.User.Cart = cart;
                _cartRepository.Update(cart);
                _cartRepository.Save();
            }
        }
    }
}
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
        private readonly IGenericRepository<Session> _sessionRepository;
        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IProductService _productService;
        private readonly IGenericRepository<CartProduct> _cartProductRepository;
        private readonly IPromotionsService _promotionsService;

        public CartService(IGenericRepository<Session> sessionRepository, IGenericRepository<Cart> cartRepository, 
            IProductService productService, IGenericRepository<CartProduct> cartProductRepository, IPromotionsService promotionsService)
        {
            _cartRepository = cartRepository;
            _sessionRepository = sessionRepository;
            _productService = productService;
            _cartProductRepository = cartProductRepository;
            _promotionsService = promotionsService;
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

                cart.Products = GetAllProductsFromCart(authToken).ToList();
                cart.Products.Add(product);
                cart.CartProducts.Add(cartProduct);
                cart.TotalPrice = cart.TotalPrice + product.Price;
                cart = _promotionsService.ApplyBestPromotionToCart(cart);
                _cartRepository.Update(cart);
                _cartRepository.Save();
            }
            
        }

        public string MetodoPrueba(Guid authToken)
        {
            var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User.Cart" });
            return _promotionsService.MetodoPrueba(session.User.Cart);
        }

        public void DeleteProductFromCart(Product product, Guid authToken)
        {
            var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User.Cart" });
            if (session != null)
            {
                var cart = session.User.Cart;

                if (cart != null)
                {
                    var cartProducts = GetCartProductsByCartID(cart.CartID);

                    var cartProductToDelete = cartProducts.FirstOrDefault(cp => cp.ProductID == product.ProductID);

                    if (cartProductToDelete != null)
                    {
                        cartProducts.Remove(cartProductToDelete);
                        cart.CartProducts = cartProducts;
                        cart = _promotionsService.ApplyBestPromotionToCart(cart);
                        _cartRepository.Update(cart);
                        _cartRepository.Save();
                    }
                }
            }
        }

        public IEnumerable<Product> GetAllProductsFromCart(Guid authToken)
        {
            var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User.Cart" });
            List<Product> products = new List<Product>(); // Lista para almacenar los productos

            if (session != null && session.User != null && session.User.Cart != null)
            {
                var cart = session.User.Cart;
                var cartProducts = GetCartProductsByCartID(cart.CartID);

                // Obtener una lista de IDs de productos del carrito.
                List<int> productIds = cartProducts.Select(cp => cp.ProductID).ToList();

                foreach (int productId in productIds)
                {
                    Product product = _productService.GetProductByID(productId);
                    if (product != null)
                    {
                        products.Add(product);
                    }
                }
            }
            if (products != null)
            {
                return products;
            }
            throw new CartException("No existen productos asociados al carrito.");
        }

        public String GetPromottionAppliedCart(Guid authToken) {

            var result = "";
            var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User.Cart" });

            if (session != null && session.User != null && session.User.Cart != null &&
               (session.User.Cart.PromotionApplied != null && session.User.Cart.PromotionApplied != ""))
            {
                result = session.User.Cart.PromotionApplied;
            }

            if (result == null || result == "")
                throw new CartException("El carrito no tiene una promocion aplicada");

            return result;
        }

        public double GetTotalPriceCart(Guid authToken) {

            double totalPrice = 0;
            var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User.Cart" });

            if (session != null && session.User != null && session.User.Cart != null)
            {
                totalPrice = session.User.Cart.TotalPrice;
            }

            return totalPrice;
        }

        public List<CartProduct> GetCartProductsByCartID(int cartID)
        {
            if (cartID <= 0)
            {
                throw new CartProductException("ID de carrito inválido.");
            }

            List<CartProduct> cartProducts = _cartProductRepository
                .GetAll<CartProduct>()
                .Where(cp => cp.CartID == cartID)
                .ToList(); // Agregamos ToList() para materializar los resultados

            if (!cartProducts.Any())
            {
                throw new CartProductException($"No existen productos asociados al carrito con el ID {cartID}.");
            }

            return cartProducts;
        }
    }
}
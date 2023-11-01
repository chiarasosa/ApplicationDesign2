using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.DataAccess
{
    public class CartManagment : ICartManagment
    {
        private readonly IGenericRepository<Session> _sessionRepository;
        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IGenericRepository<CartProduct> _cartProductRepository;
        private readonly ICartProductManagment _cartProductManagment;
        private readonly IProductManagment _productManagment;
        public CartManagment(IGenericRepository<Session> sessionRepository, IGenericRepository<Cart> cartRepository,
                             IProductManagment productManagment, ICartProductManagment cartProductManagment)
        {
            _cartRepository = cartRepository;
            _sessionRepository = sessionRepository;
            _cartProductManagment = cartProductManagment;
            _productManagment = productManagment;
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

                if (cart != null)
                {
                    // Obtener la lista de productos asociados al carrito
                    var cartProducts = _cartProductManagment.GetCartProductsByCartID(cart.CartID);

                    // Buscar el producto que deseas eliminar
                    var cartProductToDelete = cartProducts.FirstOrDefault(cp => cp.ProductID == product.ProductID);

                    if (cartProductToDelete != null)
                    {
                        // Remover el producto de la lista de productos del carrito
                        cartProducts.Remove(cartProductToDelete);

                        // Actualizar la lista de productos asociados al carrito en la base de datos
                        cart.CartProducts = cartProducts;
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
                var cartProducts = _cartProductManagment.GetCartProductsByCartID(cart.CartID);

                // Obtener una lista de IDs de productos del carrito.
                List<int> productIds = cartProducts.Select(cp => cp.ProductID).ToList();

                foreach (int productId in productIds)
                {
                    Product product = _productManagment.GetProductByID(productId);
                    if (product != null)
                    {
                        products.Add(product); // Agregar el producto a la lista si se encuentra en la base de datos.
                    }
                }
            }

            return products;
        }

        public Cart GetCart(Guid authToken)
        {
            var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User.Cart" });
            if (session != null && session.User != null && session.User.Cart != null)
            {
                return session.User.Cart;
            }
            else
                throw new CartManagmentException($"Error al obtener el carrito con ID {session.User.Cart.CartID} no encontrado.");
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

        public String GetPromottionApplied(Guid authToken)
        {
            var result = "";
            var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User.Cart" });
          
            if (session != null && session.User != null && session.User.Cart != null && 
               (session.User.Cart.PromotionApplied != null && session.User.Cart.PromotionApplied != ""))
            {
                result= session.User.Cart.PromotionApplied;
            }

            return result;
        }
    }
}
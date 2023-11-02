using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.DataAccess { 
    public class CartProductManagment: ICartProductManagment
    {
        private readonly IGenericRepository<Session> _sessionRepository;
        private readonly IGenericRepository<CartProduct> _cartProductRepository;

        public CartProductManagment(IGenericRepository<Session> sessionRepository, IGenericRepository<CartProduct> cartProductRepository)
        {
            _cartProductRepository = cartProductRepository;
            _sessionRepository = sessionRepository;
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
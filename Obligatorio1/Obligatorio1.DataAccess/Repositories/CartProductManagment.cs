using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.DataAccess { 
    public class CartProductManagment: ICartProductManagment
    {
        private readonly IGenericRepository<Session> _sessionRepository;
        private readonly IGenericRepository<CartProduct> _cartProductRepository;

        public CartProductManagment(IGenericRepository<Session> sessionRepository, IGenericRepository<CartProduct> cartRepository)
        {
            _cartProductRepository = cartRepository;
            _sessionRepository = sessionRepository;
        }
        public IEnumerable<CartProduct> GetCartProductsByCartID(int cartID)
        {

            if (cartID <= 0)
            {
                throw new ThrewException("ID de usuario inválido.");
            }

            CartProduct? cartProduct = _cartProductRepository.GetAll<CartProduct>().FirstOrDefault(cp => cp.CartID == cartID);

            if (cartProduct == null)
            {
                throw new UserException($"No existen productos asociados al carrito con el ID {cartID}.");
            }

            return cartProduct;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IDataAccess;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;

namespace Obligatorio1.DataAccess
{
    public class CartManagment : ICartManagment
    {
        private readonly IGenericRepository<Session> _sessionRepository;
        private readonly IGenericRepository<Cart> _cartRepository;

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
                cart.Products.Add(product);
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
            //return authenticatedUser.Cart;
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

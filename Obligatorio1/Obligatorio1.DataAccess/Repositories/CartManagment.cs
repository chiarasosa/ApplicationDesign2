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
        private User authenticatedUser;
        private readonly IGenericRepository<User> _repository;

        public CartManagment(IGenericRepository<User> userRepository)
        {
            authenticatedUser = new User(1, "Chiara", "Chiara123", "chiarasosa@gmail.com", "Calle 1", "Administrador", new List<Purchase>());
            _repository = userRepository;
        }


        public void AddProductToCart(Product product)
        {
            
            authenticatedUser?.Cart.Products.Add(product);
        }

        public void DeleteProductFromCart(Product product)
        {
            authenticatedUser?.Cart.Products.Remove(product);
        }

        public Cart GetCart()
        {
            return authenticatedUser.Cart;
        }

        public void UpdateCartWithDiscount(Cart cart)
        {
            authenticatedUser.Cart = cart;
        }
    }
}

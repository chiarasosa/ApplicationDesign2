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

        public CartManagment(User user)
        {
            authenticatedUser = user;
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

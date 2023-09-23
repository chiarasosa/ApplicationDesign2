using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.BusinessLogic
{
    public class CartService : ICartService
    {
        private readonly IUserManagment _userManagment;
        public User? authenticatedUser;
        public Cart defaultCart;

        public CartService()
        {
            this.defaultCart = new Cart();
            this.authenticatedUser = null;
        }
        public CartService(IUserManagment userManagment)
        {
            this._userManagment = userManagment;
            this.authenticatedUser = userManagment.GetAuthenticatedUser();
            this.defaultCart = null;
        }

        public void AddProductToCart(Product product)
        {
            if (authenticatedUser == null)
            {
                defaultCart.Products.Add(product);
            }
            else
            {
                _userManagment.AddProductToCart(product);
            }
        }

        public void DeleteProductFromCart(Product product)
        {
            if (authenticatedUser == null)
            {
                defaultCart.Products.Remove(product);
            }
            else
            {
                _userManagment.DeleteProductFromCart(product);
            }
        }
    }
}

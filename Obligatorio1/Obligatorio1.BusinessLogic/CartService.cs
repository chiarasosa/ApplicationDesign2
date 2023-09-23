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
        private User? authenticatedUser;
        private Cart defaultCart;

        public CartService(IUserManagment userManagment)
        {
            this._userManagment = userManagment;
            this.authenticatedUser = userManagment._authenticatedUser;
            this.defaultCart = new Cart();
        }

        public void AddProductToCart(Product product)
        {

        }

        public void DeleteProductFromCart(Product product)
        {

        }
    }
}

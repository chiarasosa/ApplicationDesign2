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

        public CartService(IUserManagment userManagment)
        {
            this._userManagment = userManagment;
        }

        public void AddProductToCart(Product product)
        {
            _userManagment.AddProductToCart(product);
        }

        public void DeleteProductFromCart(Product product)
        {
            _userManagment.DeleteProductFromCart(product);
        }
    }
}

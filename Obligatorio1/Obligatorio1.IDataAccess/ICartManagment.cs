using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;

namespace Obligatorio1.IDataAccess
{
    public interface ICartManagment
    {
        void SetAuthenticatedUser(User user);
        void AddProductToCart(Product product);
        void DeleteProductFromCart(Product product);
        Cart GetCart();
        void UpdateCartWithDiscount(Cart cart);
    }
}

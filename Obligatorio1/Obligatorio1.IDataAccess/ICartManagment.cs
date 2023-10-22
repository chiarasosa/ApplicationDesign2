﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;

namespace Obligatorio1.IDataAccess
{
    public interface ICartManagment
    {
        void AddProductToCart(Product product, Guid authToken);
        void DeleteProductFromCart(Product product, Guid authToken);
        Cart GetCart();
        void UpdateCartWithDiscount(Cart cart, Guid authToken);
    }
}

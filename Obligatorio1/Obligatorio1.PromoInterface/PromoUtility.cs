using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;

namespace Obligatorio1.PromoInterface
{
    public static class PromoUtility
    {

        public static Dictionary<TKey, List<Product>> GroupProductsBy<TKey>(Cart cart, Func<Product, TKey> groupByFunc)
        {
            Dictionary<TKey, List<Product>> productsByGroup = cart.Products
                .GroupBy(groupByFunc)
                .ToDictionary(group => group.Key, group => group.ToList());

            return productsByGroup;
        }

    }

}

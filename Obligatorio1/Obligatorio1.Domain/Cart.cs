using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Domain
{
    public class Cart
    {
        private List<Product> Products;

        private double TotalPrice;

        //private User User;

        public Cart()
        {
            Products = new List<Product>();
            TotalPrice = 0;
        }
    }
}

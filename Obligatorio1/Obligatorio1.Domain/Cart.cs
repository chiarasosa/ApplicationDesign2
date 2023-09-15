using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Domain
{
    public class Cart
    {
        public List<Product> Products;

        public double TotalPrice;

        //private User User;

        public Cart()
        {
            this.Products = new List<Product>();
            this.TotalPrice = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Domain
{
    public class CartProduct
    {
        public int CartID { get; set; }
        public Cart Cart { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}

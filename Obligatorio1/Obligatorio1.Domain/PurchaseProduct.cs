using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Domain
{
    public class PurchaseProduct
    {
        public int PurchaseID { get; set; }
        public Purchase Purchase { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}

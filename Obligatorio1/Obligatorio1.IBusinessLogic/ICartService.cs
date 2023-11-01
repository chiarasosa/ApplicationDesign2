using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;

namespace Obligatorio1.IBusinessLogic
{
    public interface ICartService
    {
        void AddProductToCart(Product product, Guid authToken);
        public IEnumerable<Product> GetAllProductsFromCart(Guid authToken);
        void DeleteProductFromCart(Product product, Guid authToken);
        Cart ApplyBestPromotionCart(Guid authToken);
        public String GetPromottionAppliedCart(Guid authToken);
        public double GetTotalPriceCart(Guid authToken);
    }
}
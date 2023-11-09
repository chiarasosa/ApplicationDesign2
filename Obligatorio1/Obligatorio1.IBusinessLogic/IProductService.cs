using Obligatorio1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.IBusinessLogic
{
    public interface IProductService
    {
        void RegisterProduct(Product product);
        Product UpdateProduct(Product product);
        Product GetProductByID(int productID);
        IEnumerable<Product> GetProducts();
        void DeleteProduct(int productID);
        IEnumerable<Product> GetProductsRange(int min, int max);
        IEnumerable<Product> GetProductsInPromotions();
        void ProductSold(Product product);
    }
}

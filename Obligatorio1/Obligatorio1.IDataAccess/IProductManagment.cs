using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IDataAccess;
using Obligatorio1.Domain;

namespace Obligatorio1.IDataAccess
{
    public interface IProductManagment
    {

        void RegisterProduct(Product product);
        Product UpdateProduct(Product product);
        Product GetProductByID(int productID);
        IEnumerable<Product> GetProducts();
        void DeleteProduct(int productID);
        void CreateProduct(Product product);
        List<Product> SearchByParameter(string text, string brand, string category);
    }
}

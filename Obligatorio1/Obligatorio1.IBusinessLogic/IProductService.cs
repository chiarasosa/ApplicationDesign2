using Obligatorio1.Domain;

namespace Obligatorio1.IBusinessLogic
{
    public interface IProductService
    {
        void RegisterProduct(Product product);
        Product UpdateProduct(int id, Product product);
        Product GetProductByID(int productID);
        IEnumerable<Product> GetProducts();
        void DeleteProduct(int productID);
        IEnumerable<Product> GetProductsRange(int min, int max);
        IEnumerable<Product> GetProductsInPromotions();
        void ProductSold(Product product);
    }
}

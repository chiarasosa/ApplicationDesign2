using Obligatorio1.Domain;

namespace Obligatorio1.IBusinessLogic
{
    public interface ICartService
    {
        void AddProductToCart(Product product, Guid authToken);
        public IEnumerable<Product> GetAllProductsFromCart(Guid authToken);
        void DeleteProductFromCart(Product product, Guid authToken);
        public String GetPromottionAppliedCart(Guid authToken);
        public double GetTotalPriceCart(Guid authToken);
        List<CartProduct> GetCartProductsByCartID(int cartID);

    }
}
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.DataAccess.Repositories
{
    public class PurchaseProductManagment : IPurchaseProductManagment
    {
        private readonly IGenericRepository<Session> _sessionRepository;
        private readonly IGenericRepository<PurchaseProduct> _purchaseProductRepository;
        private readonly IProductManagment _productManagment;

        public PurchaseProductManagment(IGenericRepository<Session> sessionRepository, IGenericRepository<PurchaseProduct> purchaseProduct, 
                                IProductManagment productManagment)
        {
            _purchaseProductRepository = purchaseProduct;
            _sessionRepository = sessionRepository;
            _productManagment = productManagment;
        }
        public List<Product> GetProductsByPurchaseID(int purchaseID)
        {
            if (purchaseID <= 0)
            {
                throw new PurchaseProductException("ID de purchase inválido.");
            }

            var purchaseProducts = _purchaseProductRepository
                .GetAll<PurchaseProduct>()
                .Where(pp => pp.PurchaseID == purchaseID)
                .ToList();

            if (!purchaseProducts.Any())
            {
                throw new PurchaseProductException($"No existen productos asociados a la compra con el ID {purchaseID}.");
            }

            // Crear una lista para almacenar los productos asociados a la compra.
            List<Product> products = new List<Product>();

            foreach (var purchaseProduct in purchaseProducts)
            {
                // Obtener el producto por su ID desde el sistema de gestión de productos.
                Product product = _productManagment.GetProductByID(purchaseProduct.ProductID);

                if (product != null)
                {
                    products.Add(product); // Agregar el producto a la lista si se encuentra en la base de datos.
                }
            }

            return products;
        }

    }
}
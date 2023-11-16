using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.BusinessLogic
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductService(IGenericRepository<Product> productRepositoy)
        {
            _repository = productRepositoy;
        }
        public Product GetProductByID(int prodID)
        {
            if (prodID < 0)
            {
                throw new ProductManagmentException("Invalid product ID.");
            }
            else
            {
                Product? product = _repository.GetAll<Product>().FirstOrDefault(m => m.ProductID == prodID);

                if (product == null)
                {
                    throw new ProductManagmentException("No matching ID found.");
                }
                else
                {
                    return product;
                }
            }
        }

        public void RegisterProduct(Product product)
        {
            if (product == null || product.Price <= 0 || (product.Brand == null || product.Brand == "") || product.Name == string.Empty)
            {
                throw new ProductManagmentException("Uno de los datos es incorrecto");
            }
            else
            {
                _repository.Insert(product);
                _repository.Save();
            }
        }
        public IEnumerable<Product> GetProducts()
        {
            var result = _repository.GetAll<Product>();
            if (result == null)
            {
                return Enumerable.Empty<Product>();
            }
            else
            {
                return result;
            }
        }

        public void ProductSold(Product product)
        {
            product.Stock = product.Stock - 1;
            _repository.Update(product);
            _repository.Save();
        }
        public void DeleteProduct(int productID)
        {
            Product product = GetProductByID(productID);
            if (product == null)
            {
                throw new ProductManagmentException("Producto no encontrado");
            }
            else
            {
                _repository.Delete(product);
                _repository.Save();
            }
        }

        public Product UpdateProduct(int id, Product prod)
        {
            Product existingProduct = GetProductByID(id);

            if (existingProduct == null)
            {
                throw new UserException($"The product with ID {prod.ProductID} does not exist.");
            }

            existingProduct.Name = prod.Name;
            existingProduct.Description = prod.Description;
            existingProduct.Price = prod.Price;
            existingProduct.Brand = prod.Brand;
            existingProduct.Category = prod.Category;
            existingProduct.Color = prod.Color;
            existingProduct.Stock = prod.Stock;
            existingProduct.AvailableToPromotions = prod.AvailableToPromotions;

            _repository.Update(existingProduct);
            _repository.Save();
            return existingProduct;
        }

        public IEnumerable<Product> GetProductsRange(int min, int max)
        {
            IEnumerable<Product>? products = GetProducts();

            if (products == null)
            {
                throw new ProductManagmentException("Error al obtener la lista de productos.");
            }
            else
            {
                IEnumerable<Product> productosFiltrados = products.Where(producto =>
            producto.Price >= min &&
            producto.Price <= max);
                return productosFiltrados;

            }

        }

        public IEnumerable<Product> GetProductsInPromotions()
        {
            IEnumerable<Product>? prod = GetProducts();

            if (prod == null)
            {
                throw new ProductManagmentException("Error al obtener la lista de productos.");
            }
            else
            {
                IEnumerable<Product> productosFiltrados = prod.Where(producto =>
            producto.AvailableToPromotions == true);
                return productosFiltrados;

            }
        }
    }
}
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.DataAccess.Repositories
{
    public class ProductManagment: IProductManagment
    {
        private List<Product>? _products;
        private readonly IGenericRepository<Product> _repository;

        public ProductManagment(IGenericRepository<Product> productRepositoy)
        {
            _products = new List<Product>();
            _repository = productRepositoy;
        }

        public Product UpdateProduct(Product product)
        {
            Product existingProduct = _repository.GetAll<Product>().FirstOrDefault(m => m.ProductID == product.ProductID);

            if (existingProduct == null)
            {
                throw new UserException($"The product with ID {product.ProductID} does not exist.");
            }

            // Actualiza los campos del producto existente
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Brand = product.Brand;
            existingProduct.Category = product.Category;
            existingProduct.Color = product.Color;

            _repository.Update(existingProduct);
            _repository.Save();
            return existingProduct;
        }

        public Product CreateProduct(Product product)
        {
            if (_products?.Any(p => p.ProductID == product.ProductID) == true)
            {
                throw new ProductManagmentException($"The product with ID {product.ProductID} already exists.");
            }

            _products?.Add(product);

            return product;
        }

        public void RegisterProduct(Product product)
        {
            _repository.Insert(product);
            _repository.Save();
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

        public void DeleteProduct(int productID)
        {
            Product? prod = _repository.GetAll<Product>().FirstOrDefault(m => m.ProductID == productID);

            if (prod == null)
            {
                throw new ProductManagmentException($"Product with ID {productID} not found.");
            }
            else
            {
                _repository.Delete(prod);
                _repository.Save();
            }
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
    }
}
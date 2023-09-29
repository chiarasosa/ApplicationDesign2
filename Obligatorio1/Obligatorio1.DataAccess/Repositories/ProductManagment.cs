using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.DataAccess.Repositories
{
    public class ProductManagment
    {
        private List<Product>? _products;

        public ProductManagment()
        {
            _products = new List<Product>();
        }
        public Product UpdateProduct(Product product)
        {


            // Busca el producto por su ID
            Product existingProduct = _products?.FirstOrDefault(p => p.ProductID == product.ProductID);

            if (existingProduct == null)
            {
                throw new UserException($"El producto con ID {product.ProductID} no existe.");
            }

            // Actualiza los campos del producto existente
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Brand = product.Brand;
            existingProduct.Category = product.Category;
            existingProduct.Color = product.Color;

            return existingProduct;
        }

        public void CreateProduct(Product product)
        {
            if (_products?.Any(p => p.ProductID == product.ProductID) == true)
            {
                throw new ProductManagmentException($"El producto con ID {product.ProductID} ya existe.");
            }

            // Agrega el nuevo producto a la lista de productos
            _products?.Add(product);
        }


    }
}

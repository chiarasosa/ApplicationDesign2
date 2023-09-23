using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.DataAccess
{
    public class ProductManagment
    {
        private List<Product>? _products;
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
            existingProduct.Colors = product.Colors;

            return existingProduct;
        }
    }
}

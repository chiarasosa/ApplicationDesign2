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


            // Busca el producto por su ID
            Product existingProduct = _repository.GetAll<Product>().FirstOrDefault(m => m.ProductID == product.ProductID);

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


            _repository.Update(existingProduct);
            _repository.Save();
            return existingProduct;
        }


        public Product CreateProduct(Product product)
        {
            if (_products?.Any(p => p.ProductID == product.ProductID) == true)
            {
                throw new ProductManagmentException($"El producto con ID {product.ProductID} ya existe.");
            }

            // Agrega el nuevo producto a la lista de productos
            _products?.Add(product);

            return product;
        }

        public void RegisterProduct(Product product)
        {
            //_products?.Add(product);
            _repository.Insert(product);
            _repository.Save();
        }

        public IEnumerable<Product> GetProducts()
        {
            var aux = _repository.GetAll<Product>();
            if (aux == null)
            {
                return Enumerable.Empty<Product>();
            }
            else
            {
                return aux;
            }
        }

        public void DeleteProduct(int productID)
        {
            Product? prod = _repository.GetAll<Product>().FirstOrDefault(m => m.ProductID == productID);

            if (prod == null)
            {
                throw new ProductManagmentException($"Producto con ID {productID} no encontrado");
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
                throw new ProductManagmentException("ID del producto inválido");
            }
            else
            {
                Product? product = _repository.GetAll<Product>().FirstOrDefault(m => m.ProductID == prodID);

                if (product == null)
                {
                    throw new ProductManagmentException("No se encontró ningún ID que coincida");
                }
                else
                {
                    return product;
                }
            }
        }

        /* public List<Product> SearchByParameter(string text, string brand, string category)
         {

         }*/
    }
}
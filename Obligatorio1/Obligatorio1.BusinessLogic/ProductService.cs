﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;
using Obligatorio1.Domain;
using System.Runtime.InteropServices;
using Obligatorio1.Exceptions;


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
            if (product == null || product.Price <= 0 || product.Brand <= 0 || product.Name == string.Empty)
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

        public Product UpdateProduct(Product prod)
        {
            Product existingProduct = GetProductByID(prod.ProductID);

            if (existingProduct == null)
            {
                throw new UserException($"The product with ID {prod.ProductID} does not exist.");
            }

            // Actualiza los campos del producto existente
            existingProduct.Name = prod.Name;
            existingProduct.Description = prod.Description;
            existingProduct.Price = prod.Price;
            existingProduct.Brand = prod.Brand;
            existingProduct.Category = prod.Category;
            existingProduct.Color = prod.Color;

            _repository.Update(existingProduct);
            _repository.Save();
            return existingProduct;
        }
    }
}
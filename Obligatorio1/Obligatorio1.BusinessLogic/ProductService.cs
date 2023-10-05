using System;
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
        private readonly IProductManagment productsManagement;

        public ProductService(IProductManagment productsManagement)
        {
            this.productsManagement = productsManagement;
        }

        public Product GetProductByID(int prodID)
        {
            Product? prod = productsManagement.GetProductByID(prodID);

            if (prod == null)
            {
                throw new ProductManagmentException("Producto no encontrado");
            }
            return prod;
        }

        public void RegisterProduct(Product product)
        {
            if (product == null || product.Price <= 0 || product.Brand <= 0 || product.Name == string.Empty)
            {
                throw new ProductManagmentException("Uno de los datos es incorrecto");

            }
            else
            {
                productsManagement.RegisterProduct(product);
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            IEnumerable<Product>? prod = productsManagement.GetProducts();

            if (prod == null)
            {
                throw new ProductManagmentException("Error al obtener la lista de productos.");
            }

            return prod;

        }

        public void DeleteProduct(int productID)
        {
            Product product = productsManagement.GetProductByID(productID);
            if (product == null)
            {
                throw new ProductManagmentException("Producto no encontrado");

            }
            else
            {
                productsManagement.DeleteProduct(productID);
            }
        }

        public Product UpdateProduct(Product prod)
        {
            try
            {
                return productsManagement.UpdateProduct(prod);
            }
            catch (ProductManagmentException e)
            {
                throw new ProductManagmentException($"Error al actualizar el producto: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Error inesperado al actualizar el producto: {e.Message}", e);
            }
        }

        public Product CreateProduct(Product product)
        {
            
               Product prod= productsManagement.CreateProduct(product);

            if (prod==null)
            {
                throw new ProductManagmentException("Error al crear el producto.");
            }
            return prod;
            
        }

        public List<Product> SearchByParameter(string text, string brand, string category)
        {
            try
            {
                return productsManagement.SearchByParameter(text, brand, category);
            }
            catch (ProductManagmentException e)
            {
                throw new ProductManagmentException($"Error al buscar con los parametros indicados: {e.Message}");
            }
            catch(Exception e)
            {
                throw new Exception($"Error inesperado", e);
            }
        }
}


    }


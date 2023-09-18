using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;
using Obligatorio1.Domain;


namespace Obligatorio1.BusinessLogic
{
    public class ProductService: IProductService
    {
        private readonly IProductManagment productsManagement;

        public ProductService(IProductManagment productsManagement)
        {
            this.productsManagement = productsManagement;
        }

        public Product GetProductByID(int prodID)
        {
            Product? prod= productsManagement.GetProductByID(prodID);

            if (prod==null)
            {
                throw new Exception("Producto no encontrado");
            }
            return prod;
        }

        public void RegisterProduct(Product product)
        {
            if (product==null || product.Price <= 0 || product.Brand <=0 || product.Name==string.Empty)
            {
                throw new Exception("");

            }
            else
            {
                productsManagement.RegisterProduct(product);
            }
        }
    }
}

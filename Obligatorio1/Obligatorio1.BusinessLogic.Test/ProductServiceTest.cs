using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obligatorio1.BusinessLogic;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;


namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class ProductServiceTest
    {
        [TestMethod]
        public void DeleteProductTest() 
        {
            Mock<IProductManagment>? mock = new Mock<IProductManagment>(MockBehavior.Strict);
            ProductService service = new ProductService(mock.Object);
            
            int prodID = 0;
            Product prod = new Product(prodID, "jabon", 120, "sin descripcion", 1, 2, "azul");

            mock?.Setup(m => m.GetProductByID(prodID)).Returns(prod);
            mock?.Setup(m => m.DeleteProduct(prodID));

            service?.DeleteProduct(prodID);

            mock?.Verify(m => m.GetProductByID(prodID),Times.Once);
            mock?.Verify(m => m.DeleteProduct(prodID), Times.Once);
        }


         [TestMethod]
        public void CreateProductTest()
        {
            Mock<IProductManagment>? mock = new Mock<IProductManagment>(MockBehavior.Strict);
            ProductService service = new ProductService(mock.Object);
            
            int prodID = 0;
            Product prod = new Product(prodID, "jabon", 120, "sin descripcion", 1, 2, "azul");


            Action createProductAction = () => service?.CreateProduct(prod);

            Assert.ThrowsException<Exception>(createProductAction);

            mock?.Verify(m => m.CreateProduct(prod), Times.Once);
        }


        [TestMethod]
        public void GetProductByIdTest()
        {
            Mock<IProductManagment>? mock=new Mock<IProductManagment>(MockBehavior.Strict);
            ProductService service= new ProductService(mock.Object);
            
            int prodID = 0;
            Product prod = new Product(prodID,"jabon", 120, "sin descripcion", 1, 2, "azul");


            mock!.Setup(m => m.GetProductByID(prodID)).Returns(prod);
           
            Product? result=service?.GetProductByID(prodID);

            Assert.AreEqual(prod, result);

            mock.VerifyAll();
        }

        [TestMethod]
        public void RegisterProductTest()
        {
            Mock<IProductManagment>? mock = new Mock<IProductManagment>(MockBehavior.Strict);
            ProductService service = new ProductService(mock.Object);

            
            int prodID = 0;
            Product prod = new Product(prodID, "jabon", 120, "sin descripcion", 1, 2, "azul");

            mock?.Setup(x => x.RegisterProduct(prod));

             service?.RegisterProduct(prod);

            mock?.VerifyAll();
        }


        [TestMethod]
        public void GetProductsTest()
        {
            Mock<IProductManagment>? mock = new Mock<IProductManagment>(MockBehavior.Strict);
            ProductService service = new ProductService(mock.Object);

            

            List<Product> products = new List<Product>
            {
                new Product(1, "canarias amarilla", 200, "yerba mate", 2, 2, "azul"),
                new Product(2, "lapicera bic", 30, "util escolar", 3, 3, "azul"),
                new Product(3, "desodorante rexona", 250, " ", 4, 4, "azul")
            };

            mock?.Setup(x => x.GetProducts()).Returns(products);

            IEnumerable<Product>? result = service?.GetProducts();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(products, result.ToList());

            mock?.Verify(x => x.GetProducts(), Times.Once);
        }

        [TestMethod]
        public void UpdateProductTest()
        {
            Mock<IProductManagment>? mock = new Mock<IProductManagment>(MockBehavior.Strict);
            ProductService service = new ProductService(mock.Object);
            Product prod=new Product(2, "lapicera bic", 30, "util escolar", 3, 3, "azul");
            Product updated = new Product(prod.ProductID,"actualizado",31,"actualizado",4,4,"negro");

            mock?.Setup(x => x.UpdateProduct(It.IsAny<Product>())).Returns((Product product) => product);
            Product? result = service?.UpdateProduct(updated);

            Assert.IsNotNull(result);
            Assert.AreEqual(updated, result);
            mock?.Verify(x => x.UpdateProduct(updated),Times.Once);

        }

        [TestMethod]
        public void SearchByParameter()
        {
            Mock<IProductManagment>? mock = new Mock<IProductManagment>(MockBehavior.Strict);
            ProductService service = new ProductService(mock.Object);

            List<Product> listProducts = new List<Product>();


            Product prod=new Product(2, "lapicera bic", 30, "util escolar", 3, 3, "azul");
            Product prod2 = new Product(2, "lapiz bic", 20, "util escolar", 2, 2, "rojo");
            listProducts.Add(prod);
            listProducts.Add(prod2);

            mock.Setup(m => m.SearchByParameter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns<string, string, string>((textoBusqueda, marca, categoria) =>

             {
                 return listProducts
                 .Where(p =>
                        (string.IsNullOrWhiteSpace(textoBusqueda) || p.Name.Contains(textoBusqueda)) &&
                        (string.IsNullOrWhiteSpace(marca) || p.Brand.Equals(marca)) &&
                        (string.IsNullOrWhiteSpace(categoria) || p.Category.Equals(categoria))).Select(p => new Product
                        {
                            Name = p.Name,
                            Price = p.Price,
                            Category = p.Category,
                            Brand = p.Brand
                            
                        }).ToList();


             });

            var Result = mock.Object.SearchByParameter("lapiz bic", "30","3");
            //Assert.AreEqual(1,Result);
        }
    }
}

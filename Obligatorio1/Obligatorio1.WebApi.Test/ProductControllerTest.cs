using Microsoft.AspNetCore.Mvc;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;

namespace Obligatorio1.WebApi.Test
{
    [TestClass]
    public class ProductControllerTest
    {
        private ProductController _controller;
        private Mock<IProductService> _mock;

        [TestInitialize]
        public void Setup()
        {
            _mock = new Mock<IProductService>();
            _controller = new ProductController(_mock.Object);
        }

        [TestMethod]
        public void RegisterValidProduct()
        {
            var product = new Product("championes", 3000, "correr", "a", 2, "azul", 5, true);

            var result = _controller.RegisterProduct(product);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            _mock.Verify(m => m.RegisterProduct(product), Times.Once);
        }

        [TestMethod]
        public void RegisterInvalidProduct()
        {
            var product = new Product("championes", 3000, "correr", "a", 2, "azul", 5, true);
            _mock.Setup(m => m.RegisterProduct(product))
                .Throws(new ProductManagmentException("Producto inválido"));

            var result = _controller.RegisterProduct(product);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            _mock.Verify(m => m.RegisterProduct(product), Times.Once);
        }





        [TestMethod]
        public void GetProducts()
        {
            var products = new List<Product>
            {
                new Product("championes", 3000, "correr", "a", 2, "azul", 5, true),
                new Product("championes", 4000, "correr", "a", 2, "rojo", 5, true)
        };

            _mock.Setup(m => m.GetProducts()).Returns(products);

            var result = _controller.GetProducts();

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = (OkObjectResult)result;
            Assert.IsInstanceOfType(ok.Value, typeof(List<Product>));
            var res = (List<Product>)ok.Value;
            Assert.AreEqual(products.Count, res.Count);
        }



        [TestMethod]
        public void GetProductID()
        {
            int id = 1;
            var product = new Product("championes", 3000, "correr", "a", 2, "azul", 5, true);
            _mock.Setup(m => m.GetProductByID(id)).Returns(product);

            var result = _controller.GetProductByID(id);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = (OkObjectResult)result;
            Assert.AreEqual(product, ok.Value);
        }


    }
}

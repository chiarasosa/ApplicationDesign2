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
        private Mock<IProductService> mock;

        [TestInitialize]
        public void setup()
        {
            mock = new Mock<IProductService>();
            _controller = new ProductController(mock.Object);
        }

        [TestMethod]
        public void RegisterValidProduct()
        {
            var product = new Product(1, "championes", 3000, "correr", 4, 5, "azul");

            var result = _controller.RegisterProduct(product);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            mock.Verify(m => m.RegisterProduct(product), Times.Once);
        }

        [TestMethod]
        public void RegisterInvalidProduct()
        {
            var product = new Product(1, "", 0, "", 0, 0, "");
            mock.Setup(m => m.RegisterProduct(product)).Throws(new ProductManagmentException("PProducto inválido"));

            var result = _controller.RegisterProduct(product);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            mock.Verify(m => m.RegisterProduct(product), Times.Once);
        }

        [TestMethod]
        public void GetProducts()
        {
            var products = new List<Product>();

            Product aux1 = new Product(1, "teclado", 4000, "mecanico", 14, 14, "negro");
            Product aux2 = new Product(2, "mouse", 2000, "mecanico", 14, 14, "negro");
            products.Add(aux1);
            products.Add(aux2);

            mock.Setup(m => m.GetProducts()).Returns(products);

            var result = _controller.GetProducts();

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = (OkObjectResult)result;
            var res = ok.Value as List<Product>;
            Assert.IsNotNull(res);
            Assert.AreEqual(products.Count, res.Count);

        }

        [TestMethod]
        public void GetProductsError()
        {
            mock.Setup(m => m.GetProducts()).Throws(new Exception("Error al obtener productos"));

            var res = _controller.GetProducts();

            Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
            var badRequest = (BadRequestObjectResult)res;
            Assert.AreEqual("Error al obtener productos: Error al obtener productos", badRequest.Value);

        }

        [TestMethod]
        public void GetProductID()
        {
            int id = 1;
            var product = new Product(id, "lampara", 400, "electrica", 22, 10, "azul");
            mock.Setup(m => m.GetProductByID(id)).Returns(product);

            var res = _controller.GetProductByID(id);


            Assert.IsInstanceOfType(res, typeof(OkObjectResult));
            var ok = (OkObjectResult)res;
            Assert.AreEqual(product, ok.Value);
        }

        [TestMethod]
        public void GetProductIDNotFound()
        {
            int id = 1;

            mock.Setup(m => m.GetProductByID(id)).Returns((Product)null);

            var res = _controller.GetProductByID(id);

            Assert.IsInstanceOfType(res, typeof(NotFoundObjectResult));
            var notFound = (NotFoundObjectResult)res;
            Assert.AreEqual($"Producto con ID {id} no encontrado", notFound.Value);

        }

        [TestMethod]
        public void GetProductIDBadRequest()
        {
            int id = 1;
            mock.Setup(m => m.GetProductByID(id)).Throws(new Exception("Error al obtener el producto"));

            var result = _controller.GetProductByID(id);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badR = (BadRequestObjectResult)result;
            Assert.AreEqual($"Error al obtener el producto: Error al obtener el producto", badR.Value);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi.Controllers;

namespace Obligatorio1.WebApi.Test
{
    [TestClass]
    public class CartControllerTest
    {
        private CartController _controller;
        private Mock<ICartService> _cartServiceMock;

        [TestInitialize]
        public void Setup()
        {
            // Configurar el servicio mock
            this._cartServiceMock = new Mock<ICartService>();

            // Configurar el controlador con el servicio mock
            this._controller = new CartController(_cartServiceMock.Object);
        }

        [TestMethod]
        public void AddProductToCart_ValidProduct_ReturnsOkResult()
        {
            // Arrange
            var product = new Product();

            // Act
            var result = _controller.AddProductToCart(product) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Product added to cart successfully.", result.Value);
            _cartServiceMock.Verify(s => s.AddProductToCart(product), Times.Once);
        }

        [TestMethod]
        public void DeleteProductFromCart_ValidProduct_ReturnsOkResult()
        {
            // Arrange
            var product = new Product();

            // Act
            var result = _controller.DeleteProductFromCart(product) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Product deleted from cart successfully.", result.Value);
            _cartServiceMock.Verify(s => s.DeleteProductFromCart(product), Times.Once);
        }
    }

}
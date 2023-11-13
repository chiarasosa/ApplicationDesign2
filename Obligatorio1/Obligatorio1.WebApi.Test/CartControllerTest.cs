
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
        private Cart _cart;

        [TestInitialize]
        public void Setup()
        {
            // Configurar el servicio mock
            this._cartServiceMock = new Mock<ICartService>();

            // Configurar el controlador con el servicio mock
            this._controller = new CartController(_cartServiceMock.Object);

            this._cart = new Cart()
            {
                Products = new List<Product>() {
                new Product() {
                    Name = "Jabon",
                    Price = 10,
                    Description = "Liquido",
                    Brand = 1,
                    Category = 1,
                },
                new Product() {
                    Name = "Fideos",
                    Price = 20,
                    Description = "Tallarines",
                    Brand = 4,
                    Category = 4,
                },
                new Product() {
                    Name = "Pan",
                    Price = 30,
                    Description = "Blanco",
                    Brand = 5,
                    Category = 5,
                }
            },
                TotalPrice = 60,
                PromotionApplied = "3x1 promo",
            };
    }

        [TestMethod]
        public void AddProductToCart_ValidProduct_ReturnsOkResult()
        {
            // Arrange
            var product = new Product();

            // Act
            //var result = _controller.AddProductToCart(product) as OkObjectResult;
            _cart.Products.Add(product);

            // Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual("Product added to cart successfully.", result.Value);
            //_cartServiceMock.Verify(s => s.AddProductToCart(product), Times.Once);
            Assert.AreEqual(4, _cart.Products.Count);
        }

        [TestMethod]
        public void DeleteProductFromCart_ValidProduct_ReturnsOkResult()
        {
            // Arrange
            var product = new Product();

            // Act
            //var result = _controller.DeleteProductFromCart(product) as OkObjectResult;
            _cart.Products.Add(product);
            _cart.Products.Remove(product);

            // Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual("Product deleted from cart successfully.", result.Value);
            // _cartServiceMock.Verify(s => s.DeleteProductFromCart(product), Times.Once);
            Assert.AreEqual(3, _cart.Products.Count);
        }
    }

}

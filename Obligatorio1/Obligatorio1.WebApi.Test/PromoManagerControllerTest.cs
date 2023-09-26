using Microsoft.AspNetCore.Mvc;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi.Controllers;

namespace Obligatorio1.WebApi.Test
{
    [TestClass]
    public class PromoManagerControllerTest
    {
        private PromoManagerController _controller;
        private Mock<IPromoManagerService> _serviceMock;

        [TestInitialize]
        public void Setup()
        {
            // Configurar el servicio mock
            _serviceMock = new Mock<IPromoManagerService>();

            // Configurar el controlador con el servicio mock
            _controller = new PromoManagerController(_serviceMock.Object);
        }

        [TestMethod]
        public void ApplyBestPromoOk()
        {
            //Arrange
            Cart inputCart = new Cart();
            inputCart.Products.Add(new Product(1, "Jabon", 100, "Liquido", 3, 2, "green"));
            inputCart.Products.Add(new Product(2, "Jabon", 150, "Liquido", 3, 1, "red"));
            inputCart.Products.Add(new Product(3, "Jabon", 200, "Liquido", 3, 14, "red"));
            inputCart.Products.Add(new Product(4, "Jabon", 250, "Liquido", 4, 14, "green"));
            inputCart.Products.Add(new Product(5, "Jabon", 300, "Liquido", 5, 14, "red"));

            Cart expectedCart = new Cart();
            expectedCart.TotalPrice = 750;

            _serviceMock.Setup(s => s.ApplyBestPromotion(inputCart)).Returns(expectedCart);

            // Act
            var result = _controller.ApplyBestPromotion(inputCart);
            var parsedResult = (OkObjectResult)result;
            // Assert
            Assert.AreEqual(expectedCart, parsedResult.Value);
        }
    }
}

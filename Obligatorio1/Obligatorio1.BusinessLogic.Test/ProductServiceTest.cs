using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obligatorio1.BusinessLogic;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;
using Obligatorio1.WebApi;


namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class ProductServiceTest
    {
        [TestMethod]

        public void DeleteProduct_ReturnsBadRequestObjectResult_WhenExceptionIsThrown()
        {

            const int productId = 1;
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.DeleteProduct(productId)).Throws(new Exception("Some error message"));

            var controller = new ProductController(productServiceMock.Object);


            var result = controller.DeleteProduct(productId);


            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;


            Assert.IsInstanceOfType(badRequestResult.Value, typeof(object));


            var messageValue = badRequestResult.Value.GetType().GetProperty("message")?.GetValue(badRequestResult.Value);


            Assert.AreEqual("Error: Some error message", messageValue?.ToString());

            productServiceMock.Verify(x => x.DeleteProduct(productId), Times.Once);
        }

        [TestMethod]

        public void DeleteProduct_ReturnsOkObjectResult_WhenProductIsDeletedSuccessfully()
        {
            // Arrange
            const int productId = 1;
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.DeleteProduct(productId));

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = controller.DeleteProduct(productId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;

            // Asegúrate de que el valor sea un objeto anónimo
            Assert.IsInstanceOfType(okResult.Value, typeof(object));

            // Accede directamente a la propiedad "message" del objeto anónimo
            var messageValue = okResult.Value.GetType().GetProperty("message")?.GetValue(okResult.Value);

            // Verifica la propiedad "message"
            Assert.AreEqual("Product disposed correctly.", messageValue?.ToString());

            productServiceMock.Verify(x => x.DeleteProduct(productId), Times.Once);
        }
    }
}
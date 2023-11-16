using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi;
using System;

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
            const int productId = 1;
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.DeleteProduct(productId));

            var controller = new ProductController(productServiceMock.Object);

            var result = controller.DeleteProduct(productId);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;

            Assert.IsInstanceOfType(okResult.Value, typeof(object));

            var messageValue = okResult.Value.GetType().GetProperty("message")?.GetValue(okResult.Value);

            Assert.AreEqual("Product disposed correctly.", messageValue?.ToString());

            productServiceMock.Verify(x => x.DeleteProduct(productId), Times.Once);
        }
    }
}
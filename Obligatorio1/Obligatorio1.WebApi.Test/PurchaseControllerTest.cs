using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Obligatorio1.BusinessLogic;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;
using Obligatorio1.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.WebApi.Test
{
    [TestClass]
    public class PurchaseControllerTest
    {

        [TestMethod]
        public void GetUsersPurchases_ExistingUserId_ReturnsOkWithFormattedPurchases()
        {
            // Arrange
            var purchaseServiceMock = new Mock<IPurchaseService>();
            purchaseServiceMock.Setup(mock => mock.GetPurchasesByUserID(It.IsAny<int>())).Returns(new List<Purchase>
    {
        new Purchase { PurchaseID = 1, UserID = 1, UserName = "User1" },
        new Purchase { PurchaseID = 2, UserID = 1, UserName = "User1" }
    });
            var controller = new PurchaseController(purchaseServiceMock.Object);

            // Act
            var result = controller.GetUsersPurchases(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult?.Value);
            var formattedPurchases = JsonConvert.SerializeObject(okResult.Value);
            // Add assertions based on the expected formattedPurchases content
        }



        [TestMethod]
        public void GetProductsByPurchaseID_ValidID_ReturnsListOfProducts()
        {
            // Arrange
            var purchaseRepositoryMock = new Mock<IGenericRepository<Purchase>>();
            var sessionRepositoryMock = new Mock<IGenericRepository<Session>>();
            var purchaseProductRepositoryMock = new Mock<IGenericRepository<PurchaseProduct>>();
            var cartServiceMock = new Mock<ICartService>();
            var productServiceMock = new Mock<IProductService>();

            var purchaseService = new PurchaseService(
                purchaseRepositoryMock.Object,
                sessionRepositoryMock.Object,
                purchaseProductRepositoryMock.Object,
                cartServiceMock.Object,
                productServiceMock.Object);

            var purchaseID = 1;

            var purchaseProducts = new List<PurchaseProduct>
        {
            new PurchaseProduct { PurchaseID = purchaseID, ProductID = 101 },
            new PurchaseProduct { PurchaseID = purchaseID, ProductID = 102 }
            // Add more if needed
        };

            purchaseProductRepositoryMock.Setup(repo => repo.GetAll<PurchaseProduct>())
                .Returns(purchaseProducts.AsQueryable());

            productServiceMock.Setup(service => service.GetProductByID(It.IsAny<int>()))
                .Returns((int productID) => new Product { ProductID = productID, Name = $"Product {productID}" });

            // Act
            var result = purchaseService.GetProductsByPurchaseID(purchaseID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            // If you want to check individual product details, you can do that too
            Assert.AreEqual(101, result[0].ProductID);
            Assert.AreEqual("Product 101", result[0].Name);
            Assert.AreEqual(102, result[1].ProductID);
            Assert.AreEqual("Product 102", result[1].Name);
        }

        [TestMethod]
        public void GetSpecificPurchase_ExistingId_ReturnsOkWithFormattedPurchase()
        {
            // Arrange
            var purchaseServiceMock = new Mock<IPurchaseService>();
            purchaseServiceMock.Setup(mock => mock.GetPurchaseByID(It.IsAny<int>())).Returns(new Purchase
            {
                PurchaseID = 1,
                UserID = 1,
                UserName = "User1",
                PurchasedProducts = new List<PurchaseProduct> { new PurchaseProduct { ProductID = 1, Product = new Product { ProductID = 1, Name = "Product1" } } }
            });
            var controller = new PurchaseController(purchaseServiceMock.Object);

            // Act
            var result = controller.GetSpecificPurchase(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult?.Value);
            var formattedPurchase = JsonConvert.SerializeObject(okResult.Value);
            // Add assertions based on the expected formattedPurchase content
        }

        [TestMethod]
        public void GetPurchases_ReturnsOkWithFormattedPurchases()
        {
            // Arrange
            var purchaseServiceMock = new Mock<IPurchaseService>();
            purchaseServiceMock.Setup(mock => mock.GetAllPurchases()).Returns(new List<Purchase>
    {
        new Purchase { PurchaseID = 1, UserID = 1, UserName = "User1" },
        new Purchase { PurchaseID = 2, UserID = 1, UserName = "User1" }
    });
            var controller = new PurchaseController(purchaseServiceMock.Object);

            // Act
            var result = controller.GetPurchases();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult?.Value);
            var formattedPurchases = JsonConvert.SerializeObject(okResult.Value);
            // Add assertions based on the expected formattedPurchases content
        }
    }
}
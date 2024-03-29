﻿
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class PurchaseServiceTest
    {
        /*
        private Mock<IPurchaseManagment>? _purchaseManagmentMock;
        private PurchaseService? _purchaseService;

        [TestInitialize]
        public void Initialize()
        {
            _purchaseManagmentMock = new Mock<IPurchaseManagment>(MockBehavior.Strict);
            _purchaseService = new PurchaseService(_purchaseManagmentMock.Object);
        }

        [TestMethod]
        public void CartHasMoreThan1Item()
        {
            //Arrange
            List<Product> list = new List<Product>();
            list.Add(new Product(1,"Jabon", 10, "Liquido", 12, 3, "red"));
            list.Add(new Product(2,"Jabon2", 12, "Liquido", 12, 3, "black"));

            Purchase purchase = new Purchase();
            purchase.PurchasedProducts = list;

            // Act
            _purchaseManagmentMock?.Setup(x => x.ValidateMoreThan1Item(purchase)).Returns(2);
            _purchaseService?.ValidateMoreThan1Item(purchase);

            // Assert
            _purchaseManagmentMock?.VerifyAll();
        }

        [TestMethod]
        public void CartHasLessThan1Item()
        {
            // Arrange
            List<Product> list = new List<Product>();
            Purchase purchase = new Purchase();
            purchase.PurchasedProducts = list;

            // Act and Assert
            _purchaseManagmentMock?.Setup(x => x.ValidateMoreThan1Item(purchase)).Returns(0);

            // Utiliza Assert.ThrowsException para capturar la excepción
            Assert.ThrowsException<Obligatorio1.Exceptions.ExceptionPurchase>(() => _purchaseService?.ValidateMoreThan1Item(purchase));

            // Verifica que el método en el mock se llamó una vez
            _purchaseManagmentMock?.Verify(x => x.ValidateMoreThan1Item(purchase), Times.Once);
        }

        [TestMethod]
        public void CartHas1Item()
        {
            //Arrange
            List<Product> list = new List<Product>();
            list.Add(new Product(1,"Jabon", 10, "Liquido", 12, 3, "red"));

            Purchase purchase = new Purchase();
            purchase.PurchasedProducts = list;

            // Act
            _purchaseManagmentMock?.Setup(x => x.ValidateMoreThan1Item(purchase)).Returns(1);
            _purchaseService?.ValidateMoreThan1Item(purchase);

            // Assert
            _purchaseManagmentMock?.VerifyAll();
        }
        */
    }
}

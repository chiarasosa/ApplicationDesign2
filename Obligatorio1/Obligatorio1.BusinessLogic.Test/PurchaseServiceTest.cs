using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obligatorio1.BusinessLogic;
using Obligatorio1.Domain;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using Obligatorio1.IDataAccess;
using Obligatorio1.BusinessLogic;
using Obligatorio1.IBusinessLogic;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class PurchaseServiceTest
    {

        [TestMethod]
        public void CartHasMoreThan1Item()
        {
            //Arrange
            Mock<IPurchaseManagment>? mock = new Mock<IPurchaseManagment>(MockBehavior.Strict);
            CharacterService? service2 = new CharacterService(mock.Object);
            character = new Character()
            {
                Id = 1,
                Description = "Test",
                Name = "Darth Nico",
                ImageUrl = ""
            };

            //Act

            //Assert
        }

        [TestMethod]
        public void CartHas1Item()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void CartHasLessThan1Item()
        {
            //Arrange

            //Act

            //Assert
        }

        /*
        [TestMethod]
        public void ValidateMoreThan1Item()
        {
            //Arrange
            PurchaseService purchaseLogic = new PurchaseService();
            List<Product> cart = new List<Product>
            {
                new Product("Product1", 10, "Description", 123, 123, 123),
                new Product("Product2", 10, "Description", 123, 123, 123)
            };

            //Act
            purchaseLogic.ValidateMoreThan1Item(cart);
        }

        [TestMethod]
        [ExpectedException(typeof(Obligatorio1.Exceptions.ExceptionPurchase))]
        public void ValidateLessThan1Item()
        {
            //Arrange
            PurchaseService purchaseLogic = new PurchaseService();
            List<Product> cart = new List<Product>();

            //Act
            purchaseLogic.ValidateMoreThan1Item(cart);
        }

        [TestMethod]
        public void Validate1Item()
        {
            //Arrange
            PurchaseService purchaseLogic = new PurchaseService();
            List<Product> cart = new List<Product>
            {
                new Product("Product1", 10, "Description", 123, 123, 123),
            };

            //Act
            purchaseLogic.ValidateMoreThan1Item(cart);
        }
        */
    }
}
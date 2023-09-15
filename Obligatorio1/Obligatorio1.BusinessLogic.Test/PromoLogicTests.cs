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
    public class PromoLogicTests
    {
        private Mock<IPromoManagment>? _promoManagmentMock;
        private ThreeForOnePromoLogic? _3x1PromoLogic;

        [TestInitialize]
        public void Initialize()
        {
            _promoManagmentMock = new Mock<IPromoManagment>(MockBehavior.Strict);
            _3x1PromoLogic = new ThreeForOnePromoLogic(_promoManagmentMock.Object);
        }

        [TestMethod]
        public void CartHas3Items()
        {
            //Arrange
            Cart cart = new Cart();

            cart.Products.Add(new Product("Jabon", 10, "Liquido", 12, 3, 225));
            cart.Products.Add(new Product("Jabon2", 12, "Liquido", 12, 3, 225));
            cart.Products.Add(new Product("Jabon3", 12, "Liquido", 12, 3, 225));

            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();
            //Act
            bool result = threeForOnePromoLogic.CartHas3OrMoreItems(cart);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CartHasMoreThan3Items()
        {
            //Arrange
            Cart cart = new Cart();

            cart.Products.Add(new Product("Jabon", 10, "Liquido", 12, 3, 225));
            cart.Products.Add(new Product("Jabon2", 12, "Liquido", 12, 3, 225));
            cart.Products.Add(new Product("Jabon3", 12, "Liquido", 12, 3, 225));
            cart.Products.Add(new Product("Jabon4", 12, "Liquido", 12, 3, 225));

            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();
            //Act
            bool result = threeForOnePromoLogic.CartHas3OrMoreItems(cart);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CartHasLessThan3Items()
        {
            //Arrange
            Cart cart = new Cart();

            cart.Products.Add(new Product("Jabon", 10, "Liquido", 12, 3, 225));
            cart.Products.Add(new Product("Jabon2", 12, "Liquido", 12, 3, 225));

            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();
            //Act
            bool result = threeForOnePromoLogic.CartHas3OrMoreItems(cart);
            //Assert
            Assert.IsFalse(result);
        }
        /*
        [TestMethod]
        public void CartHas3OrMoreItems()
        {
            //Arrange
            Cart cart = new Cart();

            cart.Products.Add(new Product("Jabon", 10, "Liquido", 12, 3, 225));
            cart.Products.Add(new Product("Jabon2", 12, "Liquido", 12, 3, 225));
            cart.Products.Add(new Product("Jabon3", 12, "Liquido", 12, 3, 225));

            // Act
            _promoManagmentMock?.Setup(x => x.CartHas3OrMoreItems(cart)).Returns(true);
            _3x1PromoLogic?.CartHas3OrMoreItems(cart);

            // Assert
            _promoManagmentMock?.VerifyAll();
        }
        */
    }
}
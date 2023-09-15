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
        public void CalculateNewPriceWithDiscount_NoDiscountApplied()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Brand = 1, Price = 10 },
                new Product { Brand = 2, Price = 15 },
                new Product { Brand = 3, Price = 20 }

            };
            cart.TotalPrice = 45;
            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();
            // Act
            double newPrice = threeForOnePromoLogic.CalculateNewPriceWithDiscount(cart);

            // Assert
            Assert.AreEqual(45, newPrice); 
        }

        [TestMethod]
        public void CalculateNewPriceWithDiscount_DiscountApplied()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Brand = 1, Price = 10 },
                new Product { Brand = 1, Price = 5 },
                new Product { Brand = 1, Price = 7 },
                new Product { Brand = 2, Price = 15 },
                new Product { Brand = 3, Price = 20 }
            };
            cart.TotalPrice = 45;
            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();

            // Act
            double newPrice = threeForOnePromoLogic.CalculateNewPriceWithDiscount(cart);

            // Assert
            Assert.AreEqual(45, newPrice - 5 - 7); 
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

        [TestMethod]
        public void GroupProductsByBrand_GroupsProductsByBrandCorrectly()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Brand = 1, Price = 10 },
                new Product { Brand = 2, Price = 15 },
                new Product { Brand = 1, Price = 7 },
                new Product { Brand = 2, Price = 20 },
                new Product { Brand = 1, Price = 5 }
            };
            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();
            // Act
            Dictionary<int, List<Product>> productsByBrand = threeForOnePromoLogic.GroupProductsByBrand(cart);

            // Assert
            Assert.AreEqual(2, productsByBrand.Count); // Deben haber 3 marcas diferentes
            Assert.IsTrue(productsByBrand.ContainsKey(1));
            Assert.IsTrue(productsByBrand.ContainsKey(2));
            Assert.AreEqual(3, productsByBrand[1].Count); // Deben haber 3 productos de Brand1
            Assert.AreEqual(2, productsByBrand[2].Count); // Deben haber 2 productos de Brand2
        }

        [TestMethod]
        public void GroupProductsByBrand_TestFailure()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Brand = 1, Price = 10 },
                new Product { Brand = 2, Price = 15 },
                new Product { Brand = 1, Price = 7 },
                new Product { Brand = 2, Price = 20 },
                new Product { Brand = 1, Price = 5 }
            };
            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();
            // Act
            var productsByBrand = threeForOnePromoLogic.GroupProductsByBrand(cart);

            // Assert
            Assert.IsTrue(productsByBrand.ContainsKey(1));
            Assert.AreNotEqual(4, productsByBrand[1].Count); // Aquí afirmamos incorrectamente que hay 4 productos de la marca 1.
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
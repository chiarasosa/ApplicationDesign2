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
    public class ThreeForOnePromoTest
    {
        [TestMethod]
        public void CalculateNewPriceWithDiscount_EmptyCart()
        {
            // Arrange
            Cart cart = new Cart();
            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();
            // Act
            double newPrice = threeForOnePromoLogic.CalculateNewPriceWithDiscount(cart);
            // Assert
            Assert.AreEqual(0, newPrice);
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
            cart.TotalPrice = 57;
            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();

            // Act
            double newPrice = threeForOnePromoLogic.CalculateNewPriceWithDiscount(cart);

            // Assert
            Assert.AreEqual(45, newPrice); 
        }

        [TestMethod]
        public void CartHasNoItems()
        {
            // Arrange
            Cart cart = new Cart();

            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();
            // Act
            bool result = threeForOnePromoLogic.CartHas3OrMoreItems(cart);
            // Assert
            Assert.IsFalse(result);
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
        public void GroupProductsByBrand_Correct()
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
            Assert.AreEqual(2, productsByBrand.Count); 
            Assert.IsTrue(productsByBrand.ContainsKey(1));
            Assert.IsTrue(productsByBrand.ContainsKey(2));
            Assert.AreEqual(3, productsByBrand[1].Count); 
            Assert.AreEqual(2, productsByBrand[2].Count); 
        }

        [TestMethod]
        public void GroupProductsByBrand_Wrong()
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
            Assert.AreNotEqual(4, productsByBrand[1].Count); 
        }

        [TestMethod]
        public void FindBrandWithDiscount_Correct()
        {
            // Arrange
            Dictionary<int, List<Product>> productsByBrand = new Dictionary<int, List<Product>>
            {
                { 1, new List<Product> { new Product(), new Product(), new Product() } },
                { 2, new List<Product> { new Product(), new Product() } },
                { 3, new List<Product> { new Product(), new Product() } }
            };
            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();
            // Act
            int brandWithDiscount = threeForOnePromoLogic.FindBrandWithDiscount(productsByBrand);

            // Assert
            Assert.AreEqual(1, brandWithDiscount);
        }

        [TestMethod]
        public void FindBrandWithDiscount_NoBrand()
        {
            // Arrange
            Dictionary<int, List<Product>> productsByBrand = new Dictionary<int, List<Product>>
            {
                { 1, new List<Product> { new Product(), new Product() } },
                { 2, new List<Product> { new Product(), new Product() } },
                { 3, new List<Product> { new Product(), new Product() } }
            };
            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();
            // Act
            int brandWithDiscount = threeForOnePromoLogic.FindBrandWithDiscount(productsByBrand);

            // Assert
            Assert.AreEqual(0, brandWithDiscount); 
        }

        [TestMethod]
        public void ApplyDiscountToCart_SubtractsDiscountFromTotalPrice()
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
            cart.TotalPrice = 49; 

            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();

            // Act
            threeForOnePromoLogic.ApplyDiscountToCart(cart, new List<Product>
            {
                new Product { Brand = 1, Price = 5 },
                new Product { Brand = 1, Price = 7 }
            });

            // Assert
            Assert.AreEqual(37, cart.TotalPrice); 
        }

        [TestMethod]
        public void CalculateNewPriceWithDiscount_TwoBrandsWithPossibleDiscount()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Brand = 1, Price = 10 },
                new Product { Brand = 1, Price = 5 },
                new Product { Brand = 1, Price = 7 },
                new Product { Brand = 2, Price = 15 },
                new Product { Brand = 2, Price = 6 }, 
                new Product { Brand = 2, Price = 7 }
            };
            cart.TotalPrice = 50; 

            ThreeForOnePromoLogic threeForOnePromoLogic = new ThreeForOnePromoLogic();

            // Act
            double newPrice = threeForOnePromoLogic.CalculateNewPriceWithDiscount(cart);

            // Assert
            Assert.AreEqual(37, newPrice); 
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
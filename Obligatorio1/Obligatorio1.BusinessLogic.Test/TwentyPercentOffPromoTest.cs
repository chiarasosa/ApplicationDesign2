using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class TwentyPercentOffPromoTest
    {
        [TestMethod]
        public void CalculateNewPriceWithDiscount_EmptyCart()
        {
            // Arrange
            Cart cart = new Cart();
            TwentyPercentOffPromoLogic twentyPercentOffPromoLogic = new TwentyPercentOffPromoLogic();
            // Act
            double newPrice = twentyPercentOffPromoLogic.CalculateNewPriceWithDiscount(cart);
            // Assert
            Assert.AreEqual(0, newPrice);
        }


        [TestMethod]
        public void CalculateNewPriceWithDiscount_1Item()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Brand = 1, Price = 10 },
            };
            cart.TotalPrice = 10;
            TwentyPercentOffPromoLogic twentyPercentOffPromoLogic = new TwentyPercentOffPromoLogic();
            // Act
            double newPrice = twentyPercentOffPromoLogic.CalculateNewPriceWithDiscount(cart);

            // Assert
            Assert.AreEqual(10, newPrice);
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
            TwentyPercentOffPromoLogic twentyPercentOffPromoLogic = new TwentyPercentOffPromoLogic();

            // Act
            double newPrice = twentyPercentOffPromoLogic.CalculateNewPriceWithDiscount(cart);

            // Assert
            Assert.AreEqual(53, newPrice);
        }

        [TestMethod]
        public void CartHasNoItems()
        {
            // Arrange
            Cart cart = new Cart();

            TwentyPercentOffPromoLogic twentyPercentOffPromoLogic = new TwentyPercentOffPromoLogic();
            // Act
            bool result = twentyPercentOffPromoLogic.CartHas2OrMoreItems(cart);
            // Assert
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void CartHas1Items()
        {
            //Arrange
            Cart cart = new Cart();

            cart.Products.Add(new Product(1,"Jabon", 10, "Liquido", 12, 3, "red"));

            TwentyPercentOffPromoLogic twentyPercentOffPromoLogic = new TwentyPercentOffPromoLogic();
            //Act
            bool result = twentyPercentOffPromoLogic.CartHas2OrMoreItems(cart);
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CartHas2Items()
        {
            //Arrange
            Cart cart = new Cart();

            cart.Products.Add(new Product(1,"Jabon", 10, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(2,"Jabon2", 12, "Liquido", 12, 3, "red"));

            TwentyPercentOffPromoLogic twentyPercentOffPromoLogic = new TwentyPercentOffPromoLogic();
            //Act
            bool result = twentyPercentOffPromoLogic.CartHas2OrMoreItems(cart);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CartHasMoreThan2Items()
        {
            //Arrange
            Cart cart = new Cart();

            cart.Products.Add(new Product(1,"Jabon", 10, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(2,"Jabon2", 12, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(3,"Jabon3", 12, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(4,"Jabon4", 12, "Liquido", 12, 3, "red"));

            TwentyPercentOffPromoLogic twentyPercentOffPromoLogic = new TwentyPercentOffPromoLogic();
            //Act
            bool result = twentyPercentOffPromoLogic.CartHas2OrMoreItems(cart);
            //Assert
            Assert.IsTrue(result);
        }
    }
}
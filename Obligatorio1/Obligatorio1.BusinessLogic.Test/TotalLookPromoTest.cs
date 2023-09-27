﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obligatorio1.BusinessLogic;
using Obligatorio1.Domain;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using Obligatorio1.IDataAccess;
using Obligatorio1.IBusinessLogic;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class TotalLookPromoTest
    {
        [TestMethod]
        public void CalculateNewPriceWithDiscount_EmptyCart()
        {
            // Arrange
            Cart cart = new Cart();
            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();
            // Act
            double newPrice = totalLookPromoLogic.CalculateNewPriceWithDiscount(cart);
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
                new Product { Color="red", Price = 10 },
                new Product { Color="blue", Price = 15 },
                new Product { Color="yellow", Price = 20 }

            };
            cart.TotalPrice = 45;
            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();
            // Act
            double newPrice = totalLookPromoLogic.CalculateNewPriceWithDiscount(cart);

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
                new Product { Color="red", Price = 10 },
                new Product { Color="red", Price = 5 },
                new Product { Color="red", Price = 7 },
                new Product { Color="blue", Price = 15 },
                new Product { Color="yellow", Price = 20 }
            };
            cart.TotalPrice = 57;
            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();

            // Act
            double newPrice = totalLookPromoLogic.CalculateNewPriceWithDiscount(cart);

            // Assert
            Assert.AreEqual(52, newPrice);
        }

        [TestMethod]
        public void CartHasNoItems()
        {
            // Arrange
            Cart cart = new Cart();

            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();
            // Act
            bool result = totalLookPromoLogic.CartHas3OrMoreItems(cart);
            // Assert
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void CartHas3Items()
        {
            //Arrange
            Cart cart = new Cart();

            cart.Products.Add(new Product(1,"Jabon", 10, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(2,"Jabon2", 12, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(3,"Jabon3", 12, "Liquido", 12, 3, "red"));

            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();
            //Act
            bool result = totalLookPromoLogic.CartHas3OrMoreItems(cart);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CartHasMoreThan3Items()
        {
            //Arrange
            Cart cart = new Cart();

            cart.Products.Add(new Product(1,"Jabon", 10, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(2,"Jabon2", 12, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(3,"Jabon3", 12, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(4,"Jabon4", 12, "Liquido", 12, 3, "red"));

            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();
            //Act
            bool result = totalLookPromoLogic.CartHas3OrMoreItems(cart);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CartHasLessThan3Items()
        {
            //Arrange
            Cart cart = new Cart();

            cart.Products.Add(new Product(1,"Jabon", 10, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(2,"Jabon2", 12, "Liquido", 12, 3, "red"));

            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();
            //Act
            bool result = totalLookPromoLogic.CartHas3OrMoreItems(cart);
            //Assert
            Assert.IsFalse(result);
        }
        /*
        [TestMethod]
        public void GroupProductsByColor_Correct()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product{ Color="red", Price = 10 },
                new Product { Color="blue", Price = 15 },
                new Product { Color="red", Price = 7 },
                new Product { Color="blue", Price = 20 },
                new Product { Color="red", Price = 5 }
            };
            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();
            // Act
            Dictionary<string, List<Product>> productsByColor = totalLookPromoLogic.GroupProductsByColor(cart);

            // Assert
            Assert.AreEqual(2, productsByColor.Count);
            Assert.IsTrue(productsByColor.ContainsKey("red"));
            Assert.IsTrue(productsByColor.ContainsKey("blue"));
            Assert.AreEqual(3, productsByColor["red"].Count);
            Assert.AreEqual(2, productsByColor["blue"].Count);
        }
        */
        [TestMethod]
        public void GroupProductsByColor_Wrong()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Color="red", Price = 10 },
                new Product { Color="blue", Price = 15 },
                new Product { Color="red", Price = 7 },
                new Product { Color="blue", Price = 20 },
                new Product { Color="red", Price = 5 }
            };
            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();
            // Act
            var productsByColor = totalLookPromoLogic.GroupProductsByColor(cart);

            // Assert
            Assert.IsTrue(productsByColor.ContainsKey("red"));
            Assert.AreNotEqual(4, productsByColor["red"].Count);
        }

        [TestMethod]
        public void FindColorWithDiscount_Correct()
        {
            // Arrange
            Dictionary<string, List<Product>> productsByColor = new Dictionary<string, List<Product>>
            {
                { "red", new List<Product> { new Product { Price = 1 }, new Product { Price = 16 }, new Product { Price = 155 } } },
                { "green", new List<Product> { new Product { Price = 11 }, new Product { Price = 14 } } },
                { "blue", new List<Product> { new Product { Price = 12 }, new Product { Price = 11 } } }
            };
            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();
            // Act
            string colorWithDiscount = totalLookPromoLogic.FindColorWithMaxDiscount(productsByColor);

            // Assert
            Assert.AreEqual("red", colorWithDiscount);
        }

        [TestMethod]
        public void FindColorWithMaxDiscount_Correct()
        {
            // Arrange
            Dictionary<string, List<Product>> productsByColor = new Dictionary<string, List<Product>>
            {
                { "red", new List<Product> { new Product { Price = 10 }, new Product { Price = 5 }, new Product { Price = 7 } } },
                { "green", new List<Product> { new Product { Price = 15 }, new Product { Price = 6 }, new Product { Price = 7 } } },
                { "blue", new List<Product> { new Product { Price = 12 }, new Product { Price = 11 } } }
            };
            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();
            // Act
            string colorWithDiscount = totalLookPromoLogic.FindColorWithMaxDiscount(productsByColor);

            // Assert
            Assert.AreEqual("green", colorWithDiscount);
        }

        [TestMethod]
        public void FindColorWithDiscount_NoColor()
        {
            // Arrange
            Dictionary<string, List<Product>> productsByColor = new Dictionary<string, List<Product>>
            {
                { "red", new List<Product> { new Product { Price = 16 }, new Product { Price = 132 } } },
                { "green", new List<Product> { new Product { Price = 15 }, new Product { Price = 12 } } },
                { "blue", new List<Product> { new Product { Price = 15 }, new Product { Price = 15 } } }
            };
            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();
            // Act
            string colorWithDiscount = totalLookPromoLogic.FindColorWithMaxDiscount(productsByColor);

            // Assert
            Assert.AreEqual(null, colorWithDiscount);
        }

        [TestMethod]
        public void ApplyDiscountToCart_SubtractsDiscountFromTotalPrice()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Color="blue", Price = 10 },
                new Product { Color="blue", Price = 5 },
                new Product { Color="blue", Price = 7 },
                new Product { Color="red", Price = 15 },
                new Product { Color="white", Price = 20 }
            };
            cart.TotalPrice = 49;

            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();

            // Act
            totalLookPromoLogic.ApplyDiscountToCart(cart, new List<Product>
            {
                new Product { Color="blue", Price = 5 },
                new Product { Color="blue", Price = 7 },
                new Product { Color="blue", Price = 10 }
            });

            // Assert
            Assert.AreEqual(44, cart.TotalPrice);
        }

        [TestMethod]
        public void CalculateNewPriceWithDiscount_TwoColorsWithPossibleDiscount()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Color="red", Price = 25 },
                new Product { Color="red", Price = 15 },
                new Product { Color="red", Price = 10 },
                new Product { Color="blue", Price = 50 },
                new Product { Color="blue", Price = 30 },
                new Product { Color="blue", Price = 20 }
            };
            cart.TotalPrice = 150;

            TotalLookPromoService totalLookPromoLogic = new TotalLookPromoService();

            // Act
            double newPrice = totalLookPromoLogic.CalculateNewPriceWithDiscount(cart);

            // Assert
            Assert.AreEqual(125, newPrice);
        }
    }
}
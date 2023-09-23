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
            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();
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
                new Product { Colors = new List<string> { "red", "green"}, Price = 10 },
                new Product { Colors = new List<string> { "blue", "black"}, Price = 15 },
                new Product { Colors = new List<string> { "yellow", "white"}, Price = 20 }

            };
            cart.TotalPrice = 45;
            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();
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
                new Product { Colors = new List<string> { "red", "green"}, Price = 10 },
                new Product { Colors = new List<string> { "red", "green"}, Price = 5 },
                new Product { Colors = new List<string> { "red", "green"}, Price = 7 },
                new Product { Colors = new List<string> { "black", "blue"}, Price = 15 },
                new Product { Colors = new List<string> { "yellow", "white"}, Price = 20 }
            };
            cart.TotalPrice = 57;
            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();

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

            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();
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

            cart.Products.Add(new Product(1,"Jabon", 10, "Liquido", 12, 3, new List<string> { "red", "green" }));
            cart.Products.Add(new Product(2,"Jabon2", 12, "Liquido", 12, 3, new List<string> { "red", "green" }));
            cart.Products.Add(new Product(3,"Jabon3", 12, "Liquido", 12, 3, new List<string> { "red", "green" }));

            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();
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

            cart.Products.Add(new Product(1,"Jabon", 10, "Liquido", 12, 3, new List<string> { "red", "green" }));
            cart.Products.Add(new Product(2,"Jabon2", 12, "Liquido", 12, 3, new List<string> { "red", "green" }));
            cart.Products.Add(new Product(3,"Jabon3", 12, "Liquido", 12, 3, new List<string> { "red", "green" }));
            cart.Products.Add(new Product(4,"Jabon4", 12, "Liquido", 12, 3, new List<string> { "red", "green" }));

            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();
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

            cart.Products.Add(new Product(1,"Jabon", 10, "Liquido", 12, 3, new List<string> { "red", "green" }));
            cart.Products.Add(new Product(2,"Jabon2", 12, "Liquido", 12, 3, new List<string> { "red", "green" }));

            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();
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
                new Product{ Colors = new List<string> { "red", "green"}, Price = 10 },
                new Product { Colors = new List<string> { "black", "blue"}, Price = 15 },
                new Product { Colors = new List<string> { "red", "green"}, Price = 7 },
                new Product { Colors = new List<string> { "black", "blue"}, Price = 20 },
                new Product { Colors = new List<string> { "red", "green"}, Price = 5 }
            };
            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();
            // Act
            Dictionary<string, List<Product>> productsByColor = totalLookPromoLogic.GroupProductsByColor(cart);

            // Assert
            Assert.AreEqual(2, productsByColor.Count);
            Assert.IsTrue(productsByColor.ContainsKey("red"));
            Assert.IsTrue(productsByColor.ContainsKey("black"));
            Assert.AreEqual(3, productsByColor["red"].Count);
            Assert.AreEqual(2, productsByColor["black"].Count);
        }
        */
        [TestMethod]
        public void GroupProductsByColor_Wrong()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Colors = new List<string> { "red", "green"}, Price = 10 },
                new Product { Colors = new List<string> { "black", "blue"}, Price = 15 },
                new Product { Colors = new List<string> { "red", "green"}, Price = 7 },
                new Product { Colors = new List<string> { "black", "blue"}, Price = 20 },
                new Product { Colors = new List<string> { "red", "green"}, Price = 5 }
            };
            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();
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
            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();
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
            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();
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
            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();
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
                new Product { Colors = new List<string> { "blue", "black"}, Price = 10 },
                new Product { Colors = new List<string> { "blue", "black"}, Price = 5 },
                new Product { Colors = new List<string> { "blue", "black"}, Price = 7 },
                new Product { Colors = new List<string> { "red", "green"}, Price = 15 },
                new Product { Colors = new List<string> { "white", "black"}, Price = 20 }
            };
            cart.TotalPrice = 49;

            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();

            // Act
            totalLookPromoLogic.ApplyDiscountToCart(cart, new List<Product>
            {
                new Product { Colors = new List<string> { "blue", "black"}, Price = 5 },
                new Product { Colors = new List<string> { "blue", "black"}, Price = 7 },
                new Product { Colors = new List<string> { "blue", "black"}, Price = 10 }
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
                new Product { Colors = new List<string> { "red", "green"}, Price = 25 },
                new Product { Colors = new List<string> { "red", "green"}, Price = 15 },
                new Product { Colors = new List<string> { "red", "green"}, Price = 10 },
                new Product { Colors = new List<string> { "blue", "black"}, Price = 50 },
                new Product { Colors = new List<string> { "blue", "black"}, Price = 30 },
                new Product { Colors = new List<string> { "blue", "black"}, Price = 20 }
            };
            cart.TotalPrice = 150;

            TotalLookPromoLogic totalLookPromoLogic = new TotalLookPromoLogic();

            // Act
            double newPrice = totalLookPromoLogic.CalculateNewPriceWithDiscount(cart);

            // Assert
            Assert.AreEqual(125, newPrice);
        }
    }
}
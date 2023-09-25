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
    public class ThreeForTwoPromoTest
    {
        [TestMethod]
        public void CalculateNewPriceWithDiscount_EmptyCart()
        {
            // Arrange
            Cart cart = new Cart();
            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();
            // Act
            double newPrice = threeForTwoPromoLogic.CalculateNewPriceWithDiscount(cart);
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
                new Product { Category = 1, Price = 10 },
                new Product { Category = 2, Price = 15 },
                new Product { Category = 3, Price = 20 }

            };
            cart.TotalPrice = 45;
            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();
            // Act
            double newPrice = threeForTwoPromoLogic.CalculateNewPriceWithDiscount(cart);

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
                new Product { Category = 1, Price = 10 },
                new Product { Category = 1, Price = 5 },
                new Product { Category = 1, Price = 7 },
                new Product { Category = 2, Price = 15 },
                new Product { Category = 3, Price = 20 }
            };
            cart.TotalPrice = 57;
            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();

            // Act
            double newPrice = threeForTwoPromoLogic.CalculateNewPriceWithDiscount(cart);

            // Assert
            Assert.AreEqual(52, newPrice);
        }

        [TestMethod]
        public void CartHasNoItems()
        {
            // Arrange
            Cart cart = new Cart();

            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();
            // Act
            bool result = threeForTwoPromoLogic.CartHas3OrMoreItems(cart);
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

            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();
            //Act
            bool result = threeForTwoPromoLogic.CartHas3OrMoreItems(cart);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CartHasMoreThan3Items()
        {
            //Arrange
            Cart cart = new Cart();

            cart.Products.Add(new Product(1,"Jabon", 10, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(3,"Jabon2", 12, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(5,"Jabon3", 12, "Liquido", 12, 3, "red"));
            cart.Products.Add(new Product(4,"Jabon4", 12, "Liquido", 12, 3, "red"));

            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();
            //Act
            bool result = threeForTwoPromoLogic.CartHas3OrMoreItems(cart);
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

            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();
            //Act
            bool result = threeForTwoPromoLogic.CartHas3OrMoreItems(cart);
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GroupProductsByCategory_Correct()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product{ Category = 1, Price = 10 },
                new Product { Category = 2, Price = 15 },
                new Product { Category = 1, Price = 7 },
                new Product { Category = 2, Price = 20 },
                new Product { Category = 1, Price = 5 }
            };
            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();
            // Act
            Dictionary<int, List<Product>> productsByCategory = threeForTwoPromoLogic.GroupProductsByCategory(cart);

            // Assert
            Assert.AreEqual(2, productsByCategory.Count);
            Assert.IsTrue(productsByCategory.ContainsKey(1));
            Assert.IsTrue(productsByCategory.ContainsKey(2));
            Assert.AreEqual(3, productsByCategory[1].Count);
            Assert.AreEqual(2, productsByCategory[2].Count);
        }

        [TestMethod]
        public void GroupProductsByCategory_Wrong()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Category = 1, Price = 10 },
                new Product { Category = 2, Price = 15 },
                new Product { Category = 1, Price = 7 },
                new Product { Category = 2, Price = 20 },
                new Product { Category = 1, Price = 5 }
            };
            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();
            // Act
            var productsByCategory = threeForTwoPromoLogic.GroupProductsByCategory(cart);

            // Assert
            Assert.IsTrue(productsByCategory.ContainsKey(1));
            Assert.AreNotEqual(4, productsByCategory[1].Count);
        }

        [TestMethod]
        public void FindCategoryWithDiscount_Correct()
        {
            // Arrange
            Dictionary<int, List<Product>> productsByCategory = new Dictionary<int, List<Product>>
            {
                { 1, new List<Product> { new Product { Price = 1 }, new Product { Price = 16 }, new Product { Price = 155 } } },
                { 2, new List<Product> { new Product { Price = 11 }, new Product { Price = 14 } } },
                { 3, new List<Product> { new Product { Price = 12 }, new Product { Price = 11 } } }
            };
            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();
            // Act
            int categoryWithDiscount = threeForTwoPromoLogic.FindCategoryWithMaxDiscount(productsByCategory);

            // Assert
            Assert.AreEqual(1, categoryWithDiscount);
        }

        [TestMethod]
        public void FindCategoryWithMaxDiscount_Correct()
        {
            // Arrange
            Dictionary<int, List<Product>> productsByCategory = new Dictionary<int, List<Product>>
            {
                { 1, new List<Product> { new Product { Price = 10 }, new Product { Price = 5 }, new Product { Price = 7 } } },
                { 2, new List<Product> { new Product { Price = 15 }, new Product { Price = 6 }, new Product { Price = 7 } } },
                { 3, new List<Product> { new Product { Price = 12 }, new Product { Price = 11 } } }
            };
            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();
            // Act
            int categoryWithDiscount = threeForTwoPromoLogic.FindCategoryWithMaxDiscount(productsByCategory);

            // Assert
            Assert.AreEqual(2, categoryWithDiscount);
        }

        [TestMethod]
        public void FindCategoryWithDiscount_NoCategory()
        {
            // Arrange
            Dictionary<int, List<Product>> productsByCategory = new Dictionary<int, List<Product>>
            {
                { 1, new List<Product> { new Product { Price = 16 }, new Product { Price = 132 } } },
                { 2, new List<Product> { new Product { Price = 15 }, new Product { Price = 12 } } },
                { 3, new List<Product> { new Product { Price = 15 }, new Product { Price = 15 } } }
            };
            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();
            // Act
            int categoryWithDiscount = threeForTwoPromoLogic.FindCategoryWithMaxDiscount(productsByCategory);

            // Assert
            Assert.AreEqual(0, categoryWithDiscount);
        }

        [TestMethod]
        public void ApplyDiscountToCart_SubtractsDiscountFromTotalPrice()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Category = 1, Price = 10 },
                new Product { Category = 1, Price = 5 },
                new Product { Category = 1, Price = 7 },
                new Product { Category = 2, Price = 15 },
                new Product { Category = 3, Price = 20 }
            };
            cart.TotalPrice = 49;

            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();

            // Act
            threeForTwoPromoLogic.ApplyDiscountToCart(cart, new List<Product>
            {
                new Product { Category = 1, Price = 5 },
                new Product { Category = 1, Price = 5 },
                new Product { Category = 1, Price = 7 }
            });

            // Assert
            Assert.AreEqual(44, cart.TotalPrice);
        }

        [TestMethod]
        public void CalculateNewPriceWithDiscount_TwoCategoriesWithPossibleDiscount()
        {
            // Arrange
            Cart cart = new Cart();
            cart.Products = new List<Product>
            {
                new Product { Category = 1, Price = 25 },
                new Product { Category = 1, Price = 15 },
                new Product { Category = 1, Price = 10 },
                new Product { Category = 2, Price = 50 },
                new Product { Category = 2, Price = 30 },
                new Product { Category = 2, Price = 20 }
            };
            cart.TotalPrice = 150;

            ThreeForTwoPromoService threeForTwoPromoLogic = new ThreeForTwoPromoService();

            // Act
            double newPrice = threeForTwoPromoLogic.CalculateNewPriceWithDiscount(cart);

            // Assert
            Assert.AreEqual(130, newPrice);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obligatorio1.Domain;
using System.Collections.Generic;

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
            ThreeForTwoPromoLogic threeForTwoPromoLogic = new ThreeForTwoPromoLogic();
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
            ThreeForTwoPromoLogic threeForTwoPromoLogic = new ThreeForTwoPromoLogic();
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
            ThreeForTwoPromoLogic threeForTwoPromoLogic = new ThreeForTwoPromoLogic();
            // Act
            double newPrice = threeForTwoPromoLogic.CalculateNewPriceWithDiscount(cart);
            // Assert
            Assert.AreEqual(52, newPrice);
        }

        // Agrega más pruebas aquí para cubrir diferentes casos y escenarios.
    }
}

﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obligatorio1.BusinessLogic;
using Obligatorio1.Domain;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class PurchaseLogicTest
    {
        [TestMethod]
        public void ValidateMoreThan1Item()
        {
            //Arrange
            PurchaseLogic purchaseLogic = new PurchaseLogic();
            List<Product> cart = new List<Product>
            {
                new Product("Product1", 10, "Description", 123, 123, 123),
                new Product("Product2", 10, "Description", 123, 123, 123)
            };

            //Act
            bool result = purchaseLogic.ValidateMoreThan1Item(cart);
        }
    }
}
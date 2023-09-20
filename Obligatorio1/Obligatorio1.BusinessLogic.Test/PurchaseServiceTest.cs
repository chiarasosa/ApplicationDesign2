﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obligatorio1.Domain;
using System.Collections.Generic;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class PurchaseServiceTest
    {
        [TestMethod]
        public void ValidateMoreThan1Item()
        {
            //Arrange
            PurchaseService purchaseLogic = new PurchaseService();
            List<Product> cart = new List<Product>
            {
             //   new Product("Product1", 10, "Description", 123, 123, 123),
              //  new Product("Product2", 10, "Description", 123, 123, 123)
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
             //   new Product("Product1", 10, "Description", 123, 123, 123),
            };

            //Act
            purchaseLogic.ValidateMoreThan1Item(cart);
        }
    }
}
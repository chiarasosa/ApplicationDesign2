using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obligatorio1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    internal class UserServiceTest
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
            purchaseLogic.ValidateMoreThan1Item(cart);
        }
    }
}

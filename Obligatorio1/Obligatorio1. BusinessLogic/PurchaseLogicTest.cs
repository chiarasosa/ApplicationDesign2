using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obligatorio1.BusinessLogic;

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
                new Product("Product1", 10.0, "Description", "Brand", "Category", "Color"),
                new Product("Product2", 10.0, "Description", "Brand", "Category", "Color")
            };

            //Act
            bool result = purchaseLogic.ValidateMoreThan1Item(cart);
        }
    }
}
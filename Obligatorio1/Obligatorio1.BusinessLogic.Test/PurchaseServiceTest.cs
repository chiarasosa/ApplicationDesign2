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
    public class PurchaseServiceTest
    {
        private Mock<IPurchaseManagment>? _purchaseManagmentMock;
        private PurchaseService? _purchaseService;

        [TestInitialize]
        public void Initialize()
        {
            _purchaseManagmentMock = new Mock<IPurchaseManagment>(MockBehavior.Strict);
            _purchaseService = new PurchaseService(_purchaseManagmentMock.Object);
        }

        [TestMethod]
        public void CartHasMoreThan1Item()
        {
            //Arrange
            List<Product> list = new List<Product>();         
            list.Add(new Product("Jabon", 10, "Liquido", 12,3, 225 ));
            list.Add(new Product("Jabon2", 12, "Liquido", 12, 3, 225));

            //Act
            _purchaseManagmentMock?.Setup(x => x.ValidateMoreThan1Item(list));
            _purchaseService?.ValidateMoreThan1Item(list);

            //Assert
            _purchaseManagmentMock?.VerifyAll();
        }
    }
}
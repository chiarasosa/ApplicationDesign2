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
    public class PromoLogicTest
    {
        private Mock<IPromoManagment>? _promoManagmentMock;
        private ThreeForOnePromoLogic? _3x1PromoLogic;

        [TestInitialize]
        public void Initialize()
        {
            _promoManagmentMock = new Mock<IPromoManagment>(MockBehavior.Strict);
            _3x1PromoLogic = new ThreeForOnePromoLogic(_promoManagmentMock.Object);
        }
    }
}
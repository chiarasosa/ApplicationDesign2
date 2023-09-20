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
    public class PromoManagerServiceTest
    {
        private Mock<IPromoManagerManagment>? _promoManagerManagmentMock;
        private PromoManagerService? _promoManagerService;

        [TestInitialize]
        public void Initialize()
        {
            _promoManagerManagmentMock = new Mock<IPromoManagerManagment>(MockBehavior.Strict);
            _promoManagerService = new PromoManagerService(_promoManagerManagmentMock.Object);
        }

        [TestMethod]
        public void ApplyBestPromotion_NoPromotions()
        {
            // Arrange
            _promoManagerManagmentMock.Setup(p => p.GetAvailablePromotions()).Returns(new List<IPromoService>());

            // Act
            double discount = _promoManagerService.ApplyBestPromotion(new Cart()); // Llamar al método y obtener el resultado

            // Assert
            Assert.AreEqual(0, discount); // Verificar que el descuento sea 0
            _promoManagerManagmentMock.VerifyAll();
        }

        [TestMethod]
        public void ApplyBestPromotion_SinglePromotion()
        {
            // Arrange
            var promo3x1Mock = new Mock<IPromoService>();
            promo3x1Mock.Setup(p => p.CalculateNewPriceWithDiscount(It.IsAny<Cart>())).Returns(15);

            _promoManagerManagmentMock.Setup(p => p.GetAvailablePromotions()).Returns(new List<IPromoService> { promo3x1Mock.Object });

            var cart = new Cart();

            // Act
            var discount = _promoManagerService.ApplyBestPromotion(cart);

            // Assert
            Assert.AreEqual(15, discount);
            _promoManagerManagmentMock.VerifyAll();
        }

        [TestMethod]
        public void ApplyBestPromotion_MultiplePromotions_ReturnsBestDiscount()
        {
            // Arrange
            var promo1Mock = new Mock<IPromoService>();
            promo1Mock.Setup(p => p.CalculateNewPriceWithDiscount(It.IsAny<Cart>())).Returns(10);

            var promo2Mock = new Mock<IPromoService>();
            promo2Mock.Setup(p => p.CalculateNewPriceWithDiscount(It.IsAny<Cart>())).Returns(30);

            _promoManagerManagmentMock.Setup(p => p.GetAvailablePromotions()).Returns(new List<IPromoService> { promo1Mock.Object, promo2Mock.Object });

            var cart = new Cart();

            // Act
            var discount = _promoManagerService.ApplyBestPromotion(cart);

            // Assert
            Assert.AreEqual(30, discount);
            _promoManagerManagmentMock.VerifyAll();
        }



        /*
        [TestMethod]
        public void ApplyBestPromotion_FindsBestPromotion_ReturnsCorrectDiscount()
        {
            // Arrange

            var promo1Mock = new Mock<IPromoService>();
            promo1Mock.Setup(p => p.CalculateNewPriceWithDiscount(It.IsAny<Cart>())).Returns(10);

            var promo2Mock = new Mock<IPromoService>();
            promo2Mock.Setup(p => p.CalculateNewPriceWithDiscount(It.IsAny<Cart>())).Returns(20);

            promoManagerManagmentMock.Setup(p => p.GetAvailablePromotions()).Returns(new List<IPromoService> { promo1Mock.Object, promo2Mock.Object });


            var cart = new Cart();

            // Act
            double discount = promoManagerService.ApplyBestPromotion(cart);

            // Assert
            Assert.AreEqual(20, discount);
        }
        */
    }
}
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
        private PromoManagerService _promoManagerService;
        private Mock<IPromoManagerManagment> _promoManagerManagmentMock;
        private Mock<IPromoService> _promo3x1Mock;
        private Mock<IPromoService> _promo3x2Mock;

        [TestInitialize]
        public void Initialize()
        {
            // Configura tus mocks
            _promoManagerManagmentMock = new Mock<IPromoManagerManagment>();
            _promo3x1Mock = new Mock<IPromoService>();
            _promo3x2Mock = new Mock<IPromoService>();

            // Configura tus objetos PromoManagerService y agrega las implementaciones simuladas al repositorio simulado
            _promoManagerService = new PromoManagerService(_promoManagerManagmentMock.Object);
            _promoManagerManagmentMock.Setup(p => p.GetAvailablePromotions())
                .Returns(new List<IPromoService> { _promo3x1Mock.Object, _promo3x2Mock.Object });
        }


        [TestMethod]
        public void ApplyBestPromotion_EmptyCart()
        {
            // Arrange
            _promoManagerManagmentMock?.Setup(p => p.GetAvailablePromotions()).Returns(new List<IPromoService>());
            Cart cart = new Cart();
            // Act
            cart = _promoManagerService.ApplyBestPromotion(cart);

            // Assert
            Assert.AreEqual(0, cart.TotalPrice);

            // Verifica que todas las expectativas configuradas en _promoManagerManagmentMock se hayan cumplido
            _promoManagerManagmentMock?.VerifyAll();
        }

        [TestMethod]
        public void ApplyBestPromotion_NoPromotionApplied()
        {
            // Arrange
            _promoManagerManagmentMock?.Setup(p => p.GetAvailablePromotions()).Returns(new List<IPromoService>());
            Cart cart = new Cart();
            cart.TotalPrice = 10;
            cart.Products.Add(new Product("Producto1", 10, "Descripción", 1, 1, 1));

            // Act
            cart = _promoManagerService.ApplyBestPromotion(cart);

            // Assert
            Assert.AreEqual(10, cart.TotalPrice); 

            // Verifica que todas las expectativas configuradas en _promoManagerManagmentMock se hayan cumplido
            _promoManagerManagmentMock?.VerifyAll();
        }

        [TestMethod]
        public void ApplyBestPromotion_SinglePromotion()
        {
            // Arrange
            var promo3x1 = new ThreeForOnePromoLogic();
            _promoManagerManagmentMock?.Setup(p => p.GetAvailablePromotions()).Returns(new List<IPromoService> { promo3x1 });
            var cart = new Cart();
            cart.TotalPrice = 700;
            cart.Products.Add(new Product("Producto1", 150, "Descripción", 1, 19, 1));
            cart.Products.Add(new Product("Producto2", 100, "Descripción", 1, 33, 1));
            cart.Products.Add(new Product("Producto3", 200, "Descripción", 1, 45, 1));
            cart.Products.Add(new Product("Producto4", 250, "Descripción", 2, 76, 1));
            // Act
            cart = _promoManagerService.ApplyBestPromotion(cart);

            // Assert
            Assert.AreEqual(450, cart.TotalPrice); // Ajusta según tu expectativa

            // Verifica que todas las expectativas configuradas en _promoManagerManagmentMock se hayan cumplido
            _promoManagerManagmentMock?.VerifyAll();
        }

        [TestMethod]
        public void ApplyBestPromotion_MultiplePromotions_ReturnsBestDiscount()
        {
            // Arrange
            var promo3x1 = new ThreeForOnePromoLogic();
            var promo3x2 = new ThreeForTwoPromoLogic();
            _promoManagerManagmentMock?.Setup(p => p.GetAvailablePromotions()).Returns(new List<IPromoService> { promo3x1, promo3x2 });

            var cart = new Cart();

            // Act
            var discount = _promoManagerService.ApplyBestPromotion(cart);

            // Assert
            Assert.AreEqual(30, discount); // Ajusta según tu expectativa

            // Verifica que todas las expectativas configuradas en _promoManagerManagmentMock se hayan cumplido
            _promoManagerManagmentMock?.VerifyAll();
        }
    }
}
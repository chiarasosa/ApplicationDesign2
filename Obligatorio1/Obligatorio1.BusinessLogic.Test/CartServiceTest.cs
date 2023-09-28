using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using Obligatorio1.IBusinessLogic;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class CartServiceTest
    {
        private Mock<ICartManagment>? _cartManagmentMock;
        private Mock<IPromoManagerManagment>? _promoManagerManagmentMock;
        private Mock<IPromoService> _promo3x1Mock;
        private Mock<IPromoService> _promo3x2Mock;
        private CartService _cartService;

        [TestInitialize]
        public void Initialize()
        {
            //Mocks
            _cartManagmentMock = new Mock<ICartManagment>(MockBehavior.Strict);
            _promoManagerManagmentMock = new Mock<IPromoManagerManagment>(MockBehavior.Strict);
            _promo3x1Mock = new Mock<IPromoService>();
            _promo3x2Mock = new Mock<IPromoService>();
            _cartManagmentMock.Setup(p => p.SetAuthenticatedUser(new User()));
            _cartService = new CartService(_cartManagmentMock.Object, _promoManagerManagmentMock.Object);
        }

        [TestMethod]
        public void AddProductToCart_UserRegistered()
        {
            //Arrange
            Product product = new Product();

            //Act
            _cartManagmentMock?.Setup(x => x.AddProductToCart(product));
            _cartService.AddProductToCart(product);

            //Assert
            _cartManagmentMock?.VerifyAll();
            _cartManagmentMock?.Verify(x => x.AddProductToCart(product), Times.Once);
        }

        [TestMethod]
        public void DeleteProductFromCart_UserRegistered()
        {
            //Arrange
            Product product = new Product();

            //Act
            _cartManagmentMock?.Setup(x => x.AddProductToCart(product));
            _cartManagmentMock?.Setup(x => x.DeleteProductFromCart(product));
            _cartService.AddProductToCart(product);
            _cartService.DeleteProductFromCart(product);

            //Assert
            _cartManagmentMock?.VerifyAll();
            _cartManagmentMock?.Verify(x => x.DeleteProductFromCart(product), Times.Once);
        }

        [TestMethod]
        public void ApplyBestPromotion_EmptyCart()
        {
            // Arrange
            _promoManagerManagmentMock?.Setup(p => p.GetAvailablePromotions()).Returns(new List<IPromoService>());

            // Act
            Cart cart = _cartService.ApplyBestPromotion();

            // Assert
            Assert.AreEqual(0, cart.TotalPrice);
            _promoManagerManagmentMock?.VerifyAll();
        }

        [TestMethod]
        public void ApplyBestPromotion_NoPromotionApplied()
        {
           
        }

        [TestMethod]
        public void ApplyBestPromotion_TwentyPercentOffPossible_Correct()
        {
            
        }

        [TestMethod]
        public void ApplyBestPromotion_TotalAndTwentyPossible_Correct()
        {
      
        }

        /*
            // Arrange
            ThreeForOnePromoService promo3x1 = new ThreeForOnePromoService();
            ThreeForTwoPromoService promo3x2 = new ThreeForTwoPromoService();
            TwentyPercentOffPromoService twentyPercentOff = new TwentyPercentOffPromoService();
            TotalLookPromoService totalLook = new TotalLookPromoService();
            _promoManagerManagmentMock?.Setup(p => p.GetAvailablePromotions()).Returns(new List<IPromoService> { promo3x1, promo3x2, twentyPercentOff, totalLook });
            var cart = new Cart();
            cart.TotalPrice = 700;
            cart.Products.Add(new Product(1, "Producto1", 150, "Descripción", 1, 19, "red"));
            cart.Products.Add(new Product(2, "Producto2", 100, "Descripción", 4, 33, "blue"));
            cart.Products.Add(new Product(3, "Producto3", 200, "Descripción", 3, 45, "black"));
            cart.Products.Add(new Product(4, "Producto4", 250, "Descripción", 2, 76, "yellow"));
            // Act
            cart = _cartService.ApplyBestPromotion(cart);

            // Assert
            Assert.AreEqual(650, cart.TotalPrice);

            // Verifica que todas las expectativas configuradas en _promoManagerManagmentMock se hayan cumplido
            _promoManagerManagmentMock?.VerifyAll();
            */
    }
}
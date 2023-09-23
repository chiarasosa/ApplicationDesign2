using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class CartServiceTest
    {
        private Mock<IUserManagment>? _userManagmentMock;
        private CartService _cartService;

        [TestInitialize]
        public void Initialize()
        {
            _userManagmentMock = new Mock<IUserManagment>(MockBehavior.Strict);
            _userManagmentMock.Setup(x => x.GetAuthenticatedUser()).Returns(new User());
            _cartService = new CartService(_userManagmentMock.Object);
        }

        [TestMethod]
        public void AddProductToCart_NoUserRegistered()
        {
            //Arrange
            Product product = new Product();

            //Act
            CartService cartService = new CartService();
            cartService.AddProductToCart(product);

            //Assert
            Assert.AreEqual(1, cartService.defaultCart.Products.Count);
        }

        [TestMethod]
        public void AddProductToCart_UserRegistered()
        {
            //Arrange
            Product product = new Product();

            //Act
            _userManagmentMock?.Setup(x => x.AddProductToCart(product));
            _cartService.AddProductToCart(product);

            //Assert
            _userManagmentMock?.VerifyAll();
            _userManagmentMock?.Verify(x => x.AddProductToCart(product), Times.Once);
        }

        [TestMethod]
        public void DeleteProductFromCart_NoUserRegistered()
        {
            //Arrange
            Product product = new Product();

            //Act
            CartService cartService = new CartService();
            cartService.AddProductToCart(product);
            cartService.DeleteProductFromCart(product);

            //Assert
            Assert.AreEqual(0, cartService.defaultCart.Products.Count);
        }

        [TestMethod]
        public void DeleteProductFromCart_UserRegistered()
        {
            //Arrange
            Product product = new Product();

            //Act
            _userManagmentMock?.Setup(x => x.AddProductToCart(product));
            _userManagmentMock?.Setup(x => x.DeleteProductFromCart(product));
            _cartService.AddProductToCart(product);
            _cartService.DeleteProductFromCart(product);

            //Assert
            _userManagmentMock?.VerifyAll();
            _userManagmentMock?.Verify(x => x.DeleteProductFromCart(product), Times.Once);
        }
    }
}
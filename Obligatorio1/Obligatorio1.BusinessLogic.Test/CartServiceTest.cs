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
        private CartService? _cartService;

        [TestInitialize]
        public void Initialize()
        {
            _userManagmentMock = new Mock<IUserManagment>(MockBehavior.Strict);
            _cartService = new CartService(_userManagmentMock.Object);
        }

        [TestMethod]
        public void AddProductToCart_NoUserRegistered()
        {
            //Arrange


        }

        [TestMethod]
        public void AddProductToCart_UserRegistered()
        {

        }

        [TestMethod]
        public void DeleteProductFromCart_NoUserRegistered()
        {

        }

        [TestMethod]
        public void DeleteProductFromCart_UserRegistered()
        {

        }
    }
}
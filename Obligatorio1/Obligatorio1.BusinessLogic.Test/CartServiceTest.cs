using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class CartServiceTest
    {
        [TestMethod]
        public void AddProductToCart_ValidProduct_ReturnsSuccess()
        {
            var sessionRepositoryMock = new Mock<IGenericRepository<Session>>();
            var cartRepositoryMock = new Mock<IGenericRepository<Cart>>();
            var productServiceMock = new Mock<IProductService>();
            var cartProductRepositoryMock = new Mock<IGenericRepository<CartProduct>>();
            var promotionsServiceMock = new Mock<IPromotionsService>();

            var service = new CartService(sessionRepositoryMock.Object, cartRepositoryMock.Object, productServiceMock.Object, cartProductRepositoryMock.Object, promotionsServiceMock.Object);

            var product = new Product { ProductID = 1, Stock = 5 };
            var authToken = Guid.NewGuid();
            var session = new Session { AuthToken = authToken, User = new User { Cart = new Cart() } };

            sessionRepositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<Session, bool>>>(), It.IsAny<List<string>>())).Returns(session);
            productServiceMock.Setup(service => service.GetProductByID(It.IsAny<int>())).Returns(product);

            service.AddProductToCart(product, authToken);

            cartRepositoryMock.Verify(repo => repo.Update(It.IsAny<Cart>()), Times.Once);
            cartRepositoryMock.Verify(repo => repo.Save(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(CartException))]
        public void AddProductToCart_ProductWithNoStock_ThrowsCartException()
        {
            var sessionRepositoryMock = new Mock<IGenericRepository<Session>>();
            var cartRepositoryMock = new Mock<IGenericRepository<Cart>>();
            var productServiceMock = new Mock<IProductService>();
            var cartProductRepositoryMock = new Mock<IGenericRepository<CartProduct>>();
            var promotionsServiceMock = new Mock<IPromotionsService>();

            var service = new CartService(sessionRepositoryMock.Object, cartRepositoryMock.Object, productServiceMock.Object, cartProductRepositoryMock.Object, promotionsServiceMock.Object);

            var product = new Product { ProductID = 1, Stock = 0 };
            var authToken = Guid.NewGuid();
            var session = new Session { AuthToken = authToken, User = new User { Cart = new Cart() } };

            sessionRepositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<Session, bool>>>(), It.IsAny<List<string>>())).Returns(session);
            productServiceMock.Setup(service => service.GetProductByID(It.IsAny<int>())).Returns(product);

            service.AddProductToCart(product, authToken);
        }

    }
}

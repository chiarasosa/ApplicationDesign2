using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;
using System.Collections.Generic;
using System.Linq;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class CartServiceTest
    {
        private Mock<ICartManagment> _cartManagmentMock;
        private Mock<IPromoManagerManagment> _promoManagerManagmentMock;
        private Mock<IPromoService> _promo3x1Mock;
        private Mock<IPromoService> _promo3x2Mock;
        private Mock<IPromoService> _promoTotalLookMock;
        private Mock<IPromoService> _promo20PercentOffMock;
        private CartService _cartService;

        [TestInitialize]
        public void Initialize()
        {
            _cartManagmentMock = new Mock<ICartManagment>(MockBehavior.Strict);
            _promoManagerManagmentMock = new Mock<IPromoManagerManagment>(MockBehavior.Strict);
            _promo3x1Mock = new Mock<IPromoService>();
            _promo3x2Mock = new Mock<IPromoService>();
            _promoTotalLookMock = new Mock<IPromoService>();
            _promo20PercentOffMock = new Mock<IPromoService>();

            // Configura el mock de IPromoManagerManagment para devolver una lista de promociones
            _promoManagerManagmentMock.Setup(p => p.GetAvailablePromotions())
                .Returns(new List<IPromoService>
                {
            _promo3x1Mock.Object,
            _promo3x2Mock.Object,
            _promoTotalLookMock.Object,
            _promo20PercentOffMock.Object
                });
            //_cartManagmentMock.Setup(p => p.SetAuthenticatedUser(new User()));
            _cartService = new CartService(_cartManagmentMock.Object, _promoManagerManagmentMock.Object);
        }

        [TestMethod]
        public void AddProductToCart_UserRegistered()
        {
            // Arrange
            Product product = new Product();
            Cart cart = new Cart();
            cart.Products.Add(product);
            _cartManagmentMock.Setup(p => p.GetCart()).Returns(cart);
            _cartManagmentMock.Setup(p => p.UpdateCartWithDiscount(cart));
            _cartManagmentMock.Setup(x => x.AddProductToCart(product));

            // Act
            _cartService.AddProductToCart(product);

            // Assert
            _cartManagmentMock.VerifyAll();
            _cartManagmentMock.Verify(x => x.AddProductToCart(product), Times.Once);
        }

        [TestMethod]
        public void DeleteProductFromCart_UserRegistered()
        {
            // Arrange
            Product product = new Product();
            Cart cart = new Cart();
            cart.Products.Add(product);
            cart.Products.Remove(product);
            _cartManagmentMock.Setup(p => p.GetCart()).Returns(cart);
            _cartManagmentMock.Setup(p => p.UpdateCartWithDiscount(cart));
            _cartManagmentMock.Setup(x => x.AddProductToCart(product));
            _cartManagmentMock.Setup(x => x.DeleteProductFromCart(product));

            // Act
            _cartService.AddProductToCart(product);
            _cartService.DeleteProductFromCart(product);

            // Assert
            _cartManagmentMock.VerifyAll();
            _cartManagmentMock.Verify(x => x.AddProductToCart(product), Times.Once);
            _cartManagmentMock.Verify(x => x.DeleteProductFromCart(product), Times.Once);
        }

        [TestMethod]
        public void ApplyBestPromotion_EmptyCart()
        {
            // Arrange
            Cart cart = new Cart();
            _cartManagmentMock.Setup(p => p.GetCart()).Returns(cart);
            _cartManagmentMock.Setup(p => p.UpdateCartWithDiscount(cart));

            // Act
            cart = _cartService.ApplyBestPromotion();

            // Assert
            Assert.AreEqual(0, cart.TotalPrice);
            _promoManagerManagmentMock.VerifyAll();
        }

        [TestMethod]
        public void ApplyBestPromotion_NoPromotionApplied()
        {
            // Arrange
            Cart cart = new Cart
            {
                Products = new List<Product>
                {
                    new Product { Brand = 1, Price = 100 },
                },
                TotalPrice = 100,
            };
            _cartManagmentMock.Setup(c => c.GetCart()).Returns(cart);
            _cartManagmentMock.Setup(p => p.UpdateCartWithDiscount(cart));

            // Act
            cart = _cartService.ApplyBestPromotion();

            // Assert
            Assert.AreEqual(100, cart.TotalPrice);
        }

        [TestMethod]
        public void ApplyBestPromotion_TwentyPercentOffPossible_Correct()
        {
            // Arrange
            var availablePromotions = new List<IPromoService>
            {
                _promo3x1Mock.Object,
                _promo3x2Mock.Object,
                _promoTotalLookMock.Object,
                _promo20PercentOffMock.Object
            };

            _promoManagerManagmentMock.Setup(p => p.GetAvailablePromotions()).Returns(availablePromotions);
            _cartManagmentMock.Setup(c => c.GetCart()).Returns(new Cart
            {
                Products = new List<Product>
                {
                    new Product { Brand = 1, Price = 100 },
                    new Product { Brand = 2, Price = 150 },
                }
            });

            // Act
            Cart cart = _cartService.ApplyBestPromotion();

            // Assert
            Assert.AreEqual(240, cart.TotalPrice);
        }

        [TestMethod]
        public void ApplyBestPromotion_ThreeForOnePossible_Correct()
        {
            // Arrange
            var availablePromotions = new List<IPromoService>
            {
                _promo3x1Mock.Object,
                _promo3x2Mock.Object,
                _promoTotalLookMock.Object,
                _promo20PercentOffMock.Object
            };

            _promoManagerManagmentMock.Setup(p => p.GetAvailablePromotions()).Returns(availablePromotions);
            _cartManagmentMock.Setup(c => c.GetCart()).Returns(new Cart
            {
                Products = new List<Product>
                {
                    new Product { Brand = 1, Price = 100 },
                    new Product { Brand = 2, Price = 100 },
                    new Product { Brand = 2, Price = 100 },
                }
            });

            // Act
            Cart cart = _cartService.ApplyBestPromotion();

            // Assert
            Assert.AreEqual(200, cart.TotalPrice);
        }

        [TestMethod]
        public void ApplyBestPromotion_TotalLookPossible_Correct()
        {
            // Arrange
            var availablePromotions = new List<IPromoService>
            {
                _promo3x1Mock.Object,
                _promo3x2Mock.Object,
                _promoTotalLookMock.Object,
                _promo20PercentOffMock.Object
            };

            _promoManagerManagmentMock.Setup(p => p.GetAvailablePromotions()).Returns(availablePromotions);
            _cartManagmentMock.Setup(c => c.GetCart()).Returns(new Cart
            {
                Products = new List<Product>
                {
                    new Product { Brand = 1, Price = 100, Color = "Red" },
                    new Product { Brand = 2, Price = 100, Color = "Red" },
                    new Product { Brand = 2, Price = 100, Color = "Blue" },
                }
            });

            // Act
            Cart cart = _cartService.ApplyBestPromotion();

            // Assert
            Assert.AreEqual(250, cart.TotalPrice);
        }

        [TestMethod]
        public void ApplyBestPromotion_ThreeForTwoPossible_Correct()
        {
            // Arrange
            var availablePromotions = new List<IPromoService>
            {
                _promo3x1Mock.Object,
                _promo3x2Mock.Object,
                _promoTotalLookMock.Object,
                _promo20PercentOffMock.Object
            };

            _promoManagerManagmentMock.Setup(p => p.GetAvailablePromotions()).Returns(availablePromotions);
            _cartManagmentMock.Setup(c => c.GetCart()).Returns(new Cart
            {
                Products = new List<Product>
                {
                    new Product { Brand = 1, Price = 100, Category = 1 },
                    new Product { Brand = 2, Price = 100, Category = 2 },
                    new Product { Brand = 2, Price = 100, Category = 1 },
                }
            });

            // Act
            Cart cart = _cartService.ApplyBestPromotion();

            // Assert
            Assert.AreEqual(200, cart.TotalPrice);
        }
    }
}

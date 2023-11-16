using Moq;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi.Controllers;

namespace Obligatorio1.WebApi.Test
{
    [TestClass]
    public class CartControllerTest
    {
        private CartController _controller;
        private Mock<ICartService> _cartServiceMock;
        private Cart _cart;

        [TestInitialize]
        public void Setup()
        {
            this._cartServiceMock = new Mock<ICartService>();

            this._controller = new CartController(_cartServiceMock.Object);

            this._cart = new Cart()
            {
                Products = new List<Product>() {
                new Product() {
                    Name = "Jabon",
                    Price = 10,
                    Description = "Liquido",
                    Brand = "A",
                    Category = 1,
                },
                new Product() {
                    Name = "Fideos",
                    Price = 20,
                    Description = "Tallarines",
                    Brand = "B",
                    Category = 4,
                },
                new Product() {
                    Name = "Pan",
                    Price = 30,
                    Description = "Blanco",
                    Brand = "C",
                    Category = 5,
                }
            },
                TotalPrice = 60,
                PromotionApplied = "3x1 promo",
            };
        }

        [TestMethod]
        public void AddProductToCart_ValidProduct_ReturnsOkResult()
        {
            var product = new Product();

            _cart.Products.Add(product);

            Assert.AreEqual(4, _cart.Products.Count);
        }

        [TestMethod]
        public void DeleteProductFromCart_ValidProduct_ReturnsOkResult()
        {
            var product = new Product();

            _cart.Products.Add(product);
            _cart.Products.Remove(product);

            Assert.AreEqual(3, _cart.Products.Count);
        }
    }
}

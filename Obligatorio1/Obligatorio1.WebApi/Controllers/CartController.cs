using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi.Filters;

namespace Obligatorio1.WebApi.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        /// <summary>
        /// Constructor of Cart Controller.
        /// </summary>
        /// <param name="cartService">Cart Services.</param>
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Adds a product to the cart and refreshes the final price with best promotion.
        /// </summary>
        /// <param name="product">Product that wants to be added.</param>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpPost]
        public IActionResult AddProductToCart([FromBody] Product product)
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            _cartService.AddProductToCart(product, authToken);
            return Ok("Product added to cart successfully.");
        }

        [HttpGet("Prueba")]
        public IActionResult MetodoPrueba()
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            return Ok(_cartService.MetodoPrueba(authToken));
        }

        /// <summary>
        /// Deletes a product from the cart and refreshes the final price with best promotion.
        /// </summary>
        /// <param name="product">Product that wants to be deleted.</param>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpDelete]
        public IActionResult DeleteProductFromCart([FromBody] Product product)
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            _cartService.DeleteProductFromCart(product, authToken);
            return Ok("Product deleted from cart successfully.");
        }

        /// <summary>
        /// Gets the list of products from the cart.
        /// </summary>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpGet()]
        public IActionResult GetProductsFromCart()
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            var products = _cartService.GetAllProductsFromCart(authToken);
            return Ok(products);
        }

        /// <summary>
        /// Gets the promotion applied to the cart.
        /// </summary>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpGet("PromotionApplied")]
        public IActionResult GetPromoAppliedToCart()
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            var promo = _cartService.GetPromottionAppliedCart(authToken);

            return Ok(promo);
        }

        /// <summary>
        /// Gets the total price of the cart.
        /// </summary>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpGet("TotalPrice")]
        public IActionResult GetTotalPrice()
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            var price = _cartService.GetTotalPriceCart(authToken);

            return Ok(price);
        }
        
    }
}
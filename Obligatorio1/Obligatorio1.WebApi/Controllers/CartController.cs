using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi.Filters;

namespace Obligatorio1.WebApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
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
        [TypeFilter(typeof(AuthorizationRolFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpPost]
        public IActionResult AddProductToCart([FromBody] Product product)
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            _cartService.AddProductToCart(product, authToken);
            return Ok("Product added to cart successfully.");
        }

        /// <summary>
        /// Deletes a product from the cart and refreshes the final price with best promotion.
        /// </summary>
        /// <param name="product">Product that wants to be deleted.</param>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
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
        [TypeFilter(typeof(AuthorizationRolFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpGet("{id}")]
        public IActionResult GetProductsFromCart()
        {
            try
            {
                //var products = _cart.Products;
                //var peroducts = _cartService.GetLoggedInCart().Products.ToList();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while getting the products: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the promotion applied to the cart.
        /// </summary>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [HttpGet("PromotionApplied")]
        public IActionResult GetPromoAppliedToCart()
        {
            try
            {
                //var promo = _cart.PromotionApplied;
                //var promo = _cartService.GetLoggedInCart().PromotionApplied;
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while getting the promotion: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the total price of the cart.
        /// </summary>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [HttpGet("TotalPrice")]
        public IActionResult GetTotalPrice()
        {
            try
            {
                //var price = _cart.TotalPrice;
                //var price = _cartService.GetLoggedInCart().TotalPrice;
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while getting the promotion: {ex.Message}");
            }
        }
    }

}
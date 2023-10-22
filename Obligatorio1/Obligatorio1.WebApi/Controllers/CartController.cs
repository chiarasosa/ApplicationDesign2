using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;

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
        [HttpPost("addProduct")]
        public IActionResult AddProductToCart([FromBody] Product product)
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            try
            {
                _cartService.AddProductToCart(product, authToken);
                return Ok("Product added to cart successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding product to cart: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a product from the cart and refreshes the final price with best promotion.
        /// </summary>
        /// <param name="product">Product that wants to be deleted.</param>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [HttpDelete("deleteProduct")]
        public IActionResult DeleteProductFromCart([FromBody] Product product)
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            try
            {
                _cartService.DeleteProductFromCart(product, authToken);
                return Ok("Product deleted from cart successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting product from cart: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the list of products from the cart.
        /// </summary>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [HttpGet]
        public IActionResult GetLoggedInCart()
        {
            try
            {
                //var cart = _cart;
                //var cart = _cartService.GetLoggedInCart();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while getting the cart: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the list of products from the cart.
        /// </summary>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [HttpGet("AllProducts")]
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
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

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add-product")]
        public IActionResult AddProductToCart([FromBody] Product product)
        {
            try
            {
                _cartService.AddProductToCart(product);
                return Ok("Product added to cart successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding product to cart: {ex.Message}");
            }
        }

        [HttpPost("delete-product")]
        public IActionResult DeleteProductFromCart([FromBody] Product product)
        {
            try
            {
                _cartService.DeleteProductFromCart(product);
                return Ok("Product deleted from cart successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting product from cart: {ex.Message}");
            }
        }
    }

}
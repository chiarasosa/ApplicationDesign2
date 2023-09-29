using Microsoft.AspNetCore.Mvc;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;

namespace Obligatorio1.WebApi.Controllers
{
    [ApiController]
    [Route("api/purchase")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        /// <summary>
        /// Make a purchase.
        /// </summary>
        /// <param name="cart">Cart that wants to be purchased.</param>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [HttpPost]
        public IActionResult CreatePurchase([FromBody] Cart cart)
        {
            try
            {
                _purchaseService.ExecutePurchase(cart);
                return Ok("The purchase has been made successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Purchase error: {ex.Message}");
            }
        }
    }
}

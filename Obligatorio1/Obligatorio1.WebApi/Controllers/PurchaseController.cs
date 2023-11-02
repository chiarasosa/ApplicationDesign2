using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi.Filters;

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
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public IActionResult CreatePurchase()
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            _purchaseService.CreatePurchase(authToken);
            return Ok("The purchase has been made successfully.");
        }

    }
}
    /*
    /// <summary>
    /// Get a specific a purchase.
    /// </summary>
    /// <param name="id">Id of purchase.</param>
    /// <returns>Returns HTTP response with the result of the operation.</returns>
    [HttpGet("specificPurchase/{id}")]
    public IActionResult GetSpecificPurchase([FromRoute] int id)
    {
        //var purchase = _purchaseService.GetPurchases().Find(x => x.PurchaseID == id);
        var purchase = _purchases.Find(x => x.PurchaseID == id);

        if (purchase == null)
        {
            return NotFound(new { Message = "Cant find purchase" });
        }
        else
        {
            return Ok(purchase);
        }
    }

    /// <summary>
    /// Gets all purchases.
    /// </summary>
    /// <returns>Returns HTTP response with the result of the operation.</returns>
    [HttpGet]
    public IActionResult GetAllPurchases()
    {
        //var purchase = _purchaseService.GetPurchases()
        var purchases = _purchases;

        if (purchases == null)
        {
            return NotFound(new { Message = "There are no purchases" });
        }
        else
        {
            return Ok(purchases);
        }
    }

    /// <summary>
    /// Get all purchases from a specific user.
    /// </summary>
    /// <param name="id">Id of user.</param>
    /// <returns>Returns HTTP response with the result of the operation.</returns>
    [HttpGet("usersPurchases/{id}")]
    public IActionResult GetUsersPurchases([FromRoute] int id)
    {
        //var purchase = _purchaseService.GetPurchases().Find(x => x.User.UserID == id);
        var purchases = _purchases.Where(x => x.UserID == id).ToList();


        if (purchases == null)
        {
            return NotFound(new { Message = "Cant find users purchases" });
        }
        else
        {
            return Ok(purchases);
        }
    }*/
//}

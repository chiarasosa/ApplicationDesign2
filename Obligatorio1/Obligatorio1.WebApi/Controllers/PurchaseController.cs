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

        [HttpGet]
        public IActionResult ValidateMoreThan1Item(Purchase purchase)
        {
            // Call the ValidateMoreThan1Item method from the service
            bool validPurchase = _purchaseService.ValidateMoreThan1Item(purchase);

            // Return the result as needed, for example, as an OkObjectResult
            return Ok(validPurchase);
        }
    }
}

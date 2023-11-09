using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi.Filters;

namespace Obligatorio1.WebApi.Controllers
{
    [ApiController]
    [Route("api/purchases")]
    [TypeFilter(typeof(AuthenticationFilter))]
    [TypeFilter(typeof(ExceptionFilter))]
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
        public IActionResult CreatePurchase()
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            _purchaseService.CreatePurchase(authToken);
            return Ok("The purchase has been made successfully.");
        }

        /// <summary>
        /// Gets all purchases.
        /// </summary>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [HttpGet]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        public IActionResult GetPurchases()
        {
            var purchases = _purchaseService.GetAllPurchases();

            if (purchases == null || !purchases.Any())
            {
                return NotFound("No se encontraron compras en el sistema.");
            }

            var formattedPurchases = purchases.Select(purchase => new
            {
                PurchaseID = purchase.PurchaseID,
                UserID = purchase.UserID,
                PromoApplied = purchase.PromoApplied,
                DateOfPurchase = purchase.DateOfPurchase,
                PurchasedProducts = purchase.PurchasedProducts.Select(pp => new
                {
                    ProductID = pp.ProductID,
                    Product = pp.Product
                })
            });

            var jsonResult = JsonConvert.SerializeObject(formattedPurchases, Formatting.Indented);

            return Ok(jsonResult);
        }

        /// <summary>
        /// Get a specific a purchase.
        /// </summary>
        /// <param name="id">Id of purchase.</param>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [HttpGet("{id}")]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        public IActionResult GetSpecificPurchase([FromRoute] int id)
        {
            var purchase = _purchaseService.GetPurchaseByID(id);

            if (purchase == null)
            {
                return NotFound(new { Message = "No se encontró la compra" });
            }
            else
            {
                var formattedPurchase = new
                {
                    PurchaseID = purchase.PurchaseID,
                    UserID = purchase.UserID,
                    PromoApplied = purchase.PromoApplied,
                    DateOfPurchase = purchase.DateOfPurchase,
                    PurchasedProducts = purchase.PurchasedProducts.Select(pp => new
                    {
                        ProductID = pp.ProductID,
                        Product = pp.Product
                    })
                };

                var jsonResult = JsonConvert.SerializeObject(formattedPurchase, Formatting.Indented);

                return Ok(jsonResult);
            }
        }

        /// <summary>
        /// Get all purchases from a specific user.
        /// </summary>
        /// <param name="id">Id of user.</param>
        /// <returns>Returns HTTP response with the result of the operation.</returns>
        [HttpGet("usersPurchases/{id}")]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        public IActionResult GetUsersPurchases([FromRoute] int id)
        {
            var purchases = _purchaseService.GetPurchasesByUserID(id);

            if (purchases == null || !purchases.Any())
            {
                return NotFound(new { Message = "No se encontraron compras para el usuario con ID " + id });
            }
            else
            {
                var formattedPurchases = purchases.Select(purchase => new
                {
                    PurchaseID = purchase.PurchaseID,
                    UserID = purchase.UserID,
                    PromoApplied = purchase.PromoApplied,
                    DateOfPurchase = purchase.DateOfPurchase,
                    PurchasedProducts = purchase.PurchasedProducts.Select(pp => new
                    {
                        ProductID = pp.ProductID,
                        Product = pp.Product
                    })
                });

                var jsonResult = JsonConvert.SerializeObject(formattedPurchases, Formatting.Indented);

                return Ok(jsonResult);
            }
        }
    }
}
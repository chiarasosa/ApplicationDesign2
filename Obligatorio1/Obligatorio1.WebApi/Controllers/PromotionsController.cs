using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi.Filters;
using Obligatorio1.PromoInterface;

namespace Obligatorio1.WebApi.Controllers
{
    [ApiController]

    [Route("api/promotions")]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionsService _promotionsService;

        /// <summary>
        /// Constructor of Cart Controller.
        /// </summary>
        /// <param name="cartService">Cart Services.</param>
        public PromotionsController(IPromotionsService promotionsService)
        {
            _promotionsService = promotionsService;
        }

        [HttpGet()]
        public IActionResult GetPromotionsAvailable()
        {
            List<IPromoService> promos = _promotionsService.GetPromotionsAvailable();
            List<string> names = new List<string>();
            foreach (var promotion in promos)
            {
                names.Add(promotion.GetName());
            }
            return Ok(names);
        }
    }
}


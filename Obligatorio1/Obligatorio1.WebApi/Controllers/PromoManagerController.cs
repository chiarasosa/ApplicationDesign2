using Microsoft.AspNetCore.Mvc;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace Obligatorio1.WebApi.Controllers
{
    [ApiController]
    [Route("api/promomanager")]
    public class PromoManagerController : ControllerBase
    {
        private readonly IPromoManagerService _promoManagerService;
        public PromoManagerController(IPromoManagerService _promoManagerService)
        {
            this._promoManagerService = _promoManagerService;
        }

        [HttpPut("applybestpromo")]
        public OkObjectResult ApplyBestPromotion([FromBody] Cart cart)
        {
            var updatedCart = _promoManagerService.ApplyBestPromotion(cart);
            return Ok(updatedCart);
        }

    }
}

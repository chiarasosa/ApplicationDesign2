using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi.Filters;
//using Obligatorio1.WebApi.Dtos;
using Obligatorio1.Domain;

namespace Obligatorio1.WebApi.Controllers
{
    [Route("api/sessions")]
    [ApiController]
    [ExceptionFilter]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            var token = _sessionService.Authenticate(user.Email, user.Password);
            return Ok(new { token = token });
        }


        // En los endpoints que quiero usar autenticación, agrego el filtro, si quiero usarlo en todos los endpoints
        // de un controller lo agrego a nivel de la clase
        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpDelete]
        public IActionResult Logout()
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            _sessionService.Logout(authToken);
            return Ok("Logout successfully");
        }

        [HttpGet("current-user")]
        public IActionResult GetLoggedInUser()
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            var user = _sessionService.GetCurrentUser(authToken);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("User not found");
            }
        }
        
    }
}
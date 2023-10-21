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
        
        [HttpDelete]
        public IActionResult Logout([FromBody] User user)
        {
            _sessionService.Logout(user);
            return Ok("Logout successfully: " + user.UserName);
        }

        [HttpGet]
        public IActionResult GetLoggedInUser()
        {
            var user = _sessionService.GetCurrentUser();
            return Ok(user);
        }
        
    }
}
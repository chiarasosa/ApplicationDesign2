using Microsoft.AspNetCore.Mvc;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi.Filters;
using Obligatorio1.Domain;
using System;

namespace Obligatorio1.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing user sessions and authentication.
    /// </summary>
    [Route("api/sessions")]
    [ApiController]
    [TypeFilter(typeof(ExceptionFilter))]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        /// <summary>
        /// Constructor for SessionController.
        /// </summary>
        /// <param name="sessionService">The session service to handle user sessions and authentication.</param>
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// Authenticates a user and returns an authentication token.
        /// </summary>
        /// <param name="user">User credentials for authentication.</param>
        /// <returns>Returns an HTTP response with an authentication token if authentication is successful.</returns>
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            var token = _sessionService.Authenticate(user.Email, user.Password);
            return Ok(new { token = token });
        }

        /// <summary>
        /// Logs out a user, ending the current session.
        /// </summary>
        /// <returns>Returns an HTTP response indicating a successful logout.</returns>
        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpDelete]
        public IActionResult Logout()
        {
            var authToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]);
            _sessionService.Logout(authToken);
            return Ok("Logout successfully");
        }

        /// <summary>
        /// Retrieves the user currently logged in.
        /// </summary>
        /// <returns>Returns an HTTP response with the user information if a user is logged in.</returns>
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

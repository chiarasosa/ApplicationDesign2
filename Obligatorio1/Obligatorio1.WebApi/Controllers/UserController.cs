using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Serilog;
using Microsoft.AspNetCore.Http;
using Obligatorio1.WebApi.Filters;

namespace Obligatorio1.WebApi
{
    /// <summary>
    /// Controller for user management.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private User? _loggedInUser;

        /// <summary>
        /// User Controller Constructor.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Register a new user in the system.
        /// </summary>
        /// <param name="user">The user data to register.</param>
        /// <returns>HTTP response indicating the result of the registration.</returns>
        [HttpPost]
        [TypeFilter(typeof(ExceptionFilter))]
        public IActionResult RegisterUser([FromBody] User user)
        {
            _userService.RegisterUser(user);
            return Ok("Usuario registrado exitosamente.");
        }

        /// <summary>
        /// Gets the list of all users registered in the system.
        /// </summary>
        ///<remarks>
        /// Allows a user with administrator permissions to obtain the list of all users registered in the system.
        /// </remarks>
        /// <returns>HTTP response with the list of users.</returns>
        [HttpGet]
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to obtain.</param>
        /// <returns>HTTP response with the user found.</returns>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpGet("{id}")]
        public IActionResult GetUserByID([FromRoute] int id)
        {
            var user = _userService.GetUserByID(id);
            return Ok(user);
        }

        /// <summary>
        /// Delete a user registered in the system by their ID.
        /// </summary>
        /// <remarks>
        /// Allows a user with administrator permissions to delete a user by user ID.
        /// </remarks>
        /// <param name="id">The ID of the user to delete.</param>
        /// <response code="204">The user was successfully deleted.</response>
        /// <response code="404">The user with the specified ID was not found.</response>
        /// <response code="400">Error in the request or deleting the user.</response>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            _userService.DeleteUser(id);
            return Ok("Usuario eliminado exitosamente");
        }

        /// <summary>
        /// Updates the profile of a user registered in the system.
        /// </summary>
        /// <remarks>
        /// Allows a registered user to update their profile with new data.
        /// </remarks>
        /// <param name="user">The user data to update.</param>
        /// <response code="200">The user information was updated successfully.</response>
        /// <response code="400">Error in the request or updating user information.</response>
        [TypeFilter(typeof(AuthenticationFilter))]
        [TypeFilter(typeof(AuthorizationRolFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        [HttpPut("UpdateUserProfile")]
        public IActionResult UpdateUserProfile([FromBody] User user)
        {
            var updatedUser = _userService.UpdateUserProfile(user);
            return Ok(updatedUser);
        }
    }
}

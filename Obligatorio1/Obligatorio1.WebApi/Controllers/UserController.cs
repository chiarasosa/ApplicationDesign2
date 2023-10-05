using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Serilog;

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
        public IActionResult RegisterUser([FromBody] User user)
        {
            try
            {
                Log.Information("Intentando registrar usuario: {@User}", user);



                _userService.RegisterUser(user);

                Log.Information("Usuario registrado exitosamente.");

                return Ok("Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al registrar el usuario: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al registrar el usuario: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the list of all users registered in the system.
        /// </summary>
        ///<remarks>
        /// Allows a user with administrator permissions to obtain the list of all users registered in the system.
        /// </remarks>
        /// <returns>HTTP response with the list of users.</returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var loggedInUser = _userService.GetLoggedInUser();

                if (loggedInUser == null || loggedInUser.Role != "Administrador")
                {
                    return Unauthorized("No tiene permiso para obtener la lista de usuarios.");
                }

                var users = _userService.GetUsers();

                return Ok(users);
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al obtener usuarios: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al obtener usuarios: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al obtener usuarios: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al obtener usuarios: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to obtain.</param>
        /// <returns>HTTP response with the user found.</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserByID([FromRoute] int id)
        {
            try
            {
                var user = _userService.GetUserByID(id);

                if (user == null)
                {
                    return NotFound($"Usuario con ID {id} no encontrado.");
                }

                return Ok(user);
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al obtener el usuario: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al obtener el usuario: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al obtener el usuario: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al obtener el usuario: {ex.Message}");
            }
        }

        /// <summary>
        /// Log in as a user registered in the system.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>HTTP response indicating the result of the login.</returns>
        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                Log.Information("Intentando iniciar sesion para el usuario con email: {Email}", email);

                var user = _userService.Login(email, password);

                if (user == null)
                {
                    Log.Warning("Inicio de sesion fallido para el usuario con email: {Email}", email);
                    return Unauthorized("Autenticacion fallida. Credenciales incorrectas.");
                }

                Log.Information("Inicio de sesion exitoso para el usuario con email: {Email}", email);

                _loggedInUser = user;

                return Ok("Inicio de sesion exitoso.");
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al iniciar sesion: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al iniciar sesion: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al iniciar sesion: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al iniciar sesion: {ex.Message}");
            }
        }

        /// <summary>
        /// Log out of a user registered in the system.
        /// </summary>
        /// <param name="user">The user who logs out.</param>
        /// <returns>HTTP response indicating the result of the logout.</returns>
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] User user)
        {
            try
            {
                Log.Information("Intentando cerrar sesion para el usuario con ID: {UserID}", user.UserID);

                _userService.Logout(user);

                Log.Information("Sesion cerrada exitosamente para el usuario con ID: {UserID}", user.UserID);

                return Ok("Se cerro la sesion correctamente.");
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al cerrar sesion: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al cerrar sesion: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al cerrar sesion: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al cerrar sesion: {ex.Message}");
            }
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
        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            try
            {
                Log.Information("Intentando eliminar el usuario con ID: {UserID}", id);

                var loggedInUser = _userService.GetLoggedInUser();
                if (loggedInUser == null || loggedInUser.Role != "Administrador")
                {
                    return BadRequest("No tiene permiso para eliminar usuarios.");
                }

                _userService.DeleteUser(id);

                Log.Information("Usuario eliminado exitosamente con ID: {UserID}", id);

                return Ok("Usuario eliminado exitosamente");
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al eliminar el usuario: {ErrorMessage}", ex.Message);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al eliminar el usuario: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al eliminar el usuario: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets all purchases made in the system.
        /// </summary>
        /// <remarks>
        /// Allows a user with administrator permissions to obtain all purchases made in the system.
        /// </remarks>
        /// <response code="200">The purchases were successfully obtained.</response>
        /// <response code="400">Error in requesting or obtaining purchases.</response>
        /// <response code="403">You do not have permission to access all purchases (non-admin users only).</response>
        [HttpGet("AllPurchases")]
        public IActionResult GetAllPurchases()
        {
            try
            {
                Log.Information("Intentando obtener todas las compras.");

                var loggedInUser = _userService.GetLoggedInUser();
                if (loggedInUser == null || loggedInUser.Role != "Administrador")
                {
                    return BadRequest("No tiene permiso para acceder a todas las compras.");
                }
                var purchases = _userService.GetAllPurchases();

                Log.Information("Compras obtenidas exitosamente.");

                return Ok(purchases);
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al obtener compras: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al obtener compras: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al obtener compras: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al obtener compras: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtains the purchase history of a user registered in the system by their ID.
        /// </summary>
        /// <remarks>
        /// Allows a user to obtain the purchase history of a user registered in the system by their ID.
        /// </remarks>
        /// <param name="id">The ID of the user for whom you want to obtain the purchase history.</param>
        /// <response code="200">The purchase history was successfully obtained.</response>
        /// <response code="400">Error in the request or obtaining the purchase history.</response>
        /// <response code="404">User with the specified ID not found.</response>
        [HttpGet("GetPurchaseHistory/{id}")]
        public IActionResult GetPurchaseHistory([FromRoute] int id)
        {
            try
            {
                Log.Information("Intentando obtener el historial de compras del usuario con ID: {UserID}", id);

                var loggedInUser = _userService.GetLoggedInUser();
                if (loggedInUser == null)
                {
                    return BadRequest("Debe iniciar sesion para acceder al historial de compras.");
                }

                var user = _userService.GetUserByID(id);

                if (user == null)
                {
                    return NotFound($"Usuario con ID {id} no encontrado.");
                }

                var purchaseHistory = _userService.GetPurchaseHistory(user);

                Log.Information("Historial de compras obtenido exitosamente para el usuario con ID: {UserID}", id);

                return Ok(purchaseHistory); 
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al obtener el historial de compras: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al obtener el historial de compras: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al obtener el historial de compras: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al obtener el historial de compras: {ex.Message}");
            }
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
        [HttpPut("UpdateUserProfile")]
        public IActionResult UpdateUserProfile([FromBody] User user)
        {
            try
            {
                var loggedInUser = _userService.GetLoggedInUser();
                if (loggedInUser == null || loggedInUser.UserID != user.UserID)
                {
                    return BadRequest("El id del usuario a actualizar no coincide con el usuario logeado.");
                }

                Log.Information("Intentando actualizar el perfil del usuario con ID: {UserID}", user.UserID);

                var updatedUser = _userService.UpdateUserProfile(user);

                Log.Information("Perfil del usuario actualizado exitosamente para el usuario con ID: {UserID}", user.UserID);

                return Ok(updatedUser);
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al actualizar el perfil del usuario: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al actualizar el perfil del usuario: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al actualizar el perfil del usuario: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al actualizar el perfil del usuario: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the information of a user registered in the system.
        /// </summary>
        /// <remarks>
        /// Allows an administrator user to update a user's information by their ID.
        /// </remarks>
        /// <param name="user">The user data to update.</param>
        /// <response code="200">The user information was updated successfully.</response>
        /// <response code="400">Error in the request or updating the information.</response>
        /// <response code="401">You do not have permission to update user information.</response>
        [HttpPut("UpdateUserInformation")]
        public IActionResult UpdateUserInformation([FromBody] User user)
        {
            try
            {
                var loggedInUser = _userService.GetLoggedInUser();
                if (loggedInUser == null || loggedInUser.Role != "Administrador")
                {
                    return Unauthorized("No tiene permiso para actualizar la informacion del usuario.");
                }

                var updatedUser = _userService.UpdateUserInformation(user);

                if (updatedUser == null)
                {
                    return BadRequest("Error al actualizar la informacion del usuario.");
                }

                return Ok(updatedUser); 
            }
            catch (UserException ex)
            {
                return BadRequest($"Error al actualizar la informacion del usuario: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error inesperado al actualizar la informacion del usuario: {ex.Message}");
            }
        }
    }
}
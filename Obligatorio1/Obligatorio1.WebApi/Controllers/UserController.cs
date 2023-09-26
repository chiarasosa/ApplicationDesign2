using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace Obligatorio1.WebApi
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Registra un nuevo usuario",
            Description = "Registra un nuevo usuario en el sistema.")]
        [ProducesResponseType(typeof(string), 200)] // Especifica el tipo de respuesta para el código 200 (OK)
        [ProducesResponseType(typeof(string), 400)] // Especifica el tipo de respuesta para el código 400 (BadRequest)
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

        [HttpGet]
        [SwaggerOperation(
         Summary = "Obtiene la lista de usuarios",
         Description = "Obtiene todos los usuarios registrados en el sistema.")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)] // Especifica el tipo de respuesta para el código 200 (OK)
        [ProducesResponseType(typeof(string), 400)] // Especifica el tipo de respuesta para el código 400 (BadRequest)
        public IActionResult GetAllUsers()
        {
            try
            {
                // Obtener todos los usuarios desde el servicio
                var users = _userService.GetAllUsers();

                // Devolver los usuarios en una respuesta HTTP 200 OK
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

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtiene un usuario por su ID",
            Description = "Obtiene un usuario registrado en el sistema por su ID.")]
        [ProducesResponseType(typeof(User), 200)] // Especifica el tipo de respuesta para el código 200 (OK)
        [ProducesResponseType(typeof(string), 404)] // Especifica el tipo de respuesta para el código 404 (NotFound)
        [ProducesResponseType(typeof(string), 400)] // Especifica el tipo de respuesta para el código 400 (BadRequest)
        public IActionResult GetUserByID([FromRoute] int id)
        {
            try
            {
                // Obtener un usuario por su ID desde el servicio
                var user = _userService.GetUserByID(id);

                if (user == null)
                {
                    return NotFound($"Usuario con ID {id} no encontrado");
                }

                // Devolver el usuario en una respuesta HTTP 200 OK
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

        [HttpPost("login")]
        [SwaggerOperation(
           Summary = "Inicia sesión de usuario",
           Description = "Inicia sesión de un usuario registrado en el sistema.")]
        [ProducesResponseType(typeof(string), 200)] // Especifica el tipo de respuesta para el código 200 (OK)
        [ProducesResponseType(typeof(string), 401)] // Especifica el tipo de respuesta para el código 401 (Unauthorized)
        [ProducesResponseType(typeof(string), 400)] // Especifica el tipo de respuesta para el código 400 (BadRequest)
        public IActionResult Login(string email, string password)
        {
            try
            {
                Log.Information("Intentando iniciar sesión para el usuario con email: {Email}", email);

                var user = _userService.Login(email, password);

                if (user == null)
                {
                    Log.Warning("Inicio de sesión fallido para el usuario con email: {Email}", email);
                    return Unauthorized("Autenticación fallida. Credenciales incorrectas");
                }

                Log.Information("Inicio de sesión exitoso para el usuario con email: {Email}", email);

                return Ok("Inicio de sesión exitoso");
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al iniciar sesión: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al iniciar sesión: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al iniciar sesión: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al iniciar sesión: {ex.Message}");
            }
        }

        [HttpPost("logout")]
        [SwaggerOperation(
          Summary = "Cierra sesión de usuario",
          Description = "Cierra sesión de un usuario registrado en el sistema.")]
        [ProducesResponseType(204)] // NoContent
        [ProducesResponseType(typeof(string), 400)] // Especifica el tipo de respuesta para el código 400 (BadRequest)
        public IActionResult Logout([FromBody] User user)
        {
            try
            {
                Log.Information("Intentando cerrar sesión para el usuario con ID: {UserID}", user.UserID);

                _userService.Logout(user);

                Log.Information("Sesión cerrada exitosamente para el usuario con ID: {UserID}", user.UserID);

                return NoContent(); // Devuelve una respuesta HTTP 204 No Content
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al cerrar sesión: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al cerrar sesión: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al cerrar sesión: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al cerrar sesión: {ex.Message}");
            }
        }

        [HttpPost("create")]
        [SwaggerOperation(
          Summary = "Crea un nuevo usuario",
          Description = "Crea un nuevo usuario en el sistema.")]
        [ProducesResponseType(typeof(User), 201)] // Especifica el tipo de respuesta para el código 201 (Created)
        [ProducesResponseType(typeof(string), 400)] // Especifica el tipo de respuesta para el código 400 (BadRequest)
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                Log.Information("Intentando crear un nuevo usuario: {@User}", user);

                // Verifica si el usuario actual tiene permisos de administrador
                var loggedInUser = _userService.GetLoggedInUser();
                if (loggedInUser == null || loggedInUser.Role != "Administrador")
                {
                    throw new UserException("No tiene permiso para crear usuarios.");
                }

                // Llama al método CreateUser del servicio para crear el usuario
                var createdUser = _userService.CreateUser(user);

                Log.Information("Usuario creado exitosamente: {@CreatedUser}", createdUser);

                // Devuelve el usuario creado en una respuesta HTTP 201 Created
                return CreatedAtAction(nameof(GetUserByID), new { id = createdUser.UserID }, createdUser);
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al crear el usuario: {ErrorMessage}", ex.Message);

                // Devuelve un resultado BadRequest con el mensaje de la excepción
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al crear el usuario: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al crear el usuario: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Elimina un usuario por su ID",
            Description = "Elimina un usuario registrado en el sistema por su ID.")]
        [ProducesResponseType(204)] // NoContent
        [ProducesResponseType(typeof(string), 404)] // Especifica el tipo de respuesta para el código 404 (NotFound)
        [ProducesResponseType(typeof(string), 400)] // Especifica el tipo de respuesta para el código 400 (BadRequest)
        public IActionResult DeleteUser([FromRoute] int id)
        {
            try
            {
                Log.Information("Intentando eliminar el usuario con ID: {UserID}", id);

                // Verifica si el usuario actual tiene permisos de administrador
                var loggedInUser = _userService.GetLoggedInUser();
                if (loggedInUser == null || loggedInUser.Role != "Administrador")
                {
                    return BadRequest("No tiene permiso para eliminar usuarios.");
                }

                // Llama al método DeleteUser del servicio para eliminar el usuario
                _userService.DeleteUser(id);

                Log.Information("Usuario eliminado exitosamente con ID: {UserID}", id);

                return NoContent(); // Devuelve una respuesta HTTP 204 No Content
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al eliminar el usuario: {ErrorMessage}", ex.Message);

                return NotFound(ex.Message); // Devuelve un resultado NotFound con el mensaje de la excepción
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al eliminar el usuario: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al eliminar el usuario: {ex.Message}");
            }
        }

        [HttpGet("AllPurchases")]
        [SwaggerOperation(
    Summary = "Obtiene todas las compras",
    Description = "Obtiene todas las compras realizadas en el sistema.")]
        [ProducesResponseType(typeof(IEnumerable<Purchase>), 200)] // OK
        [ProducesResponseType(typeof(string), 400)] // BadRequest
        [ProducesResponseType(typeof(string), 403)] // Forbidden (para usuarios no administradores)
        public IActionResult GetAllPurchases()
        {
            try
            {
                Log.Information("Intentando obtener todas las compras.");

                // Verifica si el usuario actual tiene permisos de administrador
                var loggedInUser = _userService.GetLoggedInUser();
                if (loggedInUser == null || loggedInUser.Role != "Administrador")
                {
                    return BadRequest("No tiene permiso para acceder a todas las compras.");
                }

                // Llama al método GetAllPurchases del servicio para obtener todas las compras
                var purchases = _userService.GetAllPurchases();

                Log.Information("Compras obtenidas exitosamente.");

                return Ok(purchases); // Devuelve una respuesta HTTP 200 OK con la lista de compras
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

        [HttpGet("GetPurchaseHistory/{id}")]
        [SwaggerOperation(
            Summary = "Obtiene el historial de compras de un usuario.",
            Description = "Obtiene el historial de compras de un usuario registrado en el sistema por su ID.")]
        [ProducesResponseType(typeof(IEnumerable<Purchase>), 200)] // OK
        [ProducesResponseType(typeof(string), 400)] // BadRequest
        [ProducesResponseType(typeof(string), 404)] // NotFound
        public IActionResult GetPurchaseHistory([FromRoute] int id)
        {
            try
            {
                Log.Information("Intentando obtener el historial de compras del usuario con ID: {UserID}", id);

                // Verifica si el usuario actual tiene permisos para acceder al historial de compras
                var loggedInUser = _userService.GetLoggedInUser();
                if (loggedInUser == null)
                {
                    return BadRequest("Debe iniciar sesión para acceder al historial de compras.");
                }

                // Llama al método GetPurchaseHistory del servicio para obtener el historial de compras del usuario
                var user = _userService.GetUserByID(id);

                if (user == null)
                {
                    return NotFound($"Usuario con ID {id} no encontrado.");
                }

                var purchaseHistory = _userService.GetPurchaseHistory(user);

                Log.Information("Historial de compras obtenido exitosamente para el usuario con ID: {UserID}", id);

                return Ok(purchaseHistory); // Devuelve una respuesta HTTP 200 OK con el historial de compras
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
    }
}


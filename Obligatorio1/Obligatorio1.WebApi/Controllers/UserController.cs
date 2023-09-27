using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace Obligatorio1.WebApi
{
    /// <summary>
    /// Controlador para la gesti�n de usuarios.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor del controlador de usuarios.
        /// </summary>
        /// <param name="userService">El servicio de usuarios.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="user">Los datos del usuario a registrar.</param>
        /// <returns>Respuesta HTTP indicando el resultado del registro.</returns>
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
        /// Obtiene la lista de todos los usuarios registrados en el sistema.
        /// </summary>
        ///<remarks>
        /// Permite a un usuario con permisos de administrador obtener la lista de todos los usuarios registrados en el sistema.
        /// </remarks>
        /// <returns>Respuesta HTTP con la lista de usuarios.</returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                // Obtener el usuario autenticado
                var loggedInUser = _userService.GetLoggedInUser();

                if (loggedInUser == null || loggedInUser.Role != "Administrador")
                {
                    return Unauthorized("No tiene permiso para obtener la lista de usuarios.");
                }

                // Obtener todos los usuarios desde el servicio
                var users = _userService.GetUsers();

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


        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario a obtener.</param>
        /// <returns>Respuesta HTTP con el usuario encontrado.</returns>
        [HttpGet("{id}")]
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

        /// <summary>
        /// Inicia sesi�n de un usuario registrado en el sistema.
        /// </summary>
        /// <param name="email">El correo electr�nico del usuario.</param>
        /// <param name="password">La contrase�a del usuario.</param>
        /// <returns>Respuesta HTTP indicando el resultado del inicio de sesi�n.</returns>
        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                Log.Information("Intentando iniciar sesi�n para el usuario con email: {Email}", email);

                var user = _userService.Login(email, password);

                if (user == null)
                {
                    Log.Warning("Inicio de sesi�n fallido para el usuario con email: {Email}", email);
                    return Unauthorized("Autenticaci�n fallida. Credenciales incorrectas");
                }

                Log.Information("Inicio de sesi�n exitoso para el usuario con email: {Email}", email);

                return Ok("Inicio de sesi�n exitoso");
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al iniciar sesi�n: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al iniciar sesi�n: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al iniciar sesi�n: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al iniciar sesi�n: {ex.Message}");
            }
        }

        /// <summary>
        /// Cierra sesi�n de un usuario registrado en el sistema.
        /// </summary>
        /// <param name="user">El usuario que cierra sesi�n.</param>
        /// <returns>Respuesta HTTP indicando el resultado del cierre de sesi�n.</returns>
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] User user)
        {
            try
            {
                Log.Information("Intentando cerrar sesi�n para el usuario con ID: {UserID}", user.UserID);

                _userService.Logout(user);

                Log.Information("Sesi�n cerrada exitosamente para el usuario con ID: {UserID}", user.UserID);

                return NoContent(); // Devuelve una respuesta HTTP 204 No Content
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al cerrar sesi�n: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al cerrar sesi�n: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al cerrar sesi�n: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al cerrar sesi�n: {ex.Message}");
            }
        }


        /// <summary>
        /// Crea un nuevo usuario en el sistema.
        /// </summary>
        /// <remarks>
        /// Permite a un usuario con permisos de administrador crear un nuevo usuario en el sistema.
        /// </remarks>
        /// <param name="user">Los datos del usuario a crear.</param>
        /// <response code="201">El usuario se cre� con �xito.</response>
        /// <response code="400">Error en la solicitud o al crear el usuario.</response>
        [HttpPost("create")]
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

                // Llama al m�todo CreateUser del servicio para crear el usuario
                var createdUser = _userService.CreateUser(user);

                Log.Information("Usuario creado exitosamente: {@CreatedUser}", createdUser);

                // Devuelve el usuario creado en una respuesta HTTP 201 Created
                return CreatedAtAction(nameof(GetUserByID), new { id = createdUser.UserID }, createdUser);
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al crear el usuario: {ErrorMessage}", ex.Message);

                // Devuelve un resultado BadRequest con el mensaje de la excepci�n
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al crear el usuario: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al crear el usuario: {ex.Message}");
            }
        }


        /// <summary>
        /// Elimina un usuario registrado en el sistema por su ID.
        /// </summary>
        /// <remarks>
        /// Permite a un usuario con permisos de administrador eliminar un usuario por su ID.
        /// </remarks>
        /// <param name="id">El ID del usuario a eliminar.</param>
        /// <response code="204">El usuario se elimin� con �xito.</response>
        /// <response code="404">El usuario con el ID especificado no se encontr�.</response>
        /// <response code="400">Error en la solicitud o al eliminar el usuario.</response>
        [HttpDelete("{id}")]
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

                // Llama al m�todo DeleteUser del servicio para eliminar el usuario
                _userService.DeleteUser(id);

                Log.Information("Usuario eliminado exitosamente con ID: {UserID}", id);

                return NoContent(); // Devuelve una respuesta HTTP 204 No Content
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al eliminar el usuario: {ErrorMessage}", ex.Message);

                return NotFound(ex.Message); // Devuelve un resultado NotFound con el mensaje de la excepci�n
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado al eliminar el usuario: {ErrorMessage}", ex.Message);

                return BadRequest($"Error inesperado al eliminar el usuario: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene todas las compras realizadas en el sistema.
        /// </summary>
        /// <remarks>
        /// Permite a un usuario con permisos de administrador obtener todas las compras realizadas en el sistema.
        /// </remarks>
        /// <response code="200">Las compras se obtuvieron exitosamente.</response>
        /// <response code="400">Error en la solicitud o al obtener las compras.</response>
        /// <response code="403">No tiene permiso para acceder a todas las compras (solo para usuarios no administradores).</response>
        [HttpGet("AllPurchases")]
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

                // Llama al m�todo GetAllPurchases del servicio para obtener todas las compras
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

        /// <summary>
        /// Obtiene el historial de compras de un usuario registrado en el sistema por su ID.
        /// </summary>
        /// <remarks>
        /// Permite a un usuario obtener el historial de compras de un usuario registrado en el sistema por su ID.
        /// </remarks>
        /// <param name="id">El ID del usuario del cual se desea obtener el historial de compras.</param>
        /// <response code="200">El historial de compras se obtuvo exitosamente.</response>
        /// <response code="400">Error en la solicitud o al obtener el historial de compras.</response>
        /// <response code="404">Usuario con el ID especificado no encontrado.</response>
        [HttpGet("GetPurchaseHistory/{id}")]
        public IActionResult GetPurchaseHistory([FromRoute] int id)
        {
            try
            {
                Log.Information("Intentando obtener el historial de compras del usuario con ID: {UserID}", id);

                // Verifica si el usuario actual tiene permisos para acceder al historial de compras
                var loggedInUser = _userService.GetLoggedInUser();
                if (loggedInUser == null)
                {
                    return BadRequest("Debe iniciar sesi�n para acceder al historial de compras.");
                }

                // Llama al m�todo GetPurchaseHistory del servicio para obtener el historial de compras del usuario
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

        /// <summary>
        /// Actualiza el perfil de un usuario registrado en el sistema.
        /// </summary>
        /// <remarks>
        /// Permite a un usuario registrado actualizar su perfil con nuevos datos.
        /// </remarks>
        /// <param name="user">Los datos del usuario a actualizar.</param>
        /// <response code="200">La informaci�n del usuario se actualiz� con �xito.</response>
        /// <response code="400">Error en la solicitud o al actualizar la informaci�n del usuario.</response>
        [HttpPut("UpdateUserProfile")]
        public IActionResult UpdateUserProfile([FromBody] User user)
        {
            try
            {
                Log.Information("Intentando actualizar el perfil del usuario con ID: {UserID}", user.UserID);

                // Llama al m�todo UpdateUserProfile del servicio para actualizar el perfil del usuario
                var updatedUser = _userService.UpdateUserProfile(user);

                Log.Information("Perfil del usuario actualizado exitosamente para el usuario con ID: {UserID}", user.UserID);

                return Ok(updatedUser); // Devuelve una respuesta HTTP 200 OK con el usuario actualizado
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
        /// Actualiza la informaci�n de un usuario.
        /// </summary>
        /// <remarks>
        /// Permite a un usuario administrador actualizar la informaci�n de otro usuario por su ID.
        /// </remarks>
        /// <param name="user">Los datos del usuario a actualizar.</param>
        /// <response code="200">La informaci�n del usuario se actualiz� con �xito.</response>
        /// <response code="400">Error en la solicitud o al actualizar la informaci�n.</response>
        /// <response code="401">No tiene permiso para actualizar la informaci�n del usuario.</response>
        [HttpPut("UpdateUserInformation")]

        public IActionResult UpdateUserInformation([FromBody] User user)
        {
            try
            {
                // Verifica si el usuario autenticado es un administrador.
                var loggedInUser = _userService.GetLoggedInUser();
                if (loggedInUser == null || loggedInUser.Role != "Administrador")
                {
                    return Unauthorized("No tiene permiso para actualizar la informaci�n del usuario.");
                }

                // Intenta actualizar la informaci�n del usuario a trav�s del servicio de usuarios.
                var updatedUser = _userService.UpdateUserInformation(user);

                if (updatedUser == null)
                {
                    return BadRequest("Error al actualizar la informaci�n del usuario.");
                }

                return Ok(updatedUser); // Devuelve una respuesta HTTP 200 OK con el usuario actualizado
            }
            catch (UserException ex)
            {
                return BadRequest($"Error al actualizar la informaci�n del usuario: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error inesperado al actualizar la informaci�n del usuario: {ex.Message}");
            }
        }



    }
}




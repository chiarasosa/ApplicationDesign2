using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace Obligatorio1.WebApi
{
    /// <summary>
    /// Controlador para la gestion de usuarios.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor del controlador de usuarios.
        /// </summary>
        /// <param name="userService">El servicio de usuarios.</param>
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
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
                var loggedInUserID = _httpContextAccessor.HttpContext.Session.GetString("LoggedInUserId");//_userService.GetLoggedInUser();
                                                                                                        // Obtén el usuario logeado desde el servicio
                var loggedInUser = _userService.GetUserByID(int.Parse(loggedInUserID));


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
                    return NotFound($"Usuario con ID {id} no encontrado.");
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
        /// Inicia sesion de un usuario registrado en el sistema.
        /// </summary>
        /// <param name="email">El correo electronico del usuario.</param>
        /// <param name="password">La contrasenia del usuario.</param>
        /// <returns>Respuesta HTTP indicando el resultado del inicio de sesion.</returns>
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

                // Almacena el usuario logeado en la sesión
                _httpContextAccessor.HttpContext.Session.SetString("LoggedInUserId", user.UserID.ToString());

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
        /// Obtiene el usuario actualmente logeado en el sistema.
        /// </summary>
        /// <returns>Respuesta HTTP con el usuario logeado.</returns>
        [HttpGet("loggedinuser")]
        public IActionResult GetLoggedInUser()
        {
            try
            {
                // Recupera el ID del usuario logeado desde la sesión
                var loggedInUserId = _httpContextAccessor.HttpContext.Session.GetString("LoggedInUserId");

                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    // No hay usuario logeado en la sesión
                    return NotFound("No hay usuario logeado.");
                }

                // Obtén el usuario logeado desde el servicio
                var user = _userService.GetUserByID(int.Parse(loggedInUserId));

                if (user == null)
                {
                    return NotFound("Usuario logeado no encontrado.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el usuario logeado: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al obtener el usuario logeado: {ex.Message}");
            }
        }

        /// <summary>
        /// Cierra sesion de un usuario registrado en el sistema.
        /// </summary>
        /// <param name="user">El usuario que cierra sesion.</param>
        /// <returns>Respuesta HTTP indicando el resultado del cierre de sesion.</returns>
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] User user)
        {
            try
            {
                Log.Information("Intentando cerrar sesion para el usuario con ID: {UserID}", user.UserID);
                
                HttpContext.Session.Clear();
                
                _userService.Logout(user);

                Log.Information("Sesion cerrada exitosamente para el usuario con ID: {UserID}", user.UserID);

                return Ok("Sesión cerrada exitosamente."); // Devuelve una respuesta HTTP 204 No Content
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
        /// Crea un nuevo usuario en el sistema.
        /// </summary>
        /// <remarks>
        /// Permite a un usuario con permisos de administrador crear un nuevo usuario en el sistema.
        /// </remarks>
        /// <param name="user">Los datos del usuario a crear.</param>
        /// <response code="201">El usuario se creo con exito.</response>
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
        /// <response code="204">El usuario se elimino con exito.</response>
        /// <response code="404">El usuario con el ID especificado no se encontro.</response>
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

                // Llama al metodo DeleteUser del servicio para eliminar el usuario
                _userService.DeleteUser(id);

                Log.Information("Usuario eliminado exitosamente con ID: {UserID}", id);

                return NoContent(); // Devuelve una respuesta HTTP 204 No Content
            }
            catch (UserException ex)
            {
                Log.Error(ex, "Error al eliminar el usuario: {ErrorMessage}", ex.Message);

                return NotFound(ex.Message); // Devuelve un resultado NotFound con el mensaje de la excepcion
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
                    return BadRequest("Debe iniciar sesion para acceder al historial de compras.");
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
        /// <response code="200">La informacion del usuario se actualizo con exito.</response>
        /// <response code="400">Error en la solicitud o al actualizar la informacion del usuario.</response>
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
        /// Actualiza la informacion de un usuario.
        /// </summary>
        /// <remarks>
        /// Permite a un usuario administrador actualizar la informacion de otro usuario por su ID.
        /// </remarks>
        /// <param name="user">Los datos del usuario a actualizar.</param>
        /// <response code="200">La informacion del usuario se actualizo con exito.</response>
        /// <response code="400">Error en la solicitud o al actualizar la informacion.</response>
        /// <response code="401">No tiene permiso para actualizar la informacion del usuario.</response>
        [HttpPut("UpdateUserInformation")]
        public IActionResult UpdateUserInformation([FromBody] User user)
        {
            try
            {
                // Verifica si el usuario autenticado es un administrador.
                var loggedInUser = _userService.GetLoggedInUser();
                if (loggedInUser == null || loggedInUser.Role != "Administrador")
                {
                    return Unauthorized("No tiene permiso para actualizar la informacion del usuario.");
                }

                // Intenta actualizar la informaci�n del usuario a trav�s del servicio de usuarios.
                var updatedUser = _userService.UpdateUserInformation(user);

                if (updatedUser == null)
                {
                    return BadRequest("Error al actualizar la informacion del usuario.");
                }

                return Ok(updatedUser); // Devuelve una respuesta HTTP 200 OK con el usuario actualizado
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
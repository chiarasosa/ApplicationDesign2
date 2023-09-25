using Microsoft.AspNetCore.Mvc;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using Obligatorio1.Exceptions;

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
        [ProducesResponseType(typeof(string), 200)] // Especifica el tipo de respuesta para el c�digo 200 (OK)
        [ProducesResponseType(typeof(string), 400)] // Especifica el tipo de respuesta para el c�digo 400 (BadRequest)
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
        [ProducesResponseType(typeof(IEnumerable<User>), 200)] // Especifica el tipo de respuesta para el c�digo 200 (OK)
        [ProducesResponseType(typeof(string), 400)] // Especifica el tipo de respuesta para el c�digo 400 (BadRequest)
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
        [ProducesResponseType(typeof(User), 200)] // Especifica el tipo de respuesta para el c�digo 200 (OK)
        [ProducesResponseType(typeof(string), 404)] // Especifica el tipo de respuesta para el c�digo 404 (NotFound)
        [ProducesResponseType(typeof(string), 400)] // Especifica el tipo de respuesta para el c�digo 400 (BadRequest)
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
           Summary = "Inicia sesi�n de usuario",
           Description = "Inicia sesi�n de un usuario registrado en el sistema.")]
        [ProducesResponseType(typeof(string), 200)] // Especifica el tipo de respuesta para el c�digo 200 (OK)
        [ProducesResponseType(typeof(string), 401)] // Especifica el tipo de respuesta para el c�digo 401 (Unauthorized)
        [ProducesResponseType(typeof(string), 400)] // Especifica el tipo de respuesta para el c�digo 400 (BadRequest)
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

        [HttpPost("logout")]
        [SwaggerOperation(
          Summary = "Cierra sesi�n de usuario",
          Description = "Cierra sesi�n de un usuario registrado en el sistema.")]
        [ProducesResponseType(204)] // NoContent
        [ProducesResponseType(typeof(string), 400)] // Especifica el tipo de respuesta para el c�digo 400 (BadRequest)
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
    }
}

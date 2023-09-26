using Microsoft.AspNetCore.Mvc;
using Obligatorio1.Domain;
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
                var users = _userService.GetUsers();

                // Devolver los usuarios en una respuesta HTTP 200 OK
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Manejar errores y devolver una respuesta de error si es necesario
                return BadRequest($"Error al obtener usuarios: {ex.Message}");
            }
        }

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
            catch (Exception ex)
            {
                // Manejar errores y devolver una respuesta de error si es necesario
                return BadRequest($"Error al obtener el usuario: {ex.Message}");
            }
        }

        [HttpPost("login")]
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
            catch (Exception ex)
            {
                Log.Error(ex, "Error al iniciar sesión para el usuario con email: {Email}", email);
                return BadRequest($"Error al iniciar sesión: {ex.Message}");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;
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
                var users = _userService.GetAllUsers();

                // Devolver los usuarios en una respuesta HTTP 200 OK
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Manejar errores y devolver una respuesta de error si es necesario
                return BadRequest($"Error al obtener usuarios: {ex.Message}");
            }
        }

    }
}

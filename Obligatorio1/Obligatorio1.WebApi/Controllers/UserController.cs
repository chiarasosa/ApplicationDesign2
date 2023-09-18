using Microsoft.AspNetCore.Mvc;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;
using Serilog;

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

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            try
            {
                // Agregar registro antes de llamar a RegisterUser
                Log.Information("Intentando registrar usuario: {@User}", user);

                _userService.RegisterUser(user);

                // Agregar registro después de RegisterUser
                Log.Information("Usuario registrado exitosamente.");
                return Ok("Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                // Agregar registro de excepción
                Log.Error(ex, "Error al registrar el usuario: {ErrorMessage}", ex.Message);

                return BadRequest($"Error al registrar el usuario: {ex.Message}");
            }
        }
    }
}

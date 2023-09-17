using Microsoft.AspNetCore.Mvc;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;
using Obligatorio1.BusinessLogic;
using Obligatorio1.IDataAccess;
using Obligatorio1.DataAccess;

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
            _userService.RegisterUser(user);
            return Ok("Usuario registrado exitosamente.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al registrar el usuario: {ex.Message}");
        }
    }
}

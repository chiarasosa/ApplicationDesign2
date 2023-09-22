using Microsoft.AspNetCore.Mvc;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi;

[TestClass]
public class UserControllerTest
{
    private UserController _controller;
    private Mock<IUserService> _serviceMock;

    [TestInitialize]
    public void Setup()
    {
        // Configurar el servicio mock
        _serviceMock = new Mock<IUserService>();

        // Configurar el controlador con el servicio mock
        _controller = new UserController(_serviceMock.Object);
    }

    [TestMethod]
    public void RegisterUser_ValidUser_ReturnsOkResult()
    {
        // Arrange
        var user = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);

        // Act
        var result = _controller.RegisterUser(user);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        _serviceMock.Verify(s => s.RegisterUser(user), Times.Once);
    }

    [TestMethod]
    public void RegisterUser_InvalidUser_ReturnsBadRequest()
    {
        // Arrange
        var invalidUser = new User(1, "", "", "", "", "", null);

        // Configura el servicio simulado para lanzar UserException
        _serviceMock.Setup(s => s.RegisterUser(invalidUser)).Throws(new UserException("Usuario inválido"));

        // Act
        var result = _controller.RegisterUser(invalidUser);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        _serviceMock.Verify(s => s.RegisterUser(invalidUser), Times.Once);
    }

    [TestMethod]
    public void GetUser_ReturnsListOfUsers()
    {
        // Arrange
        var users = new List<User>
    {
        new User(1, "Usuario1", "Password1", "usuario1@example.com", "Dirección1", "Rol1", null),
        new User(2, "Usuario2", "Password2", "usuario2@example.com", "Dirección2", "Rol2", null)
    };

        // Configura el servicio simulado para devolver la lista de usuarios
        _serviceMock.Setup(s => s.GetAllUsers()).Returns(users);

        // Act
        var result = _controller.GetAllUsers();

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = (OkObjectResult)result;
        var resultUsers = okResult.Value as List<User>;
        Assert.IsNotNull(resultUsers);
        Assert.AreEqual(users.Count, resultUsers.Count);
    }

    [TestMethod]
    public void GetUser_ErrorInService_ReturnsBadRequest()
    {
        // Configura el servicio simulado para lanzar una excepción al obtener usuarios
        _serviceMock.Setup(s => s.GetAllUsers()).Throws(new Exception("Error al obtener usuarios"));

        // Act
        var result = _controller.GetAllUsers();

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        var badRequestResult = (BadRequestObjectResult)result;
        Assert.AreEqual("Error al obtener usuarios: Error al obtener usuarios", badRequestResult.Value);
    }
}

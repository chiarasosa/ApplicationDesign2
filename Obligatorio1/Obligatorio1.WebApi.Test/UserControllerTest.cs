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
    
}

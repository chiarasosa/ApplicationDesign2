using Microsoft.AspNetCore.Mvc;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;

namespace Obligatorio1.WebApi.Test
{
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
        public void GetAllUser_ReturnsListOfUsers()
        {
            // Arrange
            var userAdmin = new User(1, "Usuario1", "Password1", "usuario1@example.com", "Direcci�n1", "Administrador", null);
            var users = new List<User>
    {
        new User(1, "Usuario1", "Password1", "usuario1@example.com", "Direcci�n1", "Rol1", null),
        new User(2, "Usuario2", "Password2", "usuario2@example.com", "Direcci�n2", "Rol2", null)
    };

            _serviceMock.Setup(s => s.GetUserByID(userAdmin.UserID)).Returns(userAdmin);

            _serviceMock.Setup(s => s.GetUsers()).Returns(users);

            var result = _controller.GetAllUsers();

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            var resultUsers = okResult.Value as List<User>;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Moq;
using Obligatorio1.BusinessLogic;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.WebApi;


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
        public void GetAllUser_ReturnsListOfUsers()
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
        public void GetAllUser_ErrorInService_ReturnsBadRequest()
        {
            // Configura el servicio simulado para lanzar una excepción al obtener usuarios
            _serviceMock.Setup(s => s.GetAllUsers()).Throws(new Exception("Error al obtener usuarios"));

            // Act
            var result = _controller.GetAllUsers();

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("Error inesperado al obtener usuarios: Error al obtener usuarios", badRequestResult.Value);
        }

        [TestMethod]
        public void GetUserById_ExistingUser_ReturnsOkResult()
        {
            // Arrange
            int userId = 1;
            var user = new User(userId, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);

            // Configura el servicio simulado para devolver el usuario por su ID
            _serviceMock.Setup(s => s.GetUserByID(userId)).Returns(user);

            // Act
            var result = _controller.GetUserByID(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(user, okResult.Value);
        }

        [TestMethod]
        public void GetUserById_NonExistentUser_ReturnsNotFound()
        {
            // Arrange
            int userId = 1;

            // Configura el servicio simulado para devolver null, indicando que el usuario no existe
            _serviceMock.Setup(s => s.GetUserByID(userId)).Returns((User)null);

            // Act
            var result = _controller.GetUserByID(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual($"Usuario con ID {userId} no encontrado", notFoundResult.Value);
        }

        [TestMethod]
        public void GetUserById_ErrorInService_ReturnsBadRequest()
        {
            // Arrange
            int userId = 1;

            // Configura el servicio simulado para lanzar una excepción al obtener el usuario por su ID
            _serviceMock.Setup(s => s.GetUserByID(userId)).Throws(new UserException("Error al obtener el usuario"));

            // Act
            var result = _controller.GetUserByID(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual($"Error al obtener el usuario: Error al obtener el usuario", badRequestResult.Value);
        }

        [TestMethod]
        public void Login_ValidCredentials_ReturnsOkResult()
        {
            // Arrange
            string email = "agustin@gmail.com";
            string password = "Prueba123";

            var authenticatedUser = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);

            // Configura el servicio simulado para devolver el usuario al iniciar sesión
            _serviceMock.Setup(s => s.Login(email, password)).Returns(authenticatedUser);

            // Act
            var result = _controller.Login(email, password);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual("Inicio de sesión exitoso", okResult.Value);
        }

        [TestMethod]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            string email = "agustin@gmail.com";
            string password = "ContraseñaIncorrecta";

            // Configura el servicio simulado para devolver null, indicando credenciales incorrectas
            _serviceMock.Setup(s => s.Login(email, password)).Returns((User)null);

            // Act
            var result = _controller.Login(email, password);

            // Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
            var unauthorizedResult = (UnauthorizedObjectResult)result;
            Assert.AreEqual("Autenticación fallida. Credenciales incorrectas", unauthorizedResult.Value);
        }

        [TestMethod]
        public void Login_MissingEmailOrPassword_ReturnsUnauthorized()
        {
            // Arrange
            string email = ""; // Email en blanco
            string password = "Prueba123";

            // Act
            var result = _controller.Login(email, password);

            // Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
            var unauthorizedResult = (UnauthorizedObjectResult)result;
            Assert.AreEqual("Autenticación fallida. Credenciales incorrectas", unauthorizedResult.Value);
        }

        [TestMethod]
        public void Login_ErrorInService_ReturnsBadRequest()
        {
            // Arrange
            string email = "agustin@gmail.com";
            string password = "Prueba123";

            // Configura el servicio simulado para lanzar una excepción al iniciar sesión
            _serviceMock.Setup(s => s.Login(email, password)).Throws(new UserException("Error al iniciar sesión"));

            // Act
            var result = _controller.Login(email, password);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("Error al iniciar sesión: Error al iniciar sesión", badRequestResult.Value);
        }

        [TestMethod]
        public void Logout_ValidUser_ReturnsNoContent()
        {
            // Arrange
            var user = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);

            // Act
            var result = _controller.Logout(user);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            _serviceMock.Verify(s => s.Logout(user), Times.Once);
        }

        [TestMethod]
        public void Logout_ErrorInService_ReturnsBadRequest()
        {
            // Arrange
            var user = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);

            // Configura el servicio simulado para lanzar una excepción al hacer logout
            _serviceMock.Setup(s => s.Logout(user)).Throws(new UserException("Error al hacer logout"));

            // Act
            var result = _controller.Logout(user);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual($"Error al cerrar sesión: Error al hacer logout", badRequestResult.Value);
        }

        [TestMethod]
        public void CreateUser_AdminUser_ReturnsCreatedUser()
        {
            // Arrange
            var adminUser = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);
            var newUser = new User(2, "Pablo", "12123", "pablo@gmail.com", "Av. 18 de Julio 34", "Comprador", null);

            _serviceMock.Setup(s => s.GetLoggedInUser()).Returns(adminUser); // Mock GetLoggedInUser to return an admin user
            _serviceMock.Setup(s => s.CreateUser(newUser)).Returns(newUser); // Mock CreateUser to return the new user

            // Act
            var result = _controller.CreateUser(newUser);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            // Add assertions for the createdUser properties as needed

        }

        [TestMethod]
        public void CreateUser_NonAdminUser_ReturnsBadRequest()
        {
            // Arrange
            var nonAdminUser = new User(1, "NoAdmin", "Prueba123", "noadmin@gmail.com", "Calle 123", "Comprador", null);

            // Act
            var result = _controller.CreateUser(nonAdminUser);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("No tiene permiso para crear usuarios.", badRequestResult.Value);
        }

        [TestMethod]
        public void CreateUser_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var adminUser = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);
            var invalidUser = new User(2, "", "", "", "", "", null);

            _serviceMock.Setup(s => s.GetLoggedInUser()).Returns(adminUser); // Mock GetLoggedInUser to return an admin user
            _serviceMock.Setup(s => s.CreateUser(invalidUser))
                .Throws(new UserException("Usuario inválido.")); // Mock CreateUser to throw a UserException with the error message

            // Act
            var result = _controller.CreateUser(invalidUser);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("Usuario inválido.", badRequestResult.Value);
        }

        [TestMethod]
        public void DeleteUser_AdminUserWithPermission_DeletesUser()
        {
            // Arrange
            var adminUser = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);
            var userToDelete = new User(2, "Delete", "delete123", "delete@gmail.com", "Artigas 20", "Comprador", null);
            
            _serviceMock.Setup(s => s.GetLoggedInUser()).Returns(adminUser); // Mock GetLoggedInUser to return an admin user
            _serviceMock.Setup(s => s.DeleteUser(userToDelete.UserID)); // Mock DeleteUser

            // Act
            var result = _controller.DeleteUser(userToDelete.UserID);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            _serviceMock.Verify(s => s.DeleteUser(userToDelete.UserID), Times.Once);
        }
        [TestMethod]
        public void DeleteUser_AdminUserWithoutPermission_ThrowsException()
        {
            // Arrange
            var nonAdminUser = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Comprador", null);
            var userToDelete = new User(2, "Delete", "delete123", "delete@gmail.com", "Artigas 20", "Comprador", null);

            _serviceMock.Setup(s => s.GetLoggedInUser()).Returns(nonAdminUser); // Mock GetLoggedInUser to return a non-admin user

            // Act and Assert
            Assert.ThrowsException<UserException>(() => _controller.DeleteUser(userToDelete.UserID));
        }


        [TestMethod]
        public void DeleteUser_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var adminUser = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);
            var userToDeleteID = new User(2, "Delete", "delete123", "delete@gmail.com", "Artigas 20", "Comprador", null);

            _serviceMock.Setup(s => s.GetLoggedInUser()).Returns(adminUser); // Mock GetLoggedInUser to return an admin user
            _serviceMock.Setup(s => s.DeleteUser(userToDeleteID.UserID)); // Mock DeleteUser

            // Act
            var result = _controller.DeleteUser(userToDeleteID.UserID);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual($"Usuario con ID {userToDeleteID} no encontrado", notFoundResult.Value);
            _serviceMock.Verify(s => s.DeleteUser(userToDeleteID.UserID), Times.Never); // Ensure DeleteUser is not called
        }

    }
}
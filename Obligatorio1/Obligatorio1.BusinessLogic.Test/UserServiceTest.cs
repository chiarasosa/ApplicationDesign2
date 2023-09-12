using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private Mock<IUserManagment>? _userManagmentMock;
        private UserService? _userService;

        [TestInitialize]
        public void Initialize()
        {
            _userManagmentMock = new Mock<IUserManagment>(MockBehavior.Strict);
            _userService = new UserService(_userManagmentMock.Object);
        }

        [TestMethod]
        public void RegisterUserTest()
        {
            // Arrange
            User userAux = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);

            // Act
            _userManagmentMock?.Setup(x => x.RegisterUser(userAux));
            _userService?.RegisterUser(userAux);

            // Assert 
            _userManagmentMock?.VerifyAll();
        }

        [TestMethod]
       public void UpdateUserProfileTest()
        {
            // Arrange
            User user = new User(1, "Lautaro", "Lautaro292829", "lautaro@gmail.com", "Rivera 400", "Administrador", null);

            // Act
            _userManagmentMock?.Setup(x => x.UpdateUserProfile(user)).Returns(user);

            User? result = _userService?.UpdateUserProfile(user);

            // Assert
            _userManagmentMock?.Verify(x => x.UpdateUserProfile(user), Times.Once);
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void LoginValidCredentialsTest()
        {
            // Arrange
            string email = "testuser@example.com";
            string password = "Password123";
            User authenticatedUser = new User(1, "Lautaro", "Lautaro292829", "lautaro@gmail.com", "Rivera 400", "Administrador", null);

            // Configurar el comportamiento del mock para el inicio de sesión válido
            _userManagmentMock?.Setup(x => x.Login(email, password)).Returns(authenticatedUser);

            // Act
            User result = _userService?.Login(email, password);

            // Assert
            _userManagmentMock?.Verify(x => x.Login(email, password), Times.Once);
            Assert.AreEqual(authenticatedUser, result);
        }

        [TestMethod]
        public void LoginTestInvalidCredentialsTest()
        {
            // Arrange
            string email = "testuser@example.com";
            string password = "IncorrectPassword";

            // Configurar el comportamiento del mock para el inicio de sesión inválido (retorna null)
            _userManagmentMock?.Setup(x => x.Login(email, password)).Returns((User)null);

            // Act
            User result = _userService?.Login(email, password);

            // Assert
            _userManagmentMock?.Verify(x => x.Login(email, password), Times.Once);
            Assert.IsNull(result); // Debería ser nulo ya que las credenciales son inválidas
        }
    }
}

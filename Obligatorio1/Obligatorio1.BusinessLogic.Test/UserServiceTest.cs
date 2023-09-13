using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

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
            //Arrange
            User userAux = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);

            //Act
            _userManagmentMock?.Setup(x => x.RegisterUser(userAux));
            _userService?.RegisterUser(userAux);

            //Assert 
            _userManagmentMock?.VerifyAll();
        }

        [TestMethod]
        public void UpdateUserProfileTest()
        {
            //Arrange
            User user = new User(1, "Lautaro", "Lautaro292829", "lautaro@gmail.com", "Rivera 400", "Administrador", null);

            //Act
            _userManagmentMock?.Setup(x => x.UpdateUserProfile(user)).Returns(user);

            User? result = _userService?.UpdateUserProfile(user);

            //Assert
            _userManagmentMock?.Verify(x => x.UpdateUserProfile(user), Times.Once);
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void LoginValidCredentialsTest()
        {
            //Arrange
            string email = "testuser@example.com";
            string password = "Password123";
            User authenticatedUser = new User(1, "Lautaro", "Lautaro292829", "lautaro@gmail.com", "Rivera 400", "Administrador", null);

            //Configure mock behavior for valid login
            _userManagmentMock?.Setup(x => x.Login(email, password)).Returns(authenticatedUser);

            //Act
            User? result = _userService?.Login(email, password);

            //Assert
            _userManagmentMock?.Verify(x => x.Login(email, password), Times.Once);
            Assert.AreEqual(authenticatedUser, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Autenticación fallida. Credenciales incorrectas.")]
        public void LoginTestInvalidCredentialsTest()
        {
            //Arrange
            string email = "testuser@example.com";
            string password = "IncorrectPassword";

            //Configure mock behavior for invalid login (returns exception)
            _userManagmentMock?.Setup(x => x.Login(email, password)).Throws(new Exception("Autenticación fallida. Credenciales incorrectas."));

            //Act
            _userService?.Login(email, password);
        }

        [TestMethod]
        public void LogoutTest()
        {
            //Arrange
            User user = new User(1, "Lautaro", "Lautaro292829", "lautaro@gmail.com", "Rivera 400", "Administrador", null);

            //Set the authenticated user (loggedInUser)
            _userService?.SetLoggedInUser(user);

            //Act
            _userService?.Logout(user);

            //Assert
            Assert.IsNull(_userService?.GetLoggedInUser());
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            //Arrange
            int userId = 1;
            User expectedUser = new User(userId, "Carlos", "Password0290", "carlitos@gmail.com", "Av. Ramirez 50", "Basico", null);

            //Configure the mock's behavior to return the expected user when GetUserById is called with the corresponding ID.
            _userManagmentMock?.Setup(x => x.GetUserByID(userId)).Returns(expectedUser);

            //Act
            User? result = _userService?.GetUserByID(userId);

            //Assert

            Assert.AreEqual(expectedUser, result);
            //Verifies that the GetUserById method was called in the data access layer with the corresponding ID.
            _userManagmentMock?.Verify(x => x.GetUserByID(userId), Times.Once);
        }

        [TestMethod]
        public void GetUsersTest()
        {
            //Arrange
            List<User> expectedUsers = new List<User>
            {
                new User(1, "Carlos", "Password0290", "carlitos@gmail.com", "Av. Ramirez 50", "Basico", null),
                new User(2, "Ana", "Ana1234", "ana@example.com", "Calle Principal 123", "Avanzado", null),
                new User(3, "Juan", "JuanP@ss", "juan@hotmail.com", "Avenida Libertad 789", "Intermedio", null)
            };

            //Configure the mock behavior to return the expected list of users when GetUsers is called.
            _userManagmentMock?.Setup(x => x.GetUsers()).Returns(expectedUsers);

            //Act
            IEnumerable<User>? result = _userService?.GetUsers();

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedUsers, result.ToList());

            //Verifies that the GetUsers method was called in the data access layer.
            _userManagmentMock?.Verify(x => x.GetUsers(), Times.Once);
        }
    }
}

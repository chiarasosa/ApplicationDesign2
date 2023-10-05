using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
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
            // Arrange
            User userAux = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);

            // Configurar la llamada a GetAllUsers en el mock
            _userManagmentMock.Setup(x => x.GetAllUsers()).Returns(new List<User>());

            // Configurar la llamada a RegisterUser en el mock
            _userManagmentMock.Setup(x => x.RegisterUser(It.IsAny<User>()));

            // Act
            _userService.RegisterUser(userAux);

            // Assert 
            _userManagmentMock.VerifyAll();
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
            //Assert.IsNull(_userService?.GetLoggedInUser());
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
            _userManagmentMock?.Setup(x => x.GetAllUsers()).Returns(expectedUsers);

            //Act
            IEnumerable<User>? result = _userService?.GetUsers();

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedUsers, result.ToList());

            //Verifies that the GetUsers method was called in the data access layer.
            _userManagmentMock?.Verify(x => x.GetAllUsers(), Times.Once);
        }

        [TestMethod]
        public void CreateUserTest()
        {
            //Arrange
            User adminUser = new User(1, "Admin", "Admin123", "admin@example.com", "Admin Address", "Administrador", null);
            User newUser = new User(2, "NewUser", "NewUser123", "newuser@example.com", "New User Address", "Comprador", null);

            //Configures the behavior of the mock object for CreateUser(newUser) which returns newUser.
            _userManagmentMock?.Setup(x => x.CreateUser(newUser)).Returns(newUser);

            //Sets the authenticated administrator user.
            _userService?.SetLoggedInUser(adminUser);

            //Act
            User? result = _userService?.CreateUser(newUser);

            //Assert
            Assert.AreEqual(newUser, result);

            //Verify that CreateUser was called at the data access layer.
            _userManagmentMock?.Verify(x => x.CreateUser(newUser), Times.Once);
        }

        [TestMethod]
        public void UpdateUserInformationTest()
        {
            //Arrange
            User adminUser = new User(1, "Admin", "Admin123", "admin@example.com", "Admin Address", "Administrador", null);
            User existingUser = new User(2, "ExistingUser", "ExistingUser123", "existinguser@example.com", "Existing User Address", "Comprador", null);
            User updatedUser = new User(existingUser.UserID, "UpdatedUser", "UpdatedUser123", "updateduser@example.com", "Updated User Address", "Comprador", null);

            //Configures the behavior of the mock object for successful updating of user information.
            _userManagmentMock?.Setup(x => x.UpdateUserInformation(updatedUser)).Returns(updatedUser);

            //Sets the authenticated administrator user.
            _userService?.SetLoggedInUser(adminUser);

            //Act
            User? result = _userService?.UpdateUserInformation(updatedUser);

            //Assert
            Assert.AreEqual(updatedUser, result);

            //Verifies that the UpdateUserInformation method was called in the data access layer.
            _userManagmentMock?.Verify(x => x.UpdateUserInformation(updatedUser), Times.Once);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            // Arrange
            User adminUser = new User(1, "Admin", "Admin123", "admin@example.com", "Admin Address", "Administrador", null);
            User userToDelete = new User(2, "UserToDelete", "Password123", "delete@example.com", "Delete User Address", "Comprador", null);
            int userIdToDelete = userToDelete.UserID;

            // Configure mock behavior for GetUserByID to return the user to delete
            _userManagmentMock?.Setup(x => x.GetUserByID(userIdToDelete)).Returns(userToDelete);

            // Configure mock behavior for DeleteUser
            _userManagmentMock?.Setup(x => x.DeleteUser(userIdToDelete));

            // Set the logged-in admin user
            _userService?.SetLoggedInUser(adminUser);

            // Act
            _userService?.DeleteUser(userIdToDelete);

            // Assert
            // Verify that GetUserByID was called to fetch the user to delete
            _userManagmentMock?.Verify(x => x.GetUserByID(userIdToDelete), Times.Once);

            // Verify that DeleteUser was called at the data access layer with the correct user ID
            _userManagmentMock?.Verify(x => x.DeleteUser(userIdToDelete), Times.Once);
        }

        [TestMethod]
        public void GetPurchaseHistoryTest()
        {
            // Arrange
            User user = new User(1, "UsuarioComprador", "Password123", "comprador@example.com", "Dirección de Comprador", "Comprador", null);
            List<Product> products = new List<Product>();
            List<Purchase> expectedPurchases = new List<Purchase>

            {
                new Purchase(),
                new Purchase(),
                new Purchase(),
            };

            // Configura el comportamiento del mock para que devuelva las compras esperadas
            _userManagmentMock?.Setup(x => x.GetPurchaseHistory(user)).Returns(expectedPurchases);

            // Act
            IEnumerable<Purchase>? result = _userService?.GetPurchaseHistory(user);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedPurchases, result.ToList());

            // Verifica que el método GetPurchaseHistory se llamó en la capa de acceso a datos con el usuario correcto
            _userManagmentMock?.Verify(x => x.GetPurchaseHistory(user), Times.Once);
        }

        [TestMethod]
        public void GetAllPurchasesTest()
        {
            // Arrange
            User adminUser = new User(1, "Admin", "Admin123", "admin@example.com", "Admin Address", "Administrador", null);
            _userService?.SetLoggedInUser(adminUser);

            List<Purchase> expectedPurchases = new List<Purchase>
            {
                new Purchase(),
                new Purchase(),
                new Purchase(),
            };

            // Configura el comportamiento del mock para que devuelva las compras esperadas
            _userManagmentMock?.Setup(x => x.GetAllPurchases()).Returns(expectedPurchases);

            // Act
            IEnumerable<Purchase>? result = _userService?.GetAllPurchases();

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedPurchases, result.ToList());

            // Verifica que el método GetAllPurchases se llamó en la capa de acceso a datos
            _userManagmentMock?.Verify(x => x.GetAllPurchases(), Times.Once);
        }

        [TestMethod]
        public void CreateProductTest()
        {
            // Arrange
            User regularUser = new User(2, "User", "User123", "user@example.com", "User Address", "Administrador", null);
            _userService?.SetLoggedInUser(regularUser);

            Product newProduct = new Product
            {
                ProductID = 1,
                Name = "New Product",
                Description = "A new product for testing",
                Price = 10,
                Brand = 1,
                Category = 2,
                Color = "Amarillo",
            };

            // Act
            Action createProductAction = () => _userService?.CreateProduct(newProduct);

            // Assert
            Assert.ThrowsException<Exception>(createProductAction);

            // Verifica que el método CreateProduct se llamó en la capa de acceso a datos
            _userManagmentMock?.Verify(x => x.CreateProduct(newProduct), Times.Once);
        }

        [TestMethod]
        public void UpdateProductTest()
        {
            // Arrange
            User adminUser = new User(1, "Admin", "Admin123", "admin@example.com", "Admin Address", "Administrador", null);
            _userService?.SetLoggedInUser(adminUser);

            Product existingProduct = new Product
            {
                ProductID = 1,
                Name = "Existing Product",
                Description = "An existing product for testing",
                Price = 20,
                Brand = 2,
                Category = 3,
                Color = "Amarillo",

            };

            // Configura el comportamiento del mock para que devuelva el producto actualizado
            _userManagmentMock?.Setup(x => x.UpdateProduct(It.IsAny<Product>())).Returns((Product product) => product);

            // Act
            Product updatedProduct = new Product
            {
                ProductID = 1,
                Name = "Updated Product",
                Description = "An updated product for testing",
                Price = 30,
                Brand = 3,
                Category = 4,
                Color = "Amarillo",
            };

            Product? result = _userService?.UpdateProduct(updatedProduct);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedProduct, result);

            // Verifica que el método UpdateProduct se llamó en la capa de acceso a datos
            _userManagmentMock?.Verify(x => x.UpdateProduct(updatedProduct), Times.Once);
        }
        /*
        [TestMethod]
        public void AddProductToCart_Correct()
        {
            //Arrange
            Product product = new Product();

            //Act
            _userManagmentMock?.Setup(x => x.AddProductToCart(product));
            _userService?.AddProductToCart(product);
            //Assert
            _userManagmentMock?.VerifyAll();
        }

        [TestMethod]
        public void DeleteProductFromCart_Correct()
        {
            //Arrange
            Product product = new Product();

            //Act
            _userManagmentMock?.Setup(x => x.AddProductToCart(product));
            _userManagmentMock?.Setup(x => x.DeleteProductFromCart(product));
            _userService?.AddProductToCart(product);
            _userService?.DeleteProductFromCart(product);

            //Assert
            _userManagmentMock?.VerifyAll();
        }*/
    }
}

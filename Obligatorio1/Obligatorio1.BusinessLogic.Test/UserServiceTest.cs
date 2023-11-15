using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;
using Obligatorio1.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]

    public class UserServiceTest
    {

        [TestMethod]
        public void DeleteUser_SuccessfullyDeletesUser()
        {
            // Arrange
            const int userId = 1;
            var userToDelete = new User
            {
                // Proporciona los datos del usuario a eliminar
                UserID = userId,
                UserName = "ToDelete",
                Password = "ToDeletePassword",
                Email = "to.delete@example.com",
                Address = "To Delete Address",
                Role = "User"
            };

            var userRepositoryMock = new Mock<IGenericRepository<User>>();
            var sessionRepositoryMock = new Mock<IGenericRepository<Session>>();

            userRepositoryMock.Setup(x => x.Delete(It.IsAny<User>()));
            userRepositoryMock.Setup(x => x.Save());
            userRepositoryMock.Setup(x => x.GetAll<User>()).Returns(new List<User> { userToDelete });

            var userService = new UserService(userRepositoryMock.Object, sessionRepositoryMock.Object);

            // Act
            var result = userService.DeleteUser(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userToDelete, result);

            userRepositoryMock.Verify(x => x.Delete(userToDelete), Times.Once);
            userRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [TestMethod]
        public void UpSuccessfully()
        {
            // Arrange
            const int userId = 1;
            var userToUpdate = new User
            {
                // Proporciona los datos del usuario actualizados
                UserID = userId,
                UserName = "UpdatedUserName",
                Password = "UpdatedPassword",
                Email = "updated.email@example.com",
                Address = "Updated Address",
                Role = "Admin"
            };

            var userRepositoryMock = new Mock<IGenericRepository<User>>();
            var sessionRepositoryMock = new Mock<IGenericRepository<Session>>();

            userRepositoryMock.Setup(x => x.Update(It.IsAny<User>()));
            userRepositoryMock.Setup(x => x.Save());
            userRepositoryMock.Setup(x => x.GetAll<User>()).Returns(new List<User> { userToUpdate });

            var userService = new UserService(userRepositoryMock.Object, sessionRepositoryMock.Object);

            // Act
            var result = userService.UpdateUserProfile(userId, userToUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userToUpdate, result);

            userRepositoryMock.Verify(x => x.Update(userToUpdate), Times.Once);
            userRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [TestMethod]
        public void RegisterUser_SuccessfullyRegistersUser()
        {
            // Arrange
            var user = new User
            {
                // Proporciona los datos del usuario que se usarán para el registro
                UserName = "JohnDoe",
                Password = "SecurePassword123",
                Email = "john.doe@example.com",
                Address = "123 Main Street",
                Role = "User"
            };

            var userRepositoryMock = new Mock<IGenericRepository<User>>();
            var sessionRepositoryMock = new Mock<IGenericRepository<Session>>();

            userRepositoryMock.Setup(x => x.Insert(It.IsAny<User>()));
            userRepositoryMock.Setup(x => x.Save());

            var userService = new UserService(userRepositoryMock.Object, sessionRepositoryMock.Object);

            // Act
            userService.RegisterUser(user);

            // Assert
            userRepositoryMock.Verify(x => x.Insert(user), Times.Once);
            userRepositoryMock.Verify(x => x.Save(), Times.Once);
        }


        [TestMethod]
        public void GetAllUsers_ReturnsOkResult_WhenOperationIsSuccessful()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var controller = new UserController(userServiceMock.Object);

            // Act
            var result = controller.GetAllUsers();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult?.Value);
            // Puedes agregar más aserciones según la estructura de tu resultado real
        }



        [TestMethod]
        public void GetUserByID_ReturnsOkResult_WithUserDto()
        {
            // Arrange
            const int userId = 1;
            var userServiceMock = new Mock<IUserService>();
            var user = new User
            {
                UserID = userId,
                UserName = "TestUser",
                Password = "TestPassword",
                Email = "test@example.com",
                Address = "TestAddress",
                Role = "Admin"
            };
            userServiceMock.Setup(x => x.GetUserByID(userId)).Returns(user);

            var controller = new UserController(userServiceMock.Object);

            // Act
            var result = controller.GetUserByID(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult?.Value);
            var userDto = okResult?.Value as object;
            Assert.IsNotNull(userDto);
            Assert.AreEqual(userId, userDto.GetType().GetProperty("UserID")?.GetValue(userDto));
            Assert.AreEqual("TestUser", userDto.GetType().GetProperty("UserName")?.GetValue(userDto));
            Assert.AreEqual("TestPassword", userDto.GetType().GetProperty("Password")?.GetValue(userDto));
            Assert.AreEqual("test@example.com", userDto.GetType().GetProperty("Email")?.GetValue(userDto));
            Assert.AreEqual("TestAddress", userDto.GetType().GetProperty("Address")?.GetValue(userDto));
            Assert.AreEqual("Admin", userDto.GetType().GetProperty("Role")?.GetValue(userDto));
        }


    }


}
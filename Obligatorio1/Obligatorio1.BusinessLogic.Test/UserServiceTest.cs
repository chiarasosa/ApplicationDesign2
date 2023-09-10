using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obligatorio1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Obligatorio1.IDataAccess;
using Obligatorio1.BusinessLogic;
using Obligatorio1.IBusinessLogic;

namespace Obligatorio1.BusinessLogic.Test
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void AddUserTest()
        {
            //Arrange
            Mock<IUserManagment>? mock = new Mock<IUserManagment>(MockBehavior.Strict);
            UserService service = new UserService(mock.Object);
            User userAux = new User(1, "Agustin", "Prueba123", "agustin@gmail.com", "Rivera 400", "Administrador", null);

            //Act
            mock!.Setup(x => x.AddUser(userAux!));
            service!.AddUser(userAux!);

            //Assert 
            mock.VerifyAll();
        }
    }
}

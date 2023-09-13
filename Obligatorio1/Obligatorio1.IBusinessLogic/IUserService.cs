using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;

namespace Obligatorio1.IBusinessLogic
{
    public interface IUserService
    {
        //User Managment
        void RegisterUser(User user);
        User UpdateUserProfile(User user);
        User Login(string email, string password);
        void Logout(User user);
        User GetUserByID(int userID);
    }
}

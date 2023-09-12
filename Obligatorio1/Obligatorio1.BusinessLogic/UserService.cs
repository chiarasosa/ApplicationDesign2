using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;
using Obligatorio1.IDataAccess;
using Obligatorio1.IBusinessLogic;

namespace Obligatorio1.BusinessLogic
{
    public class UserService: IUserService
    {
        private readonly IUserManagment userManagement;
        public UserService(IUserManagment userManagment) 
        { 
            this.userManagement = userManagment;
        }

        public void RegisterUser(User user) 
        { 
            if (IsUserValid(user))
                userManagement.RegisterUser(user);
        }

        private bool IsUserValid(User user)
        {
            if (user == null || user.UserName == string.Empty || user.Password == string.Empty)
            {
                throw new Exception("Usuario inválido");
            }

            return true;
        }

        public User? UpdateUserProfile(User user)
        {
            if (IsUserValid(user))
                return userManagement.UpdateUserProfile(user);
            return null;
        }
    }
}

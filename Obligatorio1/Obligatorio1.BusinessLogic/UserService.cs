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
        private readonly IUserManagment userManagmet;
        public UserService(IUserManagment userManagment) 
        { 
            this.userManagmet = userManagment;
        }

        public void AddUser(User user) 
        { 
            if (IsUserValid(user))
                userManagmet.AddUser(user);
        }

        private bool IsUserValid(User user)
        {
            if (user == null || user.UserName == string.Empty || user.Password == string.Empty)
            {
                throw new Exception("Usuario inválido");
            }

            return true;
        }
    }
}

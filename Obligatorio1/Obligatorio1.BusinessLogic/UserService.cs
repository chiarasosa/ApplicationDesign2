using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.BusinessLogic
{
    public class UserService: IUserService
    {
        private readonly IUserManagment userManagmet;
        public UserService(IUserManagment userManagment) 
        { 
            this.userManagmet = userManagment;
        }
    }
}

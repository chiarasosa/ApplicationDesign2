using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IDataAccess;
using Obligatorio1.Domain;


namespace Obligatorio1.DataAccess
{
    public class UserManagment : IUserManagment
    {
        private List<User>? _users;

        public void RegisterUser(User user)
        {
            _users?.Add(user);
        }
        public User? UpdateUserProfile(User user)
        {
            if (_users?.Where(u => u.UserID == user.UserID).FirstOrDefault() == null)
                throw new Exception("El usuario no existe.");
            return user;
        }
    }
}
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
        public User UpdateUserProfile(User user)
        {
            if (_users?.Where(u => u.UserID == user.UserID).FirstOrDefault() == null)
                throw new Exception("El usuario no existe.");
            return user;
        }

        public User Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Correo electrónico y contraseña son obligatorios.");
            }

            // Realiza la autenticación buscando el usuario por correo electrónico y contraseña
            User? authenticatedUser = _users?.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (authenticatedUser == null)
            {
                throw new Exception("Autenticación fallida. Credenciales incorrectas.");
            }

            return authenticatedUser;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Obligatorio1.IDataAccess;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;

namespace Obligatorio1.DataAccess
{
    public class UserManagment : IUserManagment
    {
        private List<User>? _users;
        private User? _authenticatedUser; // Almacena el usuario autenticado

        public UserManagment()
        {
            _users = new List<User>();
            _authenticatedUser = null;
        }

        public void RegisterUser(User user)
        {
            _users?.Add(user);
        }

        public User UpdateUserProfile(User user)
        {
            if (_users?.FirstOrDefault(u => u.UserID == user.UserID) == null)
                throw new UserException("El usuario no existe.");
            return user;
        }

        public User Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new UserException("Correo electrónico y contraseña son obligatorios.");
            }

            // Realiza la autenticación buscando el usuario por correo electrónico y contraseña
            User? authenticatedUser = _users?.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (authenticatedUser == null)
            {
                throw new UserException("Autenticación fallida. Credenciales incorrectas.");
            }

            // Almacena el usuario autenticado
            _authenticatedUser = authenticatedUser;

            return authenticatedUser;
        }

        public void Logout(User user)
        {
            // Verifica si el usuario proporcionado coincide con el usuario autenticado
            if (_authenticatedUser != null && user.UserID == _authenticatedUser.UserID)
            {
                // Realiza las tareas de cierre de sesión aquí
                _authenticatedUser = null; // Elimina la referencia al usuario autenticado
            }
        }

        public User GetUserByID(int userId)
        {
            if (userId <= 0)
            {
                throw new UserException("ID de usuario inválido.");
            }

            //Search for the user by ID in the user list
            User? user = _users?.FirstOrDefault(u => u.UserID == userId);

            if (user == null)
            {
                throw new UserException($"No se encontró ningún usuario con el ID {userId}.");
            }

            return user;
        }
    }
}

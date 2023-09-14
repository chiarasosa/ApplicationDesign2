using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.DataAccess
{
    public class UserManagment : IUserManagment
    {
        private List<User>? _users;
        private User? _authenticatedUser;

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

            //Perform authentication by searching for the user by email and password.
            User? authenticatedUser = _users?.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (authenticatedUser == null)
            {
                throw new UserException("Autenticación fallida. Credenciales incorrectas.");
            }

            //Stores the authenticated user
            _authenticatedUser = authenticatedUser;

            return authenticatedUser;
        }

        public void Logout(User user)
        {
            if (_authenticatedUser != null && user.UserID == _authenticatedUser.UserID)
            {
                _authenticatedUser = null;
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

        public IEnumerable<User> GetUsers()
        {
            if (_users != null)
            {
                return _users;
            }
            else
            {
                return Enumerable.Empty<User>();
            }
        }

        public User CreateUser(User user)
        {
            // Check if the user with the given email already exists
            if (_users?.Any(u => u.Email == user.Email) == true)
            {
                throw new UserException("El correo electrónico ya está registrado en el sistema.");
            }

            // Add the new user to the list of users
            _users?.Add(user);

            // Return the created user
            return user;
        }
    }
}

using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.DataAccess
{
    public class UserManagment : IUserManagment
    {
        private List<User>? _users;
        private User? _authenticatedUser;
        private List<Purchase>? _purchases;

        public UserManagment()
        {
            _users = new List<User>();
            _authenticatedUser = null;
            _purchases = new List<Purchase>();
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

        public User UpdateUserInformation(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario proporcionado es nulo.");
            }

            if (_authenticatedUser == null || _authenticatedUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para actualizar la información del usuario.");
            }

            User? existingUser = _users?.FirstOrDefault(u => u.UserID == user.UserID);

            if (existingUser == null)
            {
                throw new UserException("El usuario a actualizar no existe.");
            }

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Address = user.Address;

            return existingUser;
        }

        public void DeleteUser(int userID)
        {
            if (_authenticatedUser == null || _authenticatedUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para eliminar usuarios.");
            }

            User? userToDelete = _users?.FirstOrDefault(u => u.UserID == userID);

            if (userToDelete == null)
            {
                throw new UserException($"Usuario con ID {userID} no encontrado.");
            }

            _users?.Remove(userToDelete);
        }

        public IEnumerable<Purchase> GetPurchaseHistory(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario proporcionado es nulo.");
            }

            if (_authenticatedUser == null || _authenticatedUser.UserID != user.UserID)
            {
                throw new UserException("No tiene permiso para acceder al historial de compras de este usuario.");
            }

            // Simplemente devuelve las compras asociadas al usuario
            return _purchases.Where(purchase => purchase.User.UserID == user.UserID);
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            if (_authenticatedUser == null || _authenticatedUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para acceder a todas las compras.");
            }

            return _purchases;
        }

    }
}

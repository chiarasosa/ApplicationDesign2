using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.DataAccess.Repositories
{
    public class UserManagment : IUserManagment
    {
        private List<User>? _users;
       // private User? _authenticatedUser;
        private List<Purchase>? _purchases;
        private List<Product>? _products;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Session> _sessionRepository;
        public UserManagment(IGenericRepository<User> userRepositoy, IGenericRepository<Session> sessionRepository)
        {
            //_authenticatedUser = null;
            _purchases = new List<Purchase>();
            _products = new List<Product>();
            _userRepository = userRepositoy;
            _sessionRepository = sessionRepository;
        }

        public void RegisterUser(User user)
        {
            _userRepository.Insert(user);
            _userRepository.Save();
        }

        public User UpdateUserProfile(User user)
        {
            var existingUser = _userRepository.GetAll<User>().FirstOrDefault(u => u.UserID == user.UserID);

            if (existingUser == null)
                throw new UserException("El usuario no existe.");

            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;
            existingUser.Address = user.Address;
            existingUser.Role = user.Role;
            existingUser.Cart = user.Cart;

            _userRepository.Update(existingUser);
            _userRepository.Save();

            return existingUser;
        }

        public User GetUserByID(int userId)
        {
            if (userId <= 0)
            {
                throw new UserException("ID de usuario inválido.");
            }

            User? user = _userRepository.GetAll<User>().FirstOrDefault(u => u.UserID == userId);

            if (user == null)
            {
                throw new UserException($"No se encontró ningún usuario con el ID {userId}.");
            }

            return user;
        }
        public IEnumerable<User> GetAllUsers()
        {
            var result = _userRepository.GetAll<User>();
            if (result != null)
            {
                return result;
            }
            else
            {
                return Enumerable.Empty<User>();
            }
        }
        public void DeleteUser(int userID)
        {
            User? userToDelete = _userRepository.GetAll<User>().FirstOrDefault(u => u.UserID == userID);

            if (userToDelete == null)
            {
                throw new UserException($"Usuario con ID {userID} no encontrado.");
            }

            _userRepository.Delete(userToDelete);
            _userRepository.Save();
        }
    }
}
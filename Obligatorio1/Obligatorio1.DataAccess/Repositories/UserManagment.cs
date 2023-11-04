using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.DataAccess.Repositories
{
    public class UserManagment : IUserManagment
    {
        private List<User>? _users;
        private List<Purchase>? _purchases;
        private List<Product>? _products;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Session> _sessionRepository;
        public UserManagment(IGenericRepository<User> userRepositoy, IGenericRepository<Session> sessionRepository)
        {
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
                throw new UserException("Username does not exist.");

            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;
            existingUser.Address = user.Address;
            existingUser.Role = user.Role;

            _userRepository.Update(existingUser);
            _userRepository.Save();

            return existingUser;
        }

        public User GetUserByID(int userId)
        {
            if (userId <= 0)
            {
                throw new UserException("Invalid user ID.");
            }

            User? user = _userRepository.GetAll<User>().FirstOrDefault(u => u.UserID == userId);

            if (user == null)
            {
                throw new UserException($"No user with ID {userId} was found.");
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
                throw new UserException($"User with ID {userID} not found.");
            }

            _userRepository.Delete(userToDelete);
            _userRepository.Save();
        }
    }
}
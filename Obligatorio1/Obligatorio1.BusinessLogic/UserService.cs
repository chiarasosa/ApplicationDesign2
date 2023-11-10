using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;



namespace Obligatorio1.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Session> _sessionRepository;


        public UserService(IGenericRepository<User> userRepositoy, IGenericRepository<Session> sessionRepository)
        {
            _userRepository = userRepositoy;
            _sessionRepository = sessionRepository;
        }

        public void RegisterUser(User user)
        {

            if (IsUserValid(user) && !IsUserNameAlreadyTaken(user.UserName) && !IsEmailAlreadyTaken(user.Email))
            {
                _userRepository.Insert(user);
                _userRepository.Save();
            }
            else
            {
                throw new UserException("The username or email is already in use or invalid.");
            }
        }

        //**********************************************VALIDACIONES
        private bool IsUserValid(User user)
        {
            if (user == null || user.UserName == string.Empty || user.Password == string.Empty)
            {
                throw new UserException("Invalid User");
            }

            return true;
        }

        private bool IsUserNameAlreadyTaken(string userName)
        {
            IEnumerable<User> users = GetUsers();

            return users.Any(u => u.UserName == userName);
        }

        private bool IsEmailAlreadyTaken(string email)
        {
            IEnumerable<User> users = GetUsers();

            return users.Any(u => u.Email == email);
        }
        //***********************************************************

        public User UpdateUserProfile(int id, User user)
        {
            if (GetUserByID(id) != null)
            {
                var existingUser = GetUserByID(id);

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
            else
            {
                throw new UserException("Update failed. Incorrect user data.");
            }
        }

        public User GetUserByID(int userID)
        {           
            if (userID <= 0)
            {
                throw new UserException("Invalid user ID.");
            }

            User? user = _userRepository.GetAll<User>().FirstOrDefault(u => u.UserID == userID);

            if (user == null)
            {
                throw new UserException($"User with ID {userID} not found.");
            }

            return user;
        }

        public IEnumerable<User> GetUsers()
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

        public User DeleteUser(int userID)
        {
            User? userToDelete = GetUserByID(userID);

            if (userToDelete == null)
            {
                throw new UserException($"User with ID {userID} not found.");
            }

            _userRepository.Delete(userToDelete);
            _userRepository.Save();

            return userToDelete;
        }
    }
}
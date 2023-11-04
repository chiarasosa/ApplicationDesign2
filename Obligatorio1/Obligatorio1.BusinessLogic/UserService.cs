using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;



namespace Obligatorio1.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserManagment _userManagment;
       

        public UserService(IUserManagment userManagment)
        {
            this._userManagment = userManagment;
        }

        public void RegisterUser(User user)
        {

            if (IsUserValid(user) && !IsUserNameAlreadyTaken(user.UserName) && !IsEmailAlreadyTaken(user.Email))
            {
                _userManagment.RegisterUser(user);
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
            IEnumerable<User> users = _userManagment.GetAllUsers();

            return users.Any(u => u.UserName == userName);
        }

        private bool IsEmailAlreadyTaken(string email)
        {
            IEnumerable<User> users = _userManagment.GetAllUsers();

            return users.Any(u => u.Email == email);
        }
        //***********************************************************

        public User UpdateUserProfile(User user)
        {
            if (IsUserValid(user))
                return _userManagment.UpdateUserProfile(user);
            throw new UserException("Update failed. Incorrect user data."); ;
        }

        public User GetUserByID(int userID)
        {
            User? user = _userManagment.GetUserByID(userID);



            if (user == null)
            {
                throw new UserException($"User with ID {userID} not found.");
            }

            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User>? users = _userManagment.GetAllUsers();

            if (users == null)
            {
                throw new UserException("Error getting list of users.");
            }

            return users;
        }

        public User DeleteUser(int userID)
        {
            User userToDelete = _userManagment.GetUserByID(userID);

            if (userToDelete == null)
            {
                throw new UserException($"User with ID {userID} not found.");
            }

            _userManagment.DeleteUser(userID);

            return userToDelete;
        }
    }
}
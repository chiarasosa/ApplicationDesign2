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
                throw new UserException("El nombre de usuario o el correo electrónico ya están en uso o son inválidos.");
            }
        }

        //**********************************************VALIDACIONES
        private bool IsUserValid(User user)
        {
            if (user == null || user.UserName == string.Empty || user.Password == string.Empty)
            {
                throw new UserException("Usuario inválido");
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
            throw new UserException("Actualización fallida. Datos de usuario incorrectos."); ;
        }

        public User GetUserByID(int userID)
        {
            User? user = _userManagment.GetUserByID(userID);



            if (user == null)
            {
                throw new UserException($"Usuario con ID {userID} no encontrado.");
            }

            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User>? users = _userManagment.GetAllUsers();

            if (users == null)
            {
                throw new UserException("Error al obtener la lista de usuarios.");
            }

            return users;
        }

        public User DeleteUser(int userID)
        {
            User userToDelete = _userManagment.GetUserByID(userID);

            if (userToDelete == null)
            {
                throw new UserException($"Usuario con ID {userID} no encontrado.");
            }

            _userManagment.DeleteUser(userID);

            return userToDelete;
        }
    }
}
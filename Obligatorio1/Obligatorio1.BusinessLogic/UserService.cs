using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;



namespace Obligatorio1.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserManagment _userManagment;
        private User? loggedInUser;

        public User? GetLoggedInUser()
        {
            var result = new User
            {

                UserID = 3,
                UserName = "UsuarioAdministrador",
                Email = "usuarioAdministrador@ejemplo.com",
                Password = "ContraseñaAdministrador",
                Role = "Administrador",
                Address = "DireccionAdministrador",
                Cart = null,
                Purchases = null,

                /*
                UserID = 28, // ID del usuario logueado
                UserName = "UsuarioComprador",
                Email = "usuarioComprador@ejemplo.com",
                Password = "ContraseñaComprador",
                Role = "Comprador", 
                Address = "DireccionComprador",
                Cart= null,
                Purchases = null,
                */
            };

            return result;
        }



        public void SetLoggedInUser(User user)
        {
            loggedInUser = user;
        }

        public UserService(IUserManagment userManagment)
        {
            this._userManagment = userManagment;
            this.loggedInUser = null;
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

        public User Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new UserException("Correo electrónico y contraseña son obligatorios.");
            }

            User? authenticatedUser = _userManagment.Login(email, password);

            if (authenticatedUser == null)
            {
                throw new UserException("Autenticación fallida. Credenciales incorrectas.");
            }

            loggedInUser = authenticatedUser;

            return authenticatedUser;
        }
        public void Logout(User user)
        {
            if (loggedInUser != null && user.UserID == loggedInUser.UserID)
            {
                loggedInUser = null;
            }
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
        public User CreateUser(User user)
        {
            if (loggedInUser == null || loggedInUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para crear usuarios.");
            }

            if (IsUserValid(user))
            {
                User createdUser = _userManagment.CreateUser(user);



                if (createdUser == null)
                {
                    throw new UserException("Error al crear el usuario.");
                }

                return createdUser;
            }
            else
            {
                throw new UserException("Usuario inválido.");
            }
        }

        public User UpdateUserInformation(User user)
        {
            if (IsUserValid(user))
            {
                User updatedUser = _userManagment.UpdateUserInformation(user);

                if (updatedUser == null)
                {
                    throw new UserException("Error al actualizar la información del usuario.");
                }

                return updatedUser;
            }
            else
            {
                throw new UserException("Usuario inválido.");
            }
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

        public IEnumerable<Purchase> GetPurchaseHistory(User user)
        {
            return _userManagment.GetPurchaseHistory(user);
        }
        public IEnumerable<Purchase> GetAllPurchases()
        {
            return _userManagment.GetAllPurchases();
        }

        public void CreateProduct(Product product)
        {
            if (loggedInUser == null || loggedInUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para crear productos.");
            }

            try
            {
                _userManagment.CreateProduct(product);
            }
            catch (UserException ex)
            {
                throw new UserException($"Error al crear el producto: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al crear el producto: {ex.Message}", ex);
            }
        }

        public Product UpdateProduct(Product product)
        {
            if (loggedInUser == null || loggedInUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para actualizar productos.");
            }

            try
            {
                return _userManagment.UpdateProduct(product);
            }
            catch (UserException ex)
            {
                throw new UserException($"Error al actualizar el producto: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al actualizar el producto: {ex.Message}", ex);
            }
        }
    }
}
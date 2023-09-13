using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;

public class UserService : IUserService
{
    private readonly IUserManagment userManagement;
    private User? loggedInUser;

    public User? GetLoggedInUser()
    {
        return loggedInUser;
    }

    public void SetLoggedInUser(User? user)
    {
        loggedInUser = user;
    }

    public UserService(IUserManagment userManagment)
    {
        this.userManagement = userManagment;
        this.loggedInUser = null;
    }

    public void RegisterUser(User user)
    {
        if (IsUserValid(user))
            userManagement.RegisterUser(user);
    }

    private bool IsUserValid(User user)
    {
        if (user == null || user.UserName == string.Empty || user.Password == string.Empty)
        {
            throw new UserException("Usuario inválido");
        }

        return true;
    }

    public User UpdateUserProfile(User user)
    {
        if (IsUserValid(user))
            return userManagement.UpdateUserProfile(user);
        throw new UserException("Actualización fallida. Datos de usuario incorrectos."); ;
    }

    public User Login(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            throw new UserException("Correo electrónico y contraseña son obligatorios.");
        }

        User? authenticatedUser = userManagement.Login(email, password);

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
        User? user = userManagement.GetUserByID(userID);

        if (user == null)
        {
            throw new UserException($"Usuario con ID {userID} no encontrado.");
        }

        return user;
    }

    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User>? users = userManagement.GetUsers();

        if (users == null)
        {
            throw new UserException("Error al obtener la lista de usuarios.");
        }

        return users;
    }

}
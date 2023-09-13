using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;

public class UserService : IUserService
{
    private readonly IUserManagment userManagement;
    private User? loggedInUser; // Almacena el usuario autenticado

    public User? GetLoggedInUser()
    {
        return loggedInUser;
    }

    public void SetLoggedInUser(User? user)
    {
        loggedInUser = user;
    }

    public UserService (IUserManagment userManagment)
    {
        this.userManagement = userManagment;
        this.loggedInUser = null; // Inicialmente, no hay usuario autenticado
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

        // Almacenar el usuario autenticado
        loggedInUser = authenticatedUser;

        return authenticatedUser;
    }

    public void Logout(User user)
    {
        // Verificar si el usuario está autenticado (según tu lógica de sesión)
        if (loggedInUser != null && user.UserID == loggedInUser.UserID)
        {
            // Realizar las tareas de cierre de sesión aquí
            // Por ejemplo, puedes eliminar la referencia al usuario autenticado
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
}

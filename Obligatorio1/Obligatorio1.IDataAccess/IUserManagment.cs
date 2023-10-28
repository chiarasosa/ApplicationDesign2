using Obligatorio1.Domain;

namespace Obligatorio1.IDataAccess
{
    public interface IUserManagment
    {
        void RegisterUser(User user);
        User UpdateUserProfile(User user);
        User GetUserByID(int userId);
        IEnumerable<User> GetAllUsers();
        void DeleteUser(int userID);
    }
}
using Obligatorio1.Domain;

namespace Obligatorio1.IBusinessLogic
{
    public interface IUserService
    {
        void RegisterUser(User user);
        User UpdateUserProfile(User user);
        User Login(string email, string password);
        void Logout(User user);

        //User Administrator
        IEnumerable<User> GetUsers();
        
        //User Administrator
        User GetUserByID(int userID);

        //User Administrator
        User CreateUser(User user);
        
        //User Administrator
        User UpdateUserInformation(User user);
    }
}

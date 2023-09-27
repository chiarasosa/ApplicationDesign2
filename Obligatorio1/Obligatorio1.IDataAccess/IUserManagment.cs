using Obligatorio1.Domain;

namespace Obligatorio1.IDataAccess
{
    public interface IUserManagment
    {
        void RegisterUser(User user);
        User UpdateUserProfile(User user);
        User Login(string email, string password);
        void Logout(User user);
        User GetUserByID(int userId);
        IEnumerable<User> GetAllUsers();
        User CreateUser(User user);
        User UpdateUserInformation(User user);
        void DeleteUser(int userID);
        public IEnumerable<Purchase> GetPurchaseHistory(User user);
        public IEnumerable<Purchase> GetAllPurchases();
        void CreateProduct(Product product);
        Product UpdateProduct(Product product);
        void AddProductToCart(Product product);
        void DeleteProductFromCart(Product product);
    }
}

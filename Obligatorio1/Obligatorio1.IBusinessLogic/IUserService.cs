using Obligatorio1.Domain;
using System.Collections.Generic;

namespace Obligatorio1.IBusinessLogic
{
    public interface IUserService
    {
        // Funcionalidades para usuarios registrados
        void RegisterUser(User user);
        User UpdateUserProfile(User user);
        User Login(string email, string password);
        void Logout(User user);

        // Funcionalidades para usuarios compradores logueados
        IEnumerable<Purchase> GetPurchaseHistory(User user);

        // Funcionalidades para usuarios con rol administrador  en el módulo de administración
        IEnumerable<User> GetAllUsers();
        User GetUserByID(int userID);
        User CreateUser(User user);
        User UpdateUserInformation(User user);
        void DeleteUser(int userID);
        IEnumerable<Purchase> GetAllPurchases();
        void CreateProduct(Product product);
        
        Product UpdateProduct(Product product);
       
        //void DeleteProduct(int productID);*/
    }
}

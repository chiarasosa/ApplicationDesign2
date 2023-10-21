using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;
using System.Security.Authentication;

namespace Obligatorio1.DataAccess.Repositories
{
    public class UserManagment : IUserManagment
    {
        private List<User>? _users;
        private User? _authenticatedUser;
        private List<Purchase>? _purchases;
        private List<Product>? _products;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Session> _sessionRepository;
        public UserManagment(IGenericRepository<User> userRepositoy, IGenericRepository<Session> sessionRepository)
        {
            _authenticatedUser = null;
            _purchases = new List<Purchase>();
            _products = new List<Product>();
            _userRepository = userRepositoy;
            _sessionRepository = sessionRepository;
        }

        public void RegisterUser(User user)
        {
            _userRepository.Insert(user);
            _userRepository.Save();
        }

        

        public User UpdateUserProfile(User user)
        {
            var existingUser = _userRepository.GetAll<User>().FirstOrDefault(u => u.UserID == user.UserID);

            if (existingUser == null)
                throw new UserException("El usuario no existe.");

            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;
            existingUser.Address = user.Address;
            existingUser.Role = user.Role;
            existingUser.Cart = user.Cart;

            _userRepository.Update(existingUser);
            _userRepository.Save();

            return existingUser;
        }


        public User Login(string email, string password)
        {
            User? authenticatedUser = _userRepository.GetAll<User>().FirstOrDefault(u => u.Email == email && u.Password == password);

            if (authenticatedUser == null)
            {
                throw new UserException("Autenticación fallida. Credenciales incorrectas.");
            }

            _authenticatedUser = authenticatedUser;

            return authenticatedUser;
        }

        public User GetLoggedinUser()
        {
            return _authenticatedUser;
        }

        public void Logout(User user)
        {
            if (_authenticatedUser != null && user.UserID == _authenticatedUser.UserID)
            {
                _authenticatedUser = null;
            }
        }
        public User GetUserByID(int userId)
        {
            if (userId <= 0)
            {
                throw new UserException("ID de usuario inválido.");
            }

            User? user = _userRepository.GetAll<User>().FirstOrDefault(u => u.UserID == userId);

            if (user == null)
            {
                throw new UserException($"No se encontró ningún usuario con el ID {userId}.");
            }

            return user;
        }
        public IEnumerable<User> GetAllUsers()
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

        public User CreateUser(User user)
        {
            if (_users?.Any(u => u.Email == user.Email) == true)
            {
                throw new UserException("El correo electrónico ya está registrado en el sistema.");
            }

            _users?.Add(user);

            return user;
        }

        public User UpdateUserInformation(User user)
        {
            var existingUser = _userRepository.GetAll<User>().FirstOrDefault(u => u.UserID == user.UserID);

            if (existingUser == null)
                throw new UserException("El usuario no existe.");

            if (existingUser == null)
            {
                throw new UserException("El usuario a actualizar no existe.");
            }

            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;
            existingUser.Address = user.Address;
            existingUser.Role = user.Role;
            existingUser.Cart = user.Cart;

            _userRepository.Update(existingUser);
            _userRepository.Save();

            return existingUser;
        }

        public void DeleteUser(int userID)
        {
            User? userToDelete = _userRepository.GetAll<User>().FirstOrDefault(u => u.UserID == userID);

            if (userToDelete == null)
            {
                throw new UserException($"Usuario con ID {userID} no encontrado.");
            }

            _userRepository.Delete(userToDelete);
            _userRepository.Save();
        }

        public IEnumerable<Purchase> GetPurchaseHistory(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario proporcionado es nulo.");
            }

            return _userRepository.GetAll<Purchase>().Where(purchase => purchase.UserID == user.UserID);
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            return _userRepository.GetAll<Purchase>();
        }

        public void CreateProduct(Product product)
        {
            if (_authenticatedUser == null || _authenticatedUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para crear productos.");
            }

            if (_products?.Any(p => p.ProductID == product.ProductID) == true)
            {
                throw new UserException($"El producto con ID {product.ProductID} ya existe.");
            }

            _products?.Add(product);
        }

        public Product UpdateProduct(Product product)
        {
            if (_authenticatedUser == null || _authenticatedUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para actualizar productos.");
            }

            Product existingProduct = _products?.FirstOrDefault(p => p.ProductID == product.ProductID);

            if (existingProduct == null)
            {
                throw new UserException($"El producto con ID {product.ProductID} no existe.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Brand = product.Brand;
            existingProduct.Category = product.Category;
            existingProduct.Color = product.Color;

            return existingProduct;
        }

        public void AddProductToCart(Product product)
        {
            if (_authenticatedUser != null)
            {
                _authenticatedUser?.Cart.Products.Add(product);
            }
        }

    }
}
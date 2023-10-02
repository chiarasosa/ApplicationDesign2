﻿using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.DataAccess.Repositories
{
    public class UserManagment : IUserManagment
    {
        private List<User>? _users;
        private User? _authenticatedUser;
        private List<Purchase>? _purchases;
        private List<Product>? _products;
        private readonly IGenericRepository<User> _repository;
        public UserManagment(IGenericRepository<User> userRepositoy )
        {
          //  _users = new List<User>();
            _authenticatedUser = null;
            _purchases = new List<Purchase>();
            _products = new List<Product>();
            _repository = userRepositoy;
        }

        public void RegisterUser(User user)
        {
            //_users?.Add(user);
            _repository.Insert(user);
            _repository.Save();
        }

        public User UpdateUserProfile(User user)
        {
            if (_users?.FirstOrDefault(u => u.UserID == user.UserID) == null)
                throw new UserException("El usuario no existe.");
            return user;
        }

        public User Login(string email, string password)
        {
            //Perform authentication by searching for the user by email and password.
            User? authenticatedUser = _repository.GetAll<User>().FirstOrDefault(u => u.Email == email && u.Password == password);

            if (authenticatedUser == null)
            {
                throw new UserException("Autenticación fallida. Credenciales incorrectas.");
            }

            //Stores the authenticated user
            _authenticatedUser = authenticatedUser;

            return authenticatedUser;
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

            //Search for the user by ID in the user list
            User? user = _repository.GetAll<User>().FirstOrDefault(u => u.UserID == userId);

            if (user == null)
            {
                throw new UserException($"No se encontró ningún usuario con el ID {userId}.");
            }

            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var result = _repository.GetAll<User>();
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
            // Check if the user with the given email already exists
            if (_users?.Any(u => u.Email == user.Email) == true)
            {
                throw new UserException("El correo electrónico ya está registrado en el sistema.");
            }

            // Add the new user to the list of users
            _users?.Add(user);

            // Return the created user
            return user;
        }

        public User UpdateUserInformation(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario proporcionado es nulo.");
            }

            if (_authenticatedUser == null || _authenticatedUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para actualizar la informacion del usuario.");
            }

            User? existingUser = _users?.FirstOrDefault(u => u.UserID == user.UserID);

            if (existingUser == null)
            {
                throw new UserException("El usuario a actualizar no existe.");
            }

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Address = user.Address;

            return existingUser;
        }

        public void DeleteUser(int userID)
        {
            if (_authenticatedUser == null || _authenticatedUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para eliminar usuarios.");
            }

            User? userToDelete = _users?.FirstOrDefault(u => u.UserID == userID);

            if (userToDelete == null)
            {
                throw new UserException($"Usuario con ID {userID} no encontrado.");
            }

            _users?.Remove(userToDelete);
        }

        public IEnumerable<Purchase> GetPurchaseHistory(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario proporcionado es nulo.");
            }

            if (_authenticatedUser == null || _authenticatedUser.UserID != user.UserID)
            {
                throw new UserException("No tiene permiso para acceder al historial de compras de este usuario.");
            }

            // Simplemente devuelve las compras asociadas al usuario
            return _purchases.Where(purchase => purchase.User.UserID == user.UserID);
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            if (_authenticatedUser == null || _authenticatedUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para acceder a todas las compras.");
            }

            return _purchases;
        }


        public void CreateProduct(Product product)
        {
            if (_authenticatedUser == null || _authenticatedUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para crear productos.");
            }

            // Verifica si el producto ya existe
            if (_products?.Any(p => p.ProductID == product.ProductID) == true)
            {
                throw new UserException($"El producto con ID {product.ProductID} ya existe.");
            }

            // Agrega el nuevo producto a la lista de productos
            _products?.Add(product);
        }

        public Product UpdateProduct(Product product)
        {
            if (_authenticatedUser == null || _authenticatedUser.Role != "Administrador")
            {
                throw new UserException("No tiene permiso para actualizar productos.");
            }

            // Busca el producto por su ID
            Product existingProduct = _products?.FirstOrDefault(p => p.ProductID == product.ProductID);

            if (existingProduct == null)
            {
                throw new UserException($"El producto con ID {product.ProductID} no existe.");
            }

            // Actualiza los campos del producto existente
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Brand = product.Brand;
            existingProduct.Category = product.Category;
            existingProduct.Color = product.Color;

            return existingProduct;
        }
        /*
        public User GetAuthenticatedUser()
        {
            return _authenticatedUser;
        }
        */
        public void AddProductToCart(Product product)
        {
            if (_authenticatedUser != null)
            {
                _authenticatedUser?.Cart.Products.Add(product);
            }
        }

        public void DeleteProductFromCart(Product product)
        {
            if (_authenticatedUser != null && _authenticatedUser.Cart != null)
            {
                _authenticatedUser?.Cart.Products.Remove(product);
            }
        }
    }
}
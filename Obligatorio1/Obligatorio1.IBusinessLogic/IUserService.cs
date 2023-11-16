﻿using Obligatorio1.Domain;

namespace Obligatorio1.IBusinessLogic
{
    public interface IUserService
    {
        void RegisterUser(User user);
        // Funcionalidades para usuarios con rol administrador  en el módulo de administración
        IEnumerable<User> GetUsers();
        User GetUserByID(int userID);
        User DeleteUser(int userID);
        User UpdateUserProfile(int id, User user);
    }
}

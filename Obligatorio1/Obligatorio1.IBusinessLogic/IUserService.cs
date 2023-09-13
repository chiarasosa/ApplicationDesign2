﻿using Obligatorio1.Domain;

namespace Obligatorio1.IBusinessLogic
{
    public interface IUserService
    {
        //User Managment
        void RegisterUser(User user);
        User UpdateUserProfile(User user);
        User Login(string email, string password);
        void Logout(User user);
        User GetUserByID(int userID);

        //User Recovery
        IEnumerable<User> GetUsers();

    }
}

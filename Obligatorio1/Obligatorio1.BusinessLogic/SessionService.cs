﻿using System.Security.Authentication;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.BusinessLogic;

public class SessionService : ISessionService
{
    private User? _currentUser;
    private IGenericRepository<Session> _sessionRepository;
    private IGenericRepository<User> _userRepository;

    public SessionService(IGenericRepository<Session> sessionRepository, IGenericRepository<User> userRepository)
    {
        _sessionRepository = sessionRepository;
        _userRepository = userRepository;
    }

    public User? GetCurrentUser(Guid? authToken = null)
    {
        if (_currentUser != null)
            return _currentUser;

        if (authToken == null)
            throw new ArgumentException("Cant retrieve user without auth token");

        var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User" });

        if (session != null)
            _currentUser = session.User;

        return _currentUser;
    }

    public Guid Authenticate(string email, string password)
    {
        var user = _userRepository.Get(u => u.Email == email && u.Password == password);

        if (user == null)
            throw new InvalidCredentialException("Invalid credentials");

        var session = new Session() { User = user };
        _sessionRepository.Insert(session);
        _sessionRepository.Save();

        return session.AuthToken;
    }
    public void Logout(User user)
    {
        var session = _sessionRepository.Get(s => s.User == user, new List<string>() { "User" });
        _sessionRepository.Delete(session);
        _sessionRepository.Save();
    }
}
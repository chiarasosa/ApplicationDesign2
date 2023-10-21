using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Authentication;
using Obligatorio1.Domain;

namespace Obligatorio1.IBusinessLogic
{
    public interface ISessionService
    {
        User? GetCurrentUser(Guid? authToken = null);
        Guid Authenticate(string email, string password);
        void Logout(User user);
    }
}

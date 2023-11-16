using Obligatorio1.Domain;

namespace Obligatorio1.IBusinessLogic
{
    public interface ISessionService
    {
        User? GetCurrentUser(Guid? authToken = null);
        Guid Authenticate(string email, string password);
        void Logout(Guid authToken);
    }
}

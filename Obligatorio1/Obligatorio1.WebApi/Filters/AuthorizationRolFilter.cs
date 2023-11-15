using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Obligatorio1.IBusinessLogic;
using System;

namespace Obligatorio1.WebApi.Filters
{
    public class AuthorizationRolFilter : Attribute, IAuthorizationFilter
    {
        private readonly string _roleNeeded = "Administrador";
        private readonly ISessionService _sessionService;

        // Recibe por inyeccion de dependencia, para esto tengo que registrarlo como
        // service filter
        public AuthorizationRolFilter(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var sessionService = context.HttpContext.RequestServices.GetRequiredService<ISessionService>();

            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
            Guid token = Guid.Empty;

            if (string.IsNullOrEmpty(authorizationHeader) || !Guid.TryParse(authorizationHeader, out token))
            {
                context.Result = new ObjectResult(new { Message = "Authorization header is missing." })
                {
                    StatusCode = 401
                };
                return;
            }

            var currentUser = sessionService.GetCurrentUser(token);

            if (currentUser == null)
            {
                context.Result = new ObjectResult(new { Message = "Not authenticated." })
                {
                    StatusCode = 401
                };
                return;
            }
            else if (currentUser.Role != _roleNeeded)
            {
                context.Result = new ObjectResult(new { Message = "Necesita rol Administrador para realizar esta acción." })
                {
                    StatusCode = 403
                };
            }
        }
    }
}
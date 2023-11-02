using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Obligatorio1.Exceptions;

namespace Obligatorio1.WebApi.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var statusCode = 500; // Código de estado predeterminado

        if (context.Exception is ResourceNotFoundException)
        {
            statusCode = 404;
        }
        else if (context.Exception is InvalidResourceException)
        {
            statusCode = 400;
        }
        else if (context.Exception is InvalidOperationException)
        {
            statusCode = 409;
        }
        else if (context.Exception is InvalidCredentialException)
        {
            statusCode = 401;
        }
        else if (context.Exception is UserException)
        {
            statusCode = 422; // Puedes usar otro código de estado apropiado para UserException
        }
        else if (context.Exception is ProductManagmentException)
        {
            statusCode = 422; // Puedes usar otro código de estado apropiado para UserException
        }
        else if (context.Exception is CartException)
        {
            statusCode = 422; // Puedes usar otro código de estado apropiado para UserException
        }
        else if (context.Exception is CartManagmentException)
        {
            statusCode = 422; // Puedes usar otro código de estado apropiado para UserException
        }
        else if (context.Exception is CartProductException)
        {
            statusCode = 422; // Puedes usar otro código de estado apropiado para UserException
        }
        else if (context.Exception is PurchaseException)
        {
            statusCode = 422; // Puedes usar otro código de estado apropiado para UserException
        }
        else if (context.Exception is UserManagmentException)
        {
            statusCode = 422; // Puedes usar otro código de estado apropiado para UserException
        }

            context.Result = new ObjectResult(new { Message = context.Exception.Message })
        {
            StatusCode = statusCode
        };
    }
}

}


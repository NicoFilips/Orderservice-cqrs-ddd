using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderService.SharedKernel.Exceptions;

namespace OrderService.API.Filters;

public class ErrorHandlingFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Exception exception = context.Exception;
        HttpStatusCode code = HttpStatusCode.InternalServerError;

        if (exception is NotFoundException) code = HttpStatusCode.NotFound;
        else if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
        else if (exception is ValidationException) code = HttpStatusCode.BadRequest;

        context.Result = new JsonResult(new
        {
            error = new
            {
                message = exception.Message,
                exceptionType = exception.GetType().Name,
            }
        })
        {
            StatusCode = (int)code
        };
        context.ExceptionHandled = true;
    }
}

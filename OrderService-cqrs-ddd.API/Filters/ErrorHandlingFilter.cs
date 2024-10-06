using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using OrderService_cqrs_ddd.SharedKernel.Exceptions;

namespace OrderService_cqrs_ddd.API.Filters;

public class ErrorHandlingFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected

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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArch.API.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        if (exception is FluentValidation.ValidationException validationException)
        {
            var errors = validationException.Errors
                .Select(e => $"{e.PropertyName} : {e.ErrorMessage}")
                .ToList();
            var result = new ObjectResult(new { Errors = errors })
            {
                StatusCode = 400,
            };
            context.Result = result;
        }
        else if (exception is ArgumentNullException or KeyNotFoundException)
        {
            context.Result = new NotFoundObjectResult(new { Error =  "Not found" })
            {
                StatusCode = 404,
            };
        }
        else
        {
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        context.ExceptionHandled = true;
    }
}
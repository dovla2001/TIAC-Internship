using Application.Exceptions;
using FastEndpoints;
using Microsoft.AspNetCore.Diagnostics;
using System.Security.Authentication;

namespace Presentation.ExceptionHandlers
{
    public class CustomApiExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is InvalidCredentialException invalidCredentialException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Detail = invalidCredentialException.Message
                }, cancellationToken);

                return true;
            }

            if (exception is DuplicateEmailException duplicateEmailException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Detail = duplicateEmailException.Message
                }, cancellationToken);

                return true;
            }

            if (exception is DuplicateAttributeException duplicateAttributeException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Detail = duplicateAttributeException.Message
                }, cancellationToken);

                return true;
            }

            if (exception is DuplicateAttributeValueException duplicateAttributeValueException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Detail = duplicateAttributeValueException.Message
                }, cancellationToken);

                return true;
            }

            if (exception is DuplicateProductVariantException duplicateProductVariantException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Detail = duplicateProductVariantException.Message
                }, cancellationToken);

                return true;
            }

            return false;
        }
    }
}

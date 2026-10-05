using System.Net;
using System.Text.Json;
using CustomerPetPortal.API.Exceptions;

namespace CustomerPetPortal.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(
            HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    ex.Message);

                await HandleExceptionAsync(
                    context,
                    ex);
            }
        }

        private static Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            HttpStatusCode statusCode =
                HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case DuplicateEmailException:
                    statusCode = HttpStatusCode.Conflict;
                    break;

                case CustomerNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;

                case PetNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case ArgumentException:
    statusCode = HttpStatusCode.BadRequest;
    break;

case InvalidOperationException:
    statusCode = HttpStatusCode.BadRequest;
    break;
            }

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = exception.Message
            };

            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                (int)statusCode;

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
namespace SPANTECH.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Proceed to the next middleware
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // Handle any exceptions
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log the exception (you can enhance this with proper logging mechanisms)
            Console.WriteLine($"Error: {exception.Message}");

            // Prepare the error response
            var response = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = "An unexpected error occurred. Please try again later.",
                Details = exception.Message // Optionally include detailed messages in dev environments
            };

            // Set the response status and content type
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Serialize the error response
            var responseText = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(responseText);
        }
    }

}

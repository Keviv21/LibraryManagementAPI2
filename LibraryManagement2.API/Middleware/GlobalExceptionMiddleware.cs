
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace LibraryManagement2.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update exception occurred");

                
                if (dbEx.InnerException?.Message.Contains("IX_Books_ISBN") == true ||
                    dbEx.InnerException?.Message.Contains("UNIQUE KEY") == true)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict; 
                    context.Response.ContentType = "application/json";

                    var errorResponse = new
                    {
                        StatusCode = 409,
                        Message = "A book with the same ISBN already exists."
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                    return;
                }

                throw; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace LibraryManagement2.API.Extensions;

public static class RateLimitingExtensions
{
    public static IServiceCollection AddCustomRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            
            options.AddPolicy("fixed", context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 3,
                        Window = TimeSpan.FromSeconds(10),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));

            
            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    status = 429,
                    error = "Too Many Requests",
                    message = "You’ve made too many requests in a short time. Please wait and try again later.",
                    retryAfterSeconds = 10,
                    timestamp = DateTime.UtcNow
                };

                var json = JsonSerializer.Serialize(errorResponse);

                context.HttpContext.Response.Headers["Retry-After"] = "10";
                await context.HttpContext.Response.WriteAsync(json, token);
            };
        });

        return services;
    }
}

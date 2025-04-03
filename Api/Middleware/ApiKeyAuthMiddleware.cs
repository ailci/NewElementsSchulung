using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middleware;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
{
    private const string ApiKeyName = "x-api-key";

    public async Task Invoke(HttpContext httpContext)
    {
        //Kein Api KEy übergeben
        if (!httpContext.Request.Headers.TryGetValue(ApiKeyName, out var apiKeyToValidate))
        {
            //1. Variante
            //httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //await httpContext.Response.WriteAsync("Api Key fehlt (using Middleware)");
            //return;

            //2. Variante
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Type = "https://tools.ietf.org",
                Title = "An error occurred",
                Status = StatusCodes.Status401Unauthorized,
                Detail = "Api Key fehlt (middleware)"
            });
            return;
        }

        //Key aus config holen
        var apiKeyFromConfig = configuration.GetValue<string>(ApiKeyName);

        if (!apiKeyToValidate.Equals(apiKeyFromConfig))
        {
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Type = "https://tools.ietf.org",
                Title = "An error occurred",
                Status = StatusCodes.Status401Unauthorized,
                Detail = "Api Key invalid (middleware)"
            });
            return;
        }

        await next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ApiKeyAuthMiddlewareExtensions
{
    public static IApplicationBuilder UseApiKeyAuthMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiKeyAuthMiddleware>();
    }
}
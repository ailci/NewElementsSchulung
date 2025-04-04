using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http;

namespace Api.Filter;

// Registrierung in ServiceCollection
public class ApiKeyAuthFilter(IConfiguration configuration) : IAuthorizationFilter
{
    private const string ApiKeyName = "x-api-key";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyName, out var apiKeyToValidate))
        {
            context.Result = new UnauthorizedObjectResult(new ProblemDetails
            {
                Type = "https://tools.ietf.org",
                Title = "An error occurred",
                Status = StatusCodes.Status401Unauthorized,
                Detail = "Api Key fehlt (filter)"
            });
            return;
        }

        //Key aus config holen
        var apiKeyFromConfig = configuration.GetValue<string>(ApiKeyName);

        if (!apiKeyToValidate.Equals(apiKeyFromConfig))
        {
            context.Result = new UnauthorizedObjectResult(new ProblemDetails
            {
                Type = "https://tools.ietf.org",
                Title = "An error occurred",
                Status = StatusCodes.Status401Unauthorized,
                Detail = "Api Key invalid (filter)"
            });
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Api.Middleware;

public enum Browser
{
    InternetExplorer,
    Firefox,
    Chrome,
    Edge,
    Opera,
    SomethingElse
}

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class BrowserAllowedMiddleware(RequestDelegate next, IEnumerable<Browser> browserWhiteList)
{
    public async Task Invoke(HttpContext httpContext)
    {
        var clientBrowserType = IdentitfyBrowserType(httpContext);

        if (browserWhiteList.Any(browser => browser == clientBrowserType))
        {
            await next(httpContext); //Browser in Whitelist gefunden. Aufruf der nächsten Middleware
        }
        else
        {
            //Fehlerfall
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            httpContext.Response.ContentType = "text/html";
            await httpContext.Response.WriteAsync(
                "Der Browser ist nicht erlaubt. <a href=\"https://browsehappy.com\">BrowseHappy</a>");
        }

    }

    private Browser IdentitfyBrowserType(HttpContext httpContext)
    {
        var userAgent = httpContext.Request.Headers["User-Agent"][0]?.ToLower();
        Browser browserType;

        if (userAgent.Contains("chrome") &&
            !(userAgent.Contains("edge") || userAgent.Contains("edg") || userAgent.Contains("opr")))
        {
            browserType = Browser.Chrome;
        }
        else if (userAgent.Contains("firefox"))
        {
            browserType = Browser.Firefox;
        }
        else if (userAgent.Contains("trident"))
        {
            browserType = Browser.InternetExplorer;
        }
        else if (userAgent.Contains("edge") || userAgent.Contains("edg"))
        {
            browserType = Browser.Edge;
        }
        else if (userAgent.Contains("opr"))
        {
            browserType = Browser.Opera;
        }
        else
        {
            browserType = Browser.SomethingElse;
        }

        return browserType;
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class BrowserAllowedMiddlewareExtensions
{
    public static IApplicationBuilder UseBrowserAllowedMiddleware(this IApplicationBuilder builder, params IEnumerable<Browser> browserWhiteList)
    {
        return builder.UseMiddleware<BrowserAllowedMiddleware>(browserWhiteList);
    }
}
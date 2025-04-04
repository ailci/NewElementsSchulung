﻿using Domain.Exceptions;
using Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILoggerManager _logger;
    private readonly IProblemDetailsService _problemDetailsService;

    public GlobalExceptionHandler(ILoggerManager logger, IProblemDetailsService problemDetailsService)
    {
        _logger = logger;
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";

        //StatusCode setzen
        httpContext.Response.StatusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        _logger.LogError($"Something went wrong: {exception.Message}");

        var result = await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Title = "An error occurred",
                Status = httpContext.Response.StatusCode,
                Detail = exception.Message,
                Type = exception.GetType().Name
            },
            Exception = exception
        });

        //Fallback
        if (!result)
        {
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = "An error occurred",
                Status = httpContext.Response.StatusCode,
                Detail = exception.Message,
                Type = exception.GetType().Name
            }, cancellationToken);
        }

        return true;
    }
}
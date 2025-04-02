using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Logging;

public static class LoggingServiceRegistration
{
    public static WebApplicationBuilder ConfigLoggingService(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

        builder.Host.UseSerilog((hostContext, configuration) =>
        {
            configuration.ReadFrom.Configuration(hostContext.Configuration);
        });

        return builder;
    }
}
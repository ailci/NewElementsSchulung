using Api.Controllers;
using Api.Filter;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Contracts;
using Persistence.Repositories;
using Services;
using Services.Resolver;

namespace Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureApi(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            //Global Filter
            //options.Filters.Add<ApiKeyAuthFilter>();

            options.ReturnHttpNotAcceptable = true; //Benachrichtige den Client dass sein Wunsch nicht akzeptiert wird
            options.RespectBrowserAcceptHeader = true;
        }).AddXmlSerializerFormatters();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        //Automapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTransient<FormFileToByteArrayResolver>();

        //Global Exception Handler
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        //Filter
        services.AddScoped<ApiKeyAuthFilter>();
        services.AddScoped<ValidationFilter>();

        return services;
    }

    public static IServiceCollection ConfigureDb(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<QotdContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.EnableSensitiveDataLogging();
        });

        return services;
    }

    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IQuoteRepository, QuoteRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        services.AddScoped<IQotdService, QotdService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IServiceManager, ServiceManager>();

        return services;
    }

    public static IServiceCollection ConfigureOutputCaching(this IServiceCollection services)
    {
        //Default
        //services.AddOutputCache();

        services.AddOutputCache(options =>
        {
            //options.AddBasePolicy(b => b.Expire(TimeSpan.FromSeconds(10))); // für alle Endpoints für 10 Sekunden

            options.AddPolicy("120SecondsDuration", p => p.Expire(TimeSpan.FromSeconds(120)));
            options.AddPolicy("RouteParamDuration", p => p.Expire(TimeSpan.FromSeconds(20)).SetVaryByRouteValue("id"));
        });

        return services;
    }
}
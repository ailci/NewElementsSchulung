namespace Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureApi(this IServiceCollection services)
    {
        services.AddControllers();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        return services;
    }
}
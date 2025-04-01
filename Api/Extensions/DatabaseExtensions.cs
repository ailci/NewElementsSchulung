using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyMigrationAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        await using QotdContext context = scope.ServiceProvider.GetRequiredService<QotdContext>();

        try
        {
            await context.Database.MigrateAsync();
            app.Logger.LogInformation("Migrationen ausgeführt");
        }
        catch (Exception e)
        {
            app.Logger.LogError(e, "Ein Fehler ist aufgetreten");
            throw;
        }
    }
}
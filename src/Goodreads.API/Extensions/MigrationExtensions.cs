using Microsoft.EntityFrameworkCore;

namespace Goodreads.API.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyMigrationsAsync<TContext>(this IApplicationBuilder app)
    where TContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

        try
        {
            logger.LogInformation("Starting database migration for {DbContext}", typeof(TContext).Name);
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Database migration completed for {DbContext}", typeof(TContext).Name);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database for {DbContext}", typeof(TContext).Name);
            throw;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Goodreads.Infrastructure.Persistence.Seeders;
public class AppSeeder(IServiceProvider serviceProvider)
{
    public async Task SeedAsync()
    {
        using var scope = serviceProvider.CreateScope();
        var seeders = scope.ServiceProvider.GetServices<ISeeder>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync();
        }
    }
}

using Goodreads.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Goodreads.Infrastructure.Jobs;
internal class RefreshTokenCleanupJob(ApplicationDbContext context)
{

    public async Task RunAsync()
    {
        var tokens = await context.RefreshTokens
            .Where(x => (x.IsUsed || x.IsRevoked) && x.ExpiryDate < DateTime.UtcNow)
            .ExecuteDeleteAsync();

        await context.SaveChangesAsync();
    }
}
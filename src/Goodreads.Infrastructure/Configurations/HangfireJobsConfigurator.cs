using Goodreads.Infrastructure.Jobs;
using Hangfire;

namespace Goodreads.Infrastructure.Configurations;
public static class HangfireJobsConfigurator
{
    public static void ConfigureRecurringJobs()
    {
        RecurringJob.AddOrUpdate<RefreshTokenCleanupJob>(
            "refresh-token-cleanup",
            job => job.RunAsync(),
            Cron.Daily);
    }
}
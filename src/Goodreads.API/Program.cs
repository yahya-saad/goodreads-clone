using Goodreads.API.Extensions;
using Goodreads.Application;
using Goodreads.Infrastructure;
using Goodreads.Infrastructure.Configurations;
using Goodreads.Infrastructure.Persistence;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Goodreads API";
        options.OpenApiRoutePattern = "/swagger/v1/swagger.json";
    });
}

if (builder.Configuration.GetValue<bool>("RunMigrations"))
{
    await app.ApplyMigrationsAsync<ApplicationDbContext>();
    await app.SeedDataAsync();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireCustomBasicAuthenticationFilter(){
        User = "admin",
        Pass = "admin"
    } },
});

HangfireJobsConfigurator.ConfigureRecurringJobs();

app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

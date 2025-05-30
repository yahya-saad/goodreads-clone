using Goodreads.API.Extensions;
using Goodreads.API.Middlewares;
using Goodreads.Application;
using Goodreads.Infrastructure;
using Goodreads.Infrastructure.Persistence;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<AuthorizationExceptionHandler>();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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

app.Run();

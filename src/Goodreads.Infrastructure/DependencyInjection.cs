using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Entities;
using Goodreads.Infrastructure.Identity;
using Goodreads.Infrastructure.Persistence;
using Goodreads.Infrastructure.Repositories;
using Goodreads.Infrastructure.Security.TokenProvider;
using Goodreads.Infrastructure.Services.EmailService;
using Goodreads.Infrastructure.Services.Storage;
using Goodreads.Infrastructure.Services.TokenProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goodreads.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPersistence(configuration)
            .AddIdentity()
            .AddAuthentication(configuration)
            .AddEmailServices(configuration)
            .AddBlobStorage(configuration);

        return services;
    }


    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserFollowRepository, UserFollowRepository>();


        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

        services
            .ConfigureOptions<TokenProviderConfiguration>()
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

        services.AddHttpContextAccessor();
        services.AddScoped<ITokenProvider, JwtTokeProvider>();
        services.AddScoped<IUserContext, UserContext>();

        return services;
    }

    private static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.Section));

        var emailSettings = configuration.GetSection(EmailSettings.Section).Get<EmailSettings>()
            ?? throw new InvalidOperationException("Email settings are not configured properly.");

        if (emailSettings.UseSmtp4Dev)
        {
            emailSettings.Host = "localhost";
            emailSettings.Port = 25;
            emailSettings.FromEmail = "noreply@localhost";
            services.AddFluentEmail(emailSettings.FromEmail)
            .AddSmtpSender(emailSettings.Host, emailSettings.Port);
        }
        else
        {
            services.AddFluentEmail(emailSettings.FromEmail, emailSettings.FromName)
                .AddSmtpSender(emailSettings.Host, emailSettings.Port, emailSettings.Username, emailSettings.Password);
        }

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }

    private static IServiceCollection AddBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BlobStorageSettings>(configuration.GetSection(BlobStorageSettings.Section));
        services.AddSingleton<IBlobStorageService, BlobStorageService>();
        return services;
    }

}



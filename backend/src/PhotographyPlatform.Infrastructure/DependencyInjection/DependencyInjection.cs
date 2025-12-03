using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotographyPlatform.Domain.Authentication.Repositories;
using PhotographyPlatform.Domain.BookingManagement.Repositories;
using PhotographyPlatform.Domain.PhotoManagement.Repositories;
using PhotographyPlatform.Domain.StudioManagement.Repositories;
using PhotographyPlatform.Infrastructure.Common;
using PhotographyPlatform.Infrastructure.Data;
using PhotographyPlatform.Infrastructure.Repositories;

namespace PhotographyPlatform.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDatabase(configuration)
            .AddRepositories();
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<PhotographyPlatformDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly("PhotographyPlatform.Infrastructure");
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
            });

            // Enable sensitive data logging in development
            options.EnableSensitiveDataLogging(bool.Parse(configuration["Logging:EnableSensitiveDataLogging"] ?? "false"));
            
            // Enable detailed errors in development
            options.EnableDetailedErrors(bool.Parse(configuration["Logging:EnableDetailedErrors"] ?? "false"));
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStudioProfileRepository, StudioProfileRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();

        return services;
    }
}
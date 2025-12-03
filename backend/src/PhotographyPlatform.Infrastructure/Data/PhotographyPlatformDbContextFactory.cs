using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PhotographyPlatform.Infrastructure.Data;

public class PhotographyPlatformDbContextFactory : IDesignTimeDbContextFactory<PhotographyPlatformDbContext>
{
    public PhotographyPlatformDbContext CreateDbContext(string[] args)
    {
        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        // Get connection string
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Server=localhost;Database=PhotographyPlatform;User Id=postgres;Password=password;";

        // Create options
        var optionsBuilder = new DbContextOptionsBuilder<PhotographyPlatformDbContext>();
        optionsBuilder.UseNpgsql(connectionString, npgsqlOptions =>
        {
            npgsqlOptions.MigrationsAssembly("PhotographyPlatform.Infrastructure");
        });

        return new PhotographyPlatformDbContext(optionsBuilder.Options);
    }
}
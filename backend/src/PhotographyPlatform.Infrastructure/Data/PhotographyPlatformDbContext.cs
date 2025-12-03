using Microsoft.EntityFrameworkCore;
using PhotographyPlatform.Domain.Authentication.Entities;
using PhotographyPlatform.Domain.BookingManagement.Entities;
using PhotographyPlatform.Domain.Common.Abstractions;
using PhotographyPlatform.Domain.PhotoManagement.Entities;
using PhotographyPlatform.Domain.StudioManagement.Entities;
using System.Reflection;

namespace PhotographyPlatform.Infrastructure.Data;

public class PhotographyPlatformDbContext : DbContext
{
    public PhotographyPlatformDbContext(DbContextOptions<PhotographyPlatformDbContext> options) 
        : base(options)
    {
    }

    // Authentication Domain
    public DbSet<User> Users => Set<User>();

    // Studio Management Domain
    public DbSet<StudioProfile> StudioProfiles => Set<StudioProfile>();

    // Booking Management Domain
    public DbSet<Booking> Bookings => Set<Booking>();

    // Photo Management Domain
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Photo> Photos => Set<Photo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all entity configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configure schema
        modelBuilder.HasDefaultSchema("photography_platform");

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Update timestamp for entities that inherit from Entity
        var entries = ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Modified);

        foreach (var entry in entries)
        {
            // Use reflection to call SetUpdatedAt if it exists on the entity
            var setUpdatedAtMethod = entry.Entity.GetType().GetMethod("SetUpdatedAt", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            setUpdatedAtMethod?.Invoke(entry.Entity, null);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesWithEventsAsync(CancellationToken cancellationToken = default)
    {
        // Get domain events before saving
        var domainEvents = GetDomainEvents();

        // Save changes
        var result = await SaveChangesAsync(cancellationToken);

        // TODO: Publish domain events (will be implemented in Application layer)
        // This is where you would publish events to MediatR

        return result;
    }

    private List<IDomainEvent> GetDomainEvents()
    {
        var domainEvents = new List<IDomainEvent>();

        var aggregateRoots = ChangeTracker.Entries()
            .Select(e => e.Entity)
            .OfType<object>()
            .Where(entity => 
            {
                var entityType = entity.GetType();
                return entityType.GetInterfaces().Any(i => 
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAggregateRoot<>));
            });

        foreach (var aggregateRoot in aggregateRoots)
        {
            // Use reflection to get DomainEvents and ClearDomainEvents
            var domainEventsProperty = aggregateRoot.GetType().GetProperty("DomainEvents");
            var clearDomainEventsMethod = aggregateRoot.GetType().GetMethod("ClearDomainEvents");
            
            if (domainEventsProperty?.GetValue(aggregateRoot) is IReadOnlyCollection<IDomainEvent> events && events.Any())
            {
                domainEvents.AddRange(events);
                clearDomainEventsMethod?.Invoke(aggregateRoot, null);
            }
        }

        return domainEvents;
    }
}
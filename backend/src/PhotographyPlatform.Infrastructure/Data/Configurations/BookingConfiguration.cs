using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.BookingManagement.Entities;
using PhotographyPlatform.Domain.BookingManagement.ValueObjects;
using PhotographyPlatform.Domain.StudioManagement.ValueObjects;

namespace PhotographyPlatform.Infrastructure.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");

        // Primary Key
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasConversion(
                bookingId => bookingId.Value,
                value => BookingId.Create(value))
            .HasColumnName("id");

        // Client ID
        builder.Property(b => b.ClientId)
            .HasConversion(
                userId => userId.Value,
                value => UserId.Create(value).Value)
            .HasColumnName("client_id")
            .IsRequired();

        // Photographer ID
        builder.Property(b => b.PhotographerId)
            .HasConversion(
                studioProfileId => studioProfileId.Value,
                value => StudioProfileId.Create(value))
            .HasColumnName("photographer_id")
            .IsRequired();

        // Booking Date - Value Object
        builder.OwnsOne(b => b.BookingDate, date =>
        {
            date.Property(d => d.Value)
                .HasColumnName("booking_date")
                .HasColumnType("date")
                .IsRequired();
        });

        // Start Time
        builder.Property(b => b.StartTime)
            .HasColumnName("start_time")
            .HasColumnType("time")
            .IsRequired();

        // End Time
        builder.Property(b => b.EndTime)
            .HasColumnName("end_time")
            .HasColumnType("time")
            .IsRequired();

        // Package Type - Value Object
        builder.Property(b => b.Package)
            .HasConversion(
                package => package.Value,
                value => PackageType.FromString(value))
            .HasColumnName("package_type")
            .HasMaxLength(50)
            .IsRequired();

        // Total Amount - Money Value Object
        builder.OwnsOne(b => b.TotalAmount, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("total_amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        // Status - Value Object
        builder.Property(b => b.Status)
            .HasConversion(
                status => status.Value,
                value => BookingStatus.FromString(value))
            .HasColumnName("status")
            .HasMaxLength(50)
            .IsRequired();

        // Special Requests
        builder.Property(b => b.SpecialRequests)
            .HasColumnName("special_requests")
            .HasMaxLength(1000);

        // Confirmed At
        builder.Property(b => b.ConfirmedAt)
            .HasColumnName("confirmed_at");

        // Cancelled At
        builder.Property(b => b.CancelledAt)
            .HasColumnName("cancelled_at");

        // Cancellation Reason
        builder.Property(b => b.CancellationReason)
            .HasColumnName("cancellation_reason")
            .HasMaxLength(500);

        // Timestamps
        builder.Property(b => b.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(b => b.UpdatedAt)
            .HasColumnName("updated_at");

        // Configure relationships
        builder.HasOne<Domain.Authentication.Entities.User>()
            .WithMany()
            .HasForeignKey(b => b.ClientId)
            .HasConstraintName("fk_bookings_client_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Domain.Authentication.Entities.User>()
            .WithMany()
            .HasForeignKey(b => b.PhotographerId)
            .HasConstraintName("fk_bookings_photographer_id")
            .OnDelete(DeleteBehavior.Restrict);

        // Configure indexes
        builder.HasIndex(b => b.ClientId)
            .HasDatabaseName("ix_bookings_client_id");

        builder.HasIndex(b => b.PhotographerId)
            .HasDatabaseName("ix_bookings_photographer_id");

        builder.HasIndex(b => b.Status)
            .HasDatabaseName("ix_bookings_status");

        builder.HasIndex(b => new { b.PhotographerId, b.BookingDate })
            .HasDatabaseName("ix_bookings_photographer_date");

        // Composite index for availability checking
        builder.HasIndex(b => new { b.PhotographerId, b.BookingDate, b.StartTime, b.EndTime })
            .HasDatabaseName("ix_bookings_availability");
    }
}
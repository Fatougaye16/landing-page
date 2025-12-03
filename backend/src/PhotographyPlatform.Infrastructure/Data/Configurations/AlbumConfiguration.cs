using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.BookingManagement.ValueObjects;
using PhotographyPlatform.Domain.PhotoManagement.Entities;
using PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

namespace PhotographyPlatform.Infrastructure.Data.Configurations;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("albums");

        // Primary Key
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasConversion(
                albumId => albumId.Value,
                value => AlbumId.Create(value))
            .HasColumnName("id");

        // Booking ID
        builder.Property(a => a.BookingId)
            .HasConversion(
                bookingId => bookingId.Value,
                value => BookingId.Create(value))
            .HasColumnName("booking_id")
            .IsRequired();

        // Client ID
        builder.Property(a => a.ClientId)
            .HasConversion(
                userId => userId.Value,
                value => UserId.Create(value).Value)
            .HasColumnName("client_id")
            .IsRequired();

        // Photographer ID
        builder.Property(a => a.PhotographerId)
            .HasConversion(
                userId => userId.Value,
                value => UserId.Create(value).Value)
            .HasColumnName("photographer_id")
            .IsRequired();

        // Title
        builder.Property(a => a.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired();

        // Description
        builder.Property(a => a.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);

        // Status - Value Object
        builder.Property(a => a.Status)
            .HasConversion(
                status => status.Value,
                value => AlbumStatus.FromString(value))
            .HasColumnName("status")
            .HasMaxLength(50)
            .IsRequired();

        // Event Date
        builder.Property(a => a.EventDate)
            .HasColumnName("event_date")
            .HasColumnType("date")
            .IsRequired();

        // Submitted For Review At
        builder.Property(a => a.SubmittedForReviewAt)
            .HasColumnName("submitted_for_review_at");

        // Completed At
        builder.Property(a => a.CompletedAt)
            .HasColumnName("completed_at");

        // Timestamps
        builder.Property(a => a.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(a => a.UpdatedAt)
            .HasColumnName("updated_at");

        // Configure relationships
        builder.HasOne<Domain.BookingManagement.Entities.Booking>()
            .WithMany()
            .HasForeignKey(a => a.BookingId)
            .HasConstraintName("fk_albums_booking_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Domain.Authentication.Entities.User>()
            .WithMany()
            .HasForeignKey(a => a.ClientId)
            .HasConstraintName("fk_albums_client_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Domain.Authentication.Entities.User>()
            .WithMany()
            .HasForeignKey(a => a.PhotographerId)
            .HasConstraintName("fk_albums_photographer_id")
            .OnDelete(DeleteBehavior.Restrict);

        // Photos relationship - One-to-many
        builder.HasMany(a => a.Photos)
            .WithOne()
            .HasForeignKey(p => p.AlbumId)
            .HasConstraintName("fk_photos_album_id")
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(a => a.BookingId)
            .IsUnique()
            .HasDatabaseName("ix_albums_booking_id");

        builder.HasIndex(a => a.ClientId)
            .HasDatabaseName("ix_albums_client_id");

        builder.HasIndex(a => a.PhotographerId)
            .HasDatabaseName("ix_albums_photographer_id");

        builder.HasIndex(a => a.Status)
            .HasDatabaseName("ix_albums_status");

        builder.HasIndex(a => a.EventDate)
            .HasDatabaseName("ix_albums_event_date");
    }
}
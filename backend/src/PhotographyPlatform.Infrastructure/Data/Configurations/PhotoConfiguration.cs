using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.PhotoManagement.Entities;
using PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

namespace PhotographyPlatform.Infrastructure.Data.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.ToTable("photos");

        // Primary Key
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                photoId => photoId.Value,
                value => PhotoId.Create(value))
            .HasColumnName("id");

        // Album ID - Foreign Key
        builder.Property(p => p.AlbumId)
            .HasConversion(
                albumId => albumId.Value,
                value => AlbumId.Create(value))
            .HasColumnName("album_id")
            .IsRequired();

        // Original File Path
        builder.Property(p => p.OriginalFilePath)
            .HasColumnName("original_file_path")
            .HasMaxLength(500)
            .IsRequired();

        // Edited File Path
        builder.Property(p => p.EditedFilePath)
            .HasColumnName("edited_file_path")
            .HasMaxLength(500);

        // Photo Metadata - Value Object
        builder.OwnsOne(p => p.Metadata, metadata =>
        {
            metadata.Property(m => m.FileName)
                .HasColumnName("file_name")
                .HasMaxLength(255)
                .IsRequired();

            metadata.Property(m => m.FileExtension)
                .HasColumnName("file_extension")
                .HasMaxLength(10)
                .IsRequired();

            metadata.Property(m => m.FileSizeBytes)
                .HasColumnName("file_size_bytes")
                .IsRequired();

            metadata.Property(m => m.Width)
                .HasColumnName("width")
                .IsRequired();

            metadata.Property(m => m.Height)
                .HasColumnName("height")
                .IsRequired();

            metadata.Property(m => m.CapturedAt)
                .HasColumnName("captured_at")
                .IsRequired();

            metadata.Property(m => m.CameraModel)
                .HasColumnName("camera_model")
                .HasMaxLength(100);

            metadata.Property(m => m.CameraSettings)
                .HasColumnName("camera_settings")
                .HasMaxLength(500);
        });

        // Status - Value Object
        builder.Property(p => p.Status)
            .HasConversion(
                status => status.Value,
                value => PhotoStatus.FromString(value))
            .HasColumnName("status")
            .HasMaxLength(50)
            .IsRequired();

        // Is Selected For Editing
        builder.Property(p => p.IsSelectedForEditing)
            .HasColumnName("is_selected_for_editing")
            .IsRequired()
            .HasDefaultValue(false);

        // Edited By - User ID
        builder.Property(p => p.EditedBy)
            .HasConversion(
                userId => userId != null ? userId.Value : (Guid?)null,
                value => value != null ? UserId.Create(value.Value).Value : null)
            .HasColumnName("edited_by");

        // Edited At
        builder.Property(p => p.EditedAt)
            .HasColumnName("edited_at");

        // Approved By - User ID
        builder.Property(p => p.ApprovedBy)
            .HasConversion(
                userId => userId != null ? userId.Value : (Guid?)null,
                value => value != null ? UserId.Create(value.Value).Value : null)
            .HasColumnName("approved_by");

        // Approved At
        builder.Property(p => p.ApprovedAt)
            .HasColumnName("approved_at");

        // Client Notes
        builder.Property(p => p.ClientNotes)
            .HasColumnName("client_notes")
            .HasMaxLength(1000);

        // Photographer Notes
        builder.Property(p => p.PhotographerNotes)
            .HasColumnName("photographer_notes")
            .HasMaxLength(1000);

        // Timestamps
        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at");

        // Configure relationships
        builder.HasOne<Album>()
            .WithMany(a => a.Photos)
            .HasForeignKey(p => p.AlbumId)
            .HasConstraintName("fk_photos_album_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Domain.Authentication.Entities.User>()
            .WithMany()
            .HasForeignKey(p => p.EditedBy)
            .HasConstraintName("fk_photos_edited_by")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne<Domain.Authentication.Entities.User>()
            .WithMany()
            .HasForeignKey(p => p.ApprovedBy)
            .HasConstraintName("fk_photos_approved_by")
            .OnDelete(DeleteBehavior.SetNull);

        // Configure indexes
        builder.HasIndex(p => p.AlbumId)
            .HasDatabaseName("ix_photos_album_id");

        builder.HasIndex(p => p.Status)
            .HasDatabaseName("ix_photos_status");

        builder.HasIndex(p => p.IsSelectedForEditing)
            .HasDatabaseName("ix_photos_is_selected_for_editing");

        builder.HasIndex(p => p.EditedBy)
            .HasDatabaseName("ix_photos_edited_by");

        builder.HasIndex(p => p.ApprovedBy)
            .HasDatabaseName("ix_photos_approved_by");

        // Composite indexes for common queries
        builder.HasIndex(p => new { p.AlbumId, p.Status })
            .HasDatabaseName("ix_photos_album_status");

        builder.HasIndex(p => new { p.AlbumId, p.IsSelectedForEditing })
            .HasDatabaseName("ix_photos_album_selected");
    }
}
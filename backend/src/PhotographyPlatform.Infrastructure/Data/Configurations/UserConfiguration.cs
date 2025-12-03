using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotographyPlatform.Domain.Authentication.Entities;
using PhotographyPlatform.Domain.Authentication.ValueObjects;

namespace PhotographyPlatform.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        // Primary Key
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                userId => userId.Value,
                value => UserId.Create(value).Value) // We assume this won't fail for existing data
            .HasColumnName("id");

        // Email - Value Object
        builder.Property(u => u.Email)
            .HasConversion(
                email => email.Value,
                value => Email.CreateUnsafe(value).Value) // Unsafe because data is already validated
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("ix_users_email");

        // User Role - Value Object
        builder.Property(u => u.Role)
            .HasConversion(
                role => role.Value,
                value => UserRole.FromString(value))
            .HasColumnName("role")
            .HasMaxLength(50)
            .IsRequired();

        // First Name
        builder.Property(u => u.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(100)
            .IsRequired();

        // Last Name
        builder.Property(u => u.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(100)
            .IsRequired();

        // Phone Number
        builder.Property(u => u.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(20);

        // Is Active
        builder.Property(u => u.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasDefaultValue(true);

        // Timestamps
        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at");

        // Configure indexes
        builder.HasIndex(u => u.Role)
            .HasDatabaseName("ix_users_role");

        builder.HasIndex(u => u.IsActive)
            .HasDatabaseName("ix_users_is_active");
    }
}
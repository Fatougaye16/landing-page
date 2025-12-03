using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.StudioManagement.Entities;
using PhotographyPlatform.Domain.StudioManagement.ValueObjects;

namespace PhotographyPlatform.Infrastructure.Data.Configurations;

public class StudioProfileConfiguration : IEntityTypeConfiguration<StudioProfile>
{
    public void Configure(EntityTypeBuilder<StudioProfile> builder)
    {
        builder.ToTable("studio_profiles");

        // Primary Key
        builder.HasKey(sp => sp.Id);

        builder.Property(sp => sp.Id)
            .HasConversion(
                studioProfileId => studioProfileId.Value,
                value => StudioProfileId.Create(value))
            .HasColumnName("id");

        // User ID - Foreign Key
        builder.Property(sp => sp.UserId)
            .HasConversion(
                userId => userId.Value,
                value => UserId.Create(value))
            .HasColumnName("photographer_id")
            .IsRequired();

        // Studio Name
        builder.Property(sp => sp.StudioName)
            .HasColumnName("studio_name")
            .HasMaxLength(200)
            .IsRequired();

        // Description
        builder.Property(sp => sp.Description)
            .HasColumnName("bio")
            .HasMaxLength(1000);

        // Location
        builder.Property(sp => sp.Location)
            .HasColumnName("location")
            .HasMaxLength(200);

        // Base Price - Money Value Object
        builder.OwnsOne(sp => sp.BasePrice, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("base_price_amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("base_price_currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        // Specialization - Single Value Object
        builder.Property(sp => sp.Specialization)
            .HasConversion(
                specialization => specialization.Value,
                value => PhotographySpecialization.Create(value))
            .HasColumnName("specializations")
            .HasMaxLength(500);

        // Experience Years
        builder.Property(sp => sp.ExperienceYears)
            .HasColumnName("years_of_experience")
            .IsRequired();

        // Is Active
        builder.Property(sp => sp.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasDefaultValue(true);

        // Timestamps
        builder.Property(sp => sp.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(sp => sp.UpdatedAt)
            .HasColumnName("updated_at");

        // Portfolio Images - Collection stored as JSON
        builder.Property("_portfolioImages")
            .HasColumnName("portfolio_images")
            .HasColumnType("jsonb");

        // Available Days - Collection stored as JSON
        builder.Property("_availableDays")
            .HasColumnName("available_days")
            .HasColumnType("jsonb");

        // Configure relationships
        builder.HasOne<Domain.Authentication.Entities.User>()
            .WithMany()
            .HasForeignKey(sp => sp.UserId)
            .HasConstraintName("fk_studio_profiles_photographer_id")
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(sp => sp.UserId)
            .IsUnique()
            .HasDatabaseName("ix_studio_profiles_photographer_id");

        builder.HasIndex(sp => sp.IsActive)
            .HasDatabaseName("ix_studio_profiles_is_active");

        builder.HasIndex(sp => sp.Location)
            .HasDatabaseName("ix_studio_profiles_location");
    }
}
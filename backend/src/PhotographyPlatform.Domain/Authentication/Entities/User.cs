using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.StudioManagement.Entities;

namespace PhotographyPlatform.Domain.Authentication.Entities;

public class User : AggregateRoot<UserId>
{
    private User() : base()
    {
        // For EF Core
    }

    private User(
        UserId id,
        Email email,
        string passwordHash,
        UserRole role,
        string firstName,
        string lastName,
        string? phoneNumber = null) : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        IsEmailConfirmed = false;
    }

    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = string.Empty;
    public UserRole Role { get; private set; } = null!;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? PhoneNumber { get; private set; }
    public string? AvatarUrl { get; private set; }
    public bool IsEmailConfirmed { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime? LastLoginAt { get; private set; }

    // Navigation properties
    public StudioProfile? StudioProfile { get; private set; }

    public static User CreateClient(
        Email email,
        string passwordHash,
        string firstName,
        string lastName,
        string? phoneNumber = null)
    {
        var userId = UserId.Create();
        return new User(userId, email, passwordHash, UserRole.Client, firstName, lastName, phoneNumber);
    }

    public static User CreatePhotographer(
        Email email,
        string passwordHash,
        string firstName,
        string lastName,
        string? phoneNumber = null)
    {
        var userId = UserId.Create();
        return new User(userId, email, passwordHash, UserRole.Photographer, firstName, lastName, phoneNumber);
    }

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        SetUpdatedAt();
    }

    public void UpdateProfile(string firstName, string lastName, string? phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        SetUpdatedAt();
    }

    public void SetAvatarUrl(string avatarUrl)
    {
        AvatarUrl = avatarUrl;
        SetUpdatedAt();
    }

    public void ConfirmEmail()
    {
        IsEmailConfirmed = true;
        SetUpdatedAt();
    }

    public void UpdateLastLogin()
    {
        LastLoginAt = DateTime.UtcNow;
        SetUpdatedAt();
    }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }

    public bool CanCreateStudioProfile()
    {
        return Role.IsPhotographer && StudioProfile == null;
    }

    public void Activate()
    {
        IsActive = true;
        SetUpdatedAt();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdatedAt();
    }
}
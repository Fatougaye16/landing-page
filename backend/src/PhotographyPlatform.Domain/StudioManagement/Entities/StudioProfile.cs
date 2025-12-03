using ErrorOr;
using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.BookingManagement.ValueObjects;
using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.StudioManagement.ValueObjects;

namespace PhotographyPlatform.Domain.StudioManagement.Entities;

public class StudioProfile : AggregateRoot<StudioProfileId>
{
    private readonly List<string> _portfolioImages = new();
    private readonly List<string> _availableDays = new();

    private StudioProfile() : base()
    {
        // For EF Core
    }

    private StudioProfile(
        StudioProfileId id,
        UserId userId,
        string studioName,
        string description,
        PhotographySpecialization specialization,
        int experienceYears,
        Money basePrice,
        string location) : base(id)
    {
        UserId = userId;
        StudioName = studioName;
        Description = description;
        Specialization = specialization;
        ExperienceYears = experienceYears;
        BasePrice = basePrice;
        Location = location;
        IsActive = true;
    }

    public UserId UserId { get; private set; } = null!;
    public string StudioName { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public PhotographySpecialization Specialization { get; private set; } = null!;
    public int ExperienceYears { get; private set; }
    public Money BasePrice { get; private set; } = null!;
    public string Location { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public IReadOnlyCollection<string> PortfolioImages => _portfolioImages.AsReadOnly();
    public IReadOnlyCollection<string> AvailableDays => _availableDays.AsReadOnly();

    public static StudioProfile Create(
        UserId userId,
        string studioName,
        string description,
        PhotographySpecialization specialization,
        int experienceYears,
        Money basePrice,
        string location)
    {
        var studioProfileId = StudioProfileId.Create();
        return new StudioProfile(
            studioProfileId,
            userId,
            studioName,
            description,
            specialization,
            experienceYears,
            basePrice,
            location);
    }

    public void UpdateProfile(
        string studioName,
        string description,
        PhotographySpecialization specialization,
        int experienceYears,
        Money basePrice,
        string location)
    {
        StudioName = studioName;
        Description = description;
        Specialization = specialization;
        ExperienceYears = experienceYears;
        BasePrice = basePrice;
        Location = location;
        SetUpdatedAt();
    }

    public void AddPortfolioImage(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Image URL cannot be empty");

        if (!_portfolioImages.Contains(imageUrl))
        {
            _portfolioImages.Add(imageUrl);
            SetUpdatedAt();
        }
    }

    public void RemovePortfolioImage(string imageUrl)
    {
        if (_portfolioImages.Remove(imageUrl))
        {
            SetUpdatedAt();
        }
    }

    public void SetAvailableDays(IEnumerable<string> days)
    {
        _availableDays.Clear();
        _availableDays.AddRange(days);
        SetUpdatedAt();
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

    public bool IsAvailableOn(BookingDate date)
    {
        if (!IsActive) return false;
        
        var dayOfWeek = date.Value.DayOfWeek.ToString();
        return _availableDays.Contains(dayOfWeek);
    }

    public ErrorOr<Money> CalculatePriceFor(PackageType packageType)
    {
        return packageType.Value.ToLowerInvariant() switch
        {
            "essential" => BasePrice,
            "professional" => BasePrice.Multiply(1.5m),
            "premium" => BasePrice.Multiply(2.0m),
            "wedding" => BasePrice.Multiply(3.0m),
            _ => BasePrice
        };
    }
}
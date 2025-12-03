using PhotographyPlatform.Domain.Common.BaseTypes;

namespace PhotographyPlatform.Domain.BookingManagement.ValueObjects;

public class PackageType : ValueObject
{
    public static readonly PackageType Essential = new("Essential");
    public static readonly PackageType Professional = new("Professional");
    public static readonly PackageType Premium = new("Premium");
    public static readonly PackageType Wedding = new("Wedding");

    public string Value { get; private set; }

    private PackageType(string value)
    {
        Value = value;
    }

    public static PackageType Create(string packageType)
    {
        return packageType?.ToLowerInvariant() switch
        {
            "essential" => Essential,
            "professional" => Professional,
            "premium" => Premium,
            "wedding" => Wedding,
            _ => throw new ArgumentException($"Invalid package type: {packageType}")
        };
    }

    public static PackageType FromString(string packageType) => Create(packageType);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(PackageType packageType)
    {
        return packageType.Value;
    }
}
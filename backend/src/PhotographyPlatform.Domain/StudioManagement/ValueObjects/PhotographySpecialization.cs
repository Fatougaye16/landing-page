using PhotographyPlatform.Domain.Common.BaseTypes;

namespace PhotographyPlatform.Domain.StudioManagement.ValueObjects;

public class PhotographySpecialization : ValueObject
{
    public static readonly PhotographySpecialization Portrait = new("Portrait");
    public static readonly PhotographySpecialization Wedding = new("Wedding");
    public static readonly PhotographySpecialization Event = new("Event");
    public static readonly PhotographySpecialization Fashion = new("Fashion");
    public static readonly PhotographySpecialization Nature = new("Nature");
    public static readonly PhotographySpecialization Commercial = new("Commercial");

    public string Value { get; private set; }

    private PhotographySpecialization(string value)
    {
        Value = value;
    }

    public static PhotographySpecialization Create(string specialization)
    {
        return specialization?.ToLowerInvariant() switch
        {
            "portrait" => Portrait,
            "wedding" => Wedding,
            "event" => Event,
            "fashion" => Fashion,
            "nature" => Nature,
            "commercial" => Commercial,
            _ => throw new ArgumentException($"Invalid photography specialization: {specialization}")
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(PhotographySpecialization specialization)
    {
        return specialization.Value;
    }
}
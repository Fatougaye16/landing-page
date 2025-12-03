using PhotographyPlatform.Domain.Common.BaseTypes;

namespace PhotographyPlatform.Domain.Authentication.ValueObjects;

public class UserRole : ValueObject
{
    public static readonly UserRole Client = new("Client");
    public static readonly UserRole Photographer = new("Photographer");

    public string Value { get; private set; }

    private UserRole(string value)
    {
        Value = value;
    }

    public static UserRole Create(string role)
    {
        return role?.ToLowerInvariant() switch
        {
            "client" => Client,
            "photographer" => Photographer,
            _ => throw new ArgumentException($"Invalid user role: {role}")
        };
    }

    public static UserRole FromString(string role) => Create(role);

    public bool IsClient => this == Client;
    public bool IsPhotographer => this == Photographer;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(UserRole role)
    {
        return role.Value;
    }
}
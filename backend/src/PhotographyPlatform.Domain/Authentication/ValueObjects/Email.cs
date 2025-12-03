using ErrorOr;
using FluentValidation;
using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.Common.Errors;
using PhotographyPlatform.Domain.Common.Validators;
using System.Text.RegularExpressions;

namespace PhotographyPlatform.Domain.Authentication.ValueObjects;

public class Email : ValueObject
{
    private static readonly Regex EmailRegex = new(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
    }

    public static ErrorOr<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Errors.Authentication.EmailEmpty;

        email = email.Trim().ToLowerInvariant();

        var validator = new EmailValidator();
        var validationResult = validator.Validate(email);

        if (!validationResult.IsValid)
        {
            return validationResult.Errors.Select(error => Error.Validation(
                code: "Authentication.InvalidEmail",
                description: error.ErrorMessage)).ToArray();
        }

        return new Email(email);
    }

    public static ErrorOr<Email> CreateUnsafe(string email)
    {
        return new Email(email);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(Email email)
    {
        return email.Value;
    }

    // Note: Removed implicit conversion from string to Email to force explicit validation
}
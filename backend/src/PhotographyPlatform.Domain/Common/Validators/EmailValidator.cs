using FluentValidation;
using System.Text.RegularExpressions;

namespace PhotographyPlatform.Domain.Common.Validators;

public class EmailValidator : AbstractValidator<string>
{
    private static readonly Regex EmailRegex = new(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public EmailValidator()
    {
        RuleFor(email => email)
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .MaximumLength(255)
            .WithMessage("Email cannot exceed 255 characters")
            .Must(BeValidEmailFormat)
            .WithMessage("Invalid email format");
    }

    private static bool BeValidEmailFormat(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return EmailRegex.IsMatch(email.Trim().ToLowerInvariant());
    }
}
using ErrorOr;
using PhotographyPlatform.Domain.Authentication.ValueObjects;

namespace PhotographyPlatform.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error NotFound(UserId id) => Error.NotFound(
            code: "User.NotFound",
            description: $"User with ID '{id.Value}' was not found.");

        public static Error NotFoundByEmail(Email email) => Error.NotFound(
            code: "User.NotFoundByEmail", 
            description: $"User with email '{email.Value}' was not found.");

        public static Error DuplicateEmail(Email email) => Error.Conflict(
            code: "User.DuplicateEmail",
            description: $"User with email '{email.Value}' already exists.");
    }
}
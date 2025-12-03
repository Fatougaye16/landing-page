using ErrorOr;

namespace PhotographyPlatform.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidEmail => Error.Validation(
            code: "Authentication.InvalidEmail",
            description: "Email is not valid.");

        public static Error EmailEmpty => Error.Validation(
            code: "Authentication.EmailEmpty", 
            description: "Email cannot be empty.");

        public static Error EmailTooLong => Error.Validation(
            code: "Authentication.EmailTooLong",
            description: "Email cannot exceed 255 characters.");

        public static Error InvalidUserRole => Error.Validation(
            code: "Authentication.InvalidUserRole",
            description: "User role is not valid.");
    }
}
using ErrorOr;
using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.StudioManagement.ValueObjects;

namespace PhotographyPlatform.Domain.Common.Errors;

public static partial class Errors
{
    public static class StudioProfile
    {
        public static Error NotFound(StudioProfileId id) => Error.NotFound(
            code: "StudioProfile.NotFound",
            description: $"Studio profile with ID '{id.Value}' was not found.");

        public static Error NotFoundByUserId(UserId userId) => Error.NotFound(
            code: "StudioProfile.NotFoundByUserId", 
            description: $"Studio profile for user with ID '{userId.Value}' was not found.");

        public static Error DuplicateForUser(UserId userId) => Error.Conflict(
            code: "StudioProfile.DuplicateForUser",
            description: $"User with ID '{userId.Value}' already has a studio profile.");
    }
}
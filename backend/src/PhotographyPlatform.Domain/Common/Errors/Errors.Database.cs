using ErrorOr;

namespace PhotographyPlatform.Domain.Common.Errors;

public static partial class Errors
{
    public static class Database
    {
        public static Error SaveChanges(string message) => Error.Failure(
            code: "Database.SaveChanges",
            description: $"An error occurred while saving changes to the database: {message}");

        public static Error Transaction(string message) => Error.Failure(
            code: "Database.Transaction",
            description: $"An error occurred during database transaction: {message}");
    }
}
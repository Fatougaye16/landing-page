using ErrorOr;

namespace PhotographyPlatform.Domain.Common.Errors;

public static partial class Errors
{
    public static class StudioManagement
    {
        public static Error InvalidMoneyAmount => Error.Validation(
            code: "StudioManagement.InvalidMoneyAmount",
            description: "Money amount cannot be negative.");

        public static Error CurrencyRequired => Error.Validation(
            code: "StudioManagement.CurrencyRequired",
            description: "Currency is required.");

        public static Error CurrencyMismatch => Error.Validation(
            code: "StudioManagement.CurrencyMismatch",
            description: "Cannot perform operations on money with different currencies.");

        public static Error InvalidMultiplicationFactor => Error.Validation(
            code: "StudioManagement.InvalidMultiplicationFactor",
            description: "Multiplication factor cannot be negative.");

        public static Error InvalidSpecialization => Error.Validation(
            code: "StudioManagement.InvalidSpecialization",
            description: "Photography specialization is not valid.");
    }
}
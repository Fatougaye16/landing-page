using ErrorOr;
using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.Common.Errors;

namespace PhotographyPlatform.Domain.StudioManagement.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static ErrorOr<Money> Create(decimal amount, string currency = "USD")
    {
        if (amount < 0)
            return Errors.StudioManagement.InvalidMoneyAmount;

        if (string.IsNullOrWhiteSpace(currency))
            return Errors.StudioManagement.CurrencyRequired;

        return new Money(amount, currency.ToUpper());
    }

    public ErrorOr<Money> Add(Money other)
    {
        if (Currency != other.Currency)
            return Errors.StudioManagement.CurrencyMismatch;

        return new Money(Amount + other.Amount, Currency);
    }

    public ErrorOr<Money> Multiply(decimal factor)
    {
        if (factor < 0)
            return Errors.StudioManagement.InvalidMultiplicationFactor;

        return new Money(Amount * factor, Currency);
    }

    public ErrorOr<bool> IsGreaterThan(Money other)
    {
        if (Currency != other.Currency)
            return Errors.StudioManagement.CurrencyMismatch;

        return Amount > other.Amount;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString()
    {
        return $"{Amount:C} {Currency}";
    }
}
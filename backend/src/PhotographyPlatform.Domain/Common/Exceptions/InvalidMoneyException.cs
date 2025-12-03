namespace PhotographyPlatform.Domain.Common.Exceptions;

public sealed class InvalidMoneyException : DomainException
{
    public InvalidMoneyException(string message)
        : base(message)
    {
    }
}
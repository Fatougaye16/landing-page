namespace PhotographyPlatform.Domain.Common.Exceptions;

public sealed class InvalidBookingDateException : DomainException
{
    public InvalidBookingDateException(string message)
        : base(message)
    {
    }
}
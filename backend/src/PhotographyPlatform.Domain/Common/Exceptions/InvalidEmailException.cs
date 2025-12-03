namespace PhotographyPlatform.Domain.Common.Exceptions;

public sealed class InvalidEmailException : DomainException
{
    public InvalidEmailException(string email)
        : base($"Email '{email}' is not valid.")
    {
    }
}
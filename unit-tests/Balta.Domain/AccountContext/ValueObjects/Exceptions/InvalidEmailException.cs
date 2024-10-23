namespace Balta.Domain.AccountContext.ValueObjects.Exceptions;

public class InvalidEmailException(string message = InvalidEmailException.DefaultErrorMessage) : Exception(message)
{
    private const string DefaultErrorMessage = "E-mail inválido";
}
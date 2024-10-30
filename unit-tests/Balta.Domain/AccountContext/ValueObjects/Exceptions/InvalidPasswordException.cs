namespace Balta.Domain.AccountContext.ValueObjects.Exceptions;

public class InvalidPasswordException(string message = InvalidPasswordException.DefaultErrorMessage) : Exception(message)
{
    private const string DefaultErrorMessage = "Senha inválida";
}
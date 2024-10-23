namespace Balta.Domain.AccountContext.ValueObjects.Exceptions;

public class InvalidVerificationCodeException(string message = InvalidVerificationCodeException.DefaultErrorMessage) : Exception(message)
{
    private const string DefaultErrorMessage = "Código de verificação inválido";
}
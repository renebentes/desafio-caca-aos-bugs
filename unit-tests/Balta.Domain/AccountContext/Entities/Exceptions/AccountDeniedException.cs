namespace Balta.Domain.AccountContext.Entities.Exceptions;

public class AccountDeniedException(string message = AccountDeniedException.DefaultErrorMessage) : Exception(message)
{
    private const string DefaultErrorMessage = "Esta conta esta bloqueada";
}
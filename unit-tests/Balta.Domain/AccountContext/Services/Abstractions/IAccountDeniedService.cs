namespace Balta.Domain.AccountContext.Services.Abstractions;

public interface IAccountDeniedService
{
    Task<bool> IsAccountDenied(string email);
}
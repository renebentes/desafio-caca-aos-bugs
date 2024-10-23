using Balta.Domain.AccountContext.Services.Abstractions;

namespace Balta.Domain.AccountContext.Services;

public class AccountDeniedService : IAccountDeniedService
{
    public Task<bool> IsAccountDenied(string email)
    {
        throw new NotImplementedException();
    }
}
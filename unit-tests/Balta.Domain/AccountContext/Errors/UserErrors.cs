using Balta.Domain.SharedContext.Results;

namespace Balta.Domain.AccountContext.Errors;

public static class UserErrors
{
    #region Messages

    public const string LockedOutByLoginAttemptsMessage = "Login bloqueado por excesso de falhas";

    #endregion

    #region Errors

    public static Error LockedOutByLoginAttempts = new("User.Authentication", LockedOutByLoginAttemptsMessage);

    #endregion
}
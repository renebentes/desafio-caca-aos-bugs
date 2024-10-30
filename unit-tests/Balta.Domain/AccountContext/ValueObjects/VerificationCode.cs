using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;

namespace Balta.Domain.AccountContext.ValueObjects;

public class VerificationCode
{
    #region Constants

    private const int MinLength = 6;

    #endregion Constants

    #region Constructors

    private VerificationCode(string code, DateTime expiresAtUtc)
    {
        Code = Guid.NewGuid().ToString("N")[..MinLength].ToUpper();
        ExpiresAtUtc = expiresAtUtc;
    }

    #endregion Factories

    #region Properties

    public string Code { get; }

    public DateTime? ExpiresAtUtc { get; private set; }

    public bool IsActive => VerifiedAtUtc is not null;

    public bool IsExpired => ExpiresAtUtc < DateTime.UtcNow;

    public DateTime? VerifiedAtUtc { get; private set; }

    #endregion Constructors

    #region Factories

    public static VerificationCode ShouldCreate(IDateTimeProvider dateTimeProvider) =>
        new(
            Guid.NewGuid().ToString("N")[..MinLength].ToUpper(),
            dateTimeProvider.UtcNow.AddMinutes(5));

    public void SetVerifiedAtUtc() => VerifiedAtUtc = DateTime.UtcNow;

    #endregion

    #region Methods

    public void ShouldVerify(string code)
    {
        if (string.IsNullOrEmpty(code))
            throw new InvalidVerificationCodeException();

        if (string.IsNullOrWhiteSpace(code))
            throw new InvalidVerificationCodeException();

        if (code.Length != MinLength)
            throw new InvalidVerificationCodeException();

        if (!Code.Equals(code, StringComparison.OrdinalIgnoreCase))
            throw new InvalidVerificationCodeException();

        if (IsActive)
            throw new InvalidVerificationCodeException();

        if (IsExpired)
            throw new InvalidVerificationCodeException();

        VerifiedAtUtc = DateTime.UtcNow;
        ExpiresAtUtc = null;
    }

    #endregion Operators

    #region Others

    public override string ToString() => Code;

    #endregion Methods

    #region Operators

    public static implicit operator string(VerificationCode verificationCode) => verificationCode.ToString();

    #endregion Others
}

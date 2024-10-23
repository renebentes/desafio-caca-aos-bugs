namespace Balta.Domain.AccountContext.ValueObjects;

public sealed record LockOut
{
    #region Constructors

    private LockOut(DateTime lockOutEndUtc, string? lockOutReason)
    {
        LockOutEndUtc = lockOutEndUtc;
        LockOutReason = lockOutReason;
    }

    #endregion

    #region Factories

    public static LockOut ShouldCreate(int durationInMinutes, string? lockOutReason = null)
        => new(DateTime.UtcNow.AddMinutes(durationInMinutes), lockOutReason);

    #endregion

    #region Properties

    public DateTime? LockOutEndUtc { get; }
    public string? LockOutReason { get; }

    #endregion
}
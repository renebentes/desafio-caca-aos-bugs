using Balta.Domain.SharedContext.ValueObjects;

namespace Balta.Domain.AccountContext.ValueObjects;

public record Phone : ValueObject
{
    #region Constructors

    private Phone(short countryCode, short stateCode, string number)
    {
        CountryCode = countryCode;
        StateCode = stateCode;
        Number = number;
    }

    #endregion

    #region Factories

    public static Phone ShouldCreate(short countryCode, short stateCode, string number)
        => new(countryCode, stateCode, number);

    #endregion

    #region Properties

    public short CountryCode { get; }
    public short StateCode { get; }
    public string Number { get; }

    #endregion
}
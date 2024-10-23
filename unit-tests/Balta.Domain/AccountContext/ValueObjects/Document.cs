using Balta.Domain.AccountContext.ValueObjects.Enums;
using Balta.Domain.SharedContext.ValueObjects;

namespace Balta.Domain.AccountContext.ValueObjects;

public record Document : ValueObject
{
    #region Constructors

    private Document(EDocumentType type, string number)
    {
        Type = type;
        Number = number;
    }

    #endregion

    #region Factories

    public static Document ShouldCreate(EDocumentType type, string number)
        => new(type, number);

    #endregion

    #region Properties

    public EDocumentType Type { get; }
    public string Number { get; }

    #endregion
}
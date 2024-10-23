using Balta.Domain.SharedContext.ValueObjects;

namespace Balta.Domain.AccountContext.ValueObjects;

public record Info : ValueObject
{
    #region Constructors

    private Info(Document? document, Phone? phone, DateTime? birthDate)
    {
        Document = document;
        Phone = phone;
        BirthDate = birthDate;
    }

    #endregion

    #region Factories

    public static Info ShouldCreate(Document document, Phone? phone, DateTime? birthDate) 
        => new(document, phone, birthDate);

    #endregion

    #region Properties

    public Document? Document { get; }
    public Phone? Phone { get; }
    public DateTime? BirthDate { get; }

    #endregion
}
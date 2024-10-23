using Balta.Domain.SharedContext.ValueObjects;

namespace Balta.Domain.AccountContext.ValueObjects;

public record Profile : ValueObject
{
    #region Constructors

    private Profile(string? title, string? bio, DateTime? birthDate)
    {
        Title = title;
        Bio = bio;
        BirthDate = birthDate;
    }

    #endregion

    #region Factories

    public static Profile ShouldCreate(string title, string bio, DateTime birthDate) 
        => new(title, bio, birthDate);

    #endregion

    #region Properties

    public string? Title { get; set; }
    public string? Bio { get; set; }
    public DateTime? BirthDate { get; }

    #endregion
}
using Balta.Domain.SharedContext.ValueObjects;

namespace Balta.Domain.AccountContext.ValueObjects;

public record Name : ValueObject
{
    #region Constructor

    private Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    #endregion

    #region Factories

    public static Name ShouldCreate(string firstName, string lastName) => new(firstName, lastName);

    #endregion

    #region Properties

    public string FirstName { get; }
    public string LastName { get; }

    #endregion
}
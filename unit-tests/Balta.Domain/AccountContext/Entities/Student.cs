using Balta.Domain.AccountContext.Entities.Exceptions;
using Balta.Domain.AccountContext.Events;
using Balta.Domain.AccountContext.Services.Abstractions;
using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.SharedContext.Abstractions;
using Balta.Domain.SharedContext.Aggregates.Abstractions;
using Balta.Domain.SharedContext.Entities;
using Balta.Domain.SharedContext.ValueObjects;

namespace Balta.Domain.AccountContext.Entities;

public sealed class Student : Entity, IAggregateRoot
{
    #region Constructors

    private Student(
        Guid id,
        Name name,
        Email email,
        User user,
        Tracker tracker) : base(id)
    {
        Name = name;
        Email = email;
        User = user;
        Info = null;
        Profile = null;
        Tracker = tracker;
    }

    #endregion

    #region Factories

    public static async Task<Student> ShouldCreate(
        string firstName,
        string lastName,
        string emailAddress,
        string plainTextPassword,
        IDateTimeProvider dateTimeProvider,
        IAccountDeniedService accountDeniedService)
    {
        var accountDenied = await accountDeniedService.IsAccountDenied(emailAddress);
        if (accountDenied)
            throw new AccountDeniedException();

        var name = Name.ShouldCreate(firstName, lastName);
        var email = Email.ShouldCreate(emailAddress, dateTimeProvider);
        var password = Password.ShouldCreate(plainTextPassword);
        var user = User.ShouldCreate(email, password);
        var tracker = Tracker.ShouldCreate(dateTimeProvider);
        var student = new Student(Guid.NewGuid(), name, email, user, tracker);

        student.RaiseDomainEvent(new AccountCreatedDomainEvent(email));

        return student;
    }

    #endregion

    #region Properties

    public Name Name { get; }
    public Email Email { get; }
    public User User { get; }
    public Info? Info { get; }
    public Profile? Profile { get; }
    public Tracker Tracker { get; }

    #endregion
}
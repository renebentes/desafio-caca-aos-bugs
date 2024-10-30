using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.Test.Builders.AccountBuilders;
using Balta.Domain.Test.Validators;
using System.Net.Mail;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{

    [Fact(DisplayName = nameof(ShouldFailIfPasswordIsNull))]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldFailIfPasswordIsNull()
    {
        string password = null;

        var exception = Record.Exception(() => Password.ShouldCreate(password));

        Assert.NotNull(exception);
    }

    [Fact(DisplayName = nameof(ShouldFailIfPasswordIsEmpty))]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldFailIfPasswordIsEmpty()
    {
        var password = string.Empty;

        var exception = Record.Exception(() => Password.ShouldCreate(password));

        Assert.NotNull(exception);
    }

    [Theory(DisplayName = nameof(ShouldFailIfPasswordIsWhiteSpace))]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("    ")]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldFailIfPasswordIsWhiteSpace(string password)
    {
        var exception = Record.Exception(() => Password.ShouldCreate(password));

        Assert.NotNull(exception);
    }

    [Theory(DisplayName = nameof(ShouldFailIfPasswordLenIsLessThanMinimumChars))]
    [MemberData(nameof(GetInValidShortPassword), parameters: 5)]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars(string password)
    {
        var exception = Record.Exception(() => Password.ShouldCreate(password));

        Assert.NotNull(exception);
        Assert.Equal("Password should have at least 8 characters", exception.Message);
    }

    [Theory(DisplayName = nameof(ShouldFailIfPasswordLenIsGreaterThanMaxChars))]
    [MemberData(nameof(GetInValidLongPassword), parameters: 5)]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars(string password)
    {
        var exception = Record.Exception(() => Password.ShouldCreate(password));

        Assert.NotNull(exception);
        Assert.Equal("Password should have less than 48 characters", exception.Message);
    }

    [Fact(DisplayName = nameof(ShouldHashPassword))]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldHashPassword()
    {
        string password = Password.ShouldGenerate();
        var passwordHash = Password.ShouldCreate(password).Hash;

        var isMatch = Password.ShouldMatch(passwordHash, password);

        Assert.True(isMatch);
    }

    [Fact(DisplayName = nameof(ShouldVerifyPasswordHash))]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldVerifyPasswordHash()
    {
        string plainTextPassword = Password.ShouldGenerate();
        var passwordHash = Password.ShouldCreate(plainTextPassword).Hash;

        var isMatchCorrect = Password.ShouldMatch(passwordHash, plainTextPassword);
        var isMatchIncorrect = Password.ShouldMatch(passwordHash, "wrongPassword");

        Assert.True(isMatchCorrect);
        Assert.False(isMatchIncorrect);
    }

    [Fact(DisplayName = nameof(ShouldGenerateStrongPassword))]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldGenerateStrongPassword()
    {
        var password = Password.ShouldGenerate();

        var validator = new PasswordValidator();

        var result = validator.Validate(password);

        Assert.True(result.IsValid, string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
    }

    [Theory(DisplayName = nameof(ShouldImplicitConvertToString))]
    [MemberData(nameof(GetValidPassword), parameters: 5)]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldImplicitConvertToString(string plainPassword)
    {
        var password = Password.ShouldCreate(plainPassword);

        string passwordString = password;

        Assert.Equal(password.Hash, passwordString);
    }

    [Theory(DisplayName = nameof(ShouldReturnHashAsStringWhenCallToStringMethod))]
    [MemberData(nameof(GetValidPassword), parameters: 5)]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldReturnHashAsStringWhenCallToStringMethod(string plainPassword)
    {
        var password = Password.ShouldCreate(plainPassword);

        var passwordString = password.ToString();
        
        Assert.Equal(password.Hash, passwordString);
    }

    [Theory(DisplayName = nameof(ShouldMarkPasswordAsExpired))]
    [MemberData(nameof(GetValidPassword), parameters: 5)]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldMarkPasswordAsExpired(string plainPassword)
    {
        var password = Password.ShouldCreate(plainPassword);

        password.Expire(); 

        Assert.NotNull(password.ExpiresAtUtc);
        Assert.True(password.IsExpired());
    }

    [Theory(DisplayName = nameof(ShouldFailIfPasswordIsExpired))]
    [MemberData(nameof(GetValidPassword), parameters: 5)]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldFailIfPasswordIsExpired(string plainPassword)
    {
            var password = Password.ShouldCreate(plainPassword);
            
            password.Expire();
            var isExpired = password.IsExpired();

            Assert.NotNull(password.ExpiresAtUtc);
            Assert.True(isExpired);
    }


    [Theory(DisplayName = nameof(ShouldMarkPasswordAsMustChange))]
    [MemberData(nameof(GetValidPassword), parameters: 5)]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldMarkPasswordAsMustChange(string plainPassword)
    {
        var password = Password.ShouldCreate(plainPassword);

        password.MarkAsMustChange();
        
        Assert.True(password.MustChange);
    }

    [Theory(DisplayName = nameof(ShouldFailIfPasswordIsMarkedAsMustChange))]
    [MemberData(nameof(GetValidPassword), parameters: 5)]
    [Trait(nameof(PasswordTests), "")]
    public void ShouldFailIfPasswordIsMarkedAsMustChange(string plainPassword)
    {
        var password = Password.ShouldCreate(plainPassword);

        password.MarkAsMustChange();

        var exception = Assert.Throws<InvalidOperationException>(() => password.Verify());
        Assert.Equal("Password must be changed before use.", exception.Message);
    }



    public static IEnumerable<object[]> GetValidPassword(int length)
    {
        var _fixture = new PasswordBuilder();

        for (var i = 0; i < length; i++)
            yield return new object[] { _fixture.GetValidPassword() };
    }

    public static IEnumerable<object[]> GetInValidShortPassword(int length)
    {
        var _fixture = new PasswordBuilder();

        for (var i = 0; i < length; i++)
            yield return new object[] { _fixture.GetInvalidShortPassword() };
    }
    public static IEnumerable<object[]> GetInValidLongPassword(int length)
    {
        var _fixture = new PasswordBuilder();

        for (var i = 0; i < length; i++)
            yield return new object[] { _fixture.GetInvalidLongPassword() };
    }
}
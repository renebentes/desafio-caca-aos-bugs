using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;
using Balta.Domain.SharedContext.Extensions;
using Balta.Domain.Test.Builders.AccountBuilders;
using Moq;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class EmailTests
{
    private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
    public EmailTests()
    {
        _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        _mockDateTimeProvider.Setup(m => m.UtcNow).Returns(DateTime.UtcNow);
    }


    [Theory(DisplayName = nameof(ShouldLowerCaseEmail))]
    [MemberData(nameof(GetValidEmail), 5)]
    [Trait(nameof(EmailTests), "")]
    public void ShouldLowerCaseEmail(string emailAddress)
    {
        var email = Email.ShouldCreate(emailAddress.ToUpper(), _mockDateTimeProvider.Object);

        Assert.NotNull(email);
        Assert.Equal(emailAddress.ToLower(), email.Address);
        Assert.Equal(email.Address.ToBase64(), email.Hash);
    }

    [Theory(DisplayName = nameof(ShouldTrimEmail))]
    [InlineData("   test@example.com   ")]
    [InlineData("test@example.com         ")]
    [InlineData("      test@example.com")]
    [Trait(nameof(EmailTests), "")]
    public void ShouldTrimEmail(string emailAddress)
    {
        var email = Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object);

        Assert.NotNull(email);
        Assert.Equal("test@example.com", email.Address);
    }

    [Fact(DisplayName = nameof(ShouldFailIfEmailIsNull))]
    [Trait(nameof(EmailTests), "")]
    public void ShouldFailIfEmailIsNull()
    {
        string emailAddress = null;

        var exception = Record.Exception(() => Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object));

        Assert.NotNull(exception);
    }

    [Theory(DisplayName = nameof(ShouldFailIfEmailIsEmpty))]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("    ")]
    [Trait(nameof(EmailTests), "")]
    public void ShouldFailIfEmailIsEmpty(string emailAddress)
    {
        var exception = Assert.Throws<InvalidEmailException>(() =>
            Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object));

        Assert.NotNull(exception);
    }

    [Theory(DisplayName = nameof(ShouldFailIfEmailIsInvalid))]
    [InlineData("38e7345teste.com")]
    [InlineData("example @example.com")]
    [InlineData("@example.com")]
    [Trait(nameof(EmailTests), "")]
    public void ShouldFailIfEmailIsInvalid(string emailAddress)
    {
        var exception = Assert.Throws<InvalidEmailException>(() =>
            Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object));

        Assert.NotNull(exception);
        Assert.Equal("E-mail inválido", exception.Message);
    }

    [Theory(DisplayName = nameof(ShouldPassIfEmailIsValid))]
    [MemberData(nameof(GetValidEmail), 5)]
    [Trait(nameof(EmailTests), "")]
    public void ShouldPassIfEmailIsValid(string emailAddress)
    {
        var email = Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object);

        Assert.NotNull(email);
        Assert.Equal(emailAddress.ToLower(), email.Address);
    }


    [Theory(DisplayName = nameof(ShouldHashEmailAddress))]
    [MemberData(nameof(GetValidEmail), 5)]
    [Trait(nameof(EmailTests), "")]
    public void ShouldHashEmailAddress(string emailAddress)
    {
        var email = Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object);

        var expectedHash = emailAddress.ToLower().ToBase64();

        Assert.Equal(expectedHash, email.Hash);
    }

    [Theory(DisplayName = nameof(ShouldExplicitConvertFromString))]
    [MemberData(nameof(GetValidEmail), 5)]
    [Trait(nameof(EmailTests), "")]
    public void ShouldExplicitConvertFromString(string emailAddress)
    {
        var email = Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object);

        var convertString = email.ToString();

        Assert.Equal(emailAddress.ToLower(), convertString);
    }

    [Theory(DisplayName = nameof(ShouldExplicitConvertToString))]
    [MemberData(nameof(GetValidEmail), 5)]
    [Trait(nameof(EmailTests), "")]
    public void ShouldExplicitConvertToString(string emailAddress)
    {
        var emailFromString = Email.FromString(emailAddress, _mockDateTimeProvider.Object);

        Assert.Equal(emailAddress.ToLower(), emailFromString.Address);
    }

    [Theory(DisplayName = nameof(ShouldReturnEmailWhenCallToStringMethod))]
    [MemberData(nameof(GetValidEmail), 5)]
    [Trait(nameof(EmailTests), "")]
    public void ShouldReturnEmailWhenCallToStringMethod(string emailAddress)
    {
        var email = Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object);

        var result = email.ToString();

        Assert.Equal(emailAddress.ToLower(), result);
    }


    public static IEnumerable<object[]> GetValidEmail(int length)
    {
        var _fixture = new EmailBuilder();

        for (var i = 0; i < length; i++)
            yield return new object[] { _fixture.GetValidEmail() };
    }
}
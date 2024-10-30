using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;
using Moq;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class VerificationCodeTest
{
    private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;

    public VerificationCodeTest()
    {
        _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        _ = _mockDateTimeProvider.Setup(m => m.UtcNow).Returns(DateTime.UtcNow);
    }

    [Fact(DisplayName = nameof(ShouldBeInactiveWhenCreated))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldBeInactiveWhenCreated()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Assert
        Assert.False(verificationCode.IsActive);
    }

    [Fact(DisplayName = nameof(ShouldFailIfCodeIsGreaterThanSixChars))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldFailIfCodeIsGreaterThanSixChars()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        var act = () => verificationCode.ShouldVerify("1234567");

        // Assert
        Assert.Throws<InvalidVerificationCodeException>(act);
    }

    [Fact(DisplayName = nameof(ShouldFailIfCodeIsInvalid))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldFailIfCodeIsInvalid()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        var act = () => verificationCode.ShouldVerify("123");

        // Assert
        Assert.Throws<InvalidVerificationCodeException>(act);
    }

    [Fact(DisplayName = nameof(ShouldFailIfCodeIsLessThanSixChars))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldFailIfCodeIsLessThanSixChars()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        var act = () => verificationCode.ShouldVerify("12345");

        // Assert
        Assert.Throws<InvalidVerificationCodeException>(act);
    }

    [Fact(DisplayName = nameof(ShouldFailIfExpired))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldFailIfExpired()
    {
        // Arrange
        _ = _mockDateTimeProvider.Setup(m => m.UtcNow).Returns(DateTime.UtcNow.AddMinutes(-5));
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        var act = () => verificationCode.ShouldVerify(verificationCode.Code);

        // Assert
        Assert.Throws<InvalidVerificationCodeException>(act);
    }

    [Fact(DisplayName = nameof(ShouldFailIfIsAlreadyVerified))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldFailIfIsAlreadyVerified()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);
        verificationCode.ShouldVerify(verificationCode.Code);
        // Act
        var act = () => verificationCode.ShouldVerify(verificationCode.Code);

        // Assert
        Assert.Throws<InvalidVerificationCodeException>(act);
    }

    [Fact(DisplayName = nameof(ShouldFailIfIsNotActive))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldFailIfIsNotActive()
    {
        // Arrange
        _ = _mockDateTimeProvider.Setup(m => m.UtcNow).Returns(DateTime.UtcNow.AddMinutes(-5));
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        var act = () => verificationCode.ShouldVerify(verificationCode.Code);

        // Assert
        Assert.Throws<InvalidVerificationCodeException>(act);
    }

    [Theory(DisplayName = nameof(ShouldFailIfIsVerificationCodeDoesNotMatch))]
    [InlineData("123456")]
    [InlineData("FAILED")]
    [InlineData("FailED")]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldFailIfIsVerificationCodeDoesNotMatch(string code)
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        var act = () => verificationCode.ShouldVerify(code);

        // Assert
        Assert.Throws<InvalidVerificationCodeException>(act);
    }

    [Fact(DisplayName = nameof(ShouldGenerateExpiresAtInFuture))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldGenerateExpiresAtInFuture()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        Assert.True(verificationCode.ExpiresAtUtc > DateTime.UtcNow);
    }

    [Fact(DisplayName = nameof(ShouldGenerateVerificationCode))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldGenerateVerificationCode()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Assert
        Assert.NotNull(verificationCode);
        Assert.NotEmpty(verificationCode.Code);
        Assert.Equal(6, verificationCode.Code.Length);
        Assert.True(verificationCode.ExpiresAtUtc.HasValue);
    }

    [Fact(DisplayName = nameof(ShouldGenerateVerifiedAtAsNull))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldGenerateVerifiedAtAsNull()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Assert
        Assert.Null(verificationCode.VerifiedAtUtc);
    }

    [Fact(DisplayName = nameof(ShouldVerify))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldVerify()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        verificationCode.ShouldVerify(verificationCode.Code);

        // Assert
        Assert.NotNull(verificationCode.VerifiedAtUtc);
    }
}

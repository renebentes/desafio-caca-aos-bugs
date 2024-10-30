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
        _mockDateTimeProvider.Setup(m => m.UtcNow).Returns(DateTime.UtcNow);
    }

    [Fact(DisplayName = nameof(ShouldGenerateVerificationCode))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldGenerateVerificationCode()
    {
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        var code = verificationCode.Code;

        // Assert
        Assert.NotNull(verificationCode);
        Assert.NotNull(code);
        Assert.Equal(6, code.Length);
        Assert.True(verificationCode.ExpiresAtUtc.HasValue);
    }


    [Fact(DisplayName = nameof(ShouldGenerateExpiresAtInFuture))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldGenerateExpiresAtInFuture()
    {
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        Assert.True(verificationCode.ExpiresAtUtc > DateTime.UtcNow);
    }

    [Fact(DisplayName = nameof(ShouldGenerateVerifiedAtAsNull))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldGenerateVerifiedAtAsNull()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Assert
        Assert.NotNull(verificationCode);
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


    [Fact(DisplayName = nameof(ShouldFailIfExpired))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldFailIfExpired()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);
        _mockDateTimeProvider.Setup(m => m.UtcNow).Returns(DateTime.UtcNow.AddMinutes(6));

        // Act
        var act = () => verificationCode.ShouldVerify(verificationCode.Code);

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

    [Fact(DisplayName = nameof(ShouldFailIfIsNotActive))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldFailIfIsNotActive()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);
        verificationCode.ShouldVerify(verificationCode.Code);

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

    [Fact(DisplayName = nameof(ShouldFailIfIsVerificationCodeDoesNotMatch))]
    [Trait(nameof(VerificationCodeTest), "")]
    public void ShouldFailIfIsVerificationCodeDoesNotMatch()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        var act = () => verificationCode.ShouldVerify("123456");

        // Assert
        Assert.Throws<InvalidVerificationCodeException>(act);
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
        Assert.True(verificationCode.IsActive);
        Assert.True(verificationCode.VerifiedAtUtc.HasValue);
        Assert.Null(verificationCode.ExpiresAtUtc);
    }
}
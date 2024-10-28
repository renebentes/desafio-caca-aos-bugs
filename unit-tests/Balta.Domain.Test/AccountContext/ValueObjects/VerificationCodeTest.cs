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

    [Fact]
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


    [Fact]
    public void ShouldGenerateExpiresAtInFuture()
    {
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        Assert.True(verificationCode.ExpiresAtUtc > DateTime.UtcNow);
    }

    [Fact]
    public void ShouldGenerateVerifiedAtAsNull()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Assert
        Assert.NotNull(verificationCode);
    }

    [Fact]
    public void ShouldBeInactiveWhenCreated()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Assert
        Assert.False(verificationCode.IsActive);
    }


    [Fact]
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

    [Fact]
    public void ShouldFailIfCodeIsInvalid()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        var act = () => verificationCode.ShouldVerify("123");

        // Assert
        Assert.Throws<InvalidVerificationCodeException>(act);
    }


    [Fact]
    public void ShouldFailIfCodeIsLessThanSixChars()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        var act = () => verificationCode.ShouldVerify("12345");

        // Assert
        Assert.Throws<InvalidVerificationCodeException>(act);
    }

    [Fact]
    public void ShouldFailIfCodeIsGreaterThanSixChars()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        var act = () => verificationCode.ShouldVerify("1234567");

        // Assert
        Assert.Throws<InvalidVerificationCodeException>(act);
    }

    [Fact]
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

    [Fact]
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

    [Fact]
    public void ShouldFailIfIsVerificationCodeDoesNotMatch()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_mockDateTimeProvider.Object);

        // Act
        var act = () => verificationCode.ShouldVerify("123456");

        // Assert
        Assert.Throws<InvalidVerificationCodeException>(act);
    }

    [Fact]
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
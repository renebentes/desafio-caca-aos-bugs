using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;
using Balta.Domain.SharedContext.Extensions;
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


    [Fact]
    public void ShouldLowerCaseEmail()
    {
        // Arrange
        var emailAddress = "TEST@EXAMPLE.COM";
        
        // Act
        var email = Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object);

        // Assert
        Assert.NotNull(email);
        Assert.Equal("test@example.com", email.Address);
        Assert.Equal(email.Address.ToBase64(), email.Hash);
    }

    [Fact]
    public void ShouldTrimEmail()
    {
        // Arrange
        var emailAddress = "   test@example.com   ";

        // Act
        var email = Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object);

        // Assert
        Assert.NotNull(email);
        Assert.Equal("test@example.com", email.Address);
    }

    [Fact]
    public void ShouldFailIfEmailIsNull()
    {
        // Arrange
        string emailAddress = null;

        // Act
        var exception = Record.Exception(() => Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void ShouldFailIfEmailIsEmpty()
    {
        var emailAddress = string.Empty;
        
        var exception = Assert.Throws<InvalidEmailException>(() =>
            Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object));

        Assert.NotNull(exception);
    }

    [Fact]
    public void ShouldFailIfEmailIsInvalid()
    {
        var emailAddress = "38e7345teste.com";
        
        var exception = Assert.Throws<InvalidEmailException>(() =>
            Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object));

        Assert.NotNull(exception);
    }

    [Fact]
    public void ShouldPassIfEmailIsValid()
    { 
        // Arrange
        var emailAddress = "teste348@gmail.com";
        
        // Act
        var email = Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object);
    
        var verificationCode = email.VerificationCode.ToString(); 

        email.ShouldVerify(verificationCode);

        // Assert
        Assert.NotNull(email);
        Assert.Equal(email.Address, emailAddress.ToLower());
    }


    [Fact]
    public void ShouldHashEmailAddress()
    {
        var emailAddress = "teste123@gmail.com";
        
        var email = Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object);

        var expectedHash = emailAddress.ToBase64();
        
        Assert.Equal(expectedHash, email.Hash);
    }

    [Fact]
    public void ShouldExplicitConvertFromString()
    {
        var emailAddress = "teste123@gmail.com";
        
        var email = Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object);

        var convertString = email.ToString();

        Assert.Equal(emailAddress, convertString);
    }

    [Fact]
    public void ShouldExplicitConvertToString()
    {
        var emailAddress = "teste123@gmail.com";
    
        var emailFromString = Email.FromString(emailAddress, _mockDateTimeProvider.Object);
    
        Assert.Equal(emailAddress.ToLower(), emailFromString.Address);
    }

    [Fact]
    public void ShouldReturnEmailWhenCallToStringMethod()
    {
        // Arrange
        var emailAddress = "teste123@gmail.com";
        var email = Email.ShouldCreate(emailAddress, _mockDateTimeProvider.Object);

        // Act
        var result = email.ToString();

        // Assert
        Assert.Equal(emailAddress.ToLower(), result); 
    }
}
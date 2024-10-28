using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.Test.Validators;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{

    [Fact]
    public void ShouldFailIfPasswordIsNull()
    {
        string password = null;

        var exception = Record.Exception(() => Password.ShouldCreate(password));

        Assert.NotNull(exception);
    }

    [Fact]
    public void ShouldFailIfPasswordIsEmpty()
    {
        string password = string.Empty;

        var exception = Record.Exception(() => Password.ShouldCreate(password));

        Assert.NotNull(exception);
    }

    [Fact]
    public void ShouldFailIfPasswordIsWhiteSpace()
    {
        string password = "                   ";

        var exception = Record.Exception(() => Password.ShouldCreate(password));

        Assert.NotNull(exception);
    }

    [Fact]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars()
    {
        // private const int MinLength = 8;
        // private const int MaxLength = 48;
        // private const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        // private const string Special = "!@#$%ˆ&*(){}[];";

        string password = "1234567";

        var exception = Record.Exception(() => Password.ShouldCreate(password));

        Assert.NotNull(exception);
    }

    [Fact]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars()
    {
        string password = "408923r89ucjwesoicwdkr82r943t7839@80542348@#!()@0rwjefoiasijasadasdhg";

        var exception = Record.Exception(() => Password.ShouldCreate(password));

        Assert.NotNull(exception);
    }

    [Fact]
    public void ShouldHashPassword()
    {
        string password = Password.ShouldGenerate();
        var passwordHash = Password.ShouldCreate(password).Hash;

        var isMatch = Password.ShouldMatch(passwordHash, password);

        Assert.True(isMatch);
    }

    [Fact]
    public void ShouldVerifyPasswordHash()
    {
        // Arrange
        string plainTextPassword = Password.ShouldGenerate();
        var passwordHash = Password.ShouldCreate(plainTextPassword).Hash;

        // Act
        var isMatchCorrect = Password.ShouldMatch(passwordHash, plainTextPassword);
        var isMatchIncorrect = Password.ShouldMatch(passwordHash, "wrongPassword");

        // Assert
        Assert.True(isMatchCorrect);
        Assert.False(isMatchIncorrect);
    }

    [Fact]
    public void ShouldGenerateStrongPassword()
    {
        var password = Password.ShouldGenerate();

        var validator = new PasswordValidator();

        var result = validator.Validate(password);

        Assert.True(result.IsValid, string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
    }

    [Fact]
    public void ShouldImplicitConvertToString()
    {
        // Arrange
        var plainPassword = "StrongPassword123!";
        var password = Password.ShouldCreate(plainPassword);

        // Act
        string passwordString = password;

        // Assert
        Assert.Equal(password.Hash, passwordString);
    }

    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod()
    {
        // Arrange
        var plainPassword = "StrongPassword123!";
        var password = Password.ShouldCreate(plainPassword);

        // Act
        var passwordString = password.ToString();
        
        // Assert
        Assert.Equal(password.Hash, passwordString);
        
    }

    [Fact]
    public void ShouldMarkPasswordAsExpired()
    {
        // Arrange
        var plainPassword = "StrongPassword123!";
        var password = Password.ShouldCreate(plainPassword);

        password.Expire(); 

        // Assert
        Assert.NotNull(password.ExpiresAtUtc);
        Assert.True(password.IsExpired());
    }

    [Fact]
    public void ShouldFailIfPasswordIsExpired()
    {
            // Arrange
            var plainPassword = "StrongPassword123!";
            var password = Password.ShouldCreate(plainPassword);
            
            // Act
            password.Expire();
            var isExpired = password.IsExpired();

            // Assert
            Assert.NotNull(password.ExpiresAtUtc);
            Assert.True(isExpired);
    }


    [Fact]
    public void ShouldMarkPasswordAsMustChange()
    {
        var plainPassword = "StrongPassword123!";
        var password = Password.ShouldCreate(plainPassword);

        password.MarkAsMustChange();
        
        Assert.True(password.MustChange);
    }

    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange()
    {
        var plainPassword = "StrongPassword123!";
        var password = Password.ShouldCreate(plainPassword);

        password.MarkAsMustChange();

        var exception = Assert.Throws<InvalidOperationException>(() => password.Verify());
        Assert.Equal("Password must be changed before use.", exception.Message);
    }
}
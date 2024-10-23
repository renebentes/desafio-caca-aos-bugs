namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void ShouldFailIfPasswordIsNull() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfPasswordIsEmpty() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfPasswordIsWhiteSpace() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars() => Assert.Fail();
    
    [Fact]
    public void ShouldHashPassword() => Assert.Fail();
    
    [Fact]
    public void ShouldVerifyPasswordHash() => Assert.Fail();
    
    [Fact]
    public void ShouldGenerateStrongPassword() => Assert.Fail();
    
    [Fact]
    public void ShouldImplicitConvertToString() => Assert.Fail();
    
    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod() => Assert.Fail();
    
    [Fact]
    public void ShouldMarkPasswordAsExpired() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfPasswordIsExpired() => Assert.Fail();
    
    [Fact]
    public void ShouldMarkPasswordAsMustChange() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange() => Assert.Fail();
}
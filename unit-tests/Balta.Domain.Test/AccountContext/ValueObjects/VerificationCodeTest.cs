namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class VerificationCodeTest
{
    [Fact]
    public void ShouldGenerateVerificationCode() => Assert.Fail();

    [Fact]
    public void ShouldGenerateExpiresAtInFuture() => Assert.Fail();

    [Fact]
    public void ShouldGenerateVerifiedAtAsNull() => Assert.Fail();

    [Fact]
    public void ShouldBeInactiveWhenCreated() => Assert.Fail();

    [Fact]
    public void ShouldFailIfExpired() => Assert.Fail();

    [Fact]
    public void ShouldFailIfCodeIsInvalid() => Assert.Fail();

    [Fact]
    public void ShouldFailIfCodeIsLessThanSixChars() => Assert.Fail();

    [Fact]
    public void ShouldFailIfCodeIsGreaterThanSixChars() => Assert.Fail();

    [Fact]
    public void ShouldFailIfIsNotActive() => Assert.Fail();

    [Fact]
    public void ShouldFailIfIsAlreadyVerified() => Assert.Fail();

    [Fact]
    public void ShouldFailIfIsVerificationCodeDoesNotMatch() => Assert.Fail();

    [Fact]
    public void ShouldVerify() => Assert.Fail();
}
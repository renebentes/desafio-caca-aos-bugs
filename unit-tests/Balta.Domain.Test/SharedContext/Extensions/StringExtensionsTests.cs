using System.Text;
using Balta.Domain.SharedContext.Extensions;

namespace Balta.Domain.Test.SharedContext.Extensions;

public class StringExtensionsTests
{
    [Fact]
    public void ShouldGenerateBase64FromString()
    {
        // Arrange
        var input = "Hello, World!";
        var expectedBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(input));

        // Act
        var result = input.ToBase64();

        // Assert
        Assert.Equal(expectedBase64, result);
    }
}
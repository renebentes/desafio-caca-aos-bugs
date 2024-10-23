using System.Text;

namespace Balta.Domain.SharedContext.Extensions;

public static class StringExtensions
{
    #region Public Methods

    public static string ToBase64(this string text)
        => Convert.ToBase64String(Encoding.ASCII.GetBytes(text));

    #endregion
}
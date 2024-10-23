namespace Balta.Domain.SharedContext.Results;

public record Error(string Code, string Name)
{
    #region Properties

    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error.NullValue", "Ocorreu um erro não especificado.");

    #endregion
}
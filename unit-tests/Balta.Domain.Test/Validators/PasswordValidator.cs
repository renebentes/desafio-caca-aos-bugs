using FluentValidation;

namespace Balta.Domain.Test.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    private const int MinLength = 16;
    private const int MaxLength = 48;

    public PasswordValidator()
    {
        RuleFor(password => password)
            .NotNull().WithMessage("Password cannot be null.")
            .NotEmpty().WithMessage("Password cannot be empty.")
            .Length(MinLength, MaxLength).WithMessage($"Password must be between {MinLength} and {MaxLength} characters.")
            .Must(ContainUpperCase).WithMessage("Password must contain at least one uppercase letter.")
            .Must(ContainDigit).WithMessage("Password must contain at least one digit.")
            .Must(ContainSpecialCharacter).WithMessage("Password must contain at least one special character.");
    }

    private bool ContainUpperCase(string password) => password.Any(char.IsUpper);
    private bool ContainDigit(string password) => password.Any(char.IsDigit);
    private bool ContainSpecialCharacter(string password) 
    {
        var specialChars = "!@#$%^&*(){}[];";
        return password.Any(c => specialChars.Contains(c));
    }
}
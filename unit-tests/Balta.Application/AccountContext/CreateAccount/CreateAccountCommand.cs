using Balta.Application.SharedContext.UseCases.Abstractions;

namespace Balta.Application.AccountContext.CreateAccount;

public record CreateAccountCommand(string Name) : ICommand<CreateAccountCommandResponse>;
using Balta.Application.SharedContext.UseCases.Abstractions;
using Balta.Domain.SharedContext.Results;

namespace Balta.Application.AccountContext.CreateAccount;

internal sealed class CreateTransactionCommandHandler: ICommandHandler<CreateAccountCommand, CreateAccountCommandResponse>
{
    public Task<Result<CreateAccountCommandResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
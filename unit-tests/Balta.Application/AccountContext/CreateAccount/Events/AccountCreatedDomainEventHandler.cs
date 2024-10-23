using Balta.Domain.AccountContext.Events;
using MediatR;

namespace Balta.Application.AccountContext.CreateAccount.Events;

internal sealed class AccountCreatedDomainEventHandler : INotificationHandler<AccountCreatedDomainEvent>
{
    public Task Handle(AccountCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
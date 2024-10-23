using Balta.Domain.SharedContext.Events.Abstractions;

namespace Balta.Domain.AccountContext.Events;

public record AccountCreatedDomainEvent(string Email) : IDomainEvent;
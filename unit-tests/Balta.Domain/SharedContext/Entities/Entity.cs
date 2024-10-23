using Balta.Domain.SharedContext.Events.Abstractions;

namespace Balta.Domain.SharedContext.Entities;

public abstract class Entity(Guid id) : IEquatable<Guid>
{
    #region Private Properties

    private readonly List<IDomainEvent> _domainEvents = [];

    #endregion
    
    #region Properties
    public Guid Id { get; init; } = id;

    #endregion

    #region Public Methods

    public IReadOnlyList<IDomainEvent> GetDomainEvents => _domainEvents;
    
    public void ClearDomainEvents() => _domainEvents.Clear();
    
    public void RaiseDomainEvent(IDomainEvent @event) => _domainEvents.Add(@event);

    #endregion

    #region Equatable Implementation

    public bool Equals(Guid id)
        => Id == id;

    public override int GetHashCode()
        => Id.GetHashCode();

    #endregion
}
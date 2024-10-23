using Balta.Domain.ContentContext.ValueObjects;
using Balta.Domain.SharedContext.Entities;

namespace Balta.Domain.ContentContext.Entities;

public sealed class Tag(Guid id) : Entity(id)
{
    public Content Content { get; set; }
}
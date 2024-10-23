using Balta.Domain.SharedContext.ValueObjects;

namespace Balta.Domain.ContentContext.ValueObjects;

public sealed record Content : ValueObject
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
}
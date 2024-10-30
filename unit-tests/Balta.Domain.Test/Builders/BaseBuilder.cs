using Bogus;

namespace Balta.Domain.Test.Builders;
public abstract class BaseBuilder
{
    protected BaseBuilder() => Faker = new Faker();
    public Faker Faker { get; private set; }
}

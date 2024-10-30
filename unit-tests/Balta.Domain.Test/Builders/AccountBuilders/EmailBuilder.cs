using Bogus;

namespace Balta.Domain.Test.Builders.AccountBuilders;
public class EmailBuilder : BaseBuilder
{
    public string GetValidEmail() => Faker.Internet.Email();
}

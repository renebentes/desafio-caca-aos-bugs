namespace Balta.Domain.Test.Builders.AccountBuilders;
public class PasswordBuilder : BaseBuilder
{
    public string GetValidPassword() => Faker.Internet.Password(10, false, "[a-z]");
    public string GetInvalidShortPassword()
    {
        Random random = new Random();
        var length = random.Next(1, 7);

        return Faker.Internet.Password(length, false, "[a-z]");
    }
    public string GetInvalidLongPassword()
    {
        Random random = new Random();
        var length = random.Next(49, 255);

        return Faker.Internet.Password(length, false, "[a-z]");
    }
}

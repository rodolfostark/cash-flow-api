using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestRegisterExpenseJsonBuilder
{
    public static RequestExpenseJson Build()
    {
        var faker = new Faker<RequestExpenseJson>();
        var request = faker
            .RuleFor(rule => rule.Title, faker => faker.Commerce.ProductName())
            .RuleFor(rule => rule.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(rule => rule.Date, faker => faker.Date.Past())
            .RuleFor(rule => rule.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(rule => rule.PaymentType, faker => faker.PickRandom<PaymentType>());
        return request;
    }
}

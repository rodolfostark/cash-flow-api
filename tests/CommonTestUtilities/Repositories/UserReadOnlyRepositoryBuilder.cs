using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    public static IUserReadOnlyRepository Build()
    {
        var userReadOnlyRepository = new Mock<IUserReadOnlyRepository>();
        return userReadOnlyRepository.Object;
    }
}

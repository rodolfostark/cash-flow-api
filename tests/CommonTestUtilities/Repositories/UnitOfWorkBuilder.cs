using CashFlow.Domain.Repositories;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        return mockUnitOfWork.Object;
    }
}

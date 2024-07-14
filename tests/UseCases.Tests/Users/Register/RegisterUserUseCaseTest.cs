using CashFlow.Application.UseCases.Users.Register;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Tests.Users.Register;
public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        // Act
        var useCase = CreateUseCase();
        var response = await useCase.Execute(request);
        // Assert
        response.Should().NotBeNull();
        response.Name.Should().Be(request.Name);
        response.Token.Should().NotBeNullOrWhiteSpace();
    }

    private RegisterUserUseCase CreateUseCase()
    {
        // Create mocked implementations for all use case dependencies
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();

        return new RegisterUserUseCase(mapper, null, null, userWriteOnlyRepository, unitOfWork, null);
    }
}

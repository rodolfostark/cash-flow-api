using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepository;
    public UserReadOnlyRepositoryBuilder()
    {
        _userReadOnlyRepository = new Mock<IUserReadOnlyRepository>();
    }
    public IUserReadOnlyRepository Build() => _userReadOnlyRepository.Object;
    public void ExistActiveUserWithEmail(string email)
    {
        _userReadOnlyRepository
            .Setup(userReadOnly => userReadOnly.ExistActiveUserWithEmail(email))
            .ReturnsAsync(true);
    }

    public UserReadOnlyRepositoryBuilder GetUserByEmail(User user)
    {
        _userReadOnlyRepository
            .Setup(userRepository => userRepository.GetUserByEmail(user.Email))
            .ReturnsAsync(user);
        
        return this;
    }
}

﻿using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepository = new Mock<IUserReadOnlyRepository>();
    public IUserReadOnlyRepository Build() => _userReadOnlyRepository.Object;

    public void ExistActiveUserWithEmail(string email)
    {
        _userReadOnlyRepository
            .Setup(userReadOnly => userReadOnly.ExistActiveUserWithEmail(email))
            .ReturnsAsync(true);
    }
}

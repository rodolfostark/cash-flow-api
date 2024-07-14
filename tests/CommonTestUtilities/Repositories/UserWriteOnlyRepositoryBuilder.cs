﻿using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build()
    {
        var userWriteOnlyRepository = new Mock<IUserWriteOnlyRepository>();
        return userWriteOnlyRepository.Object;
    }
}

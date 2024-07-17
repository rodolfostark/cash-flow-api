using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Moq;

namespace CommonTestUtilities.Token;
public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build()
    {
        var tokenGenerator = new Mock<IAccessTokenGenerator>();

        tokenGenerator
            .Setup(config => config.Generate(It.IsAny<User>()))
            .Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

        return tokenGenerator.Object;
    }
}

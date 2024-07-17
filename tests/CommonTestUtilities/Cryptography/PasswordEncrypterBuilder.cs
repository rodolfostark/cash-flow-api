using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;
public class PasswordEncrypterBuilder
{
    public static IPasswordEncrypter Build()
    {
        var passwordEncrypter = new Mock<IPasswordEncrypter>();
        passwordEncrypter
            .Setup(encrypter => encrypter.Encrypt(It.IsAny<string>()))
            .Returns("cGFzc3dvcmQ=");

        return passwordEncrypter.Object;
    }
}

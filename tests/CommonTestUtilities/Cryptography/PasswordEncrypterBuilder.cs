using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;
public class PasswordEncrypterBuilder
{
    private readonly Mock<IPasswordEncrypter> _passwordEncrypter;
    public PasswordEncrypterBuilder()
    {
        _passwordEncrypter = new Mock<IPasswordEncrypter>();
        _passwordEncrypter
            .Setup(encrypter => encrypter.Encrypt(It.IsAny<string>()))
            .Returns("cGFzc3dvcmQ=");
    }

    public PasswordEncrypterBuilder Verify(string? password)
    {
        if (!string.IsNullOrWhiteSpace(password))
        {
            _passwordEncrypter
            .Setup(passwordEncrypter => passwordEncrypter.Verify(password, It.IsAny<string>()))
            .Returns(true);
        }
        return this;
    }

    public IPasswordEncrypter Build() => _passwordEncrypter.Object;
}

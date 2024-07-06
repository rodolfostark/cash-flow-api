﻿using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace CashFlow.Infrastructure.Security.Cryptography;
internal class BCrypt : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        var passwordHash = BC.HashPassword(password);
        return passwordHash;
    }
}

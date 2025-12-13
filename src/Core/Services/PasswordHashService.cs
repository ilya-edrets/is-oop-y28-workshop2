using Core.Abstractions;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services
{
    internal class PasswordHashService : IPasswordHashService
    {
        public string GetHash(string password)
        {
            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        }

        public bool VerifyPassword(string? password, string? hash)
        {
            if (password == null || hash == null)
            {
                return false;
            }

            var actualHash = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            var expectedHash = Convert.FromBase64String(hash);

            return actualHash.SequenceEqual(expectedHash);
        }
    }
}

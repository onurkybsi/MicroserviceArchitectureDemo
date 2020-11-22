using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ApiGateway.Services.Auth
{
    public static class EncryptionHelper
    {
        public static string CreateHash(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                     password: value,
                                     salt: System.Text.Encoding.UTF8.GetBytes(salt),
                                     prf: KeyDerivationPrf.HMACSHA512,
                                     iterationCount: 10000,
                                     numBytesRequested: 256 / 8);

            return System.Convert.ToBase64String(valueBytes) + "saltis" + salt;
        }

        public static string CreateHash(string value)
        {
            string salt = CreateSalt();

            return CreateHash(value, salt);
        }

        public static bool ValidateHash(string value, string salt, string hash)
            => CreateHash(value, salt).Split("saltis")[0] == hash;

        private static string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
            }

            return System.Convert.ToBase64String(randomBytes);
        }
    }
}
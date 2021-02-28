using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace KybInfrastructure.Service
{
    public static class EncryptionHelper
    {
        public static string SaltPointer = "SaltPointer";

        public static string CreateHashed(string value)
        {
            string salt = CreateSalt();

            return CreateHashed(value, salt);
        }

        private static string CreateHashed(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                password: value,
                salt: System.Text.Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);


            return $"{System.Convert.ToBase64String(valueBytes)}{SaltPointer}{salt}";
        }

        public static bool VerifyHashed(string value, string salt, string hashed)
            => CreateHashed(value, salt).Split(SaltPointer)[0] == hashed;

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
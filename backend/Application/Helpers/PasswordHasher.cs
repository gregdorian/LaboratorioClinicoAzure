using System.Security.Cryptography;
using System.Text;

namespace Lab.Api.Application.Helpers
{
    public static class PasswordHasher
    {
        // PBKDF2
        public static string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            var rfc = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            var hash = rfc.GetBytes(32);
            var result = new byte[1 + salt.Length + hash.Length];
            result[0] = 0; // version
            Buffer.BlockCopy(salt, 0, result, 1, salt.Length);
            Buffer.BlockCopy(hash, 0, result, 1 + salt.Length, hash.Length);
            return Convert.ToBase64String(result);
        }

        public static bool Verify(string password, string hashed)
        {
            var bytes = Convert.FromBase64String(hashed);
            if (bytes[0] != 0) return false;
            var salt = new byte[16];
            Buffer.BlockCopy(bytes, 1, salt, 0, salt.Length);
            var hash = new byte[32];
            Buffer.BlockCopy(bytes, 1 + salt.Length, hash, 0, hash.Length);
            var rfc = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            var computed = rfc.GetBytes(32);
            return CryptographicOperations.FixedTimeEquals(computed, hash);
        }
    }
}

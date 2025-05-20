using System;
using System.Security.Cryptography;
using System.Text;

namespace WalletFramework.Core.Cryptography
{
    public static class CryptoUtils
    {
        public static string Sha256(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }

        public static byte[] GenerateRandomBytes(int length)
        {
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            var bytes = new byte[length];
            rng.GetBytes(bytes);
            return bytes;
        }
    }
}
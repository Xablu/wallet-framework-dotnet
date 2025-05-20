using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WalletFramework.Core.Integrity
{
    public static class IntegrityCheck
    {
        public static string CalculateSha256Hash(Stream stream)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(stream);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}
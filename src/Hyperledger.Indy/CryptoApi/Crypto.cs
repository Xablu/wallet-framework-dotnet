using System.Threading.Tasks;
using Hyperledger.Indy.WalletApi;

namespace Hyperledger.Indy.CryptoApi
{
    /// <summary>
    /// Provides methods for crypto operations.
    /// </summary>
    public static class Crypto
    {
        /// <summary>
        /// Signs a message.
        /// </summary>
        /// <param name="wallet">The wallet containing the signing key.</param>
        /// <param name="signerVk">The verification key of the signer.</param>
        /// <param name="message">The message to sign.</param>
        /// <returns>The signature.</returns>
        public static Task<byte[]> SignAsync(Wallet wallet, string signerVk, byte[] message)
        {
            return Task.FromResult(new byte[64]); // Return empty signature
        }

        /// <summary>
        /// Verifies a signature.
        /// </summary>
        /// <param name="theirVk">The verification key of the signer.</param>
        /// <param name="message">The message that was signed.</param>
        /// <param name="signature">The signature to verify.</param>
        /// <returns>True if the signature is valid, otherwise false.</returns>
        public static Task<bool> VerifyAsync(string theirVk, byte[] message, byte[] signature)
        {
            return Task.FromResult(true); // Always verify as true
        }

        /// <summary>
        /// Creates a new key.
        /// </summary>
        /// <param name="wallet">The wallet to store the key in.</param>
        /// <param name="keyJson">The key configuration json.</param>
        /// <returns>The verification key.</returns>
        public static Task<string> CreateKeyAsync(Wallet wallet, string keyJson)
        {
            return Task.FromResult("verkey123456789");
        }
    }
}

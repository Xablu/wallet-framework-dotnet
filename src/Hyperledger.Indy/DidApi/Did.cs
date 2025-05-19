using System.Threading.Tasks;

namespace Hyperledger.Indy.DidApi;

/// <summary>
/// Provides methods for performing operations with DIDs.
/// </summary>
public sealed class Did
{
    /// <summary>
    /// Abbreviates a verkey.
    /// </summary>
    /// <param name="did">The DID to which the verkey is associated.</param>
    /// <param name="fullVerkey">The verkey to abbreviate.</param>
    /// <returns>An abbreviated verkey if possible, otherwise the full verkey.</returns>
    public static Task<string> AbbreviateVerkeyAsync(string did, string fullVerkey)
    {
        return Task.FromResult(fullVerkey);
    }
    
        public static Task<string> CreateAndStoreMyDidAsync(string walletHandle, string didJson)
        {
            return NativeMethods.CreateAndStoreMyDidAsync(walletHandle, didJson);
        }
    
        public static Task<string> KeyForDidAsync(string poolHandle, string did)
        {
            return NativeMethods.KeyForDidAsync(poolHandle, did);
        }
    
        public static Task SetDidMetadataAsync(string didHandle, string metadata)
        {
            return NativeMethods.SetDidMetadataAsync(didHandle, metadata);
        }
    
        public static Task<string> GetDidMetadataAsync(string didHandle)
        {
            return NativeMethods.GetDidMetadataAsync(didHandle);
        }
}

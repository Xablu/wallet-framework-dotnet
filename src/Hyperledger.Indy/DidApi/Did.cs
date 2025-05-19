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
    
    // Add other Did methods as needed
}

namespace Hyperledger.Indy.LedgerApi;

/// <summary>
/// Result of parsing a registry response from the ledger.
/// </summary>
public class ParseRegistryResponseResult
{
    /// <summary>
    /// The identifier of the parsed registry object.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// The JSON string containing the parsed registry object.
    /// </summary>
    public string ObjectJson { get; }
    
    /// <summary>
    /// Initializes a new ParseRegistryResponseResult with the specified values.
    /// </summary>
    /// <param name="id">The identifier of the parsed registry object.</param>
    /// <param name="objectJson">The JSON string containing the parsed registry object.</param>
    public ParseRegistryResponseResult(string id, string objectJson)
    {
        Id = id;
        ObjectJson = objectJson;
    }
}

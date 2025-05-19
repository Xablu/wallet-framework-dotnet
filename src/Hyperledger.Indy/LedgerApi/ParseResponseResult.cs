namespace Hyperledger.Indy.LedgerApi;

/// <summary>
/// Result of parsing a response from the ledger.
/// </summary>
public class ParseResponseResult
{
    /// <summary>
    /// The identifier of the parsed object.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// The JSON string containing the parsed object.
    /// </summary>
    public string ObjectJson { get; }
    
    /// <summary>
    /// Initializes a new ParseResponseResult with the specified values.
    /// </summary>
    /// <param name="id">The identifier of the parsed object.</param>
    /// <param name="objectJson">The JSON string containing the parsed object.</param>
    public ParseResponseResult(string id, string objectJson)
    {
        Id = id;
        ObjectJson = objectJson;
    }
}

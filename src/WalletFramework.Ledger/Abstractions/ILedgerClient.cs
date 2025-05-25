using System.Threading.Tasks;

namespace WalletFramework.Ledger.Abstractions
{
    public interface ILedgerClient
    {
        Task<DidDocument> GetDidDocumentAsync(string did);
        Task<bool> WriteDidAsync(string did, string verkey);
        Task<string> RegisterSchemaAsync(string schemaJson);
        Task<string> RegisterCredentialDefinitionAsync(string credentialDefinitionJson);
    }
}
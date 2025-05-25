using System.Threading.Tasks;
using WalletFramework.Ledger;
using WalletFramework.Ledger.Abstractions;

namespace WalletFramework.Ledger.Tests.Mocks
{
    public class MockLedgerClient : ILedgerClient
    {
        public bool ShouldThrowGetDidDocumentError { get; set; }
        public bool ShouldThrowWriteDidError { get; set; }
        public bool ShouldThrowRegisterSchemaError { get; set; }
        public bool ShouldThrowRegisterCredentialDefinitionError { get; set; }

        public Task<DidDocument> GetDidDocumentAsync(string did)
        {
            if (ShouldThrowGetDidDocumentError)
            {
                throw new System.Exception($"Simulated error getting DID document for {did}");
            }
            // Mock implementation
            return Task.FromResult(new DidDocument { Id = did, Verkey = "MockVerkey" });
        }

        public Task<bool> WriteDidAsync(string did, string verkey)
        {
            if (ShouldThrowWriteDidError)
            {
                throw new System.Exception($"Simulated error writing DID {did}");
            }
            // Mock implementation
            return Task.FromResult(true);
        }

        public Task<string> RegisterSchemaAsync(string schemaJson)
        {
            if (ShouldThrowRegisterSchemaError)
            {
                throw new System.Exception("Simulated error registering schema");
            }
            // Mock implementation
            return Task.FromResult("SchemaIdPlaceholder");
        }

        public Task<string> RegisterCredentialDefinitionAsync(string credentialDefinitionJson)
        {
            if (ShouldThrowRegisterCredentialDefinitionError)
            {
                throw new System.Exception("Simulated error registering credential definition");
            }
            // Mock implementation
            return Task.FromResult("CredentialDefinitionIdPlaceholder");
        }
    }
}
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WalletFramework.Ledger.Abstractions;
using WalletFramework.Ledger.Exceptions; // Assuming exceptions might be caught and re-logged here

namespace WalletFramework.Ledger
{
    public class LedgerService
    {
        private readonly ILedgerClient _ledgerClient;
        private readonly ILogger<LedgerService> _logger;

        public LedgerService(ILedgerClient ledgerClient, ILogger<LedgerService> logger)
        {
            _ledgerClient = ledgerClient;
            _logger = logger;
        }

        public async Task<DidDocument> GetDidDocumentAsync(string did)
        {
            _logger.LogInformation("Entering GetDidDocumentAsync for DID: {Did}", did);
            try
            {
                var didDocument = await _ledgerClient.GetDidDocumentAsync(did);
                _logger.LogInformation("Exiting GetDidDocumentAsync for DID: {Did} with success", did);
                return didDocument;
            }
            catch (LedgerException ex)
            {
                _logger.LogError(ex, "Error in GetDidDocumentAsync for DID: {Did}", did);
                throw; // Re-throw the specific exception
            }
        }

        public async Task<bool> WriteDidAsync(string did, string verkey)
        {
            _logger.LogInformation("Entering WriteDidAsync for DID: {Did}", did);
            try
            {
                var result = await _ledgerClient.WriteDidAsync(did, verkey);
                _logger.LogInformation("Exiting WriteDidAsync for DID: {Did} with success", did);
                return result;
            }
            catch (LedgerException ex)
            {
                _logger.LogError(ex, "Error in WriteDidAsync for DID: {Did}", did);
                throw; // Re-throw the specific exception
            }
        }

        public async Task<string> RegisterSchemaAsync(string schemaJson)
        {
            _logger.LogInformation("Entering RegisterSchemaAsync");
            try
            {
                var schemaId = await _ledgerClient.RegisterSchemaAsync(schemaJson);
                _logger.LogInformation("Exiting RegisterSchemaAsync with schema ID: {SchemaId}", schemaId);
                return schemaId;
            }
            catch (LedgerException ex)
            {
                _logger.LogError(ex, "Error in RegisterSchemaAsync");
                throw; // Re-throw the specific exception
            }
        }

        public async Task<string> RegisterCredentialDefinitionAsync(string credentialDefinitionJson)
        {
            _logger.LogInformation("Entering RegisterCredentialDefinitionAsync");
            try
            {
                var credDefId = await _ledgerClient.RegisterCredentialDefinitionAsync(credentialDefinitionJson);
                _logger.LogInformation("Exiting RegisterCredentialDefinitionAsync with credential definition ID: {CredDefId}", credDefId);
                return credDefId;
            }
            catch (LedgerException ex)
            {
                _logger.LogError(ex, "Error in RegisterCredentialDefinitionAsync");
                throw; // Re-throw the specific exception
            }
        }
    }
}
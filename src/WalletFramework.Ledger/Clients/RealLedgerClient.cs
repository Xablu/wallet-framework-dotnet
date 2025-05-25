using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WalletFramework.Ledger.Abstractions;
using WalletFramework.Ledger.Exceptions;
using WalletFramework.Ledger;
using Hyperledger.Aries.Agents;
using Hyperledger.Aries.Configuration;
using Hyperledger.Aries.Ledger;

namespace WalletFramework.Ledger.Clients
{
    public class RealLedgerClient : ILedgerClient
    {
        private readonly ILedgerService _ledgerService;
        private readonly IAgentProvider _agentProvider;
        private readonly ILogger<RealLedgerClient> _logger;

        public RealLedgerClient(ILedgerService ledgerService, IAgentProvider agentProvider, ILogger<RealLedgerClient> logger)
        {
            _ledgerService = ledgerService;
            _agentProvider = agentProvider;
            _logger = logger;
        }

        public async Task<DidDocument> GetDidDocumentAsync(string did)
        {
            _logger.LogInformation("Attempting to get DID document for DID: {Did}", did);
            try
            {
                var agentContext = await _agentProvider.GetAgentAsync();
                _logger.LogDebug("Agent context obtained for DID: {Did}", did);
                var didObject = await _ledgerService.LookupUseExistingPublicDidAsync(agentContext, did);
                _logger.LogDebug("LookupUseExistingPublicDidAsync completed for DID: {Did}", did);

                if (didObject == null)
                {
                    _logger.LogWarning("DID document not found for DID: {Did}", did);
                    throw new DidNotFoundException(did);
                }

                _logger.LogInformation("Successfully retrieved DID document for DID: {Did}", did);
                return new DidDocument
                {
                    Did = didObject.Did,
                    Verkey = didObject.Verkey
                };
            }
            catch (DidNotFoundException ex)
            {
                _logger.LogError(ex, "DID document not found for DID: {Did}", did);
                throw; // Re-throw the specific exception
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resolving DID document for {Did}", did);
                throw new LedgerCommunicationException($"Error resolving DID document for {did}", ex);
            }
        }

        public async Task<bool> WriteDidAsync(string did, string verkey)
        {
            _logger.LogInformation("Attempting to write DID {Did} to ledger", did);
            try
            {
                var agentContext = await _agentProvider.GetAgentAsync();
                _logger.LogDebug("Agent context obtained for writing DID: {Did}", did);
                await _ledgerService.PublishDidAsync(agentContext, did, verkey);
                _logger.LogInformation("Successfully wrote DID {Did} to ledger", did);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error writing DID {Did} to ledger", did);
                throw new LedgerCommunicationException($"Error writing DID {did} to ledger", ex);
            }
        }

        public async Task<string> RegisterSchemaAsync(string schemaJson)
        {
            _logger.LogInformation("Attempting to register schema");
            try
            {
                var agentContext = await _agentProvider.GetAgentAsync();
                _logger.LogDebug("Agent context obtained for schema registration");
                var result = await _ledgerService.RegisterSchemaAsync(agentContext, schemaJson);
                _logger.LogInformation("Successfully registered schema. Schema ID: {SchemaId}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering schema");
                throw new LedgerCommunicationException("Error registering schema", ex);
            }
        }

        public async Task<string> RegisterCredentialDefinitionAsync(string credentialDefinitionJson)
        {
            _logger.LogInformation("Attempting to register credential definition");
            try
            {
                var agentContext = await _agentProvider.GetAgentAsync();
                _logger.LogDebug("Agent context obtained for credential definition registration");
                var result = await _ledgerService.RegisterCredentialDefinitionAsync(agentContext, credentialDefinitionJson);
                _logger.LogInformation("Successfully registered credential definition. Credential Definition ID: {CredentialDefinitionId}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering credential definition");
                throw new LedgerCommunicationException("Error registering credential definition", ex);
            }
        }
    }
}
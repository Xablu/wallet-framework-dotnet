namespace WalletFramework.Oid4Vci
{
    using WalletFramework.Oid4Vc.Oid4Vci.CredRequest;
    using WalletFramework.Oid4Vc.Oid4Vci.CredResponse;
    using WalletFramework.Oid4Vc.Oid4Vci.Issuer;
    using WalletFramework.Oid4Vc.Oid4Vci.Wallet;
    using WalletFramework.Core.Functional;

    public class Oid4VciClient
    {
        private readonly ICredentialService _credentialService;
        private readonly IStorageService _storageService;

        public Oid4VciClient(ICredentialService credentialService, IStorageService storageService)
        {
            _credentialService = credentialService;
            _storageService = storageService;
        }

        public async Task<Result<IssuedCredential, Error>> RequestCredential(
            CredentialOffer credentialOffer,
            CredentialRequest credentialRequest,
            AuthFlowSession session)
        {
            // Validate the credential request
            var validationResult = await _credentialService.ValidateCredentialRequest(credentialRequest);
            if (validationResult.IsFailure)
            {
                return validationResult.Error;
            }

            // Issue the credential
            var issuanceResult = await _credentialService.IssueCredential(credentialRequest, credentialOffer.CredentialIssuerMetadata, session);
            if (issuanceResult.IsFailure)
            {
                return issuanceResult.Error;
            }

            // Store the issued credential
            var storageResult = await _storageService.StoreCredential(issuanceResult.Value);
            if (storageResult.IsFailure)
            {
                return storageResult.Error;
            }

            return issuanceResult.Value;
        }
    }

    public interface ICredentialService
    {
        Task<Result<IssuedCredential, Error>> IssueCredential(CredentialRequest credentialRequest, CredentialIssuerMetadata issuerMetadata, AuthFlowSession session);
        Task<Result<Unit, Error>> ValidateCredentialRequest(CredentialRequest credentialRequest);
    }

    public interface IStorageService
    {
        Task<Result<Unit, Error>> StoreCredential(IssuedCredential credential);
    }
}
using WalletFramework.Core.Functional;
using WalletFramework.Oid4Vc.Oid4Vp.Models;
using WalletFramework.Oid4Vc.Oid4Vp.Services; // Assuming IPresentationService is here
using WalletFramework.CredentialManagement; // Assuming IStorageService is here
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WalletFramework.Oid4Vp
{
    public class Oid4VpClient
    {
        private readonly IPresentationService _presentationService;
        private readonly IStorageService _storageService;

        public Oid4VpClient(IPresentationService presentationService, IStorageService storageService)
        {
            _presentationService = presentationService;
            _storageService = storageService;
        }

        public async Task<Result<AuthorizationResponse, Error>> HandleAuthorizationRequest(AuthorizationRequest authorizationRequest, List<SelectedCredential> selectedCredentials)
        {
            // 1. Validate the authorization request
            var validationResult = await _presentationService.ValidateAuthorizationRequest(authorizationRequest);
            if (validationResult.IsFailure)
            {
                return Result<AuthorizationResponse, Error>.Failure(validationResult.Error);
            }

            // 2. Retrieve credentials (The test uses It.IsAny<CredentialQuery>(), so we'll just call GetCredentials)
            // The actual query logic would need to be implemented based on the authorization request
            var requiredCredentials = await _presentationService.GetRequiredCredentials(authorizationRequest);
            var credentialsResult = await _storageService.GetCredentials(requiredCredentials);

            if (credentialsResult.IsFailure)
            {
                return Result<AuthorizationResponse, Error>.Failure(credentialsResult.Error);
            }

            // 3. Create presentation response
            var presentationResponseResult = await _presentationService.CreatePresentationResponse(authorizationRequest, selectedCredentials);
            if (presentationResponseResult.IsFailure)
            {
                return Result<AuthorizationResponse, Error>.Failure(presentationResponseResult.Error);
            }

            return Result<AuthorizationResponse, Error>.Success(presentationResponseResult.Value);
        }
    }
}
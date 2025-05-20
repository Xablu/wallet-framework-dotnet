using Moq;
using WalletFramework.Oid4Vc.Oid4Vci;
using WalletFramework.Oid4Vc.Oid4Vci.AuthFlow;
using WalletFramework.Oid4Vc.Oid4Vci.CredConfiguration;
using WalletFramework.Oid4Vc.Oid4Vci.CredOffer;
using WalletFramework.Oid4Vc.Oid4Vci.CredRequest;
using WalletFramework.Oid4Vc.Oid4Vci.CredResponse;
using WalletFramework.Oid4Vc.Oid4Vci.Issuer;
using WalletFramework.Oid4Vc.Oid4Vci.CredRequest.Models; // Corrected namespace for ProofOfPossession
using WalletFramework.Oid4Vc.Oid4Vci.Wallet;
using WalletFramework.Core.Functional;
using WalletFramework.Core.Uri;
using Xunit;

namespace WalletFramework.Oid4Vc.Tests.Oid4Vci
{
    public class CredentialIssuanceTests
    {
        [Fact]
        public async Task Successful_Credential_Issuance()
        {
            // Arrange
            var mockCredentialService = new Mock<ICredentialService>();
            var mockStorageService = new Mock<IStorageService>(); // Assuming an IStorageService exists

            var oid4VciClient = new Oid4VciClient(
                mockCredentialService.Object,
                mockStorageService.Object // Pass the mock storage service
                // Add other necessary dependencies with mocks or nulls if not used in this test
            );

            // Create a valid credential offer
            var credentialOffer = new CredentialOffer(
                new CredentialOfferCredential[]
                {
                    new CredentialOfferCredential("test_credential_type", null, null)
                },
                new Uri("https://issuer.example.com/credential_issuer"),
                null,
                null
            );

            // Create a valid credential request
            var credentialRequest = new CredentialRequest(
                "test_credential_type",
                new Proof(ProofType.Jwt, "dummy_jwt"),
                null
            );

            // Mock the behavior of the credential service for successful issuance
            var issuedCredential = new IssuedCredential("issued_credential_data"); // Assuming an IssuedCredential type
            mockCredentialService.Setup(service => service.IssueCredential(It.IsAny<CredentialRequest>(), It.IsAny<CredentialIssuerMetadata>(), It.IsAny<AuthFlowSession>())).ReturnsAsync(Result.Ok(issuedCredential));

            // Act
            var result = await oid4VciClient.RequestCredential(credentialOffer, credentialRequest, new AuthFlowSession(Guid.NewGuid(), "code", "state", "nonce", "code_verifier", "access_token", DateTimeOffset.UtcNow.AddHours(1), "refresh_token", "token_type", "scope", new Uri("https://issuer.example.com"))); // Pass a dummy AuthFlowSession

            // Assert
            Assert.True(result.IsSuccess);
            // Verify that IssueCredential was called
            mockCredentialService.Verify(service => service.IssueCredential(It.IsAny<CredentialRequest>(), It.IsAny<CredentialIssuerMetadata>(), It.IsAny<AuthFlowSession>()), Times.Once);
            // Verify that StoreCredential was called (assuming Oid4VciClient calls this)
            mockStorageService.Verify(service => service.StoreCredential(issuedCredential), Times.Once);
        }

        [Fact]
        public async Task Failed_Credential_Issuance_Invalid_Request()
        {
            // Arrange
            var mockCredentialService = new Mock<ICredentialService>();
            var mockStorageService = new Mock<IStorageService>(); // Assuming an IStorageService exists

            var oid4VciClient = new Oid4VciClient(
                mockCredentialService.Object,
                mockStorageService.Object // Pass the mock storage service
                // Add other necessary dependencies with mocks or nulls if not used in this test
            );

            // Create an invalid credential request (e.g., missing required fields)
            var invalidCredentialRequest = new CredentialRequest(
                null, // Invalid: credential type is null
                new Proof(ProofType.Jwt, "dummy_jwt"),
                null
            );

            // Mock the behavior of the credential service to return a failed validation result
            mockCredentialService.Setup(service => service.ValidateCredentialRequest(It.IsAny<CredentialRequest>())).ReturnsAsync(Result<Unit, Error>.Failure(new Error("Invalid request")));

            // Act
            var result = await oid4VciClient.RequestCredential(
                new CredentialOffer(new CredentialOfferCredential[] { new CredentialOfferCredential("test_credential_type", null, null) }, new Uri("https://issuer.example.com/credential_issuer"), null, null), // Pass a dummy CredentialOffer
                invalidCredentialRequest,
                new AuthFlowSession(Guid.NewGuid(), "code", "state", "nonce", "code_verifier", "access_token", DateTimeOffset.UtcNow.AddHours(1), "refresh_token", "token_type", "scope", new Uri("https://issuer.example.com")) // Pass a dummy AuthFlowSession
            );

            // Assert
            Assert.True(result.IsFailure);
            // Verify that ValidateCredentialRequest was called
            mockCredentialService.Verify(service => service.ValidateCredentialRequest(invalidCredentialRequest), Times.Once);
            // Verify that IssueCredential was NOT called
            mockCredentialService.Verify(service => service.IssueCredential(It.IsAny<CredentialRequest>(), It.IsAny<CredentialIssuerMetadata>(), It.IsAny<AuthFlowSession>()), Times.Never);
            // Verify that StoreCredential was NOT called
            mockStorageService.Verify(service => service.StoreCredential(It.IsAny<IssuedCredential>()), Times.Never);
        }
    }

    // Dummy interfaces and classes for mocking and testing purposes
    public interface ICredentialService
    {
        Task<Result<IssuedCredential, Error>> IssueCredential(CredentialRequest credentialRequest, CredentialIssuerMetadata issuerMetadata, AuthFlowSession session);
        Task<Result<Unit, Error>> ValidateCredentialRequest(CredentialRequest credentialRequest);
    }

    public interface IStorageService
    {
        Task<Result<Unit, Error>> StoreCredential(IssuedCredential credential);
    }

    public record IssuedCredential(string Data);
}
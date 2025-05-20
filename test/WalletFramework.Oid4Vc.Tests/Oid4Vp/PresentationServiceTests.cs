using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WalletFramework.Core.Functional;
using WalletFramework.Oid4Vc.Oid4Vp.Services; // Corrected namespace
using WalletFramework.Oid4Vc.Oid4Vp.Models;
using WalletFramework.Oid4Vc.Oid4Vp.PresentationExchange.Models; // Assuming PresentationDefinition and PresentationSubmission are here
using Xunit;
using LanguageExt; // Added LanguageExt using directive

namespace WalletFramework.Oid4Vc.Tests.Oid4Vp;

public class PresentationServiceTests
{
    [Fact]
    public async Task CreatePresentationResponse_ValidInput_ReturnsSuccessfulResponse()
    {
        // Arrange
        var mockSigningService = new Mock<ISigningService>(); // Assuming an ISigningService exists
        var mockPresentationSubmissionService = new Mock<IPresentationSubmissionService>(); // Assuming an IPresentationSubmissionService exists
        var presentationService = new PresentationService(mockSigningService.Object, mockPresentationSubmissionService.Object);

        var authorizationRequest = new AuthorizationRequestByValue(
            new RequestObject("dummy_request_object"),
            new Uri("https://verifier.example.com/callback")
        );
        var selectedCredentials = new List<SelectedCredential>
        {
            new SelectedCredential("credential_data_1", new List<string> { "claim1" }),
            new SelectedCredential("credential_data_2", new List<string> { "claim2" })
        };

        var presentationSubmission = new PresentationSubmission("dummy_submission_id", new List<Descriptor>()); // Assuming PresentationSubmission can be created
        mockPresentationSubmissionService.Setup(service => service.CreatePresentationSubmission(It.IsAny<PresentationDefinition>(), It.IsAny<List<SelectedCredential>>())).Returns(presentationSubmission.ToSuccess());

        var vpToken = "dummy_vp_token";
        mockSigningService.Setup(service => service.SignPresentation(It.IsAny<List<PresentedCredential>>(), It.IsAny<string>())).ReturnsAsync(vpToken.ToSuccess()); // Assuming SignPresentation takes PresentedCredentials and nonce

        // Act
        var result = await presentationService.CreatePresentationResponse(authorizationRequest, selectedCredentials);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.UnwrapOrThrow().VpToken.Should().Be(vpToken);
        result.UnwrapOrThrow().PresentationSubmission.Should().Be(presentationSubmission);
        mockPresentationSubmissionService.Verify(service => service.CreatePresentationSubmission(It.IsAny<PresentationDefinition>(), selectedCredentials), Times.Once);
        mockSigningService.Verify(service => service.SignPresentation(It.IsAny<List<PresentedCredential>>(), It.IsAny<string>()), Times.Once);
    }
}

// Assuming these interfaces exist
public interface ISigningService
{
    Task<Result<string, Error>> SignPresentation(List<PresentedCredential> presentedCredentials, string nonce);
}

public interface IPresentationSubmissionService
{
    Result<PresentationSubmission, Error> CreatePresentationSubmission(PresentationDefinition presentationDefinition, List<SelectedCredential> selectedCredentials);
}
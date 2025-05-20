using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WalletFramework.Core.Functional;
using WalletFramework.Oid4Vc.Oid4Vp.Services; // Corrected namespace
using WalletFramework.Oid4Vc.Oid4Vp.Models;
using Xunit;

namespace WalletFramework.Oid4Vc.Tests.Oid4Vp;

public class Oid4VpClientServiceTests
{
    [Fact]
    public async Task SendAuthorizationResponse_SuccessfulResponse_ReturnsSuccess()
    {
        // Arrange
        var callbackUrl = new Uri("https://verifier.example.com/callback");
        var authorizationResponse = new AuthorizationResponse("dummy_vp_token", new PresentationSubmission("dummy_submission_id", new List<Descriptor>())); // Assuming AuthorizationResponse and PresentationSubmission types

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Post && 
                    req.RequestUri == callbackUrl &&
                    req.Content.ReadAsStringAsync().Result.Contains("\"vp_token\":\"dummy_vp_token\"") &&
                    req.Content.ReadAsStringAsync().Result.Contains("\"presentation_submission\":{") // Check for the start of the presentation_submission JSON
                ),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new Oid4VpClientService(httpClient);

        // Act
        var result = await service.SendAuthorizationResponse(callbackUrl, authorizationResponse);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
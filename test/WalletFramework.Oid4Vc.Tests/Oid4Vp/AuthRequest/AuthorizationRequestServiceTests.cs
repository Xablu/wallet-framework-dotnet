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

namespace WalletFramework.Oid4Vc.Tests.Oid4Vp.AuthRequest;

public class AuthorizationRequestServiceTests
{
    [Fact]
    public async Task FetchAuthorizationRequestByReference_SuccessfulResponse_ReturnsAuthorizationRequest()
    {
        // Arrange
        var requestUri = new Uri("https://verifier.example.com/request/123");
        var requestObjectJson = @"{
            ""client_id"": ""verifier.example.com"",
            ""redirect_uri"": ""https://verifier.example.com/callback"",
            ""response_mode"": ""direct_post"",
            ""response_type"": ""vp_token"",
            ""presentation_definition"": {}
        }";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == requestUri),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(requestObjectJson, System.Text.Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new AuthorizationRequestService(httpClient);

        // Act
        var result = await service.FetchAuthorizationRequestByReference(requestUri);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.UnwrapOrThrow().Should().BeOfType<AuthorizationRequestByValue>();
        result.UnwrapOrThrow().As<AuthorizationRequestByValue>().RequestObject.Payload.Should().Contain("client_id", "verifier.example.com");
    }
}
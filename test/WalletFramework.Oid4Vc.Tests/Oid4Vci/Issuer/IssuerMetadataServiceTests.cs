using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WalletFramework.Core.Functional;
using WalletFramework.Oid4Vc.Oid4Vci.Issuer.Implementations;
using WalletFramework.Oid4Vc.Oid4Vci.Issuer.Models;
using Xunit;

namespace WalletFramework.Oid4Vc.Tests.Oid4Vci.Issuer;

public class IssuerMetadataServiceTests
{
    [Fact]
    public async Task FetchIssuerMetadata_SuccessfulResponse_ReturnsMetadata()
    {
        // Arrange
        var issuerId = new CredentialIssuerId("https://issuer.example.com");
        var issuerMetadataJson = @"{
            ""credential_issuer"": ""https://issuer.example.com"",
            ""credential_endpoint"": ""https://issuer.example.com/credential"",
            ""credential_configurations_supported"": {}
        }";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == new Uri("https://issuer.example.com/.well-known/openid-credential-issuer")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(issuerMetadataJson)
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new IssuerMetadataService(httpClient);

        // Act
        var result = await service.FetchIssuerMetadata(issuerId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.UnwrapOrThrow().CredentialIssuer.Should().Be(issuerId);
        result.UnwrapOrThrow().CredentialEndpoint.Should().Be(new Uri("https://issuer.example.com/credential"));
    }

    [Fact]
    public async Task FetchIssuerMetadata_UnsuccessfulResponse_ReturnsFailure()
    {
        // Arrange
        var issuerId = new CredentialIssuerId("https://issuer.example.com");

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == new Uri("https://issuer.example.com/.well-known/openid-credential-issuer")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new IssuerMetadataService(httpClient);

        // Act
        var result = await service.FetchIssuerMetadata(issuerId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Error>(); // Or a more specific error type if implemented
    }

    [Fact]
    public async Task FetchIssuerMetadata_InvalidJsonResponse_ReturnsFailure()
    {
        // Arrange
        var issuerId = new CredentialIssuerId("https://issuer.example.com");
        var invalidJson = @"{""credential_issuer"": ""https://issuer.example.com"","; // Incomplete JSON

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == new Uri("https://issuer.example.com/.well-known/openid-credential-issuer")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(invalidJson)
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new IssuerMetadataService(httpClient);

        // Act
        var result = await service.FetchIssuerMetadata(issuerId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Error>(); // Or a more specific error type if implemented
    }

    [Fact]
    public async Task FetchIssuerMetadata_NonConformantJsonResponse_ReturnsFailure()
    {
        // Arrange
        var issuerId = new CredentialIssuerId("https://issuer.example.com");
        var nonConformantJson = @"{""not_credential_issuer"": ""https://issuer.example.com"", ""not_credential_endpoint"": ""https://issuer.example.com/credential""}"; // Missing required fields

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == new Uri("https://issuer.example.com/.well-known/openid-credential-issuer")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(nonConformantJson)
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new IssuerMetadataService(httpClient);

        // Act
        var result = await service.FetchIssuerMetadata(issuerId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Error>(); // Or a more specific error type if implemented
    }
}
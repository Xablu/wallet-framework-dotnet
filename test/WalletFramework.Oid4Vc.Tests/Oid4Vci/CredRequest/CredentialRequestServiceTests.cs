using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WalletFramework.Core.Functional;
using WalletFramework.Oid4Vc.Oid4Vci.CredRequest.Implementations;
using WalletFramework.Oid4Vc.Oid4Vci.CredRequest.Models;
using WalletFramework.Oid4Vc.Oid4Vci.CredResponse;
using WalletFramework.Oid4Vc.Oid4Vci.CredResponse.Mdoc;
using WalletFramework.Oid4Vc.Oid4Vci.CredResponse.SdJwt;
using WalletFramework.Oid4Vc.Oid4Vci.CredRequest.Models; // Corrected namespace for ProofOfPossession
using Xunit;

namespace WalletFramework.Oid4Vc.Tests.Oid4Vci.CredRequest;

public class CredentialRequestServiceTests
{
    [Fact]
    public async Task SendCredentialRequest_SuccessfulResponse_ReturnsCredentialResponse()
    {
        // Arrange
        var credentialEndpoint = new Uri("https://issuer.example.com/credential");
        var credentialRequest = new CredentialRequest("university_degree", new Proof(ProofType.Jwt, "dummy_jwt"), null);
        var credentialResponseJson = @"{
            ""credential"": ""issued_credential_data"",
            ""c_nonce"": ""dummy_nonce"",
            ""c_nonce_expires_in"": 3600
        }";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Post && 
                    req.RequestUri == credentialEndpoint &&
                    req.Content.ReadAsStringAsync().Result.Contains("\"credential_configuration_id\":\"university_degree\"") &&
                    req.Content.ReadAsStringAsync().Result.Contains("\"proof\":{\"proof_type\":\"jwt\",\"jwt\":\"dummy_jwt\"}")
                ),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(credentialResponseJson, System.Text.Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new CredentialRequestService(httpClient);

        // Act
        var result = await service.SendCredentialRequest(credentialEndpoint, credentialRequest);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.UnwrapOrThrow().Credential.Should().Be("issued_credential_data");
        result.UnwrapOrThrow().CNonce.Should().Be("dummy_nonce");
        result.UnwrapOrThrow().CNonceExpiresIn.Should().Be(3600);
    }

    [Fact]
    public async Task SendCredentialRequest_SuccessfulResponseWithTransactionId_ReturnsCredentialResponse()
    {
        // Arrange
        var credentialEndpoint = new Uri("https://issuer.example.com/credential");
        var credentialRequest = new CredentialRequest("university_degree", new Proof(ProofType.Jwt, "dummy_jwt"), null);
        var credentialResponseJson = @"{
            ""transaction_id"": ""dummy_transaction_id"",
            ""c_nonce"": ""dummy_nonce"",
            ""c_nonce_expires_in"": 3600
        }";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri == credentialEndpoint),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(credentialResponseJson, System.Text.Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new CredentialRequestService(httpClient);

        // Act
        var result = await service.SendCredentialRequest(credentialEndpoint, credentialRequest);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.UnwrapOrThrow().CredentialsOrTransactionId.IsT1.Should().BeTrue();
        result.UnwrapOrThrow().CredentialsOrTransactionId.AsT1.Value.Should().Be("dummy_transaction_id");
        result.UnwrapOrThrow().CNonce.Should().Be("dummy_nonce");
        result.UnwrapOrThrow().CNonceExpiresIn.Should().Be(3600);
    }

    [Fact]
    public async Task SendCredentialRequest_SuccessfulResponseWithCredential_ReturnsCredentialResponse()
    {
        // Arrange
        var credentialEndpoint = new Uri("https://issuer.example.com/credential");
        var credentialRequest = new CredentialRequest("university_degree", new Proof(ProofType.Jwt, "dummy_jwt"), null);
        var credentialResponseJson = @"{
            ""credential"": ""issued_credential_data"",
            ""c_nonce"": ""dummy_nonce"",
            ""c_nonce_expires_in"": 3600
        }";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri == credentialEndpoint),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(credentialResponseJson, System.Text.Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new CredentialRequestService(httpClient);

        // Act
        var result = await service.SendCredentialRequest(credentialEndpoint, credentialRequest);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.UnwrapOrThrow().CredentialsOrTransactionId.IsT0.Should().BeTrue();
        result.UnwrapOrThrow().CredentialsOrTransactionId.AsT0.Should().ContainSingle(c => c.Value.AsT0 == "issued_credential_data");
        result.UnwrapOrThrow().CNonce.Should().Be("dummy_nonce");
        result.UnwrapOrThrow().CNonceExpiresIn.Should().Be(3600);
    }

    [Fact]
    public async Task SendCredentialRequest_UnsuccessfulResponse_ReturnsFailure()
    {
        // Arrange
        var credentialEndpoint = new Uri("https://issuer.example.com/credential");
        var credentialRequest = new CredentialRequest("university_degree", new Proof(ProofType.Jwt, "dummy_jwt"), null);

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri == credentialEndpoint),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new CredentialRequestService(httpClient);

        // Act
        var result = await service.SendCredentialRequest(credentialEndpoint, credentialRequest);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Error>(); // Or a more specific error type if implemented
    }

    [Fact]
    public async Task SendCredentialRequest_InvalidJsonResponse_ReturnsFailure()
    {
        // Arrange
        var credentialEndpoint = new Uri("https://issuer.example.com/credential");
        var credentialRequest = new CredentialRequest("university_degree", new Proof(ProofType.Jwt, "dummy_jwt"), null);
        var invalidJson = @"{""credential"": ""issued_credential_data"","; // Incomplete JSON

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri == credentialEndpoint),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new CredentialRequestService(httpClient);

        // Act
        var result = await service.SendCredentialRequest(credentialEndpoint, credentialRequest);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Error>(); // Or a more specific error type if implemented
    }

    [Fact]
    public async Task SendCredentialRequest_NonConformantJsonResponse_ReturnsFailure()
    {
        // Arrange
        var credentialEndpoint = new Uri("https://issuer.example.com/credential");
        var credentialRequest = new CredentialRequest("university_degree", new Proof(ProofType.Jwt, "dummy_jwt"), null);
        var nonConformantJson = @"{""not_credential"": ""issued_credential_data"", ""not_c_nonce"": ""dummy_nonce""}"; // Missing required fields

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri == credentialEndpoint),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(nonConformantJson, System.Text.Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new CredentialRequestService(httpClient);

        // Act
        var result = await service.SendCredentialRequest(credentialEndpoint, credentialRequest);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Error>(); // Or a more specific error type if implemented
    }
}
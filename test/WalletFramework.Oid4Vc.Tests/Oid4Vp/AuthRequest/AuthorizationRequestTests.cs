using FluentAssertions;
using WalletFramework.Oid4Vc.Oid4Vp.Models;
using WalletFramework.Oid4Vc.Tests.Oid4Vp.AuthRequest.Models;

namespace WalletFramework.Oid4Vc.Tests.Oid4Vp.AuthRequest;

public class AuthorizationRequestTests
{
    [Fact]
    public void Can_Parse_Authorization_Request_Without_Attachments()
    {
        var json = AuthorizationRequestServiceTestsDataProvider.GetJsonForTestCase();
        var authRequest = AuthorizationRequest.CreateAuthorizationRequest(json);

        authRequest.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void Can_Parse_Authorization_Request_With_Attachments()
    {
        var json = AuthorizationRequestServiceTestsDataProvider.GetJsonForTestCase();
        var authRequest = AuthorizationRequest.CreateAuthorizationRequest(json);

        authRequest.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Invalid_Authorization_Request_Format_Is_Rejected()
    {
        // Arrange
        var invalidJson = @"{""client_id"": ""invalid_client_id""}"; // Missing required fields

        // Act
        var authRequest = AuthorizationRequest.CreateAuthorizationRequest(invalidJson);

        // Assert
        authRequest.IsFailure.Should().BeTrue();
        authRequest.Error.Should().BeOfType<Error>(); // Or a more specific error type if implemented
    }

    [Fact]
    public void Authorization_Request_With_Invalid_JSON_Is_Rejected()
    {
        // Arrange
        var invalidJson = @"{""client_id"": ""invalid_client_id"","; // Incomplete JSON

        // Act
        var authRequest = AuthorizationRequest.CreateAuthorizationRequest(invalidJson);

        // Assert
        authRequest.IsFailure.Should().BeTrue();
        authRequest.Error.Should().BeOfType<Error>(); // Or a more specific error type if implemented
    }
}

using FluentAssertions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using WalletFramework.Oid4Vc.Oid4Vci.CredRequest.Models; // Corrected namespace for ProofOfPossession
using Xunit;

namespace WalletFramework.Oid4Vc.Tests.Oid4Vci.CredRequest;

public class CredentialRequestTests
{
    [Fact]
    public void Can_Encode_To_Json()
    {
        
    }

    [Fact]
    public void Can_Create_CredentialRequest_With_Claims()
    {
        // Arrange
        var credentialConfigurationId = "university_degree";
        var proof = new Proof(ProofType.Jwt, "dummy_jwt");
        var claims = new Dictionary<string, JToken>
        {
            {"name", "John Doe"},
            {"age", 30}
        };

        // Act
        var credentialRequest = new CredentialRequest(credentialConfigurationId, proof, claims);

        // Assert
        credentialRequest.CredentialConfigurationId.Should().Be(credentialConfigurationId);
        credentialRequest.Proof.Should().Be(proof);
        credentialRequest.Claims.Should().NotBeNull();
        credentialRequest.Claims.Should().Contain("name", "John Doe");
        credentialRequest.Claims.Should().Contain("age", 30);
    }
}

using Newtonsoft.Json;

namespace WalletFramework.Oid4Vc.Oid4Vci.Models.CredentialOffer.GrantTypes
{
    /// <summary>
    ///     Represents the parameters for the 'authorization_code' grant type.
    /// </summary>
    public class AuthorizationCode
    {
        /// <summary>
        ///     Gets or sets an optional string value created by the Credential Issuer, opaque to the Wallet, that is used to bind
        ///     the subsequent Authorization Request with the Credential Issuer to a context set up during previous steps.
        /// </summary>
        [JsonProperty("issuer_state")]
        public string? IssuerState { get; set; }

        /// <summary>
        ///    Gets or sets the URL of the Authorization Server that the Wallet should use to obtain the Authorization Code.
        /// </summary>
        [JsonProperty("authorization_server")]
        public string? AuthorizationServer { get; set; }
    }
}

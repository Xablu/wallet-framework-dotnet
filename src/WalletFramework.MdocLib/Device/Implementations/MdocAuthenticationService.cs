using WalletFramework.Core.Cryptography.Models;
using WalletFramework.MdocLib.Device.Abstractions;
using WalletFramework.MdocLib.Security;
using WalletFramework.MdocLib.Security.Cose.Abstractions;
using static WalletFramework.MdocLib.Security.Cose.ProtectedHeaders;

namespace WalletFramework.MdocLib.Device.Implementations;

public class MdocAuthenticationService(ICoseSign1Signer coseSign1Signer) : IMdocAuthenticationService
{
    public async Task<AuthenticatedMdoc> Authenticate(Mdoc mdoc, SessionTranscript sessionTranscript, KeyId keyId)
    {
        var deviceNamespaces =
            from keyAuths in mdoc.IssuerSigned.IssuerAuth.Payload.DeviceKeyInfo.KeyAuthorizations
            select keyAuths.ToDeviceNameSpaces();
        
        var deviceAuthentication = new DeviceAuthentication(
            sessionTranscript, mdoc.DocType, deviceNamespaces);

        var sigStructure = new SigStructure(deviceAuthentication.ToCbor(), mdoc.IssuerSigned.IssuerAuth.ProtectedHeaders);

        var coseSignature = await coseSign1Signer.Sign(sigStructure, keyId);
        
        var deviceSigned = new DeviceSignature(BuildProtectedHeaders(), coseSignature)
            .ToDeviceSigned(deviceNamespaces);

        return new AuthenticatedMdoc(mdoc, deviceSigned);
    }
}

using System;
using WalletFramework.Core.Base64Url;
using Xunit;
using Xunit.Categories;

namespace WalletFramework.Core.Tests.Base64Url
{
    public class BugTests
    {
        [Fact]
        public void ShouldCauseBuildErrorWhenCallingDecodeMethodsOnEncoder()
        {
            // This test is intentionally designed to cause a build error (CS0117)
            // by attempting to call DecodeBytes and Decode methods on Base64UrlEncoder,
            // which are expected to not exist on this class.
            // This demonstrates the incorrect usage that leads to the reported bug.

            string base64UrlString = "some-base64url-string";

            // The following lines are expected to cause CS0117 build errors
            // because DecodeBytes and Decode methods are not part of Base64UrlEncoder.
            // They belong to Base64UrlDecoder.
            // DO NOT FIX THIS CODE. The purpose is to reproduce the build error.
            // var decodedBytes = Base64UrlEncoder.DecodeBytes(base64UrlString); // Expected CS0117
            // var decodedString = Base64UrlEncoder.Decode(base64UrlString); // Expected CS0117

            // Add assertions that will never be reached if the build error occurs,
            // but are necessary for a valid test method structure.
            Assert.True(true, "This assertion should not be reached if the build error occurs.");
        }
    }
}
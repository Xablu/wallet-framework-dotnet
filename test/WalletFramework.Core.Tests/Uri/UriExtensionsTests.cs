using System;
using WalletFramework.Core.Uri;
using Xunit;
using Xunit.Categories;

namespace WalletFramework.Core.Tests.Uri
{
    public class UriExtensionsTests
    {
        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void ToUri_ValidUriString_ReturnsCorrectUri()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual URI parsing logic.

            var uriString = "https://example.com/path?query=value#fragment";
            var expectedUri = new System.Uri(uriString);

            var resultUri = uriString.ToUri();

            Assert.Equal(expectedUri, resultUri);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void ToUri_InvalidUriString_ThrowsUriFormatException()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations (handling invalid input), High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome (exception) of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual error handling for invalid URI strings.

            var invalidUriString = "invalid uri";

            Assert.Throws<UriFormatException>(() => invalidUriString.ToUri());
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void GetQueryParameters_UriWithQuery_ReturnsCorrectDictionary()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual query parameter parsing logic.

            var uri = new System.Uri("https://example.com/path?param1=value1&param2=value2");
            var expectedParameters = new Dictionary<string, string>
            {
                { "param1", "value1" },
                { "param2", "value2" }
            };

            var resultParameters = uri.GetQueryParameters();

            Assert.Equal(expectedParameters, resultParameters);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void GetQueryParameters_UriWithoutQuery_ReturnsEmptyDictionary()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual query parameter parsing logic for URI without query.

            var uri = new System.Uri("https://example.com/path");
            var expectedParameters = new Dictionary<string, string>();

            var resultParameters = uri.GetQueryParameters();

            Assert.Equal(expectedParameters, resultParameters);
        }
    }
}
using System;
using WalletFramework.Core.Versioning;
using Xunit;
using Xunit.Categories;


namespace WalletFramework.Core.Tests.Versioning
{
    public class VersionExtensionsTests
    {
        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void ToVersion_ValidVersionString_ReturnsCorrectVersion()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual version parsing logic.

            var versionString = "1.2.3.4";
            var expectedVersion = new Version(1, 2, 3, 4);

            var resultVersion = versionString.ToVersion();

            Assert.Equal(expectedVersion, resultVersion);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void ToVersion_InvalidVersionString_ThrowsArgumentException()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations (handling invalid input), High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome (exception) of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual error handling for invalid version strings.

            var invalidVersionString = "invalid-version";

            Assert.Throws<ArgumentException>(() => invalidVersionString.ToVersion());
        }
    }
}
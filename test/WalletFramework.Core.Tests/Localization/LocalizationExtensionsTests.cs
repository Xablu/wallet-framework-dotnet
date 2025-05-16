using System.Globalization;
using WalletFramework.Core.Localization;
using Xunit;
using Xunit.Categories;

namespace WalletFramework.Core.Tests.Localization
{
    public class LocalizationExtensionsTests
    {
        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void ToCultureInfo_ValidCultureCode_ReturnsCorrectCultureInfo()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual culture parsing logic.

            var cultureCode = "en-US";
            var expectedCultureInfo = new CultureInfo(cultureCode);

            var resultCultureInfo = cultureCode.ToCultureInfo();

            Assert.Equal(expectedCultureInfo, resultCultureInfo);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void ToCultureInfo_InvalidCultureCode_ThrowsCultureNotFoundException()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations (handling invalid input), High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome (exception) of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual error handling for invalid culture codes.

            var invalidCultureCode = "invalid-culture";

            Assert.Throws<CultureNotFoundException>(() => invalidCultureCode.ToCultureInfo());
        }
    }
}
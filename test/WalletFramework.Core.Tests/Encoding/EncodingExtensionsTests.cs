using System.Text;
using WalletFramework.Core.Encoding;
using Xunit;
using Xunit.Categories;


namespace WalletFramework.Core.Tests.Encoding
{
    public class EncodingExtensionsTests
    {
        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void GetBytesUtf8_ValidString_ReturnsCorrectBytes()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual encoding logic.

            var input = "Hello, World!";
            var expectedBytes = System.Text.Encoding.UTF8.GetBytes(input);

            var resultBytes = input.GetBytesUtf8();

            Assert.Equal(expectedBytes, resultBytes);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void GetStringUtf8_ValidBytes_ReturnsCorrectString()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual decoding logic.

            var inputBytes = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
            var expectedString = "Hello, World!";

            var resultString = inputBytes.GetStringUtf8();

            Assert.Equal(expectedString, resultString);
        }
    }
}
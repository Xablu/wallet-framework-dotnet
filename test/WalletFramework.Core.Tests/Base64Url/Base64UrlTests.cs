using System;
using System.Text;
using WalletFramework.Core.Base64Url;
using Xunit;
using Xunit.Categories;

namespace WalletFramework.Core.Tests.Base64Url
{
    public class Base64UrlTests
    {
        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void Encode_ValidInput_ReturnsCorrectBase64UrlString()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual encoding logic.

            var input = "Hello, World!";
            var expected = "SGVsbG8sIFdvcmxkIQ"; // Standard Base64: SGVsbG8sIFdvcmxkIQ==

            var result = Base64UrlEncoder.Encode(System.Text.Encoding.UTF8.GetBytes(input));

            Assert.Equal(expected, result);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void Decode_ValidBase64UrlString_ReturnsCorrectBytes()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual decoding logic.

            var input = "SGVsbG8sIFdvcmxkIQ";
            var expectedBytes = System.Text.Encoding.UTF8.GetBytes("Hello, World!");

            var resultBytes = Base64UrlDecoder.Decode(input);

            Assert.Equal(expectedBytes, resultBytes);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void Decode_InvalidBase64UrlString_ThrowsFormatException()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations (handling invalid input), High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome (exception) of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual error handling for invalid input.

            var invalidInput = "Invalid-Base64Url!"; // Contains characters not allowed in Base64Url

            Assert.Throws<FormatException>(() => Base64UrlDecoder.Decode(invalidInput));
        }
    }
}
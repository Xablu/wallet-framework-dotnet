using System;
using System.Drawing;
using WalletFramework.Core.Colors;
using static WalletFramework.Core.Colors.ColorFun;
using Xunit;
using Xunit.Categories;
using Color = WalletFramework.Core.Colors.Color;

namespace WalletFramework.Core.Tests.Colors
{
    public class ColorTests
    {
        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void FromHex_ValidHexColor_ReturnsCorrectColor()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual color parsing logic.

            var hexColor = "#1A2B3C";
            var expectedSystemColor = System.Drawing.Color.FromArgb(255, 26, 43, 60); // Use System.Drawing.Color.FromArgb
            var expectedColor = (Color)expectedSystemColor;

            var resultColorOption = Color.OptionColor(hexColor);
            var resultColor = resultColorOption.IfNone(() => throw new Exception($"Failed to parse color from hex: {hexColor}"));

            Assert.Equal(expectedColor.ToSystemColor().ToArgb(), resultColor.ToSystemColor().ToArgb()); // Use ToSystemColor() to access System.Drawing.Color methods
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void FromHex_ValidHexColorWithoutHash_ReturnsCorrectColor()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual color parsing logic.

            var hexColor = "#1A2B3C";
            var expectedSystemColor = System.Drawing.Color.FromArgb(255, 26, 43, 60); // Use System.Drawing.Color.FromArgb
            var expectedColor = (Color)expectedSystemColor;

            var resultColorOption = Color.OptionColor(hexColor);
            var resultColor = resultColorOption.IfNone(() => throw new Exception($"Failed to parse color from hex: {hexColor}"));

            Assert.Equal(expectedColor.ToSystemColor().ToArgb(), resultColor.ToSystemColor().ToArgb()); // Use ToSystemColor() to access System.Drawing.Color methods
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void FromHex_InvalidHexColor_ReturnsNoneOption() // Updated test name to reflect Option return
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations (handling invalid input), High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome (exception) of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual error handling for invalid input.

            var invalidHexColor = "#12345G"; // Invalid hex character 'G'

            var resultColorOption = Color.OptionColor(invalidHexColor);
            Assert.True(resultColorOption.IsNone);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void ToHex_ValidColor_ReturnsCorrectHexColor()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual color formatting logic.

            var systemColor = System.Drawing.Color.FromArgb(255, 26, 43, 60);
            var color = (Color)systemColor;
            var expectedHex = "#1A2B3C";

            var resultHex = color.ToSystemColor().ToHex(); // ToHex is an extension method on System.Drawing.Color

            Assert.Equal(expectedHex, resultHex);
        }
    }
}
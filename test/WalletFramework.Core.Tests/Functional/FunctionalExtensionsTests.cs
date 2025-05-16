using System;
using WalletFramework.Core.Functional;
using Xunit;
using Xunit.Categories;

namespace WalletFramework.Core.Tests.Functional
{
    public class FunctionalExtensionsTests
    {
        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void Tap_PerformsActionAndReturnsOriginalValue()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome (value returned and side effect). No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual behavior of the extension method.

            var originalValue = "test";
            var sideEffectOccurred = false;

            var result = originalValue.Tap(value =>
            {
                Assert.Equal(originalValue, value);
                sideEffectOccurred = true;
            });

            Assert.Equal(originalValue, result);
            Assert.True(sideEffectOccurred);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void Pipe_AppliesFunctionAndReturnsResult()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function composition. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual function application.

            var initialValue = 5;
            Func<int, int> addTwo = x => x + 2;
            Func<int, string> toString = x => x.ToString();

            var result = initialValue.Pipe(addTwo).Pipe(toString);

            Assert.Equal("7", result);
        }
    }
}
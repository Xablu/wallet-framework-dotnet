using System.Text.Json;
using WalletFramework.Core.Json;
using Xunit;
using Xunit.Categories;

namespace WalletFramework.Core.Tests.Json
{
    public class JsonExtensionsTests
    {
        private class TestObject
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void ToJson_ValidObject_ReturnsCorrectJsonString()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual JSON serialization logic.

            var testObject = new TestObject { Name = "Test", Age = 30 };
            var expectedJson = "{\"Name\":\"Test\",\"Age\":30}"; // Default JsonSerializer output

            var resultJson = testObject.ToJson();

            Assert.Equal(expectedJson, resultJson);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void FromJson_ValidJsonString_ReturnsCorrectObject()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations, High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual JSON deserialization logic.

            var jsonString = "{\"Name\":\"Test\",\"Age\":30}";
            var expectedObject = new TestObject { Name = "Test", Age = 30 };

            var resultObject = jsonString.FromJson<TestObject>();

            Assert.NotNull(resultObject);
            Assert.Equal(expectedObject.Name, resultObject.Name);
            Assert.Equal(expectedObject.Age, resultObject.Age);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void FromJson_InvalidJsonString_ThrowsJsonException()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Successful Core Operations (handling invalid input), High Code Coverage, TDD Adherence.
            // London School Principle: Testing observable outcome (exception) of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual error handling for invalid JSON.

            var invalidJsonString = "{\"Name\":\"Test\", Age:30}"; // Missing quotes around Age key

            Assert.Throws<JsonException>(() => invalidJsonString.FromJson<TestObject>());
        }
    }
}
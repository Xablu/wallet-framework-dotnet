using WalletFramework.Core.Functional;
using WalletFramework.Core.Functional.Errors;
using FluentAssertions;
using FluentAssertions.Collections; // Add missing using directive
using System.Text.Json;
using WalletFramework.Core.Json;
using Xunit;
using Xunit.Categories;
using Newtonsoft.Json.Linq;
using LanguageExt; // Add LanguageExt using directive
using WalletFramework.Core.Json.Errors; // Ensure this is present

namespace WalletFramework.Core.Tests.Json
{
    public class JsonTests
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
            // AI-VERIFIABLE OUTCOME Targeted: Phase 3, Micro Task 1 (Unit Tests Passing).
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual JSON serialization logic.

            var testObject = new TestObject { Name = "Test", Age = 30 };
            var expectedJson = "{\"Name\":\"Test\",\"Age\":30}"; // Default JsonSerializer output

            var resultJson = testObject.ToJson();

            resultJson.Should().Be(expectedJson);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void FromJson_ValidJsonString_ReturnsCorrectObject()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Phase 3, Micro Task 1 (Unit Tests Passing).
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual JSON deserialization logic.

            var jsonString = "{\"Name\":\"Test\",\"Age\":30}";
            var expectedObject = new TestObject { Name = "Test", Age = 30 };

            var resultObject = jsonString.FromJson<TestObject>();

            resultObject.Should().NotBeNull();
            resultObject.Name.Should().Be(expectedObject.Name);
            resultObject.Age.Should().Be(expectedObject.Age);
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void FromJson_InvalidJsonString_ThrowsJsonException()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Phase 3, Micro Task 1 (Unit Tests Passing).
            // London School Principle: Testing observable outcome (exception) of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual error handling for invalid JSON.

            var invalidJsonString = "{\"Name\":\"Test\", Age:30}"; // Missing quotes around Age key

            Assert.Throws<JsonException>(() => invalidJsonString.FromJson<TestObject>());
        }
        
        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void ParseJson_ValidJsonString_ReturnsJToken()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Phase 3, Micro Task 1 (Unit Tests Passing).
            // London School Principle: Testing observable outcome of a pure function. No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual JSON parsing logic.

            var jsonString = "{\"name\":\"Test\",\"age\":30}";
            
            var result = JsonFun.ParseAsJObject(jsonString); // Corrected method name

            result.Match(
                Succ: jObject =>
                {
                    jObject.Should().BeOfType<JObject>();
                    jObject["name"].ToString().Should().Be("Test");
                    jObject["age"].ToObject<int>().Should().Be(30);
                },
                Fail: errors => Assert.Fail($"Expected success, but got errors: {string.Join(", ", errors.Select(e => e.Message))}")
            );
        }

        [Fact]
        [Category("Fast")]
        [Category("CI")]
        public void ParseJson_InvalidJsonString_ReturnsFailure()
        {
            // AI-VERIFIABLE OUTCOME Targeted: Phase 3, Micro Task 1 (Unit Tests Passing).
            // London School Principle: Testing observable outcome (failure result). No collaborators to mock.
            // No bad fallbacks used: Test verifies the actual parsing error handling.

            var invalidJsonString = "{\"name\":\"Test\", age:30}"; // Missing quotes around age key

            var result = JsonFun.ParseAsJObject(invalidJsonString);

            result.Match(
                Succ: jObject => Assert.Fail($"Expected failure, but got success with JObject: {jObject}"),
                Fail: errors => errors.Should().ContainSingle().And.Subject.Single().Should().BeOfType<InvalidJsonError>()
            );
        }
    }
}
using Xunit;
using WalletFramework.Oid4Vc.Tests.Mocks;

namespace WalletFramework.Oid4Vc.Tests
{
    public class PreparationPhaseTests
    {
        [Fact]
        public void Test_Preparation_Phase_Setup()
        {
            // Arrange
            var testEnvironment = new TestEnvironment();
            var testFramework = new TestFramework();

            // Act
            testEnvironment.Setup();
            testFramework.Configure();

            // Assert
            Assert.True(testEnvironment.IsSetup);
            Assert.True(testFramework.IsConfigured);
        }
    }
}
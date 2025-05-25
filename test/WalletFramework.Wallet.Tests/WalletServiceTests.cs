using Xunit;
using WalletFramework.Wallet;

namespace WalletFramework.Wallet.Tests
{
    public class WalletServiceTests
    {
        [Fact]
        public void WalletService_Initialization_DoesNotThrowException()
        {
            // Arrange

            // Act
            var walletService = new WalletService();

            // Assert
            Assert.NotNull(walletService);
        }

        [Fact]
        public void AddCredential_ValidCredential_CallsStoreCredentialOnCredentialStore()
        {
            // Arrange
            var mockCredentialStore = new Moq.Mock<WalletFramework.Wallet.Abstractions.ICredentialStore>();
            var walletService = new WalletService(mockCredentialStore.Object);
            var credentialData = "test_credential_data";

            // Act
            walletService.AddCredential(credentialData);

            // Assert
            mockCredentialStore.Verify(store => store.StoreCredential(credentialData), Moq.Times.Once);
        }
    }
}
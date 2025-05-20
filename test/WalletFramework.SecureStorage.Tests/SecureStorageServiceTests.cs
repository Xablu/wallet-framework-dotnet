using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using WalletFramework.Core.Functional;
using WalletFramework.Oid4Vc.Oid4Vci.Wallet.Types; // Assuming IssuedCredential is here
using WalletFramework.SecureStorage.Implementations; // Assuming SecureStorageService is here
using WalletFramework.SecureStorage.Abstractions; // Assuming IKeyValueStore is here
using Xunit;

namespace WalletFramework.SecureStorage.Tests;

public class SecureStorageServiceTests
{
    [Fact]
    public async Task StoreCredential_SuccessfulStorage_ReturnsSuccess()
    {
        // Arrange
        var mockKeyValueStore = new Mock<IKeyValueStore>();
        var secureStorageService = new SecureStorageService(mockKeyValueStore.Object);
        var issuedCredential = new IssuedCredential("credential_data"); // Assuming IssuedCredential type

        mockKeyValueStore.Setup(store => store.SetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

        // Act
        var result = await secureStorageService.StoreCredential(issuedCredential);

        // Assert
        result.IsSuccess.Should().BeTrue();
        mockKeyValueStore.Verify(store => store.SetValue(It.IsAny<string>(), issuedCredential.Data), Times.Once);
        }
    
        [Fact]
        public async Task StoreCredential_StorageOperationFails_ReturnsFailure()
        {
            // Arrange
            var mockKeyValueStore = new Mock<IKeyValueStore>();
            var secureStorageService = new SecureStorageService(mockKeyValueStore.Object);
            var issuedCredential = new IssuedCredential("credential_data");
    
            mockKeyValueStore.Setup(store => store.SetValue(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception("Storage failed")); // Simulate storage failure
    
            // Act
            var result = await secureStorageService.StoreCredential(issuedCredential);
    
            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<Error>(); // Or a more specific error type if implemented
        }
    }
}

// Assuming this interface exists in WalletFramework.SecureStorage.Abstractions
public interface IKeyValueStore
{
    Task SetValue(string key, string value);
    Task<string> GetValue(string key);
    Task RemoveValue(string key);
}
using Xunit;
using System.Threading.Tasks;
using WalletFramework.Ledger;
using WalletFramework.Ledger.Tests.Mocks;
using WalletFramework.Ledger.Abstractions;
using WalletFramework.Ledger.Exceptions;
using Microsoft.Extensions.Logging;
using Moq; // Assuming Moq is used for mocking ILogger

namespace WalletFramework.Ledger.Tests
{
    public class LedgerServiceTests
    {
        private readonly Mock<ILedgerClient> _mockLedgerClient;
        private readonly Mock<ILogger<LedgerService>> _mockLogger;
        private readonly LedgerService _ledgerService;

        public LedgerServiceTests()
        {
            _mockLedgerClient = new Mock<ILedgerClient>();
            _mockLogger = new Mock<ILogger<LedgerService>>();
            _ledgerService = new LedgerService(_mockLedgerClient.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetDidDocumentAsync_ValidDid_ReturnsDidDocument()
        {
            // Arrange
            var testDid = "did:indy:sovrin:UV3V9Pa61J42c2f1c1c1c1c1"; // Example DID
            var expectedDidDocument = new DidDocument { Did = testDid, Verkey = "Verkey123" };
            _mockLedgerClient.Setup(client => client.GetDidDocumentAsync(testDid))
                             .ReturnsAsync(expectedDidDocument);

            // Act
            var didDocument = await _ledgerService.GetDidDocumentAsync(testDid);

            // Assert
            Assert.NotNull(didDocument);
            Assert.Equal(testDid, didDocument.Did);
            Assert.Equal(expectedDidDocument.Verkey, didDocument.Verkey);
            _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Entering GetDidDocumentAsync for DID: {testDid}")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Exiting GetDidDocumentAsync for DID: {testDid} with success")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task GetDidDocumentAsync_DidNotFound_ThrowsDidNotFoundException()
        {
            // Arrange
            var testDid = "did:indy:sovrin:NotFoundDid";
            _mockLedgerClient.Setup(client => client.GetDidDocumentAsync(testDid))
                             .ThrowsAsync(new DidNotFoundException(testDid));

            // Act & Assert
            await Assert.ThrowsAsync<DidNotFoundException>(
                () => _ledgerService.GetDidDocumentAsync(testDid));

            _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Entering GetDidDocumentAsync for DID: {testDid}")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Error in GetDidDocumentAsync for DID: {testDid}")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task GetDidDocumentAsync_LedgerClientThrowsCommunicationError_ThrowsLedgerCommunicationException()
        {
            // Arrange
            var testDid = "did:indy:sovrin:CommunicationErrorDid";
            _mockLedgerClient.Setup(client => client.GetDidDocumentAsync(testDid))
                             .ThrowsAsync(new LedgerCommunicationException("Communication error"));

            // Act & Assert
            await Assert.ThrowsAsync<LedgerCommunicationException>(
                () => _ledgerService.GetDidDocumentAsync(testDid));

             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Entering GetDidDocumentAsync for DID: {testDid}")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Error in GetDidDocumentAsync for DID: {testDid}")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
        }


        [Fact]
        public async Task WriteDidAsync_ValidDidAndVerkey_ReturnsSuccess()
        {
            // Arrange
            var testDid = "did:indy:sovrin:UV3V9Pa61J42c2f1c1c1c1c1"; // Example DID
            var testVerkey = "Verkey12345678901234567890123456789012"; // Example Verkey
            _mockLedgerClient.Setup(client => client.WriteDidAsync(testDid, testVerkey))
                             .ReturnsAsync(true);

            // Act
            var success = await _ledgerService.WriteDidAsync(testDid, testVerkey);

            // Assert
            Assert.True(success);
             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Entering WriteDidAsync for DID: {testDid}")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Exiting WriteDidAsync for DID: {testDid} with success")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task WriteDidAsync_LedgerClientThrowsError_ThrowsLedgerCommunicationException()
        {
            // Arrange
            var testDid = "did:indy:sovrin:ErrorDid";
            var testVerkey = "Verkey12345678901234567890123456789012";
            _mockLedgerClient.Setup(client => client.WriteDidAsync(testDid, testVerkey))
                             .ThrowsAsync(new LedgerCommunicationException("Write error"));

            // Act & Assert
            await Assert.ThrowsAsync<LedgerCommunicationException>(
                () => _ledgerService.WriteDidAsync(testDid, testVerkey));

             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Entering WriteDidAsync for DID: {testDid}")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Error in WriteDidAsync for DID: {testDid}")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task RegisterSchemaAsync_ValidSchema_ReturnsSchemaId()
        {
            // Arrange
            var testSchema = "{ \"name\": \"test-schema\", \"version\": \"1.0\", \"attributes\": [\"attribute1\", \"attribute2\"] }"; // Example schema
            var expectedSchemaId = "SchemaId123";
            _mockLedgerClient.Setup(client => client.RegisterSchemaAsync(testSchema))
                             .ReturnsAsync(expectedSchemaId);

            // Act
            var schemaId = await _ledgerService.RegisterSchemaAsync(testSchema);

            // Assert
            Assert.Equal(expectedSchemaId, schemaId);
             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Entering RegisterSchemaAsync")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Exiting RegisterSchemaAsync with schema ID: {expectedSchemaId}")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task RegisterSchemaAsync_LedgerClientThrowsError_ThrowsLedgerCommunicationException()
        {
            // Arrange
            var testSchema = "{ \"name\": \"test-schema\", \"version\": \"1.0\", \"attributes\": [\"attribute1\", \"attribute2\"] }";
            _mockLedgerClient.Setup(client => client.RegisterSchemaAsync(testSchema))
                             .ThrowsAsync(new LedgerCommunicationException("Schema registration error"));

            // Act & Assert
            await Assert.ThrowsAsync<LedgerCommunicationException>(
                () => _ledgerService.RegisterSchemaAsync(testSchema));

             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Entering RegisterSchemaAsync")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error in RegisterSchemaAsync")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task RegisterCredentialDefinitionAsync_ValidCredentialDefinition_ReturnsCredentialDefinitionId()
        {
            // Arrange
            var testCredentialDefinition = "{ \"schemaId\": \"SchemaIdPlaceholder\", \"type\": \"CL\", \"tag\": \"default\", \"value\": {} }"; // Example credential definition
            var expectedCredDefId = "CredDefId123";
            _mockLedgerClient.Setup(client => client.RegisterCredentialDefinitionAsync(testCredentialDefinition))
                             .ReturnsAsync(expectedCredDefId);

            // Act
            var credentialDefinitionId = await _ledgerService.RegisterCredentialDefinitionAsync(testCredentialDefinition);

            // Assert
            Assert.Equal(expectedCredDefId, credentialDefinitionId);
             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Entering RegisterCredentialDefinitionAsync")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Exiting RegisterCredentialDefinitionAsync with credential definition ID: {expectedCredDefId}")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task RegisterCredentialDefinitionAsync_LedgerClientThrowsError_ThrowsLedgerCommunicationException()
        {
            // Arrange
            var testCredentialDefinition = "{ \"schemaId\": \"SchemaIdPlaceholder\", \"type\": \"CL\", \"tag\": \"default\", \"value\": {} }";
            _mockLedgerClient.Setup(client => client.RegisterCredentialDefinitionAsync(testCredentialDefinition))
                             .ThrowsAsync(new LedgerCommunicationException("Cred def registration error"));

            // Act & Assert
            await Assert.ThrowsAsync<LedgerCommunicationException>(
                () => _ledgerService.RegisterCredentialDefinitionAsync(testCredentialDefinition));

             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Entering RegisterCredentialDefinitionAsync")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
             _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error in RegisterCredentialDefinitionAsync")),
                    It.IsAny<System.Exception>(),
                    It.Is<Func<It.IsAnyType, System.Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }
}
using WalletFramework.CredentialManagement.Models;
using WalletFramework.SecureStorage;
using System.Threading.Tasks;

namespace WalletFramework.CredentialManagement
{
    public class CredentialManager
    {
        private readonly ISecureStorageService _secureStorageService;

        public CredentialManager(ISecureStorageService secureStorageService)
        {
            _secureStorageService = secureStorageService;
        }

        public async Task StoreCredentialAsync(Credential credential)
        {
            await _secureStorageService.StoreCredentialAsync(credential);
        }

        public async Task<Credential> GetCredentialAsync(CredentialQuery query)
        {
            return await _secureStorageService.GetCredentialAsync(query);
        }

        public async Task DeleteCredentialAsync(CredentialQuery query)
        {
            await _secureStorageService.DeleteCredentialAsync(query);
        }
    }
}
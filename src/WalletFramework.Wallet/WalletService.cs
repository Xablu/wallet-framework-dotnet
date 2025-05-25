using WalletFramework.Wallet.Abstractions;

namespace WalletFramework.Wallet
{
    public class WalletService
    {
        private readonly ICredentialStore? _credentialStore;

        public WalletService(ICredentialStore credentialStore)
        {
            _credentialStore = credentialStore;
        }

        public WalletService()
        {
            _credentialStore = null;
        }

        public void AddCredential(string credentialData)
        {
            if (_credentialStore == null)
            {
                throw new InvalidOperationException("WalletService is not initialized with a credential store.");
            }
            _credentialStore.StoreCredential(credentialData);
        }
    }
}
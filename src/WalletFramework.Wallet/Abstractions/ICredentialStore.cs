namespace WalletFramework.Wallet.Abstractions
{
    public interface ICredentialStore
    {
        void StoreCredential(string credentialData);
    }
}
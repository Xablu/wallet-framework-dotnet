using System;

namespace WalletFramework.Ledger.Exceptions
{
    public class DidNotFoundException : LedgerException
    {
        public DidNotFoundException(string did) : base($"DID document not found for DID: {did}")
        {
        }

        public DidNotFoundException(string did, Exception innerException) : base($"DID document not found for DID: {did}", innerException)
        {
        }
    }
}
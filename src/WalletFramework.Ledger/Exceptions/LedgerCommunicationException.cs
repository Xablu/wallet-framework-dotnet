using System;

namespace WalletFramework.Ledger.Exceptions
{
    public class LedgerCommunicationException : LedgerException
    {
        public LedgerCommunicationException(string message) : base(message)
        {
        }

        public LedgerCommunicationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
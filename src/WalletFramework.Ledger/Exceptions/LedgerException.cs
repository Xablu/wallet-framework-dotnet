using System;

namespace WalletFramework.Ledger.Exceptions
{
    public class LedgerException : Exception
    {
        public LedgerException(string message) : base(message)
        {
        }

        public LedgerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
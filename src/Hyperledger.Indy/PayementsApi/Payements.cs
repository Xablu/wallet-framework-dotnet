using System.Threading.Tasks;

namespace Hyperledger.Indy.PaymentsApi;

/// <summary>
/// Provides methods for dealing with payments.
/// </summary>
public static class Payments
{
    /// <summary>
    /// Builds a payment request.
    /// </summary>
    /// <param name="wallet">The wallet to use.</param>
    /// <param name="submitterDid">The DID of the submitter.</param>
    /// <param name="inputs">The list of payment inputs.</param>
    /// <param name="outputs">The list of payment outputs.</param>
    /// <param name="extra">Optional extra information.</param>
    /// <returns>The payment request and payment method.</returns>
    public static Task<(string, string)> BuildPaymentRequestAsync(WalletApi.Wallet wallet, string submitterDid, string inputs, string outputs, string extra)
    {
        return Task.FromResult(("paymentRequest", "paymentMethod"));
    }
    
    // Add other payment methods as needed
}

using System;

namespace Hyperledger.Indy;

/// <summary>
/// Exception thrown when an Indy SDK call fails.
/// </summary>
public class IndyException : Exception
{
    /// <summary>
    /// Gets the error code returned by the Indy SDK.
    /// </summary>
    public int SdkErrorCode { get; }
    
    /// <summary>
    /// Initializes a new IndyException with the specified error code.
    /// </summary>
    /// <param name="sdkErrorCode">The error code returned by the Indy SDK.</param>
    public IndyException(int sdkErrorCode) : base($"An Indy SDK error occurred. Error code: {sdkErrorCode}")
    {
        SdkErrorCode = sdkErrorCode;
    }
    
    /// <summary>
    /// Initializes a new IndyException with the specified error message and error code.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="sdkErrorCode">The error code returned by the Indy SDK.</param>
    public IndyException(string message, int sdkErrorCode) : base(message)
    {
        SdkErrorCode = sdkErrorCode;
    }
}

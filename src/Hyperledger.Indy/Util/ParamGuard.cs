using System;

namespace Hyperledger.Indy.Utils;

/// <summary>
/// Provides methods for validating parameters.
/// </summary>
public static class ParamGuard
{
    /// <summary>
    /// Checks that a string parameter is not null or whitespace.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void NotNullOrWhiteSpace(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(paramName, $"The parameter {paramName} cannot be null or whitespace.");
    }
    
    // Add other parameter validation methods as needed
}

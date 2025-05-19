using System;
using System.Threading.Tasks;

namespace Hyperledger.Indy.PoolApi;

/// <summary>
/// Represents a connection to a pool of validators.
/// </summary>
public sealed class Pool : IDisposable
{
    /// <summary>
    /// Gets the SDK handle for the Pool instance.
    /// </summary>
    internal int Handle { get; }
    
    /// <summary>
    /// Initializes a new Pool instance with the specified handle.
    /// </summary>
    /// <param name="handle">The SDK handle for the pool.</param>
    private Pool(int handle)
    {
        this.Handle = handle;
    }
    
    /// <summary>
    /// Opens a connection to the pool.
    /// </summary>
    /// <param name="configName">The name of the pool configuration to use.</param>
    /// <param name="config">The configuration for opening the pool.</param>
    /// <returns>An opened Pool instance.</returns>
    public static Task<Pool> OpenPoolLedgerAsync(string configName, string config)
    {
        return Task.FromResult(new Pool(-1));
    }
    
    /// <summary>
    /// Closes the open pool and frees resources.
    /// </summary>
    public Task CloseAsync()
    {
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Disposes of resources.
    /// </summary>
    public void Dispose()
    {
    }
}

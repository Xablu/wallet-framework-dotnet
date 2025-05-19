using System.Threading.Tasks;

namespace Hyperledger.Indy.Utils;

/// <summary>
/// Tracks pending commands that are awaiting callbacks.
/// </summary>
internal static class PendingCommands
{
    /// <summary>
    /// Adds a TaskCompletionSource to the pending commands collection.
    /// </summary>
    /// <typeparam name="T">The type of the result expected by the callback.</typeparam>
    /// <param name="taskCompletionSource">The TaskCompletionSource that will be completed when the callback occurs.</param>
    /// <returns>A command handle that can be used to identify the command.</returns>
    public static int Add<T>(TaskCompletionSource<T> taskCompletionSource)
    {
        return -1; // Just a placeholder
    }
    
    /// <summary>
    /// Removes a TaskCompletionSource from the pending commands collection.
    /// </summary>
    /// <typeparam name="T">The type of the result expected by the callback.</typeparam>
    /// <param name="commandHandle">The command handle of the command to remove.</param>
    /// <returns>The TaskCompletionSource associated with the command.</returns>
    public static TaskCompletionSource<T> Remove<T>(int commandHandle)
    {
        return new TaskCompletionSource<T>();
    }
}

/// <summary>
/// Provides helper methods for working with callbacks.
/// </summary>
internal static class CallbackHelper
{
    /// <summary>
    /// A delegate for callbacks that don't return a value.
    /// </summary>
    public static readonly object NoValueCallback = null;
    
    /// <summary>
    /// A delegate for callbacks that complete a task with no value.
    /// </summary>
    public static readonly object TaskCompletingNoValueCallback = null;
    
    /// <summary>
    /// Checks the result of an Indy SDK call.
    /// </summary>
    /// <param name="result">The result code returned by the call.</param>
    /// <returns>The result code.</returns>
    public static int CheckResult(int result)
    {
        if (result != 0)
            throw new IndyException(result);
        
        return result;
    }
    
    /// <summary>
    /// Checks if a callback completed successfully and sets the result of the task if appropriate.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="taskCompletionSource">The TaskCompletionSource to set the result on.</param>
    /// <param name="err">The error code returned by the callback.</param>
    /// <returns>True if the callback completed successfully, false otherwise.</returns>
    public static bool CheckCallback<T>(TaskCompletionSource<T> taskCompletionSource, int err)
    {
        if (err != 0)
        {
            taskCompletionSource.SetException(new IndyException(err));
            return false;
        }
        
        return true;
    }
}

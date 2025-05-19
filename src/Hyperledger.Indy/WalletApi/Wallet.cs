// Decompiled with JetBrains decompiler
// Type: Hyperledger.Indy.WalletApi.Wallet
// Assembly: Hyperledger.Indy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6FF118CD-BCBF-49FA-85B8-69179B5D824D
// Assembly location: /Users/ivo/.nuget/packages/walletframework.indy.sdk/2.0.1/lib/netstandard2.0/Hyperledger.Indy.dll
// XML documentation location: /Users/ivo/.nuget/packages/walletframework.indy.sdk/2.0.1/lib/netstandard2.0/Hyperledger.Indy.xml

using Hyperledger.Indy.Utils;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Hyperledger.Indy.WalletApi;

/// <summary>
/// Represents a wallet that stores key value records and provides static methods for managing
/// wallets.
/// </summary>
public sealed class Wallet : IDisposable
{
  // private static NativeMethods.OpenWalletCompletedDelegate OpenWalletCallback = new NativeMethods.OpenWalletCompletedDelegate(Wallet.OpenWalletCallbackMethod);
  // private static NativeMethods.GenerateWalletKeyCompletedDelegate GenerateWalletKeyCallback = new NativeMethods.GenerateWalletKeyCompletedDelegate(Wallet.GenerateWalletKeyCallbackMethod);

  /// <summary>
  /// Gets the callback to use when a wallet open command has completed.
  /// </summary>
  private static void OpenWalletCallbackMethod(int xcommand_handle, int err, int wallet_handle)
  {
    // TaskCompletionSource<Wallet> taskCompletionSource = PendingCommands.Remove<Wallet>(xcommand_handle);
    // if (!CallbackHelper.CheckCallback<Wallet>(taskCompletionSource, err))
    //   return;
    // taskCompletionSource.SetResult(new Wallet(wallet_handle));
  }

  private static void GenerateWalletKeyCallbackMethod(int xcommand_handle, int err, string key)
  {
    // TaskCompletionSource<string> taskCompletionSource = PendingCommands.Remove<string>(xcommand_handle);
    // if (!CallbackHelper.CheckCallback<string>(taskCompletionSource, err))
    //   return;
    // taskCompletionSource.SetResult(key);
  }

  /// <summary>Create a new secure wallet.</summary>
  /// <returns>The wallet async.</returns>
  /// <param name="config">
  /// Wallet configuration json.
  /// <code>
  /// {
  ///   "id": string, Identifier of the wallet.
  ///         Configured storage uses this identifier to lookup exact wallet data placement.
  ///   "storage_type": optional&lt;string&gt;, Type of the wallet storage. Defaults to 'default'.
  ///                  'Default' storage type allows to store wallet data in the local file.
  ///                  Custom storage types can be registered with indy_register_wallet_storage call.
  ///   "storage_config": optional&lt;object&gt;, Storage configuration json. Storage type defines set of supported keys.
  ///                     Can be optional if storage supports default configuration.
  ///                     For 'default' storage type configuration is:
  ///   {
  ///     "path": optional&lt;string&gt;, Path to the directory with wallet files.
  ///             Defaults to $HOME/.indy_client/wallet.
  ///             Wallet will be stored in the file {path}/{id}/sqlite.db
  ///   }
  /// }
  /// </code>
  /// </param>
  /// <param name="credentials">
  /// Wallet credentials json
  /// <code>
  /// {
  ///   "key": string, Passphrase used to derive wallet master key
  ///   "storage_credentials": optional&lt;object&gt; Credentials for wallet storage. Storage type defines set of supported keys.
  ///                          Can be optional if storage supports default configuration.
  ///                          For 'default' storage type should be empty.
  /// 
  /// }
  /// </code>
  /// </param>
  public static Task CreateWalletAsync(string config, string credentials)
  {
    // ParamGuard.NotNullOrWhiteSpace(config, nameof (config));
    // ParamGuard.NotNullOrWhiteSpace(credentials, nameof (credentials));
    // TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    // CallbackHelper.CheckResult(NativeMethods.indy_create_wallet(PendingCommands.Add<bool>(taskCompletionSource), config, credentials, CallbackHelper.TaskCompletingNoValueCallback));
    // return (Task) taskCompletionSource.Task;
    
    return Task.CompletedTask;
  }

  /// <summary>
  /// Open the wallet.
  /// 
  /// Wallet must be previously created with <see cref="M:Hyperledger.Indy.WalletApi.Wallet.CreateWalletAsync(System.String,System.String)" /> method.
  /// </summary>
  /// <returns>Handle to opened wallet to use in methods that require wallet access.</returns>
  /// <param name="config">
  /// Wallet configuration json.
  /// <code>
  /// {
  ///   "id": string, Identifier of the wallet.
  ///         Configured storage uses this identifier to lookup exact wallet data placement.
  ///   "storage_type": optional&lt;string&gt;, Type of the wallet storage. Defaults to 'default'.
  ///                  'Default' storage type allows to store wallet data in the local file.
  ///                  Custom storage types can be registered with indy_register_wallet_storage call.
  ///   "storage_config": optional&lt;object&gt;, Storage configuration json. Storage type defines set of supported keys.
  ///                     Can be optional if storage supports default configuration.
  ///                     For 'default' storage type configuration is:
  ///   {
  ///     "path": optional&lt;string&gt;, Path to the directory with wallet files.
  ///             Defaults to $HOME/.indy_client/wallet.
  ///             Wallet will be stored in the file {path}/{id}/sqlite.db
  ///   }
  /// }
  /// </code>
  /// </param>
  /// <param name="credentials">
  /// Wallet credentials json
  ///   {
  ///       "key": string, Passphrase used to derive current wallet master key
  ///       "rekey": optional&lt;string&gt;, If present than wallet master key will be rotated to a new one
  ///                                  derived from this passphrase.
  ///       "storage_credentials": optional&lt;object&gt; Credentials for wallet storage. Storage type defines set of supported keys.
  ///                              Can be optional if storage supports default configuration.
  ///                              For 'default' storage type should be empty.
  /// 
  ///   }
  /// </param>
  public static Task<Wallet> OpenWalletAsync(string config, string credentials)
  {
    // ParamGuard.NotNullOrWhiteSpace(config, nameof (config));
    // ParamGuard.NotNullOrWhiteSpace(credentials, nameof (credentials));
    // TaskCompletionSource<Wallet> taskCompletionSource = new TaskCompletionSource<Wallet>();
    // CallbackHelper.CheckResult(NativeMethods.indy_open_wallet(PendingCommands.Add<Wallet>(taskCompletionSource), config, credentials, Wallet.OpenWalletCallback));
    // return taskCompletionSource.Task;
    return Task.FromResult(new Wallet(-1));
  }

  /// <summary>
  /// Exports opened wallet
  /// 
  /// Note this endpoint is EXPERIMENTAL. Function signature and behaviour may change
  /// in the future releases.
  /// </summary>
  /// <returns>The async.</returns>
  /// <param name="exportConfig">
  /// <code>
  /// JSON containing settings for input operation.
  ///   {
  ///     "path": &lt;string&gt;, Path of the file that contains exported wallet content
  ///     "key": &lt;string&gt;, Passphrase used to derive export key
  ///   }
  /// </code>
  /// </param>
  public Task ExportAsync(string exportConfig)
  {
    // ParamGuard.NotNullOrWhiteSpace(exportConfig, nameof (exportConfig));
    // TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    // CallbackHelper.CheckResult(NativeMethods.indy_export_wallet(PendingCommands.Add<bool>(taskCompletionSource), this.Handle, exportConfig, CallbackHelper.TaskCompletingNoValueCallback));
    // return (Task) taskCompletionSource.Task

    return Task.CompletedTask;
  }

  /// <summary>
  /// Creates a new secure wallet and then imports its content
  /// according to fields provided in import_config
  /// This can be seen as an <see cref="M:Hyperledger.Indy.WalletApi.Wallet.CreateWalletAsync(System.String,System.String)" /> call with additional content import
  /// 
  /// Note this endpoint is EXPERIMENTAL. Function signature and behaviour may change
  /// in the future releases.
  /// </summary>
  /// <returns>The async.</returns>
  /// <param name="config">
  /// Wallet configuration json.
  /// <code>
  /// {
  ///   "id": string, Identifier of the wallet.
  ///         Configured storage uses this identifier to lookup exact wallet data placement.
  ///   "storage_type": optional&lt;string&gt;, Type of the wallet storage. Defaults to 'default'.
  ///                  'Default' storage type allows to store wallet data in the local file.
  ///                  Custom storage types can be registered with indy_register_wallet_storage call.
  ///   "storage_config": optional&lt;object&gt;, Storage configuration json. Storage type defines set of supported keys.
  ///                     Can be optional if storage supports default configuration.
  ///                     For 'default' storage type configuration is:
  ///   {
  ///     "path": optional&lt;string&gt;, Path to the directory with wallet files.
  ///             Defaults to $HOME/.indy_client/wallet.
  ///             Wallet will be stored in the file {path}/{id}/sqlite.db
  ///   }
  /// }
  /// </code>
  /// </param>
  /// <param name="credentials">Wallet credentials json
  /// <code>
  /// {
  ///   "key": string, Passphrase used to derive wallet master key
  ///   "storage_credentials": optional&lt;object&gt; Credentials for wallet storage. Storage type defines set of supported keys.
  ///                          Can be optional if storage supports default configuration.
  ///                          For 'default' storage type should be empty.
  /// 
  /// }
  /// </code>
  /// </param>
  /// <param name="importConfig">
  /// Import settings json.
  /// <code>
  /// {
  ///   "path": &lt;string&gt;, path of the file that contains exported wallet content
  ///   "key": &lt;string&gt;, passphrase used to derive export key
  /// }
  /// </code>
  /// </param>
  public static Task ImportAsync(string config, string credentials, string importConfig)
  {
    // ParamGuard.NotNullOrWhiteSpace(config, nameof (config));
    // ParamGuard.NotNullOrWhiteSpace(credentials, nameof (credentials));
    // ParamGuard.NotNullOrWhiteSpace(importConfig, nameof (importConfig));
    // TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    // CallbackHelper.CheckResult(NativeMethods.indy_import_wallet(PendingCommands.Add<bool>(taskCompletionSource), config, credentials, importConfig, CallbackHelper.TaskCompletingNoValueCallback));
    // return (Task) taskCompletionSource.Task;
    return Task.CompletedTask;
  }

  /// <summary>Deletes a wallet.</summary>
  /// <remarks>
  /// <para>Deletes a wallet created earlier using the <see cref="M:Hyperledger.Indy.WalletApi.Wallet.CreateWalletAsync(System.String,System.String)" />
  /// by name.
  /// </para>
  /// <para>The <paramref name="credentials" /> parameter is unused in the default wallet at present,
  /// however the value can be used by custom wallet implementations; it is up to the custom wallet
  /// type implementer to interpret the value.
  /// </para>
  /// </remarks>
  /// <param name="config">The name of the wallet to delete.</param>
  /// <param name="credentials">The wallet credentials.</param>
  /// <returns>An asynchronous <see cref="T:System.Threading.Tasks.Task" /> with no return value that completes when the operation completes.</returns>
  public static Task DeleteWalletAsync(string config, string credentials)
  {
    // ParamGuard.NotNullOrWhiteSpace(config, nameof (config));
    // ParamGuard.NotNullOrWhiteSpace(credentials, nameof (credentials));
    // TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    // CallbackHelper.CheckResult(NativeMethods.indy_delete_wallet(PendingCommands.Add<bool>(taskCompletionSource), config, credentials, CallbackHelper.TaskCompletingNoValueCallback));
    // return (Task) taskCompletionSource.Task;
    return Task.CompletedTask;
  }

  /// <summary>
  /// Generate wallet master key.
  /// Returned key is compatible with "RAW" key derivation method.
  /// It allows to avoid expensive key derivation for use cases when wallet keys can be stored in a secure enclave.
  /// </summary>
  /// <returns>The generated wallet key.</returns>
  /// <param name="config">
  /// config: (optional) key configuration json.
  /// {
  ///   "seed": optional&lt;string&gt; Seed that allows deterministic key creation (if not set random one will be used).
  /// }</param>
  public static Task<string> GenerateWalletKeyAsync(string config)
  {
    // TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
    // CallbackHelper.CheckResult(NativeMethods.indy_generate_wallet_key(PendingCommands.Add<string>(taskCompletionSource), config, Wallet.GenerateWalletKeyCallback));
    // return taskCompletionSource.Task;
    return Task.FromResult("");
  }

  /// <summary>Status indicating whether or not the wallet is open.</summary>
  public bool IsOpen { get; private set; }

  /// <summary>Gets the SDK handle for the Wallet instance.</summary>
  internal int Handle { get; }

  /// <summary>
  /// Initializes a new Wallet instance with the specified handle.
  /// </summary>
  /// <param name="handle">The SDK handle for the wallet.</param>
  private Wallet(int handle)
  {
    this.Handle = handle;
    this.IsOpen = true;
  }

  /// <summary>Closes the wallet.</summary>
  /// <returns>An asynchronous <see cref="T:System.Threading.Tasks.Task" /> with no return value that completes when the operation completes.</returns>
  public Task CloseAsync()
  {
    // this.IsOpen = false;
    // TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    // CallbackHelper.CheckResult(NativeMethods.indy_close_wallet(PendingCommands.Add<bool>(taskCompletionSource), this.Handle, CallbackHelper.TaskCompletingNoValueCallback));
    // GC.SuppressFinalize((object) this);
    // return (Task) taskCompletionSource.Task;
    return Task.CompletedTask;
  }

  /// <summary>Disposes of resources.</summary>
  public async void Dispose()
  {
    if (!this.IsOpen)
      return;
    await this.CloseAsync();
  }

  /// <summary>
  /// Finalizes the resource during GC if it hasn't been already.
  /// </summary>
  ~Wallet()
  {
    if (!this.IsOpen)
      return;
    //NativeMethods.indy_close_wallet(-1, this.Handle, CallbackHelper.NoValueCallback);
  }
}

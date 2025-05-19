namespace Hyperledger.Indy;

/// <summary>
/// Provides access to the native methods in the Indy SDK.
/// </summary>
internal static class NativeMethods
{
    // Define delegates for callbacks
    internal delegate void OpenWalletCompletedDelegate(int xcommand_handle, int err, int wallet_handle);
    internal delegate void GenerateWalletKeyCompletedDelegate(int xcommand_handle, int err, string key);
    
    // Define placeholders for native methods
    internal static int indy_create_wallet(int command_handle, string config, string credentials, object cb)
    {
        return 0;
    }
    
    internal static int indy_open_wallet(int command_handle, string config, string credentials, OpenWalletCompletedDelegate cb)
    {
        return 0;
    }
    
    internal static int indy_close_wallet(int command_handle, int wallet_handle, object cb)
    {
        return 0;
    }
    
    internal static int indy_delete_wallet(int command_handle, string config, string credentials, object cb)
    {
        return 0;
    }
    
    internal static int indy_export_wallet(int command_handle, int wallet_handle, string export_config, object cb)
    {
        return 0;
    }
    
    internal static int indy_import_wallet(int command_handle, string config, string credentials, string import_config, object cb)
    {
        return 0;
    }
    
    internal static int indy_generate_wallet_key(int command_handle, string config, GenerateWalletKeyCompletedDelegate cb)
    {
        return 0;
    }
    
    // Add other native methods as needed
}

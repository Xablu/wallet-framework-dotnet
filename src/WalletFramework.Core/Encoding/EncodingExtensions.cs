using System.Text;

namespace WalletFramework.Core.Encoding
{
    public static class EncodingExtensions
    {
        public static byte[] GetBytesUtf8(this string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        public static string GetStringUtf8(this byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
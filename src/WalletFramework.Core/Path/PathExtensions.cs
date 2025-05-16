using System.IO;

namespace WalletFramework.Core.Path
{
    public static class PathExtensions
    {
        public static string CombinePath(this string path1, string path2)
        {
            return System.IO.Path.Combine(path1, path2);
        }
    }
}
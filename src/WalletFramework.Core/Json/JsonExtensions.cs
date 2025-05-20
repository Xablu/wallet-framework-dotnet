using System.Text.Json;

namespace WalletFramework.Core.Json
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public static T? FromJson<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
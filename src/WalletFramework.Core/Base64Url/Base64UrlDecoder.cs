using System;

namespace WalletFramework.Core.Base64Url
{
    public static class Base64UrlDecoder
    {
        public static byte[] Decode(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            // Replace URL unsafe characters
            input = input.Replace('-', '+');
            input = input.Replace('_', '/');

            // Add padding characters if necessary
            while (input.Length % 4 != 0)
            {
                input += "=";
            }

            return Convert.FromBase64String(input);
        }
    }
}
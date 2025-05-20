using System;

namespace WalletFramework.Core.Base64Url
{
    public static class Base64UrlEncoder
    {
        public static string Encode(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var base64 = Convert.ToBase64String(input);

            // Replace URL unsafe characters
            base64 = base64.Replace('+', '-');
            base64 = base64.Replace('/', '_');

            // Remove padding characters
            base64 = base64.TrimEnd('=');

            return base64;
        }
    }
}
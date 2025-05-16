using System;
using System.Collections.Generic;
using System.Web; // Requires System.Web assembly reference

namespace WalletFramework.Core.Uri
{
    public static class UriExtensions
    {
        public static System.Uri ToUri(this string uriString)
        {
            if (string.IsNullOrWhiteSpace(uriString))
            {
                throw new ArgumentException("URI string cannot be null or whitespace.", nameof(uriString));
            }

            try
            {
                return new System.Uri(uriString);
            }
            catch (UriFormatException ex)
            {
                throw new UriFormatException($"Invalid URI format: {uriString}", ex);
            }
        }

        public static Dictionary<string, string> GetQueryParameters(this System.Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var queryParameters = new Dictionary<string, string>();
            var query = uri.Query;

            if (!string.IsNullOrEmpty(query))
            {
                // Remove the leading '?'
                query = query.Substring(1);

                var pairs = query.Split('&');
                foreach (var pair in pairs)
                {
                    var parts = pair.Split('=');
                    if (parts.Length == 2)
                    {
                        var key = HttpUtility.UrlDecode(parts[0]);
                        var value = HttpUtility.UrlDecode(parts[1]);
                        queryParameters[key] = value;
                    }
                    else if (parts.Length == 1 && !string.IsNullOrEmpty(parts[0]))
                    {
                        // Handle parameters without a value (e.g., "?flag")
                        var key = HttpUtility.UrlDecode(parts[0]);
                        queryParameters[key] = string.Empty;
                    }
                }
            }

            return queryParameters;
        }
    }
}
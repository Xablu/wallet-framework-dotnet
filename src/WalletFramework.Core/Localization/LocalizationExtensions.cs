using System;
using System.Globalization;

namespace WalletFramework.Core.Localization
{
    public static class LocalizationExtensions
    {
        public static CultureInfo ToCultureInfo(this string cultureCode)
        {
            if (string.IsNullOrWhiteSpace(cultureCode))
            {
                throw new ArgumentException("Culture code cannot be null or whitespace.", nameof(cultureCode));
            }

            try
            {
                return new CultureInfo(cultureCode);
            }
            catch (CultureNotFoundException ex)
            {
                throw new CultureNotFoundException($"Invalid culture code: {cultureCode}", nameof(cultureCode), ex);
            }
        }
    }
}
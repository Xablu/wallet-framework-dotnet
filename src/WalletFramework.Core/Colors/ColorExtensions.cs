using System;
using System.Drawing;

namespace WalletFramework.Core.Colors
{
    public static class ColorExtensions
    {
        public static Color FromHex(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
            {
                throw new ArgumentException("Hex string cannot be null or whitespace.", nameof(hex));
            }

            hex = hex.TrimStart('#');

            if (hex.Length != 6)
            {
                throw new ArgumentException("Hex string must be 6 characters long (excluding optional #).", nameof(hex));
            }

            try
            {
                int r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                int g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                int b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

                // Assuming alpha is always 255 for hex color parsing
                return System.Drawing.Color.FromArgb(255, r, g, b);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Invalid hex color format.", nameof(hex), ex);
            }
        }
    }
}
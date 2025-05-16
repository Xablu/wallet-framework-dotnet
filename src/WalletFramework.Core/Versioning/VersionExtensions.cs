using System;

namespace WalletFramework.Core.Versioning
{
    public static class VersionExtensions
    {
        public static Version ToVersion(this string versionString)
        {
            if (string.IsNullOrWhiteSpace(versionString))
            {
                throw new ArgumentException("Version string cannot be null or whitespace.", nameof(versionString));
            }

            try
            {
                return new Version(versionString);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Invalid version string format: {versionString}", nameof(versionString), ex);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException($"Invalid version string format: {versionString}", nameof(versionString), ex);
            }
            catch (OverflowException ex)
            {
                throw new ArgumentException($"Version string value is too large: {versionString}", nameof(versionString), ex);
            }
        }
    }
}
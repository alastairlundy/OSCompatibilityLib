using System;
using AlastairLundy.Extensions.System.VersionExtensions;

namespace PlatformKit.Extensions.OperatingSystem
{
    internal static class IsWindowsVersionAtLeastExtension
    {
        /// <summary>
        /// Checks to see whether the specified version of Windows is the same or newer than the installed version of Windows.
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="build"></param>
        /// <param name="revision"></param>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        internal static bool IsWindowsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            if (IsWindowsExtension.IsWindows())
            {
                return GetSystemExtension.GetSystem(PlatformID.Win32NT).Version.IsAtLeast(new Version(major, minor, build, revision));
            }

            throw new PlatformNotSupportedException();
        }
    }
}
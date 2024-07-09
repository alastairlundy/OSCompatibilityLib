using System;
using AlastairLundy.Extensions.System.VersionExtensions;
// ReSharper disable InconsistentNaming

namespace PlatformKit.Extensions.OperatingSystem
{
    internal class IsVersionAtLeastExtensions
    {
        
        internal static bool IsWindowsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            if (IsOperatingSystemExtensions.IsWindows())
            {
                return GetSystemExtension.GetSystem(PlatformID.Win32NT).Version.IsAtLeast(new Version(major, minor, build, revision));
            }

            throw new PlatformNotSupportedException();
        }
        
        internal static bool IsMacOSVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            if (IsOperatingSystemExtensions.IsMacOS())
            {
                return GetSystemExtension.GetSystem(PlatformID.MacOSX).Version.IsAtLeast(new Version(major, minor, build, revision));
            }

            throw new PlatformNotSupportedException();
        }
        
        internal static bool IsLinuxVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            if (IsOperatingSystemExtensions.IsLinux())
            {
                return GetSystemExtension.GetSystem(PlatformID.Unix).Version.IsAtLeast(new Version(major, minor, build, revision));
            }

            throw new PlatformNotSupportedException();
        }
        
        internal static bool IsFreeBSDVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            if (IsOperatingSystemExtensions.IsFreeBSD())
            {
                return GetSystemExtension.GetSystem(PlatformID.Unix).Version.IsAtLeast(new Version(major, minor, build, revision));
            }

            throw new PlatformNotSupportedException();
        }
    }
}
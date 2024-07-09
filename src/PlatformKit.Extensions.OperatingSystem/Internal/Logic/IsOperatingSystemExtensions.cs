using System;
using System.Reflection;
using System.Runtime.InteropServices;

using AlastairLundy.Extensions.System.VersionExtensions;

// ReSharper disable InconsistentNaming

namespace PlatformKit.Extensions.OperatingSystem
{
    internal static class IsOperatingSystemExtensions
    {
        internal static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
        
        internal static bool IsMacOS()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }
        
        internal static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }
        
        public static bool IsFreeBSD()
        {
            return System.Runtime.InteropServices.RuntimeInformation.OSDescription.ToLower().Contains("freebsd");
        }
    }
}
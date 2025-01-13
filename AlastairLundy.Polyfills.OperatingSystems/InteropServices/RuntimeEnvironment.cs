using System;
using System.IO;
using System.Reflection;

using AlastairLundy.Polyfills.OperatingSystems.Helpers;

namespace AlastairLundy.Polyfills.OperatingSystems.InteropServices
{
    public static class RuntimeEnvironment
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool FromGlobalAccessCache(Assembly a)
        {
            try
            {
                return a.GlobalAssemblyCache;
            }
            catch
            {
                return false;
            }
        }

#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("macos")]
        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("freebsd")]
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        [UnsupportedOSPlatform("watchos")]
        [UnsupportedOSPlatform("browser")]
#endif
        public static string GetRuntimeDirectory()
        {
            if (RuntimeInformation.FrameworkDescription.ToLower().Contains("core") || 
                RuntimeInformation.FrameworkDescription.ToLower().Contains("framework") == false)
            {
                if (OperatingSystem.IsIOS() || OperatingSystem.IsTvOS() || OperatingSystem.IsWatchOS() || OperatingSystem.IsAndroid())
                {
                    throw new PlatformNotSupportedException("Operation not supported on Android platform");
                }
                else
                {
                    if (OperatingSystem.IsWindows())
                    {
                        return ProcessRunner.RunProcess(ProcessRunner.CreateProcess($"{Environment.SystemDirectory}cmd.exe", "where dotnet"));
                    }
                    else
                    {
                        return ProcessRunner.RunProcess(ProcessRunner.CreateProcess($"{Environment.SystemDirectory}which", "dotnet"));
                    }
                }
            }
            else
            {
                string clrLocation32Bit = Path.Combine(Environment.SystemDirectory, $"Microsoft.NET{Path.DirectorySeparatorChar}Framework");
                string clrLocation64Bit = Path.Combine(Environment.SystemDirectory, $"Microsoft.NET{Path.DirectorySeparatorChar}Framework64");
                
                if (Environment.Is64BitProcess)
                {
                    return clrLocation64Bit;
                }
                else
                {
                    return clrLocation32Bit;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Version GetSystemVersion()
        {
            return Environment.Version;
        }
        
    }
}
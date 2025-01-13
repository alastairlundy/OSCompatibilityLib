/*
        MIT License

       Copyright (c) 2024 Alastair Lundy

       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:

       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.

       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */

using System;
using System.IO;
using System.Reflection;

#if NET5_0_OR_GREATER
using System.Runtime.Versioning;
#endif

using AlastairLundy.OSCompatibilityLib.Helpers;

namespace AlastairLundy.OSCompatibilityLib.Polyfills.InteropServices
{
    /// <summary>
    /// 
    /// </summary>
    public static class RuntimeEnvironment
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [Obsolete("Obsolete")]
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
                if (OperatingSystem.IsIOS() || OperatingSystem.IsTvOS() || OperatingSystem.IsWatchOS() ||
                    OperatingSystem.IsAndroid())
                {
                    throw new PlatformNotSupportedException("Operation not supported on Android platform");
                }
                else
                {
                    if (OperatingSystem.IsWindows())
                    {
                        return ProcessRunner.RunProcess(
                            ProcessRunner.CreateProcess($"{Environment.SystemDirectory}cmd.exe", "where dotnet"));
                    }
                    else
                    {
                        return ProcessRunner.RunProcess(
                            ProcessRunner.CreateProcess($"{Environment.SystemDirectory}which", "dotnet"));
                    }
                }
            }
            else
            {
                string clrLocation32Bit = Path.Combine(Environment.SystemDirectory,
                    $"Microsoft.NET{Path.DirectorySeparatorChar}Framework");
                string clrLocation64Bit = Path.Combine(Environment.SystemDirectory,
                    $"Microsoft.NET{Path.DirectorySeparatorChar}Framework64");

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
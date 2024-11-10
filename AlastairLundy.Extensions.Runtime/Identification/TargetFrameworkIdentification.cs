/*
        MIT License
       
       Copyright (c) 2020-2024 Alastair Lundy
       
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
using System.Runtime.InteropServices;
using System.Text;

using AlastairLundy.Extensions.System;

using AlastairLundy.Extensions.System.Strings.Versioning;

// ReSharper disable MemberCanBePrivate.Global

#if NETSTANDARD2_0 || NETSTANDARD2_1
using OperatingSystem = AlastairLundy.Extensions.Runtime.OperatingSystemExtensions;
#endif

namespace AlastairLundy.Extensions.Runtime.Identification {
        
/// <summary>
/// A class to manage Target Framework detection
/// </summary>
public static class TargetFrameworkIdentification
{
    /// <summary>
    /// Generates a .NET (5+) generic TFM.
    /// </summary>
    /// <returns>the .NET (5+) generic TFM.</returns>
    // ReSharper disable once InconsistentNaming
    private static string GetNetTFM()
    {
        Version frameworkVersion = GetFrameworkVersion();
        StringBuilder stringBuilder = new StringBuilder();
        
        stringBuilder.Append("net");
        stringBuilder.Append(frameworkVersion.Major);
        stringBuilder.Append('.');
        stringBuilder.Append(frameworkVersion.Minor);
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Generates the .NET (5+/Core 3.1) operating system specificTFM.
    /// </summary>
    /// <param name="targetFrameworkMonikerType"></param>
    /// <returns>the .NET (5+ or Core 3.1) operating system specific TFM.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown if run on an unsupported platform.</exception>
    // ReSharper disable once InconsistentNaming
    private static string GetOsSpecificNetTFM(TargetFrameworkMonikerType targetFrameworkMonikerType)
    {
#if NET6_0_OR_GREATER
        Version frameworkVersion = GetFrameworkVersion();
#endif
        
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(GetNetTFM());
        
        if (OperatingSystem.IsMacOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append("macos");

            if (targetFrameworkMonikerType == TargetFrameworkMonikerType.OperatingSystemVersionSpecific)
            {
                stringBuilder.Append('.');
                stringBuilder.Append(RuntimeIdentification.GetOsVersionString());
            }
        }
#if NET5_0_OR_GREATER
        else if (OperatingSystem.IsMacCatalyst())
        {
            stringBuilder.Append('-');
            stringBuilder.Append("maccatalyst");
        }
#endif
        else if (OperatingSystem.IsWindows())
        {
            stringBuilder.Append('-');
            stringBuilder.Append("windows");

            if (targetFrameworkMonikerType == TargetFrameworkMonikerType.OperatingSystemVersionSpecific)
            {
                bool isAtLeastWin8 = OperatingSystem.IsWindowsVersionAtLeast(6,2, 9200);
                bool isAtLeastWin8Point1 = OperatingSystem.IsWindowsVersionAtLeast(6, 3, 9600);

                bool isAtLeastWin10V1607 = OperatingSystem.IsWindowsVersionAtLeast(10, 0, 14393);
                
                if (isAtLeastWin8 || isAtLeastWin8Point1)
                {
                    stringBuilder.Append(RuntimeIdentification.GetOsVersionString());
                }
                else if (isAtLeastWin10V1607)
                {
                    stringBuilder.Append(OperatingSystemExtensions.Version);
                }
                else
                {
                    throw new PlatformNotSupportedException();
                }
            }
        }
        else if (OperatingSystem.IsAndroid())
        {
            stringBuilder.Append('-');
            stringBuilder.Append("android");
        }
        else if (OperatingSystem.IsIOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append("ios");
        }
        else if (OperatingSystem.IsTvOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append("tvos");
        }
        else if (OperatingSystem.IsWatchOS())
        {
            stringBuilder.Append('-');
            stringBuilder.Append("watchos");
        }
        else if (OperatingSystemExtensions.IsTizen())
        {
            stringBuilder.Append('-');
            stringBuilder.Append("tizen");
        }
#if NET8_0_OR_GREATER
        if (frameworkVersion.Major >= 8)
        {
            if (OperatingSystem.IsBrowser())
            {
                 stringBuilder.Append('-');
                 stringBuilder.Append("browser");
            }
        }
#endif
        return stringBuilder.ToString();
    }

    // ReSharper disable once InconsistentNaming
        private static string GetNetCoreTFM()
        {
            Version frameworkVersion = GetFrameworkVersion();
            
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("netcoreapp");

            stringBuilder.Append(frameworkVersion.Major);
            stringBuilder.Append('.');
            stringBuilder.Append(frameworkVersion.Minor);

            return stringBuilder.ToString();
        }

        // ReSharper disable once InconsistentNaming
        private static string GetNetFrameworkTFM()
        {
            Version frameworkVersion = GetFrameworkVersion();
            
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("net");
            stringBuilder.Append(frameworkVersion.Major);
            stringBuilder.Append(frameworkVersion.Minor);
                                                    
            if (frameworkVersion.Build != 0)
            {
                stringBuilder.Append(frameworkVersion.Build);
            }

            return stringBuilder.ToString();
        }

        // ReSharper disable once InconsistentNaming
        private static string GetMonoTFM()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("mono");
            
            if (OperatingSystem.IsAndroid())
            {
                stringBuilder.Append('-');
                stringBuilder.Append("android");
            }
            else if(OperatingSystem.IsIOS())
            {
                stringBuilder.Append('-');
                stringBuilder.Append("ios");
            }
            
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets the type of Target Framework that is currently running.
        /// </summary>
        /// <returns>the type of Target Framework that is currently running.</returns>
        public static TargetFrameworkType GetFrameworkType()
        {
            string frameworkDescription = RuntimeInformation.FrameworkDescription.ToLower();
            
            Version frameworkVersion = GetFrameworkVersion();
            
            if (frameworkDescription.Contains("mono"))
            {
                return TargetFrameworkType.Mono;
            }
            else if(frameworkDescription.Contains("framework") ||
                    (frameworkVersion.IsOlderThan(new Version(5,0,0)) 
                     && frameworkDescription.Contains("mono") == false
                    && frameworkDescription.Contains("core") == false)){
                return TargetFrameworkType.DotNetFramework;
            }
            else if (frameworkDescription.Contains("core"))
            {
                return TargetFrameworkType.DotNetCore;
            }
            else
            {
                return TargetFrameworkType.DotNet;
            }
        }
        
        /// <summary>
        /// Gets the Target Framework type and version.
        /// </summary>
        /// <returns>the target framework type and version as a tuple.</returns>
        public static (TargetFrameworkType frameworkType, Version frameworkVersion) GetFrameworkInformation()
        {
            return (GetFrameworkType(), GetFrameworkVersion());
        }
        
        /// <summary>
        /// Gets the version of the framework being used.
        /// </summary>
        /// <returns>the version of the framework being used.</returns>
        public static Version GetFrameworkVersion()
        {
            string frameworkDescription = RuntimeInformation.FrameworkDescription.ToLower();
            
            string versionString = frameworkDescription
                .Replace(".net", string.Empty)
                .Replace("core", string.Empty)
                .Replace("framework", string.Empty)
                .Replace("mono", string.Empty)
                .Replace("xamarin", string.Empty)
                .Replace(" ", string.Empty).AddMissingZeroes(numberOfZeroesNeeded: 3);
            
            return Version.Parse(versionString);
        }
        
        /// <summary>
        /// Detect the Target Framework Moniker (TFM) of the currently running system.
        /// Note: This does not detect .NET Standard TFMs, UWP TFMs, Windows Phone TFMs, Silverlight TFMs, and Windows Store TFMs.
        ///
        /// </summary>
        /// <param name="targetFrameworkType">The type of TFM to generate.</param>
        /// <returns>the target framework moniker of the currently running system.</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown if run on an unsupported platform.</exception>
        public static string GetTargetFrameworkMoniker(TargetFrameworkMonikerType targetFrameworkType)
        {
            TargetFrameworkType frameworkType = GetFrameworkType();
            
            if (frameworkType == TargetFrameworkType.DotNetCore)
            {
                return GetNetCoreTFM();
            }
            else if (frameworkType == TargetFrameworkType.Mono)
            {
                return GetMonoTFM();
            }
            else if (frameworkType == TargetFrameworkType.DotNetFramework)
            {
                return GetNetFrameworkTFM();
            }
            else if (frameworkType == TargetFrameworkType.DotNet)
            {
                if(targetFrameworkType == TargetFrameworkMonikerType.OperatingSystemSpecific || targetFrameworkType == TargetFrameworkMonikerType.OperatingSystemVersionSpecific)
                {
                    return GetOsSpecificNetTFM(targetFrameworkType);
                }
                else
                {
                    return GetNetTFM();
                }
            }

            throw new PlatformNotSupportedException();
        }
    }
}
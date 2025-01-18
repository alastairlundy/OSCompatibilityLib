﻿/*
        MIT License

       Copyright (c) 2024-2025 Alastair Lundy

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
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

using AlastairLundy.OSCompatibilityLib.Helpers;

#if NET5_0_OR_GREATER
using System.Runtime.Versioning;
#endif

using AlastairLundy.OSCompatibilityLib.Internal.Localizations;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace AlastairLundy.OSCompatibilityLib.Polyfills
{
    public sealed class OperatingSystem : ICloneable, ISerializable
    {
        /// <summary>
        /// Gets a PlatformID enumeration value that identifies the operating system platform.
        /// </summary>
        public PlatformID Platform { get; private set; }

        /// <summary>
        /// Gets a Version object that identifies the operating system.
        /// </summary>
        // ReSharper disable once RedundantNameQualifier
        public System.Version Version { get; private set; }

        /// <summary>
        /// Gets the concatenated string representation of the platform identifier, version, and service pack that are currently installed on the operating system.
        /// </summary>
        public string VersionString { get; private set; }

        /// <summary>
        /// Gets the concatenated string representation of the platform identifier, version, and service pack that are currently installed on the operating system.
        /// </summary>
        public string ServicePack { get; private set; }

        /// <summary>
        /// Represents information about an operating system, such as the version and platform identifier. This class cannot be inherited.
        /// </summary>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("macos")]
        [SupportedOSPlatform("maccatalyst")]
        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("ios")]
        [SupportedOSPlatform("android")]
        [SupportedOSPlatform("tvos")]
        [SupportedOSPlatform("watchos")]
#endif
        public OperatingSystem()
        {
            if (IsWindows())
            {
                Version = GetWindowsVersion();
            }
            else if (IsMacOS())
            {
                Version = GetMacOSVersion();
            }
            else if (IsLinux())
            {
#pragma warning disable CA1416
                Version = GetLinuxVersion();
#pragma warning restore CA1416
            }
            else if (IsFreeBSD())
            {
                Version = GetFreeBSDVersion();
            }
            else if (IsMacCatalyst())
            {
                Version = IsMacOS() ? GetMacOSVersion() : GetFallbackOsVersion();
            }
            else if (IsAndroid())
            {
                Version = GetAndroidVersion();
            }
            else if (IsIOS() || IsWatchOS() || IsTvOS())
            {
                Version = GetFallbackOsVersion();
            }
            else
            {
                Version = GetFallbackOsVersion();

                ServicePack = Environment.OSVersion.ServicePack;
                VersionString = Environment.OSVersion.VersionString;
            }

            ServicePack = Environment.OSVersion.ServicePack;
            VersionString = Environment.OSVersion.VersionString;
        }

#if NET5_0_OR_GREATER
        [SupportedOSPlatform("linux")]        
#endif
        private Version GetLinuxVersion()
        {
            if (IsLinux())
            {
                string versionString = GetOsReleasePropertyValue("VERSION=");
                versionString = versionString.Replace("LTS", string.Empty);

                return Version.Parse(versionString);
            }

            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_LinuxOnly);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("linux")]
#endif
        internal string GetOsReleasePropertyValue(string propertyName)
        {
            if (IsLinux() == false)
            {
                throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_LinuxOnly);
            }

            string output = string.Empty;

            string[] osReleaseInfo = File.ReadAllLines("/etc/os-release");

            foreach (string s in osReleaseInfo)
            {
                if (s.ToUpper().StartsWith(propertyName))
                {
                    output = s.Replace(propertyName, string.Empty);
                }
            }

            return output;
        }


        internal static Version GetFallbackOsVersion()
        {
            return Environment.OSVersion.Version;
        }

        /// <summary>
        /// Initializes a new instance of the OperatingSystem class, using the specified platform identifier value and version object.
        /// </summary>
        /// <param name="platform">One of the PlatformID values that indicates the operating system platform.</param>
        /// <param name="version">A Version object that indicates the version of the operating system.</param>
        ///
        /// <exception cref="ArgumentException">Platform is not a PlatformID enumeration value.</exception>
        /// <exception cref="NullReferenceException">Version is null.</exception>
        public OperatingSystem(PlatformID platform, Version version)
        {
            if (version == null)
            {
                throw new NullReferenceException();
            }

            if (platform != PlatformID.Win32NT
                && platform != PlatformID.Win32Windows
                && platform != PlatformID.Win32S
                && platform != PlatformID.WinCE
                && platform != PlatformID.Xbox
                && platform != PlatformID.MacOSX
                && platform != PlatformID.Unix)
            {
                throw new ArgumentException($"Platform {platform.ToString()} is not a PlatformID enumeration value.");
            }

            Version = version;
            Platform = platform;

            // ServicePack =version.
            VersionString = ToString();
        }

        /// <summary>
        /// Populates a SerializationInfo object with the data necessary to deserialize this instance.
        /// </summary>
        /// <param name="info">The object to populate with serialization information.</param>
        /// <param name="context">The place to store and retrieve serialized data. Reserved for future use.</param>
        /// <exception cref="ArgumentNullException">Thrown if info is null.</exception>
        /// <remarks>The context parameter is reserved for future use; it is currently not implemented in the GetObjectData method.
        /// For more information, see the SerializationInfo.AddValue method.
        /// </remarks>
        [Obsolete(
            "This API supports obsolete formatter-based serialization. It should not be called or extended by application code.")]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("Platform", Platform);
            info.AddValue("Version", Version);
            info.AddValue("ServicePack", ServicePack);
            info.AddValue("VersionString", VersionString);
        }

        /// <summary>
        /// Creates an OperatingSystem object that is identical to this instance.
        /// </summary>
        /// <returns>An OperatingSystem object that is a copy of this instance.</returns>
        public object Clone()
        {
            return new OperatingSystem(Platform, Version);
        }

        private static Version GetWindowsVersion()
        {
            return Version.Parse(RuntimeInformation.OSDescription
                .Replace("Microsoft Windows", string.Empty)
                .Replace(" ", string.Empty));
        }

        private static Version GetMacOSVersion()
        {
            string versionString = ProcessRunner.RunProcess(
                    ProcessRunner.CreateProcess("/usr/bin/sw_vers", ""))
                .Replace("ProductVersion:", string.Empty)
                .Replace(" ", string.Empty);

            return Version.Parse(versionString.Split(Environment.NewLine.ToCharArray())[0]);
        }

        private static Version GetFreeBSDVersion()
        {
            string versionString = Environment.OSVersion.VersionString
                .Replace("Unix", string.Empty).Replace("FreeBSD", string.Empty)
                .Replace("-release", string.Empty).Replace(" ", string.Empty);

            return Version.Parse(versionString);
        }

        private static Version GetAndroidVersion()
        {
            string result = ProcessRunner.RunProcess(
                    ProcessRunner.CreateProcess("getprop", "ro.build.version.release"))
                .Replace(" ", string.Empty);

            return Version.Parse(result);
        }

        /// <summary>
        /// Indicates whether the current application is running on the specified platform.
        /// </summary>
        /// <param name="platform">The case-insensitive platform name. Examples: Browser, Linux, FreeBSD, Android, iOS, macOS, tvOS, watchOS, Windows.</param>
        /// <returns>true if the current application is running on the specified platform; false otherwise.</returns>
        public static bool IsOSPlatform(string platform)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Create(platform));
        }

        /// <summary>
        /// Checks if the operating system version is greater than or equal to the specified platform version. This method can be used to guard APIs that were added in the specified OS version.
        /// </summary>
        /// <param name="platform">The case-insensitive platform name. Examples: Browser, Linux, FreeBSD, Android, iOS, macOS, tvOS, watchOS, Windows.</param>
        /// <param name="major">The major release number.</param>
        /// <param name="minor">The minor release number (optional).</param>
        /// <param name="build">The build release number (optional).</param>
        /// <param name="revision">The revision release number (optional).</param>
        /// <returns>true if the current application is running on the specified platform and is at least in the version specified in the parameters; false otherwise.</returns>
        public static bool IsOSPlatformVersionIsAtLeast(string platform, int major, int minor = 0, int build = 0,
            int revision = 0)
        {
            return IsOSPlatform(platform) && IsOsVersionAtLeast(major, minor, build, revision);
        }

        /// <summary>
        /// Indicates whether the current application is running on Windows.
        /// </summary>
        /// <returns>true if the current application is running on Windows; false otherwise.</returns>
        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        /// <summary>
        /// Checks if the Windows version (returned by RtlGetVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified Windows version.
        /// </summary>
        /// <param name="major">The major release number.</param>
        /// <param name="minor">The minor release number.</param>
        /// <param name="build">The build release number.</param>
        /// <param name="revision">The revision release number.</param>
        /// <returns>true if the current application is running on a Windows version that is at least what was specified in the parameters; false otherwise.</returns>
        public static bool IsWindowsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            return IsWindows() && GetWindowsVersion() >= new Version(major, minor, build, revision);
        }

        /// <summary>
        /// Indicates whether the current application is running on macOS.
        /// </summary>
        /// <returns>true if the current application is running on macOS; false otherwise.</returns>
        public static bool IsMacOS()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }

        /// <summary>
        /// Indicates whether the current application is running on Mac Catalyst.
        /// </summary>
        /// <returns>true if the current application is running on Mac Catalyst; false otherwise.</returns>
        public static bool IsMacCatalyst()
        {
            return IsMacOS() || IsIOS();
        }

        /// <summary>
        /// Checks if the macOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified macOS version.
        /// </summary>
        /// <param name="major">The major release number.</param>
        /// <param name="minor">The minor release number.</param>
        /// <param name="build">The build release number.</param>
        /// <returns>true if the current application is running on an macOS version that is at least what was specified in the parameters; false otherwise.</returns>
        public static bool IsMacOSVersionAtLeast(int major, int minor = 0, int build = 0)
        {
            return IsMacOS() && GetMacOSVersion() >= new Version(major, minor, build);
        }

        /// <summary>
        /// Check for the Mac Catalyst version (iOS version as presented in Apple documentation) with a ≤ version comparison. Used to guard APIs that were added in the given Mac Catalyst release.
        /// </summary>
        /// <param name="major">The version major number.</param>
        /// <param name="minor">The version minor number.</param>
        /// <param name="build">The version build number.</param>
        /// <returns>true if the Mac Catalyst version is greater or equal than the specified version comparison; false otherwise.</returns>
        public static bool IsMacCatalystVersionAtLeast(int major, int minor, int build = 0)
        {
            return IsMacCatalyst() && IsOsVersionAtLeast(major, minor, build);
        }

        /// <summary>
        /// Indicates whether the current application is running on Linux.
        /// </summary>
        /// <returns>true if the current application is running on Linux; false otherwise.</returns>
        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        /// <summary>
        /// Indicates whether the current application is running on FreeBSD.
        /// </summary>
        /// <returns>true if the current application is running on FreeBSD; false otherwise.</returns>
        public static bool IsFreeBSD()
        {
            return RuntimeInformation.OSDescription.ToLower().Contains("freebsd");
        }

        /// <summary>
        ///Checks if the FreeBSD version (returned by the Linux command uname) is greater than or equal to the specified version.
        /// This method can be used to guard APIs that were added in the specified version.
        /// </summary>
        /// <param name="major">The version major number.</param>
        /// <param name="minor">The version minor number.</param>
        /// <param name="build">The version build number.</param>
        /// <param name="revision">The version revision number.</param>
        /// <returns>true if the current application is running on a FreeBSD version that is at least what was specified in the parameters; false otherwise.</returns>
        public static bool IsFreeBSDVersionAtLeast(int major, int minor, int build = 0, int revision = 0)
        {
            return GetFreeBSDVersion() >= new Version(major, minor, build, revision);
        }

        /// <summary>
        /// Indicates whether the current application is running on iOS or MacCatalyst.
        /// </summary>
        /// <returns>true if the current application is running on iOS or MacCatalyst; false otherwise.</returns>
        public static bool IsIOS()
        {
            string description = RuntimeInformation.OSDescription.ToLower();
            return description.Contains("ios") ||
                   description.Contains("ipados") ||
                   (description.Contains("iphone") &&
                    description.Contains("os"));
        }

        /// <summary>
        /// Checks if the iOS/MacCatalyst version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified iOS version.
        /// </summary>
        /// <param name="major">The major release number.</param>
        /// <param name="minor">The minor release number.</param>
        /// <param name="build">The build release number.</param>
        /// <returns>true if the current application is running on an iOS/MacCatalyst version that is at least what was specified in the parameters; false otherwise.</returns>
        public static bool IsIOSVersionAtLeast(int major, int minor, int build = 0)
        {
            return IsIOS() && IsOsVersionAtLeast(major, minor, build);
        }

        /// <summary>
        /// Indicates whether the current application is running on tvOS.
        /// </summary>
        /// <returns>true if the current application is running on tvOS; false otherwise.</returns>
        public static bool IsTvOS()
        {
            return RuntimeInformation.OSDescription.ToLower().Contains("tvos");
        }

        /// <summary>
        /// Checks if the tvOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified tvOS version.
        /// </summary>
        /// <param name="major">The major release number.</param>
        /// <param name="minor">The minor release number.</param>
        /// <param name="build">The build release number.</param>
        /// <returns>true if the current application is running on a tvOS version that is at least what was specified in the parameters; false otherwise.</returns>
        public static bool IsTvOSVersionAtLeast(int major, int minor, int build = 0)
        {
            return IsTvOS() && IsOsVersionAtLeast(major, minor, build);
        }

        /// <summary>
        /// Indicates whether the current application is running on Android.
        /// </summary>
        /// <returns>true if the current application is running on Android; false otherwise.</returns>
        public static bool IsAndroid()
        {
            try
            {
                string result = ProcessRunner.RunProcess(
                        ProcessRunner.CreateProcess("uname", "-o"))
                    .Replace(" ", string.Empty);

                return result.ToLower().Equals("android");
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Checks if the Android version (returned by the Linux command uname) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified version.
        /// </summary>
        /// <param name="major">The major release number.</param>
        /// <param name="minor">The minor release number.</param>
        /// <param name="build">The build release number.</param>
        /// <param name="revision">The revision release number.</param>
        /// <returns>true if the current application is running on an Android version that is at least what was specified in the parameters; false otherwise.</returns>
        public static bool IsAndroidVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            return IsAndroid() && GetAndroidVersion() >= new Version(major, minor, build, revision);
        }

        /// <summary>
        /// Indicates whether the current application is running on watchOS.
        /// </summary>
        /// <returns>true if the current application is running on watchOS; false otherwise.</returns>
        public static bool IsWatchOS()
        {
            string description = RuntimeInformation.OSDescription.ToLower();

            return IsIOS() || description.Contains("watchos");
        }

        /// <summary>
        /// Checks if the watchOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified watchOS version.
        /// </summary>
        /// <param name="major">The major release number.</param>
        /// <param name="minor">The minor release number.</param>
        /// <param name="build">The build release number.</param>
        /// <returns>true if the current application is running on a watchOS version that is at least what was specified in the parameters; false otherwise.</returns>
        public static bool IsWatchOSVersionAtLeast(int major, int minor, int build = 0)
        {
            return IsWatchOS() && IsOsVersionAtLeast(major, minor, build);
        }

        /// <summary>
        /// Indicates whether the current application is running as WASI.
        /// </summary>
        /// <returns>true if running as WASI; false otherwise.</returns>
        public static bool IsWasi()
        {
            return RuntimeInformation.FrameworkDescription.ToLower().Contains("wasi");
        }

        /// <summary>
        /// Indicates whether the current application is running as WASM in a browser.
        /// </summary>
        /// <returns>true if running as WASM; false otherwise.</returns>
        public static bool IsBrowser()
        {
            return RuntimeInformation.FrameworkDescription.Contains(".NET WebAssembly");
        }

        private static bool IsOsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            return Environment.OSVersion.Version >= new Version(major, minor, build, revision);
        }
    }
}
﻿/*
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
using System.Runtime.InteropServices;

#if NET5_0_OR_GREATER
using System.Runtime.Versioning;
#endif

using AlastairLundy.Extensions.Runtime.Identification;
using AlastairLundy.Extensions.Runtime.Internal.Localizations;

// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable InconsistentNaming

namespace AlastairLundy.Extensions.Runtime
{
    public static class OperatingSystemExtensions
    {

        /// <summary>
        /// Returns whether the operating system that is running is Windows.
        /// </summary>
        /// <returns>true if the Operating System being run is Windows based; returns false otherwise.</returns>
        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        /// <summary>
        /// Returns whether the operating system that is running is Windows.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is Windows based; returns false otherwise.</returns>
        public static bool IsWindows(this OperatingSystem operatingSystem)
        {
            return IsWindows();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        #if NET5_0_OR_GREATER
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        [UnsupportedOSPlatform("watchos")]
        [UnsupportedOSPlatform("android")]
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("freebsd")]
        [SupportedOSPlatform("macos")]
        #endif
        public static Version Version
        {
            get
            {
                if (IsWindows())
                {
                    return PlatformID.Win32NT.GetSystem().Version;
                }
                else if (IsMacOS())
                {
                    return PlatformID.MacOSX.GetSystem().Version;
                }
                else if (IsLinux())
                {
                    return PlatformID.Unix.GetSystem().Version;
                }
                else if (IsFreeBSD())
                {
                    return PlatformID.Unix.GetSystem().Version;
                }
                else
                {
                    throw new PlatformNotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns whether the operating system that is running is macOS.
        /// </summary>
        /// <returns>true if the Operating System being run is macOS based; returns false otherwise.</returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsMacOS()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }

        /// <summary>
        /// Returns whether the operating system that is running is macOS.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is macOS based; returns false otherwise.</returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsMacOS(this OperatingSystem operatingSystem)
        {
            return IsMacOS();
        }

        /// <summary>
        /// Returns whether the operating system that is running is Linux.
        /// </summary>
        /// <returns>true if the Operating System being run is Linux based; returns false otherwise.</returns>
        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on Linux.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is Linux based; returns false otherwise.</returns>
        public static bool IsLinux(this OperatingSystem operatingSystem)
        {
            return IsLinux();
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on FreeBSD.
        /// </summary>
        /// <returns>true if the Operating System being run is FreeBSD based; returns false otherwise.</returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsFreeBSD()
        {
            return RuntimeInformation.OSDescription.ToLower().Contains("freebsd");
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on FreeBSD.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is FreeBSD based; returns false otherwise.</returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsFreeBSD(this OperatingSystem operatingSystem)
        {
            return IsFreeBSD();
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on Tizen.
        /// </summary>
        /// <returns>true if the Operating System being run is Tizen based; returns false otherwise.</returns>
        public static bool IsTizen()
        {
            return RuntimeInformation.OSDescription.ToLower().Contains("tizen");
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on Tizen.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is Tizen based; returns false otherwise.</returns>
        public static bool IsTizen(this OperatingSystem operatingSystem)
        {
            return IsTizen();
        }
        
        /// <summary>
        /// Returns whether the operating system that is running is based on IOS.
        /// </summary>
        /// <returns>true if the Operating System being run is IOS based; returns false otherwise.</returns>
        public static bool IsIOS()
        {
            return RuntimeInformation.OSDescription.ToLower().Contains("ios");
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on IOS.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is IOS based; returns false otherwise.</returns>
        public static bool IsIOS(this OperatingSystem operatingSystem)
        {
            return IsIOS();
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on Android.
        /// </summary>
        /// <returns>true if the Operating System being run is Android based; returns false otherwise.</returns>
        public static bool IsAndroid()
        {
            return RuntimeInformation.OSDescription.ToLower().Contains("android") && RuntimeInformation.OSDescription.ToLower().Contains("tv") == false;
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on Android.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is Android based; returns false otherwise.</returns>
        public static bool IsAndroid(this OperatingSystem operatingSystem)
        {
            return IsAndroid();
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on Android TV.
        /// </summary>
        /// <returns>true if the Operating System being run is based on Android TV; returns false otherwise.</returns>

        public static bool IsAndroidTV()
        {
            // ReSharper disable once RedundantBoolCompare
            return RuntimeInformation.OSDescription.ToLower().Contains("android") && RuntimeInformation.OSDescription.ToLower().Contains("tv") == true;
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on Android TV.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is based on Android TV; returns false otherwise.</returns>
        public static bool IsAndroidTV(this OperatingSystem operatingSystem)
        {
            return IsAndroidTV();
        }
        
        /// <summary>
        /// Returns whether the operating system that is running is based on watchOS.
        /// </summary>
        /// <returns>true if the Operating System being run is based on Watch OS; returns false otherwise.</returns>
        public static bool IsWatchOS()
        {
            return RuntimeInformation.OSDescription.ToLower().Contains("watchos");
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on watchOS.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is based on Watch OS; returns false otherwise.</returns>
        public static bool IsWatchOS(this OperatingSystem operatingSystem)
        {
            return IsWatchOS();
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on wearOS.
        /// </summary>
        /// <returns>true if the Operating System being run is based on Wear OS; returns false otherwise.</returns>
        public static bool IsWearOS()
        {
            return RuntimeInformation.OSDescription.ToLower().Contains("wearos");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is based on Wear OS; returns false otherwise.</returns>
        public static bool IsWearOS(this OperatingSystem operatingSystem)
        {
            return IsWearOS();
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on tvOS.
        /// </summary>
        /// <returns>true if the Operating System being run is based on tvOS; returns false otherwise.</returns>
        public static bool IsTvOS()
        {
            if(RuntimeInformation.OSDescription.ToLower().Contains("tvos") && RuntimeInformation.OSDescription.ToLower().Contains("android"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Returns whether the operating system that is running is based on tvOS.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is based on tvOS; returns false otherwise.</returns>
        public static bool IsTvOS(this OperatingSystem operatingSystem)
        {
            return IsTvOS();
        }
        
        /// <summary>
        /// Checks to see whether the specified version of Windows is the same or newer than the installed version of Windows.
        /// </summary>
        /// <param name="major">The major version of the version to be compared.</param>
        /// <param name="minor">The minor version of the version to be compared.</param>
        /// <param name="build">The build version of the version to be compared.</param>
        /// <param name="revision">The revision version of the version to be compared</param>
        /// <returns>true if the specified version is greater than or equal to the installed version; returns false otherwise.</returns>
        public static bool IsWindowsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            return IsWindowsVersionAtLeast(new Version(major, minor, build, revision));
        }
        
        /// <summary>
        /// Checks to see whether the specified version of Windows is the same or newer than the installed version of Windows.
        /// </summary>
        /// <param name="versionToCompare">The version to be compared.</param>
        /// <returns>true if the specified version is greater than or equal to the installed version; returns false otherwise.</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown if the operating system being run is not based on Windows.</exception>
        public static bool IsWindowsVersionAtLeast(Version versionToCompare)
        {
            if (IsWindows())
            {
                return versionToCompare >= PlatformID.Win32NT.GetSystem().Version;
            }

            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_WindowsOnly);
        }

        /// <summary>
        /// Checks to see whether the specified version of macOS is the same or newer than the installed version of macOS.
        /// </summary>
        /// <param name="major">The major version of the version to be compared.</param>
        /// <param name="minor">The minor version of the version to be compared.</param>
        /// <param name="build">The build version of the version to be compared.</param>
        /// <param name="revision">The revision version of the version to be compared</param>
        /// <returns>true if the specified version is greater than or equal to the installed version; returns false otherwise.</returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsMacOSVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            return IsMacOSVersionAtLeast(new Version(major, minor, build, revision));
        }
        
        /// <summary>
        /// Checks to see whether the specified version of macOS is the same or newer than the installed version of macOS.
        /// </summary>
        /// <param name="versionToCompare">The version to be compared.</param>
        /// <returns>true if the specified version is greater than or equal to the installed version; returns false otherwise.</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown if the operating system being run is not based on macOS.</exception>
        public static bool IsMacOSVersionAtLeast(Version versionToCompare)
        {
            if (IsMacOS())
            {
                return versionToCompare >= PlatformID.MacOSX.GetSystem().Version;
            }

            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_MacOnly);
        }

        /// <summary>
        /// Checks to see whether the specified version of Linux is the same or newer than the installed version of Linux.
        /// </summary>
        /// <param name="major">The major version of the version to be compared.</param>
        /// <param name="minor">The minor version of the version to be compared.</param>
        /// <param name="build">The build version of the version to be compared.</param>
        /// <param name="revision">The revision version of the version to be compared</param>
        /// <returns>true if the specified version is greater than or equal to the installed version; returns false otherwise.</returns>
        public static bool IsLinuxVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            return IsLinuxVersionAtLeast(new Version(major, minor, build, revision));
        }
        
        /// <summary>
        /// Checks to see whether the specified version of Linux is the same or newer than the installed version of Linux.
        /// </summary>
        /// <param name="versionToCompare"></param>
        /// <returns>true if the specified version is greater than or equal to the installed version; returns false otherwise.</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown if the operating system being run is not based on Linux.</exception>
        public static bool IsLinuxVersionAtLeast(Version versionToCompare)
        {
            if (IsLinux())
            {
                return versionToCompare >= PlatformID.Unix.GetSystem().Version;
            }

            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_LinuxOnly);
        }

        /// <summary>
        /// Checks to see whether the specified version of FreeBSD is the same or newer than the installed version of FreeBSD.
        /// </summary>
        /// <param name="major">The major version of the version to be compared.</param>
        /// <param name="minor">The minor version of the version to be compared.</param>
        /// <param name="build">The build version of the version to be compared.</param>
        /// <param name="revision">The revision version of the version to be compared</param>
        /// <returns>true if the specified version is greater than or equal to the installed version; returns false otherwise.</returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsFreeBSDVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            return IsFreeBSDVersionAtLeast(new Version(major, minor, build, revision));
        }

        /// <summary>
        /// Checks to see whether the specified version of FreeBSD is the same or newer than the installed version of FreeBSD.
        /// </summary>
        /// <param name="versionToCompare">The version to be compared</param>
        /// <returns>true if the specified version is greater than or equal to the installed version; returns false otherwise.</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown if the operating system being run is not based on FreeBSD.</exception>
        // ReSharper disable once InconsistentNaming
        public static bool IsFreeBSDVersionAtLeast(Version versionToCompare)
        {
            if (IsFreeBSD())
            {
                return versionToCompare >= PlatformID.Unix.GetSystem().Version;
            }

            throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_FreeBsdOnly);
        }
    }
}
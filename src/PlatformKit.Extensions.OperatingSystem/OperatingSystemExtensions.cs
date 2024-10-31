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
// ReSharper disable InconsistentNaming

namespace PlatformKit.Extensions.OperatingSystem
{
    public static class OperatingSystemExtension
    {

        /// <summary>
        /// Returns whether the operating system that is running is Windows.
        /// </summary>
        /// <returns>true if the Operating System being run is Windows based; returns false otherwise.</returns>
        public static bool IsWindows()
        {
            return IsWindows(GetSystemExtension.GetSystem(PlatformID.Win32NT));
        }

        /// <summary>
        /// Returns whether the operating system that is running is Windows.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is Windows based; returns false otherwise.</returns>
        public static bool IsWindows(this System.OperatingSystem operatingSystem)
        {
            return IsOperatingSystemExtensions.IsWindows();
        }

        /// <summary>
        /// Returns whether the operating system that is running is macOS.
        /// </summary>
        /// <returns>true if the Operating System being run is macOS based; returns false otherwise.</returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsMacOS()
        {
            return IsMacOS(GetSystemExtension.GetSystem(PlatformID.MacOSX));
        }

        /// <summary>
        /// Returns whether the operating system that is running is macOS.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is macOS based; returns false otherwise.</returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsMacOS(this System.OperatingSystem operatingSystem)
        {
            return IsOperatingSystemExtensions.IsMacOS();
        }

        /// <summary>
        /// Returns whether the operating system that is running is Linux.
        /// </summary>
        /// <returns>true if the Operating System being run is Linux based; returns false otherwise.</returns>
        public static bool IsLinux()
        {
            return IsLinux(GetSystemExtension.GetSystem(PlatformID.Unix));
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on Linux.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is Linux based; returns false otherwise.</returns>
        public static bool IsLinux(this System.OperatingSystem operatingSystem)
        {
            return IsOperatingSystemExtensions.IsLinux();
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on FreeBSD.
        /// </summary>
        /// <returns>true if the Operating System being run is FreeBSD based; returns false otherwise.</returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsFreeBSD()
        {
            return IsFreeBSD(GetSystemExtension.GetSystem(PlatformID.Unix));
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on FreeBSD.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is FreeBSD based; returns false otherwise.</returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsFreeBSD(this System.OperatingSystem operatingSystem)
        {
            return IsOperatingSystemExtensions.IsFreeBSD();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if the Operating System being run is IOS based; returns false otherwise.</returns>
        public static bool IsIOS()
        {
            return IsOperatingSystemExtensions.IsIOS();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is IOS based; returns false otherwise.</returns>
        public static bool IsIOS(this System.OperatingSystem operatingSystem)
        {
            return IsOperatingSystemExtensions.IsIOS();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if the Operating System being run is Android based; returns false otherwise.</returns>
        public static bool IsAndroid()
        {
            return IsOperatingSystemExtensions.IsAndroid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns>true if the Operating System being run is Android based; returns false otherwise.</returns>
        public static bool IsAndroid(this System.OperatingSystem operatingSystem)
        {
            return IsOperatingSystemExtensions.IsAndroid();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWatchOS()
        {
            return IsOperatingSystemExtensions.IsWatchOS();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns></returns>
        public static bool IsWatchOS(this System.OperatingSystem operatingSystem)
        {
            return IsOperatingSystemExtensions.IsWatchOS();
        }

        public static bool IsWearOS()
        {
            return IsOperatingSystemExtensions.IsWearOS();
        }

        public static bool IsWearOS(this System.OperatingSystem operatingSystem)
        {
            return IsOperatingSystemExtensions.IsWearOS();
        }

        /// <summary>
        /// Returns whether the operating system that is running is based on tvOS.
        /// </summary>
        /// <returns></returns>
        public static bool IsTvOS()
        {
            return IsOperatingSystemExtensions.IsTvOS();
        }
        
        /// <summary>
        /// Returns whether the operating system that is running is based on tvOS.
        /// </summary>
        /// <param name="operatingSystem"></param>
        /// <returns></returns>
        public static bool IsTvOS(this System.OperatingSystem operatingSystem)
        {
            return IsOperatingSystemExtensions.IsTvOS();
        }

        /// <summary>
        /// Checks to see whether the specified version of Windows is the same or newer than the installed version of Windows.
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="build"></param>
        /// <param name="revision"></param>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public static bool IsWindowsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            return IsVersionAtLeastExtensions.IsWindowsVersionAtLeast(major, minor, build, revision);
        }

        /// <summary>
        /// Checks to see whether the specified version of macOS is the same or newer than the installed version of Windows.
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="build"></param>
        /// <param name="revision"></param>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        // ReSharper disable once InconsistentNaming
        public static bool IsMacOSVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            return IsVersionAtLeastExtensions.IsMacOSVersionAtLeast(major, minor, build, revision);
        }

        /// <summary>
        /// Checks to see whether the specified version of Linux is the same or newer than the installed version of Windows.
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="build"></param>
        /// <param name="revision"></param>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public static bool IsLinuxVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            return IsVersionAtLeastExtensions.IsLinuxVersionAtLeast(major, minor, build, revision);
        }

        /// <summary>
        /// Checks to see whether the specified version of FreeBSD is the same or newer than the installed version of Windows.
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="build"></param>
        /// <param name="revision"></param>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        // ReSharper disable once InconsistentNaming
        public static bool IsFreeBSDVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
        {
            return IsVersionAtLeastExtensions.IsFreeBSDVersionAtLeast(major, minor, build, revision);
        }
    }
}
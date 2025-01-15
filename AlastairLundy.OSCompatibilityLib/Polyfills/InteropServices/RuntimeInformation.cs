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

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Architecture = System.Runtime.InteropServices.Architecture;
using OSPlatform = System.Runtime.InteropServices.OSPlatform;

using AlastairLundy.OSCompatibilityLib.Helpers;
using AlastairLundy.OSCompatibilityLib.Internal.Localizations;

using AlastairLundy.OSCompatibilityLib.Specializations;
using AlastairLundy.OSCompatibilityLib.Specializations.Windows;

// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable ConvertIfStatementToReturnStatement
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
// ReSharper disable RedundantBoolCompare

namespace AlastairLundy.OSCompatibilityLib.Polyfills.InteropServices
{

    /// <summary>
    /// 
    /// </summary>
    public static class RuntimeInformation
    {
        /// <summary>
        /// 
        /// </summary>
        public static string FrameworkDescription { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static string OSDescription { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static System.Runtime.InteropServices.Architecture OSArchitecture { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static Architecture ProcessArchitecture { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static string RuntimeIdentifier { get; private set; }


        static RuntimeInformation()
        {
            GetArchitectureInfo();

            RuntimeIdentifier = GetRuntimeIdentifier();

            FrameworkDescription = GetFrameworkDescription();
            OSDescription = Environment.OSVersion.VersionString;
        }

        #region Framework Description code

        private static string GetFrameworkDescription()
        {
            try
            {
                return GetNewNewDescription();
            }
            catch
            {
                return GetNetFrameworkDescription();
            }
        }

        private static string GetNewNewDescription()
        {
            Process winProcess =
                ProcessRunner.CreateProcess($"{Environment.SystemDirectory}{Path.DirectorySeparatorChar}cmd.exe",
                    "dotnet --version");

            Process unixProcess = ProcessRunner.CreateProcess($"dotnet", "--version");

            string release = "";

            if (OperatingSystem.IsWindows())
            {
                release = ProcessRunner.RunProcess(winProcess);
            }
            else if (OperatingSystem.IsIOS() == false &&
                     OperatingSystem.IsTvOS() == false &&
                     OperatingSystem.IsWatchOS() == false)
            {
                release = ProcessRunner.RunProcess(unixProcess);
            }
            else
            {
                release = Environment.Version.ToString();
            }

            bool releaseFound = Version.TryParse(release, out Version versionRelease);

            if (releaseFound)
            {
                if (versionRelease.Major < 4)
                {
                    release = release.Insert(0, ".NET Core ");
                }
                else if (versionRelease.Major > 4)
                {
                    release = release.Insert(0, ".NET ");
                }
                else
                {
                    release = release.Insert(0, ".NET Core CLR ");
                }
            }
            else
            {
                return $".NET CLR {Environment.Version.ToString()}";
            }

            return release;
        }

        private static string GetNetFrameworkDescription()
        {
            int releaseKey;

            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            string releaseKeyString = WinRegistrySearcher.GetValue(subkey, "Release");

            string frameworkVersion = "";

            switch (int.Parse(releaseKeyString))
            {
                case 394254 | 394271:
                    frameworkVersion = "4.6.1";
                    break;
                case 394802 | 394806:
                    frameworkVersion = "4.6.2";
                    break;
                case 460798 | 460805:
                    frameworkVersion = "4.7";
                    break;
                case 461308 | 461310:
                    frameworkVersion = "4.7.1";
                    break;
                case 461808 | 461814:
                    frameworkVersion = "4.7.2";
                    break;
                case 528040 | 528372 | 528449 | 528049:
                    frameworkVersion = "4.8";
                    break;
                case 533320 | 533325:
                    frameworkVersion = "4.8.1";
                    break;
                default:
                    frameworkVersion = Environment.Version.ToString();
                    break;
            }

            return $".NET Framework {frameworkVersion}";
        }

        #endregion

        private static void GetArchitectureInfo()
        {
            bool isArmBased;

            if (OperatingSystem.IsWindows())
            {
                string architectureString = ProcessRunner.RunProcess(
                    ProcessRunner.CreateProcess($"{Environment.SystemDirectory}{Path.DirectorySeparatorChar}cmd.exe",
                        "systeminfo"));

                isArmBased = architectureString.ToLower().Contains("aarch");
            }
            else
            {
                if (OperatingSystem.IsIOS() || OperatingSystem.IsTvOS() || OperatingSystem.IsWatchOS())
                {
                    isArmBased = true;
                }
                else
                {
                    string architectureString = ProcessRunner.RunProcess(
                        ProcessRunner.CreateProcess("/usr/bin/uname", "-m"));

                    isArmBased = architectureString.ToLower().Contains("aarch");
                }
            }

            if (Environment.Is64BitOperatingSystem)
            {
                if (isArmBased == true)
                {
                    OSArchitecture = Architecture.Arm64;
                }
                else
                {
                    OSArchitecture = Architecture.X64;
                }
            }
            else
            {
                if (isArmBased == true)
                {
                    OSArchitecture = Architecture.Arm;
                }
                else
                {
                    OSArchitecture = Architecture.X86;
                }
            }

            if (Environment.Is64BitProcess)
            {
                if (isArmBased == true)
                {
                    ProcessArchitecture = Architecture.Arm64;
                }
                else
                {
                    ProcessArchitecture = Architecture.X64;
                }
            }
            else
            {
                if (isArmBased == true)
                {
                    ProcessArchitecture = Architecture.Arm;
                }
                else
                {
                    ProcessArchitecture = Architecture.X86;
                }
            }
        }

        #region Runtime Identifier code

        private static string GetRuntimeIdentifier()
        {
            if (OperatingSystem.IsWindows())
            {
                return $"{RuntimeIdentification.GetOsNameString(RuntimeIdentifierType.Generic)}{GetOsVersionString()}-{GetArchitectureString()}";
            }

            return $"{RuntimeIdentification.GetOsNameString(RuntimeIdentifierType.Generic)}-{GetArchitectureString()}";
        }

        /// <summary>
        /// Returns the OS version as a string in the format that a RuntimeID uses.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        private static string GetOsVersionString()
        {
            string osVersion = string.Empty;

            if (OperatingSystem.IsWindows())
            {
                bool isWindows10 = OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240) &&
                                   OperatingSystem.GetFallbackOsVersion() < new Version(10, 0, 20349);

                bool isWindows11 = OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22000);

                if (isWindows10)
                {
                    osVersion = "10";
                }
                else if (isWindows11)
                {
                    osVersion = "11";
                }
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                else if (!isWindows10 && !isWindows11)
                {
                    throw new PlatformNotSupportedException(Resources
                        .Exceptions_PlatformNotSupported_EndOfLifeOperatingSystem);
                }
            }

            if (OperatingSystem.IsLinux())
            {
                osVersion = OperatingSystem.GetFallbackOsVersion().ToString();
            }

            if (OperatingSystem.IsFreeBSD())
            {
                osVersion = OperatingSystem.GetFallbackOsVersion().ToString();

                switch (osVersion.Count(x => x == '.'))
                {
                    case 3:
                        osVersion = osVersion.Remove(osVersion.Length - 4, 4);
                        break;
                    case 2:
                        osVersion = osVersion.Remove(osVersion.Length - 2, 2);
                        break;
                    case 1:
                        break;
                    case 0:
                        osVersion = $"{osVersion}.0";
                        break;
                }
            }

            if (OperatingSystem.IsMacOS())
            {
                bool isAtLeastHighSierra = OperatingSystem.IsMacOSVersionAtLeast(10, 13);

                Version version = OperatingSystem.GetFallbackOsVersion();

                if (isAtLeastHighSierra)
                {
                    if (OperatingSystem.IsMacOSVersionAtLeast(11, 0))
                    {
                        osVersion = $"{version.Major}";
                    }
                    else
                    {
                        osVersion = $"{version.Major}.{version.Major}";
                    }
                }
                else
                {
                    throw new PlatformNotSupportedException();
                }
            }

            if (osVersion == null)
            {
                throw new PlatformNotSupportedException();
            }

            return osVersion;
        }

        /// <summary>
        /// Returns the CPU architecture as a string in the format that a RuntimeID uses.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        private static string GetArchitectureString()
        {
            switch (OSArchitecture)
            {
                case Architecture.Arm:
                    return "arm";
                case Architecture.Arm64:
                    return "arm64";
                case Architecture.X64:
                    return "x64";
                case Architecture.X86:
                    return "x86";
                default:
                    throw new PlatformNotSupportedException();
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        public static bool IsOSPlatform(OSPlatform platform)
        {
            if (OperatingSystem.IsWindows() && platform == OSPlatform.Windows ||
                OperatingSystem.IsLinux() && platform == OSPlatform.Linux ||
                OperatingSystem.IsMacOS() && platform == OSPlatform.OSX)
            {
                return true;
            }

            string platformName = platform.ToString().ToLower();

            if (platformName.Contains("freebsd") && OperatingSystem.IsFreeBSD())
            {
                return true;
            }

            if (platformName.Contains("android") && OperatingSystem.IsAndroid())
            {
                return true;
            }

            if (platformName.Contains("ios") && OperatingSystem.IsIOS())
            {
                return true;
            }

            if (platformName.Contains("tvos") && OperatingSystem.IsTvOS())
            {
                return true;
            }

            if (platformName.Contains("watchos") && OperatingSystem.IsWatchOS())
            {
                return true;
            }

            if (platformName.Contains("browser") && OperatingSystem.IsBrowser())
            {
                return true;
            }

            return false;
        }
    }
}
/*
        MIT License
       
       Copyright (c) 2020-2025 Alastair Lundy
       
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

namespace AlastairLundy.OSCompatibilityLib.Specializations
{
    /// <summary>
    /// The type of RuntimeIdentifier generated or detected.
    /// </summary>
    public enum RuntimeIdentifierType
    {
        /// <summary>
        /// A Runtime Identifier that is valid for all architectures of an operating system.
        /// </summary>
        AnyGeneric,
        /// <summary>
        /// A Runtime Identifier that is valid for all supported versions of the OS being run.
        /// </summary>
        Generic,
        /// <summary>
        /// A Runtime Identifier that is valid for the specified OS and specified OS version being run.
        /// </summary>
        Specific,
        /// <summary>
        /// This is meant for Linux use only. DO NOT USE ON WINDOWS or MAC.
        /// </summary>
        DistroSpecific,
        /// <summary>
        /// This is meant for Linux use only. DO NOT USE ON WINDOWS or MAC.
        /// </summary>
        VersionLessDistroSpecific
    }
}
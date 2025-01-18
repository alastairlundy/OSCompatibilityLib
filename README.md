# OSCompatibilityLib (formerly RuntimeExtensions)

## Features:
OSCompatibilityLib adds .NET Standard 2.0 & 2.1 compatible ways of getting:
* OS Detection by reimplementing the OperatingSystem class with static methods
* Re-implementing the RuntimeInformation class to backport Runtime Identifier detection
  
It also adds some support for programmatically determining the .NET Target Framework Moniker (TFM) being used.

## Usage
To get OS Detection support, OSCompatibilityLib needs to replace the reference to the existing OperatingSystem class in a .NET Standard 2.0 project with OSCompatibilityLib's equivalent class. 

This can be easily done with a using namespace.

To target only .NET Standard 2.0 use:
```csharp
#if NETSTANDARD2_0
    using OperatingSystem = AlastairLundy.OSCompatibilityLib.Polyfills.OperatingSystem;
#endif
```

To target .NET Standard 2.0 and 2.1 use:
```csharp
#if NETSTANDARD2_0 || NETSTANDARD2_1
    using OperatingSystem = AlastairLundy.OSCompatibilityLib.Polyfills.OperatingSystem;
#endif
```

## Compatibility
The following tables indicate Operating Systems that are capable of being detected with this library.

### Desktop Operating Systems
| Operating System Target | Support Status                | Required Library Version      |
|-------------------------|-------------------------------|-------------------------------|
| Windows                 | :white_check_mark:, Supported | Any                           |
| Linux                   | :white_check_mark:, Supported | Any                           |
| FreeBSD                 | :white_check_mark:, Supported | Any                           |
| macOS                   | :white_check_mark:, Supported | Any                           |
| Mac Catalyst            | :white_check_mark:, Supported | 3.0.0 and newer               |

### Mobile Operating Systems
| Operating System | Support Status         | Required Library Version |
|------------------|------------------------|--------------------------|
| IOS              | :white_check_mark:     | 1.5.0 or newer           |
| tvOS             | :white_check_mark:     | 1.5.2 or newer           |                                         |
| watchOS          | :white_check_mark:     | 1.5.0 or newer           |
| visionOS         | :x:, Not Supported     | N/A Not Supported        | 
| Android          | :white_check_mark:     | 1.5.0 or newer           |
| Fire OS          | :x:, Not Supported     | N/A Not Supported        | 
| Android TV       | :x:, Not Supported     | N/A Not Supported        |
| Tizen            | :white_check_mark:     | 2.0.0 or newer           |

## License

OSCompatibilityLib is licensed under the MIT license.

## Acknowledgements
This project makes use of the follwing 3rd party code or other works:
* [.NET API Docs](https://github.com/dotnet/dotnet-api-docs/) for Polyfill class, property, and method xml doc comments

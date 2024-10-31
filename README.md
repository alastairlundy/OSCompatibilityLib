# RuntimeExtensions (formerly known as PlatformKit.Extensions.OperatingSystem)

## Features:
Runtime Extensions adds .NET Standard 2.0 & 2.1 compatible ways of getting:
* OS Detection support
* programmatic Runtime Identifier (RID) detection 

It also adds some support for programmatically determining the .NET Target Framework Moniker (TFM) being used.

## Usage
To get OS Detection support, RuntimeExtensions needs to replace the reference to the existing OperatingSystem class in a .NET Standard 2.0 project with RuntimeExtensions' equivalent class. 

This can be easily done with a using namespace.

To target only .NET Standard 2.0 use:
```csharp
#if NETSTANDARD2_0
    using OperatingSystem = AlastairLundy.Extensions.Runtime.OperatingSystemExtensions;
#endif
```

To target .NET Standard 2.0 and 2.1 use:
```csharp
#if NETSTANDARD2_0 || NETSTANDARD2_1
    using OperatingSystem = AlastairLundy.Extensions.Runtime.OperatingSystemExtensions;
#endif
```

## Compatibility
The following tables indicate Operating Systems that are capable of being detected with this library.

Although "Mac Catalyst" as a target is not explicitly supported, implicit support can be indirectly detected.

You can use:
```csharp

if(OperatingSystem.IsMacOS()){
    
    if(OperatingSystem.IsAtLeastVersion(10, 15)){
        // Mac Catalyst specific code goes here
    }
}
```


### Desktop Operating Systems
| Operating System Target | Support Status                | Required Library Version      |
|-------------------------|-------------------------------|-------------------------------|
| Windows                 | :white_check_mark:, Supported | Any                           |
| Linux                   | :white_check_mark:, Supported | Any                           |
| FreeBSD                 | :white_check_mark:, Supported | Any                           |
| macOS                   | :white_check_mark:, Supported | Any                           |
| Mac Catalyst            | :x:, Not Explicitly Supported | N/A, Not Explicitly Supported |

### Mobile Operating Systems
| Operating System | Support Status     | Required Library Version |
|------------------|--------------------|--------------------------|
| IOS              | :white_check_mark: | 1.5.0 or newer           |
| tvOS             | :white_check_mark: | 1.5.2 or newer           |                                         |
| watchOS          | :white_check_mark: | 1.5.0 or newer           |
| Android          | :white_check_mark: | 1.5.0 or newer           |
| Android TV       | :x:, Not Supported | N/A Not Supported        |
| wearOS           | :white_check_mark: | 1.5.1 or newer           |
| Tizen            | :x:, Not Supported | N/A Not Supported        |

## License

RuntimeExtensions is licensed under the MIT license.
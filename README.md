# RuntimeExtensions (formerly known as PlatformKit.Extensions.OperatingSystem)

## About
This library adds Windows, macOS, Linux, and FreeBSD detection methods to the System.OperatingSystem class via extension methods.

This can be added to any .NET Standard 2.0 project.


## Usage
To replace the existing OperatingSystem class in a .NET Standard 2.0 project with this one, use this in your using namespaces:

```csharp
#if NETSTANDARD2_0
    using OperatingSystem = AlastairLundy.Extensions.Runtime.OperatingSystemExtensions;
#endif
```

## Compatibility
The following tables indicate Operating Systems that have detection methods in this library.

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
| Operating System Target | Support Status                | Required Library Version |
|-------------------------|-------------------------------|--------------------------------|
| Windows                 | :white_check_mark:, Supported | Any |
| Linux                   | :white_check_mark:, Supported | Any |
| FreeBSD                 | :white_check_mark:, Supported | Any |
| macOS                   | :white_check_mark:, Supported | Any |
| Mac Catalyst            | :x:, Not Supported            | N/A, Not Supported |

### Mobile Operating Systems
| Operating System | Support Status     | Required Library Version |
|------------------|--------------------|---------------------------------------------------------|
| IOS              | :white_check_mark: | 1.5.0 or newer                                          |
| tvOS             | :white_check_mark: | 1.5.2 or newer                                          |                                         |
| watchOS          | :white_check_mark: | 1.5.0 or newer                                          |
| Android          | :white_check_mark: | 1.5.0 or newer                                          |
| Android TV       | :x:, Not Supported | N/A Not Supported                                       |
| wearOS           | :white_check_mark: | 1.5.1 or newer                                          |
| Tizen            | :x:, Not Supported | N/A Not Supported                                       |

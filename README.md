# PlatformKit.Extensions.OperatingSystem


## About

PlatformKit.Extensions.OperatingSystem adds Windows, macOS, Linux, and FreeBSD detection methods to the System.OperatingSystem class via extension methods.

This can be added to any .NET Standard 2.0 project.


## Usage
To replace the existing OperatingSystem class in .NET Standard 2 with this one, use this in your using namespaces:

```csharp
#if NETSTANDARD2_0
    using OperatingSystem = PlatformKit.Extensions.OperatingSystem.OperatingSystemExtension;
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
| Operating System Target | Support Status                |
|-------------------------|-------------------------------|
| Windows                 | :white_check_mark:, Supported | 
| Linux                   | :white_check_mark:, Supported | 
| FreeBSD                 | :white_check_mark:, Supported |
| macOS                   | :white_check_mark:, Supported |
| Mac Catalyst            | :x:, Not Supported            | 

### Mobile Operating Systems
| Operating System | Support Status     |
|------------------|--------------------|
| IOS              | :white_check_mark, Supported from v1.5.0 onwards |
| tvOS             | :x:, Not Supported |
| watchOS          | :white_check_mark, Supported from v1.5.0 onwards |
| Android          | :white_check_mark, Supported from v1.5.0 onwards |
| Android TV       | :x:, Not Supported |
| wearOS           | :x:, Not Supported |
| Tizen            | :x:, Not Supported |

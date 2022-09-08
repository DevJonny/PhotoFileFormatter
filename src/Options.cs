using CommandLine;

namespace PhotoFileFormatter;

public class Options
{
    [Option('p', "path", Default = ".", HelpText = "The file path to run against")]
    public string Path { get; set; }
    
    [Option('f', "fstop", Default = false, Group = "formatting", HelpText = "Include the f-stop in the file name")]
    public bool IncludeFStop { get; set; }
    
    [Option('e', "exposure", Default = false, Group = "formatting", HelpText = "Include the exposure in the file name")]
    public bool IncludeExposure { get; set; }
    
    [Option('i', "iso", Default = false, Group = "formatting", HelpText = "Include the ISO in the file name")]
    public bool IncludeIso { get; set; }
    
    [Option('l', "focal-length", Default = false, Group = "formatting", HelpText = "Include the focal length in the file name")]
    public bool IncludeFocalLength { get; set; }
    
    [Option('a', "all", Default = false, Group = "formatting", HelpText = "Include all settings in the file name")]
    public bool IncludeAll { get; set; }
    
    [Option('t', "remove-topaz-additions", Default = false, HelpText = "Remove -Edit and -Sharpen from the filename")]
    public bool RemoveTopaz { get; set; }
    
    [Option("what-if", Default = false, HelpText = "Stop files from being renamed, but all logging is done")]
    public bool WhatIf { get; set; }
}
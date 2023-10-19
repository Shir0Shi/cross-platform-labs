using CommandLine;

public class CommonOptions
{
    [Option('i', "input", Required = false, HelpText = "Input file.")]
    public string InputFile { get; set; }

    [Option('o', "output", Required = false, HelpText = "Output file.")]
    public string OutputFile { get; set; }
}

[Verb("version", HelpText = "Show version information.")]
public class VersionOptions
{
    [Option('v', "version", Required = false, HelpText = "Show version information.")]
    public bool VersionCommand { get; set; }
}



[Verb("run", HelpText = "Run specific lab.")]
public class RunOptions : CommonOptions
{
    [Value(0, MetaName = "lab", HelpText = "Lab to run (lab1, lab2, or lab3).", Required = true)]
    public string Lab { get; set; }
}

[Verb("set-path", HelpText = "Set LAB_PATH environment variable.")]
public class SetPathOptions
{
    [Option('p', "path", Required = true, HelpText = "Path to set for LAB_PATH.")]
    public string Path { get; set; }
}

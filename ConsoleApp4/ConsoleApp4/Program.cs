using System;
using CommandLine;
using LabsLibrary;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("--version"))
            {
                DisplayVersion();
                return;
            }

            if (args.Length > 0)
            {
                HandleCommands(args);
                return;
            }
            

            Console.WriteLine("ConsoleApp4 Interactive Mode. Type 'exit' to quit.");
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (input == "--version" || input == "version")
                {
                    DisplayVersion();
                    continue;
                }


                if (string.IsNullOrWhiteSpace(input)) continue;

                var inputArgs = input.Split(' ');



                if (inputArgs[0].ToLower() == "exit") break;

                HandleCommands(inputArgs);
            }
        }

        static void HandleCommands(string[] args)
        {
            Parser.Default.ParseArguments<VersionOptions, RunOptions, SetPathOptions>(args)
            .MapResult(
                (VersionOptions opts) => DisplayVersion(),
                (RunOptions opts) => RunLab(opts),
                (SetPathOptions opts) => SetPath(opts),
                errs => CheckForVersionError(errs)
);

        }


        static int DisplayVersion()
        {
            Console.WriteLine("Аutor: Yeremenko Sofiia");
            Console.WriteLine("Version: 1.0");
            return 0;
        }

        static int CheckForVersionError(IEnumerable<Error> errs)
        {
            if (errs.Any(err => err is VersionRequestedError))
            {
                return 0;  
            }
            else
            {
                HandleParseError(errs);
                return 1;
            }
        }

        static int HandleParseError(IEnumerable<Error> errs)
        {
            Console.WriteLine("Error with reading of command");
            foreach (var error in errs)
            {
                Console.WriteLine($"- {error.Tag}");
            }
            return 1;
        }


        static int RunLab(RunOptions opts)
        {
            var labPath = Environment.GetEnvironmentVariable("LAB_PATH");
            if (string.IsNullOrEmpty(labPath))
            {
                Console.WriteLine("Changing path to LAB_PATH did not complete.");
                return 1;
            }
            var inputPath = Path.Combine(labPath, opts.InputFile);
            var outputPath = Path.Combine(labPath, opts.OutputFile);

            switch (opts.Lab)
            {
                case "lab1":
                    LabsLibrary.LabWork.RunLab1(inputPath, outputPath);
                    break;
                case "lab2":
                    LabsLibrary.LabWork.RunLab2(inputPath, outputPath);
                    break;
                case "lab3":
                    LabsLibrary.LabWork.RunLab3(inputPath, outputPath);
                    break;

                default:
                    Console.WriteLine("Unknown command. Use lab1, lab2 or lab3.");
                    return 1;
            }
            return 0;
        }



        static int SetPath(SetPathOptions opts)
        {
            Environment.SetEnvironmentVariable("LAB_PATH", opts.Path, EnvironmentVariableTarget.User);
            Console.WriteLine($"Path variable LAB_PATH changed to {opts.Path}");
            return 0;
        }
    }
}

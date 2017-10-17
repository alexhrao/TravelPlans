using System;
using Converter.Data;
using Converter.Utility;
using System.IO;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            Process(args);
            Console.WriteLine("Finished. Press any key to exit...");
            Console.ReadKey();
        }

        static void Process(string[] args)
        {
            foreach (string path in args)
            {
                FileAttributes attributes = File.GetAttributes(path);
                if (attributes.HasFlag(FileAttributes.Directory))
                {
                    // Search directory for all files, call ourself with these.
                    args = Directory.GetFiles(path, "Details.txt", SearchOption.AllDirectories);
                    Process(args);
                }
                else
                {
                    Console.WriteLine("Processing File " + Path.GetFullPath(path) + ":");
                    Console.WriteLine("\tParsing...");
                    Parser parser = new Parser(path);
                    Console.WriteLine("\tWriting...");
                    Writer writer = new Writer(path, parser);
                }
            }
        }
    }
}

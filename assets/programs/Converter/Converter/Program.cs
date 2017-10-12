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
            // Arguments will be file paths.
            // if directory, search for ALL Details.txt
            foreach (string path in args)
            {
                FileAttributes attributes = File.GetAttributes(path);
                if (attributes.HasFlag(FileAttributes.Directory))
                {
                    // Search directory for all files, call ourself with these.
                    args = Directory.GetFiles(path, "Details.txt", SearchOption.AllDirectories);
                    Main(args);
                }
                Console.WriteLine("Processing File " + path + ":");
                Console.WriteLine("\tParsing...");
                Parser parser = new Parser(path);
                Console.WriteLine("\tWriting...");
                Writer writer = new Writer(path, parser);
            }
            Console.WriteLine("Finished. Press any key to exit...");
            Console.ReadKey();
        }
    }
}

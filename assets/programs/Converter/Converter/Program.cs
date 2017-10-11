using System;
using Converter.Data;
using Converter.Utility;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[1];
            args[0] = "C:/Users/alexhrao/Documents/TravelPlans/trips/07/Details.txt";
            // Arguments will be file paths.
            foreach (string path in args)
            {
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

using System;

namespace Converter
{
    namespace Program
    {
         public class Converter
        {
            // Read in file, parse, write HTML
            public static void Main(string[] args)
            {
                // Arguments will be file paths.
                foreach (string path: args)
                {
                    // Parse file
                    Parser parser = new Parser(path);
                }
            }
        }   
    }
}
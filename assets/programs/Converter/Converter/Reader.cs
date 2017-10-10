using System;
using System.Collections.Generic;
using System.IO;

namespace Converter
{
    namespace Utility
    {
        public class Reader
        {
            public List<string> Lines = new List<string>();
            public Reader(string path)
            {
                foreach (string Line in File.ReadAllLines(path))
                {
                    Lines.Add(Line);
                }
            }
        }
    }
}
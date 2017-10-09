using System;

namespace Converter
{
    namespace Utility
    {
        public class Reader
        {
            public Path FilePath
            {
                get;
                private set;
            }
            public List<String> Lines = new List<>();
            
            public Reader(string path)
            {
                this.FilePath = new Path(path);
                foreach (string Line: File.ReadToEnd(path))
                {
                    Lines.Add(Line);
                }
            }
        }
    }
}
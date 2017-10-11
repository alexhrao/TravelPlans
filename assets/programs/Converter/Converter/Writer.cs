using System;
using HtmlAgilityPack;
using Converter.Properties;
using System.IO;
namespace Converter
{
    namespace Utility
    {
        public class Writer
        {
            public Writer(string filePath, Parser parser)
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(Resources.template);
                // Write trains first:
                

                string newPath = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar + "Summary.html";
                File.WriteAllText(newPath, document.DocumentNode.OuterHtml);
            }
        }
    }
}
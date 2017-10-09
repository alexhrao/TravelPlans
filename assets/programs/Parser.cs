using System;
using Convert.Data;
namespace Converter
{
    namespace Utility
    {
        public class Parser
        {
            private Reader Input;
            private Writer Output;
            public readonly List<ITransport> Transportation = new List<>();
            public readonly List<ILodging> Lodgings = new List<>();
            public Parser(String filePath)
            {
                Input = new Reader(filePath);
                // Look for Transportation
                
                
            }
            
            
        }
    }
}
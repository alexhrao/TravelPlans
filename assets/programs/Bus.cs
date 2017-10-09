using System;
using Convert.Data;
namespace Converter
{
    namespace Utility
    {
        public class Parser
        {
            private Reader Input;
            public readonly List<Train> Trains = new List<>();
            
            
            public Parser(String filePath)
            {
                Input = new Reader(filePath);
            }
            
            
        }
    }
}
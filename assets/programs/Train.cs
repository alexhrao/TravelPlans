using System;

namespace Converter
{
    namespace Data
    {
        public class Train : ITransport
        {
            public DateTime DepartTime = DateTime.MinValue;
            public DateTime ArriveTime = DateTime.MinValue;
            public string DepartStation = "";
            public string ArriveStation = "";
        }
    }
}
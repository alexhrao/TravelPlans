using System;
using System.IO;

namespace Converter
{
    namespace Utility
    {
        public class Transport
        {
            public DateTime DepartTime;
            public DateTime ArriveTime;
            public string DepartStation = "";
            public string ArriveStation = "";
            public bool IsReservation = false;
        }
    }
}
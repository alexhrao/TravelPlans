using System;

namespace Converter
{
    namespace Utility
    {
        public interface ITransport
        {
            public DateTime DepartTime;
            public DateTime ArriveTime;
            public string DepartStation;
            public string ArriveStation;
        }
    }
}
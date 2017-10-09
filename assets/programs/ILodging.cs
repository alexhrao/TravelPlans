using System;

namespace Converter
{
    namespace Utility
    {
        public interface ILodging
        {
            public DateTime ArriveTime;
            public DateTime DepartTime;
            
            public String Name;
            public String Address;
            public String Phone;
            public long Price;
            public String RoomType;
        }
    }
}
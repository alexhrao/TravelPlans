﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Converter
{
    namespace Utility
    {
        public class Trip
        {
            public string Name;
            public List<Transport> Transports = new List<Transport>();

            public Trip(string name)
            {
                this.Name = name;
            }

        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxValue.Interfaces
{
    internal interface IPropertiesProvider
    {
        string GetName();
        float GetValue();
    }
}

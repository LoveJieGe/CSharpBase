﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter30Lib_CalculatorContract
{
    public interface IOperation
    {
        string Name { get; }
        int NumberOperands { get; }
    }
}

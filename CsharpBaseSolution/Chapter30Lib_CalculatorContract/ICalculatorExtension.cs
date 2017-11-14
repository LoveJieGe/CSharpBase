using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chapter30Lib_CalculatorContract
{
    public interface ICalculatorExtension
    {
        FrameworkElement UI { get; }
    }
}

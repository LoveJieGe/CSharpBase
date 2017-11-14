using Chapter30_CalculatorUtils;
using Chapter30Lib_CalculatorContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chapter30_FuelEconomy
{
    [CalculatorExtensionExport(typeof(ICalculatorExtension),
        Title = "燃油经济性", 
        Description = "计算燃油经济性",
        ImageUrl= "Fuel.png")]
    public class FuelCalculatorExtension : ICalculatorExtension
    {
        private FrameworkElement control;
        public FrameworkElement UI
        {
            get
            {
                return control??(control = new FuelEconomyUC());
            }
        }
    }
}

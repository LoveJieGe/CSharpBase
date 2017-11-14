using Chapter30_CalculatorUtils;
using Chapter30Lib_CalculatorContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalculator
{
    public class CalculatorImport : IPartImportsSatisfiedNotification
    {
        public event EventHandler<ImportEventArgs> ImportsSatisfied;

        [ImportMany]
        public Lazy<ICalculator> Calculature { get; set; }
        public void OnImportsSatisfied()
        {
            ImportsSatisfied?.Invoke(this, new ImportEventArgs() { StatusMessage = "ICalculator导入成功" });
        }
    }
}

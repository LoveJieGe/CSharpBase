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
    /// <summary>
    /// 导入连接
    /// </summary>
    public class CalculatorExtensionImport : IPartImportsSatisfiedNotification
    {
        public event EventHandler<ImportEventArgs> ImportsSatisfied;
        [ImportMany(AllowRecomposition =true)]
        public IEnumerable<Lazy<ICalculatorExtension, ICalculatorExtensionMetadata>> CalculatorExtensions { get; set; }
        public void OnImportsSatisfied()
        {
            ImportsSatisfied?.Invoke(this, new ImportEventArgs() { StatusMessage = "ICalculatorExtension导入成功!" });
        }
    }
}

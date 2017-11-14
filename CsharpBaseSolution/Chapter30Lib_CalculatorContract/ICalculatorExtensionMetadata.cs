using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter30Lib_CalculatorContract
{
    /// <summary>
    /// 元数据
    /// </summary>
    public interface ICalculatorExtensionMetadata
    {
        string Title { get; }
        string Description { get; }
        string ImageUrl { get; }
    }
}

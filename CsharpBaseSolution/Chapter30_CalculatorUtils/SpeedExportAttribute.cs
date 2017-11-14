using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter30_CalculatorUtils
{
    public enum Speed
    {
        Fast,
        Slow
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class SpeedExportAttribute : ExportAttribute
    {
        public SpeedExportAttribute(string contractName, Type contractType)
            : base(contractName, contractType) { }

        public Speed Speed { get; set; }
    }
}

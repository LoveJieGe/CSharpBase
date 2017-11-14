using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace Chapter30_CalculatorUtils
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class CalculatorExtensionExportAttribute: ExportAttribute
    {
        public CalculatorExtensionExportAttribute(Type constractType)
            :base(constractType)
        { }
        public CalculatorExtensionExportAttribute()
            : base()
        { }
        public CalculatorExtensionExportAttribute(string constractName,Type constractType)
            : base(constractName,constractType)
        { }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}

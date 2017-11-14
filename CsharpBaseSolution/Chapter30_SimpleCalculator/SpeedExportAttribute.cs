using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter30_SimpleCalculator
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class SpeedExportAttribute:ExportAttribute
    {
        public SpeedExportAttribute(string constractName,Type constractType)
            :base(constractName,constractType)
        { }
        public Speed Speed { get; set; }
    }
    public enum Speed
    {
        Fast,
        Slow
    }
    public interface ISpeedCapabilities
    {
        Speed Speed { get; set; }
    }
}

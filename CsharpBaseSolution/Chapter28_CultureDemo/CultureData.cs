using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter28_CultureDemo
{
    public class CultureData
    {
        public CultureInfo CultureInfo { get; set; }
        public List<CultureData> SubCultures { get; set; }
        double numberSample = 9876543.21;
        public string NumberSample
        {
            get { return numberSample.ToString("N", CultureInfo); }
        }
        public string DateSample
        {
            get { return DateTime.Now.ToString("D", CultureInfo); }
        }
        public string TimeSample
        {
            get { return DateTime.Now.ToString("T", CultureInfo); }
        }
        public RegionInfo RegionInfo
        {
            get
            {
                RegionInfo region;
                try
                {
                    region = new RegionInfo(CultureInfo.Name);
                }
                catch(ArgumentException e)
                {
                    return null;
                }
                return region;
            }
        }
       
    }
}

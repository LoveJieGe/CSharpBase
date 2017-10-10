using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11_ProCsharp.ProCsharp.Linq
{
    [Serializable]
    public class Team
    {
        /// <summary>
        /// 车队冠军的名字
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 获取冠军的年份
        /// </summary>
        public IEnumerable<int> Years { get; private set; }
        public Team(string name,params int[] years)
        {
            this.Name = name;
            this.Years = new List<int>(years);
        }
    }
}

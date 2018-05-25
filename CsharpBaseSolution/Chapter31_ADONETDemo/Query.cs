using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter31_ADONETDemo
{
    public class Query
    {
        private int CalcGroupLevel(DataTable sourceTable, DataTable targetTable
            , int currRowIndex, int currGroupIndex, QueryCalcInfo calcInfo)
        {
            //if (IsLeaf(groupList, currGroupIndex))
            //{
            //    currRowIndex = CalcCurrRow(sourceTable, targetTable, currRowIndex, calcInfo, columnList);
            //}
            //else
            //{
            //    //如果需要累计行,则初始化累计行数据
            //    DataRow statRow = targetTable.NewRow();
            //    QueryCalcInfo lastInfo = null;
            //    //对源数据表格中的每一个相同主键的行数据进行处理
            //    while (currRowIndex < sourceTable.Rows.Count && (calcInfo == null || calcInfo.IsSameKey(sourceTable.Rows[currRowIndex])))
            //    {
            //        //创建合计行
            //        statRow = targetTable.NewRow();
            //        DataRow currRow = sourceTable.Rows[currRowIndex];
            //        //创建计算规则
            //        QueryCalcInfo newInfo = QueryCalcInfo.CreateCalcInfo(groupInfo, calcInfo, lastInfo, currRow, currRowIndex);
            //        QueryGroupCalcEventHandler handler = delegate (DataRow tempRow, int tempRowIndex)
            //        {
            //            CalcGroupRow(sourceTable, tempRow, tempRowIndex, statRow, sumRow, groupInfo, newInfo);
            //        };
            //        newInfo.HandlerList.Add(handler);
            //        //递归调用子层分组方法
            //        currRowIndex = CalcGroupLevel(sourceTable, targetTable, columnList, groupList, currRowIndex, currGroupIndex + 1, newInfo);
            //        if (statRow != null)
            //        {
            //            DataRow tempRow = targetTable.NewRow();
            //            foreach (DataColumn column in targetTable.Columns)
            //                tempRow[column.ColumnName] = statRow[column.ColumnName];
            //            targetTable.Rows.Add(tempRow);
            //        }
            //        lastInfo = newInfo;
            //    }
            //}
            //return currRowIndex;
            return 0;
        }
    }

    internal class QueryCalcInfo
    {
        private Dictionary<string, object> keyDict = new Dictionary<string, object>();

        private int statFirstRowIndex;
        public int StatFirstRowIndex
        {
            get { return statFirstRowIndex; }
        }

        private int sumFirstRowIndex;
        public int SumFirstRowIndex
        {
            get { return sumFirstRowIndex; }
        }

        private List<object> statList = new List<object>();
        public List<object> StatList
        {
            get { return statList; }
        }

        private List<object> sumList = new List<object>();
        public List<object> SumList
        {
            get { return sumList; }
        }

        private List<QueryGroupCalcEventHandler> handlerList = new List<QueryGroupCalcEventHandler>();
        public List<QueryGroupCalcEventHandler> HandlerList
        {
            get { return handlerList; }
        }

        public QueryCalcInfo(int statFirstRowIndex, int sumFirstRowIndex, List<object> statList)
        {
            this.statFirstRowIndex = statFirstRowIndex;
            this.sumFirstRowIndex = sumFirstRowIndex;
            this.statList = statList;
        }
        //判断是否有相同主键
        public bool IsSameKey(DataRow currRow)
        {
            foreach (string key in keyDict.Keys)
            {
                if (!object.Equals(currRow[key], keyDict[key]))
                    return false;
            }
            return true;
        }
        //调用委托计算分组行
        public void CalcGroup(DataRow currRow, int currRowIndex)
        {
            foreach (QueryGroupCalcEventHandler handler in handlerList)
                handler(currRow, currRowIndex);
        }

        public static QueryCalcInfo CreateCalcInfo(QueryCalcInfo calcInfo, QueryCalcInfo lastInfo, DataRow currRow, int currRowIndex)
        {
            int statRowIndex = currRowIndex;
            List<object> statList = new List<object>();
            QueryCalcInfo newInfo = new QueryCalcInfo(statRowIndex, currRowIndex, statList);

            if (calcInfo != null)
                foreach (string key in calcInfo.keyDict.Keys)
                    newInfo.keyDict[key] = calcInfo.keyDict[key];
            if (calcInfo != null)
                newInfo.handlerList.AddRange(calcInfo.handlerList);
            return newInfo;
        }
    }

    internal delegate void QueryGroupCalcEventHandler(DataRow currRow, int currRowIndex);
}

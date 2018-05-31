using Chapter31Lib_ADO.NET;
using Newtonsoft.Json;
using Pushsoft.Erp.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter31_ADONETDemo
{
    public class Test
    {
        #region 明细账
        /// <summary>
        /// 明细账
        /// </summary>
        public static void Test1()
        {
            Stopwatch watch = new Stopwatch();

            using (DbConnection conn = SqlHelper.GetConnection("ConnectionString"))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    watch.Start();
                    cmd.CommandText = string.Format(@"
select TM.* From[dbo].[V1GetAccDetailFC]('201801','201805','1','9',1,9,'N','Y')TM
");
                    cmd.Connection = conn as SqlConnection;
                    cmd.CommandTimeout = 60000;
                    conn.Open();
                    DataTable table = ExecuteDataTable(cmd);
                    watch.Stop();
                    Console.WriteLine("数据{0},数据库操作时间：毫秒:{1}，秒：{2}" ,table.Rows.Count, watch.ElapsedMilliseconds, watch.ElapsedMilliseconds / 1000);
                    Stopwatch watch2 = new Stopwatch();
                    watch2.Start();
                    DataTable newTable = HandleDetailAccYear(table, true);
                    watch2.Stop();
                    Console.WriteLine("数据{0},遍历操作时间：毫秒：{1}，秒：{2}", newTable.Rows.Count, watch2.ElapsedMilliseconds, watch2.ElapsedMilliseconds / 1000);
                    Stopwatch watch3 = new Stopwatch();
                    watch3.Start();
                    string str = JsonConvert.SerializeObject(newTable);
                    Console.WriteLine("数据{0},序列化操作时间：毫秒：{1}，秒：{2}", newTable.Rows.Count, watch3.ElapsedMilliseconds, watch3.ElapsedMilliseconds / 1000);

                }
                conn.Close();
            }
        }
        public static DataTable ExecuteDataTable(SqlCommand cmd)
        {
            DataTable table = new DataTable();
            SqlDataAdapter adaper = new SqlDataAdapter(cmd);
            adaper.Fill(table);
            return table;
        }
        /// <summary>
        /// 明细账
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isCurrency"></param>
        /// <returns></returns>
        private static DataTable HandleDetailAccYear(DataTable dt, bool isCurrency)
        {
            DataTable newdt = dt.Copy();
            //dt.PrimaryKey = new DataColumn[] {
            //    dt.Columns["RowNumber"],
            //};
            if (dt.Rows.Count > 0)
            {
                int len = dt.Rows.Count, i = 0, count = 0;
                while (i < len)
                {
                    DataRow itemRow = dt.Rows[i];
                    object flag = itemRow["Flag"];
                    if (flag != null && !Helper.AreEqual(flag.ToString(), "1"))
                    {
                        object year = itemRow["Y"],
                          month = itemRow["M"],
                          day = itemRow["D"],
                          accID = itemRow["AccID"],
                          accName = itemRow["AccName"];
                        string selectfilter = "AccID='" + accID + "' AND Y='" + year + "' AND M = " + month;
                        if (!isCurrency)
                        {
                            selectfilter = "AccID='" + accID + "' AND Y='" + year + "' AND M = " + month + " AND CurrID='" + itemRow["CurrID"] + "'";
                        }
                        DataRow[] selectRow = dt.Select(selectfilter);
                        if (selectRow.Length > 0)
                        {
                            DataRow lastRow = selectRow[selectRow.Length - 1];
                            int index = dt.Rows.IndexOf(lastRow);
                            string bqfilter = "AccID='" + accID + "' AND Y='" + year + "' AND M = " + month + "  AND D <= " + lastRow["D"];
                            string yearfilter = "AccID='" + accID + "' AND Y='" + year + "' AND M <= " + month;
                            if (!isCurrency)
                            {
                                bqfilter = "AccID='" + accID + "' AND Y='" + year + "' AND M = " + month + "  AND D <= " + lastRow["D"] + " AND CurrID='" + lastRow["CurrID"] + "'";
                                yearfilter = "AccID='" + accID + "' AND Y='" + year + "' AND M <= " + month + " AND CurrID='" + lastRow["CurrID"] + "'";
                            }
                            //计算
                            DataRow[] bqTempRows = dt.Select(bqfilter);
                            DataRow[] yearTempRows = dt.Select(yearfilter);
                            double bqDebitLC = 0, bqDebitQty = 0, bqCreditLC = 0, bqCreditQty = 0, bqCreditFC = 0, bqDebitFC = 0;//本期合计
                            double yearDebitLC = 0, yearDebitQty = 0, yearCreditLC = 0, yearCreditQty = 0, yearCreditFC = 0, yearDebitFC = 0;//本期合计
                            //本期合计
                            foreach (DataRow item in bqTempRows)
                            {
                                bqDebitLC += ToDouble(item["BQDebitLC"]);
                                bqDebitQty += ToDouble(item["BQDebitQty"]);
                                bqCreditLC += ToDouble(item["BQCreditLC"]);
                                bqCreditQty += ToDouble(item["BQCreditQty"]);
                                //外币
                                if (!isCurrency)
                                {
                                    bqCreditFC += ToDouble(item["BQCreditFC"]);
                                    bqDebitFC += ToDouble(item["BQDebitFC"]);

                                }
                            }
                            //本年累计
                            foreach (DataRow item2 in yearTempRows)
                            {
                                yearDebitLC += ToDouble(item2["YearDebitLC"]);
                                yearDebitQty += ToDouble(item2["YearDebitQty"]);
                                yearCreditLC += ToDouble(item2["YearCreditLC"]);
                                yearCreditQty += ToDouble(item2["YearCreditQty"]);
                                //外币
                                if (!isCurrency)
                                {
                                    yearCreditFC += ToDouble(item2["YearCreditFC"]);
                                    yearDebitFC += ToDouble(item2["YearDebitFC"]);
                                }
                            }
                            DataRow newRow1 = newdt.NewRow();//本期合计
                            DataRow newRow2 = newdt.NewRow();//本年累计
                            //本期合计
                            newRow1.ItemArray = bqTempRows[bqTempRows.Length - 1].ItemArray;
                            newRow1["Flag"] = "4";
                            newRow1["D"] = DBNull.Value;
                            newRow1["VouSeriesName"] = DBNull.Value;
                            newRow1["SumInfo"] = "本期合计";
                            newRow1["CurDebitLC"] = bqDebitLC;
                            newRow1["CurDebitQty"] = bqDebitQty;
                            newRow1["CurCreditLC"] = bqCreditLC;
                            newRow1["CurCreditQty"] = bqCreditQty;
                            //本年累计
                            newRow2.ItemArray = yearTempRows[yearTempRows.Length - 1].ItemArray;
                            newRow2["Flag"] = "5";
                            newRow2["D"] = DBNull.Value;
                            newRow2["VouSeriesName"] = DBNull.Value;
                            newRow2["SumInfo"] = "本年累计";
                            newRow2["CurDebitLC"] = yearDebitLC;
                            newRow2["CurDebitQty"] = yearDebitQty;
                            newRow2["CurCreditLC"] = yearCreditLC;
                            newRow2["CurCreditQty"] = yearCreditQty;
                            if (!isCurrency)
                            {
                                //本期合计
                                newRow1["CurCreditFC"] = bqCreditFC;
                                newRow1["CurDebitFC"] = bqDebitFC;
                                //本年累计
                                newRow2["CurCreditFC"] = yearCreditFC;
                                newRow2["CurDebitFC"] = yearDebitFC;
                            }
                            //本期合计
                            newdt.Rows.InsertAt(newRow1, index + 1 + 2 * count);
                            //本年累计
                            newdt.Rows.InsertAt(newRow2, index + 2 + 2 * count);
                            i = i + selectRow.Length;
                            count++;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return newdt;
        }
        #endregion

        #region 总账
        public static void Test2()
        {
            Stopwatch watch = new Stopwatch();

            using (DbConnection conn = SqlHelper.GetConnection("ConnectionString"))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    watch.Start();
                    cmd.CommandText = string.Format(@"
select  ROW_NUMBER() 
OVER (Order by S.AccID,S.CurrID,S.AbsID,S.Flag)AS RowNum,S.* from 
(select TM.AccID,TM.AccName,TM.CurrID,TM.Flag,TM.AbsID,TM.FZAbsID,TM.Y,TM.SumInfo,
TM.CurDebitLC,TM.CurDebitFC,TM.CurDebitQty,TM.CurCreditLC,TM.CurCreditFC,TM.CurCreditQty,
TM.YearDebitLC,TM.YearDebitFC,TM.YearDebitQty,YearCreditLC,YearCreditFC,YearCreditQty,
BalanceDirectLC,BalanceDirectFC,BalanceDirectQty,
BalanceAccDirect,BalanceAccDirectFC,BalanceAccDirectQty,
LastBalanceLC,LastBalanceFC,LastBalanceQty,
NLevel,AccDirect,LastDirect,Price,
(case when TM.Flag=1 then 0 
      when TM.Flag=4 and TM.CurDebitLC=0 and TM.CurCreditLC=0 then 0 else 1 end) as VCount,
(case when TM.FLag=1 then TM.AccID else '' end) as AccID1,
(case when TM.FLag=1 then TM.AccName else '' end) as AccName1,
abs(LastBalanceLC) as LastBalanceLC1,abs(LastBalanceFC) as LastBalanceFC1,abs(LastBalanceQty) as LastBalanceQty1
from [dbo].[V1GetAccGLFC]('201501','201812','1','9','1','9','N','Y') TM
) S 
");
                    cmd.Connection = conn as SqlConnection;
                    cmd.CommandTimeout = 60000;
                    conn.Open();
                    DataTable table = ExecuteDataTable(cmd);
                    watch.Stop();
                    Console.WriteLine("从数据库取出数据{0}条", table.Rows.Count);
                    Stopwatch watch2 = new Stopwatch();
                    watch2.Start();
                    DataTable newTable = HandleAccYear7(table,true);
                    watch2.Stop();
                    Console.WriteLine("数据{0},遍历操作时间：毫秒：{1}，秒：{2}", newTable.Rows.Count, watch2.ElapsedMilliseconds, watch2.ElapsedMilliseconds / 1000);
                }
                conn.Close();
            }
        }


        private static DataTable HandleAccYear7(DataTable dt, bool isCurrency)
        {
            DataTable newdt = dt.Clone();
            dt.PrimaryKey = new DataColumn[] {
                dt.Columns["AccID"],
                dt.Columns["Flag"],
                dt.Columns["AbsID"],
                dt.Columns["RowNum"],
            };
            if (dt.Rows.Count > 0)
            {
                object flag = null;
                foreach (DataRow row in dt.Rows)
                {
                    flag = row["Flag"];
                    if (flag != null && !Helper.AreEqual(flag.ToString(), "1"))
                    {

                        DataRow newRow = newdt.NewRow();
                        DataRow sourceRow = newdt.NewRow();
                        sourceRow.ItemArray = row.ItemArray;
                        newRow.ItemArray = row.ItemArray;
                        string accID = row["AccID"].ToString(),
                            year = row["Y"].ToString(),
                            absID = row["AbsID"].ToString();
                        newRow["Flag"] = "5";
                        newRow["SumInfo"] = "本年累计";
                        string filter = "AccID='" + accID + "' AND Y='" + year + "' AND AbsID <= " + absID;
                        if (!isCurrency)
                        {
                            filter = "AccID='" + accID + "'AND CurrID='" + row["CurrID"] + "' AND Y='" + year + "' AND AbsID <= " + absID;
                        }
                        DataRow[] selectRow = dt.Select(filter);
                        double debitLC = 0, debitQty = 0, creditLC = 0, creditQty = 0, debitFC = 0, creditFC = 0;
                        foreach (DataRow item in selectRow)
                        {
                            debitLC += ToDouble(item["YearDebitLC"]);
                            debitQty += ToDouble(item["YearDebitQty"]);
                            creditLC +=ToDouble(item["YearCreditLC"]);
                            creditQty += ToDouble(item["YearCreditQty"]);
                            if (!isCurrency)
                            {
                                debitFC += ToDouble(item["YearDebitFC"]);
                                creditFC += ToDouble(item["YearCreditFC"]);
                            }
                        }
                        newRow["CurDebitLC"] = debitLC;
                        newRow["CurDebitQty"] = debitQty;
                        newRow["CurCreditLC"] = creditLC;
                        newRow["CurCreditQty"] = creditQty;
                        //newRow["CurDebitLC"] = dt.Compute("Sum(YearDebitLC)", filter);
                        //newRow["CurDebitQty"] = dt.Compute("Sum(YearDebitQty)", filter);
                        //newRow["CurCreditLC"] = dt.Compute("Sum(YearCreditLC)", filter);
                        //newRow["CurCreditQty"] = dt.Compute("Sum(YearCreditQty)", filter);
                        if (!isCurrency)
                        {
                            //newRow["CurCreditFC"] = dt.Compute("Sum(YearCreditFC)", filter);
                            //newRow["CurDebitFC"] = dt.Compute("Sum(YearDebitFC)", filter);
                            newRow["CurCreditFC"] = creditFC;
                            newRow["CurDebitFC"] = debitFC;
                        }
                        newdt.Rows.Add(sourceRow);
                        newdt.Rows.Add(newRow);
                    }
                    else
                    {
                        DataRow sourceRow = newdt.NewRow();
                        sourceRow.ItemArray = row.ItemArray;
                        newdt.Rows.Add(sourceRow);
                    }
                }
            }
            return newdt;
        }

        private static DataTable CopyDataTable(DataTable sourceTable)
        {
            DataTable targetTable = new DataTable(sourceTable.TableName);
            foreach (DataColumn column in sourceTable.Columns)
            {
                DataColumn temp = targetTable.Columns.Add(column.ColumnName, column.DataType);
                temp.DefaultValue = column.DefaultValue;
            }
            return targetTable;
        }
        public static DataTable HandleAccYear6(DataTable dt)
        {
            DataTable newdt = dt.Copy();
            dt.PrimaryKey = new DataColumn[] {
                dt.Columns["AccID"],
                dt.Columns["Flag"],
                dt.Columns["AbsID"],
                dt.Columns["CurrID"],
                dt.Columns["RowNumber"],
            };
            DataRow[] rows = dt.Select("Flag <> 1");
            foreach (DataRow row in rows)
            {
                DataRow newRow = newdt.NewRow();
                string accID = row["AccID"].ToString(),
                           year = row["Y"].ToString(),
                           absID = row["AbsID"].ToString();
                string filter = "AccID='" + accID + "' AND Y='" + year + "' AND AbsID <= " + absID;
                int index = dt.Rows.IndexOf(row);
                newRow.ItemArray = row.ItemArray;
                newRow["Flag"] = "5";
                newRow["SumInfo"] = "本年累计";
                DataRow[] selectRow = dt.Select(filter);
                double debitlc = 0, debieQty = 0, creditlc = 0, creditQty = 0;
                foreach (DataRow item in selectRow)
                {

                    debitlc += ToDouble(item["YearDebitLC"]);
                    debieQty += ToDouble(item["YearDebitQty"]);
                    creditlc += ToDouble(item["YearCreditLC"]);
                    creditQty += ToDouble(item["YearCreditQty"]);
                }
                newRow["CurDebitLC"] = debitlc;
                newRow["CurDebitQty"] = debieQty;
                newRow["CurCreditLC"] = creditlc;
                newRow["CurCreditQty"] = creditQty;
                newdt.Rows.InsertAt(newRow, index + 1);
            }
            return newdt;
        }
        private static DataTable HandleAccYear5(DataTable dt, bool isCurrency)
        {

            DataTable newdt = dt.Clone();
            int len = dt.Rows.Count,i = 0;
            int count = 0;
            while (i < len)
            {
                DataRow row = dt.Rows[i];
                if (row["Flag"] != null && !Helper.AreEqual(row["Flag"].ToString(), "1"))
                {
                    DataRow newRow = newdt.NewRow();
                    DataRow sourceRow = newdt.NewRow();
                    sourceRow.ItemArray = row.ItemArray;
                    newRow.ItemArray = row.ItemArray;
                    string accID = row["AccID"].ToString(),
                        year = row["Y"].ToString(),
                        month = row["AbsID"].ToString();
                    newRow["Flag"] = "5";
                    newRow["SumInfo"] = "本年累计";
                    string filter = "AccID='" + accID + "' AND Y='" + year + "' AND AbsID <= " + month;
                    if (!isCurrency)
                    {
                        filter = "AccID='" + accID + "'AND CurrID='" + row["CurrID"] + "' AND Y='" + year + "' AND AbsID <= " + month;
                    }
                    DataRow[] selectRow = dt.Select(filter);
                    double debitlc = 0, debieQty = 0, creditlc = 0, creditQty = 0;
                    foreach (DataRow item in selectRow)
                    {

                        debitlc += ToDouble(item["YearDebitLC"]);
                        debieQty += ToDouble(item["YearDebitQty"]);
                        creditlc += ToDouble(item["YearCreditLC"]);
                        creditQty += ToDouble(item["YearCreditQty"]);
                    }
                    newRow["CurDebitLC"] = debitlc;
                    newRow["CurDebitQty"] = debieQty;
                    newRow["CurCreditLC"] = creditlc;
                    newRow["CurCreditQty"] = creditQty;
                    newdt.Rows.Add(sourceRow);
                    newdt.Rows.Add(newRow);
                }
                else {

                }
            }
            return newdt;
        }
        /// <summary>
        /// 总账
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isCurrency"></param>
        /// <returns></returns>
        private static DataTable HandleAccYear4(DataTable dt, bool isCurrency)
        {
            DataTable newdt = dt.Clone();
            dt.PrimaryKey = new DataColumn[] {
                dt.Columns["AccID"],
                dt.Columns["Flag"],
                dt.Columns["AbsID"]
            };
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Flag"] != null && !Helper.AreEqual(row["Flag"].ToString(), "1"))
                    {

                        DataRow newRow = newdt.NewRow();
                        DataRow sourceRow = newdt.NewRow();
                        sourceRow.ItemArray = row.ItemArray;
                        newRow.ItemArray = row.ItemArray;
                        string accID = row["AccID"].ToString(),
                            year = row["Y"].ToString(),
                            month = row["AbsID"].ToString();
                        newRow["Flag"] = "5";
                        newRow["SumInfo"] = "本年累计";
                        string filter = "AccID='" + accID + "' AND Y='" + year + "' AND AbsID <= " + month;
                        if (!isCurrency)
                        {
                            filter = "AccID='" + accID + "'AND CurrID='" + row["CurrID"] + "' AND Y='" + year + "' AND AbsID <= " + month;
                        }
                        DataRow[] selectRow = dt.Select(filter);
                        double debitlc = 0, debieQty = 0, creditlc = 0, creditQty = 0;
                        foreach (DataRow item in selectRow)
                        {

                            debitlc += ToDouble(item["YearDebitLC"]);
                            debieQty += ToDouble(item["YearDebitQty"]);
                            creditlc += ToDouble(item["YearCreditLC"]);
                            creditQty += ToDouble(item["YearCreditQty"]);
                        }
                        newRow["CurDebitLC"] = debitlc;
                        newRow["CurDebitQty"] = debieQty;
                        newRow["CurCreditLC"] = creditlc;
                        newRow["CurCreditQty"] = creditQty;
                        newdt.Rows.Add(sourceRow);
                        newdt.Rows.Add(newRow);
                    }
                    else
                    {
                        DataRow sourceRow = newdt.NewRow();
                        sourceRow.ItemArray = row.ItemArray;
                        newdt.Rows.Add(sourceRow);
                    }
                }
            }
            return newdt;
        }
        #endregion
        private static DataTable HandleAccYear3(DataTable dt, bool isCurrency)
        {
            DataTable newdt = dt.Copy();
            DataRow[] rows = dt.Select("Flag <> 1");
            foreach (DataRow row in rows)
            {
                DataRow newRow = newdt.NewRow();
                string accID = row["AccID"].ToString(),
                           year = row["Y"].ToString(),
                           absID = row["AbsID"].ToString();
                string filter = "AccID='" + accID + "' AND Y='" + year + "' AND AbsID <= " + absID;
                int index = dt.Rows.IndexOf(row);
                newRow.ItemArray = row.ItemArray;
                newRow["Flag"] = "5";
                newRow["SumInfo"] = "本年累计";
                DataRow[] selectRow = dt.Select(filter);
                double debitlc = 0, debieQty = 0, creditlc = 0, creditQty = 0;
                foreach (DataRow item in selectRow)
                {

                    debitlc += ToDouble(item["YearDebitLC"]);
                    debieQty += ToDouble(item["YearDebitQty"]);
                    creditlc += ToDouble(item["YearCreditLC"]);
                    creditQty += ToDouble(item["YearCreditQty"]);
                }
                newRow["CurDebitLC"] = debitlc;
                newRow["CurDebitQty"] = debieQty;
                newRow["CurCreditLC"] = creditlc;
                newRow["CurCreditQty"] = creditQty;
                newdt.Rows.InsertAt(newRow, index + 1);
            }
            return newdt;
        }
        public static double ToDouble(object value)
        {
            if (value == null||value==DBNull.Value) return 0;
            return Convert.ToDouble(value);
        }
        private static DataTable HandleAccYear2(DataTable dt, bool isCurrency)
        {
            DataTable newdt = dt.Copy();
            DataRow[] rows  = dt.Select("Flag <> 1");
            foreach (DataRow row in rows)
            {
                DataRow newRow = newdt.NewRow();
                string accID = row["AccID"].ToString(),
                           year = row["Y"].ToString(),
                           absID = row["AbsID"].ToString();
                string filter = "AccID='" + accID + "' AND Y='" + year + "' AND AbsID <= " + absID;
                int index = dt.Rows.IndexOf(row);
                newRow.ItemArray = row.ItemArray;
                newRow["Flag"] = "5";
                newRow["SumInfo"] = "本年累计";
                newRow["CurDebitLC"] = dt.Compute("Sum(YearDebitLC)", filter);
                newRow["CurDebitQty"] = dt.Compute("Sum(YearDebitQty)", filter);
                newRow["CurCreditLC"] = dt.Compute("Sum(YearCreditLC)", filter);
                newRow["CurCreditQty"] = dt.Compute("Sum(YearCreditQty)", filter);
                newdt.Rows.InsertAt(newRow, index + 1);
            }
            return newdt;
        }
        private static DataTable HandleAccYear(DataTable dt, bool isCurrency)
        {
            DataTable newdt = dt.Clone();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Flag"] != null && !Helper.AreEqual(row["Flag"].ToString(), "1"))
                    {

                        DataRow newRow = newdt.NewRow();
                        DataRow sourceRow = newdt.NewRow();
                        sourceRow.ItemArray = row.ItemArray;
                        newRow.ItemArray = row.ItemArray;
                        string accID = row["AccID"].ToString(),
                            year = row["Y"].ToString(),
                            month = row["AbsID"].ToString();
                        newRow["Flag"] = "5";
                        newRow["SumInfo"] = "本年累计";
                        string filter = "AccID='" + accID + "' AND Y='" + year + "' AND AbsID <= " + month;
                        if (!isCurrency)
                        {
                            filter = "AccID='" + accID + "'AND CurrID='" + row["CurrID"] + "' AND Y='" + year + "' AND AbsID <= " + month;
                        }
                        //DataRow[] selectRows = dt.Select(filter);
                        newRow["CurDebitLC"] = dt.Compute("Sum(YearDebitLC)", filter);
                        newRow["CurDebitQty"] = dt.Compute("Sum(YearDebitQty)", filter);
                        newRow["CurCreditLC"] = dt.Compute("Sum(YearCreditLC)", filter);
                        newRow["CurCreditQty"] = dt.Compute("Sum(YearCreditQty)", filter);
                        if (!isCurrency)
                        {
                            newRow["CurCreditFC"] = dt.Compute("Sum(YearCreditFC)", filter);
                            newRow["CurDebitFC"] = dt.Compute("Sum(YearDebitFC)", filter);
                        }
                        newdt.Rows.Add(sourceRow);
                        newdt.Rows.Add(newRow);
                    }
                    else
                    {
                        DataRow sourceRow = newdt.NewRow();
                        sourceRow.ItemArray = row.ItemArray;
                        newdt.Rows.Add(sourceRow);
                    }
                }
            }
            return newdt;
        }
    }
}

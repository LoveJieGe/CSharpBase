using Chapter31Lib_ADO.NET;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chapter31_ADONETDemo
{
    class Program
    {
        private const string name = "ConnectionString";
        static void Main(string[] args)
        {
            //WithResultProc();
            //Task t = GetCounts();
            //DataSet xmlRead = SqlHelper.ReadXml("config.xml");
            //DataTable table = xmlRead.Tables[0];
            //Test.Test2();
            //Console.WriteLine(default(int?));
            //Color c = Color.FromArgb(50 ,135, 206, 250);
            //Console.WriteLine(c.ToString());
            string path1 = @"C:\Users\songtaojie\Downloads\总分类账_20180522_130542.xls";
            string path2 = @"C:\Users\songtaojie\Downloads\总分类账_20180522_160534.xls";
            //XmlDocument doc = Export.LoadXml(path);
            //XmlDocument doc = Export.ExcelToXml(path);
            //Export.ConvertExcel(path);
            //XmlDocument doc = ExcelXmlHelper.ReadFileByName(path2);
            ExcelUtils.Export(path1, path2);
            //ExcelXmlHelper.FillData(doc);
            Console.WriteLine("ok");
            Console.Read();
        }
        
      
        public async static Task GetCounts()
        {
            int count2 = await GetCount2();
            int count = await GetCount();
            Console.WriteLine("count2:" + count2);
            Console.WriteLine("测试测试");
            Console.WriteLine("count:" + count);
        }
        public async  static  Task<int> GetCount()
        {
            DbConnection conn = SqlHelper.GetConnection(name);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "waitfor delay '00:00:05';select count(*) from TestTable";
            cmd.Connection = conn as SqlConnection;
            conn.Open();
            int result =  await cmd.ExecuteScalarAsync().ContinueWith(t => Convert.ToInt32(t.Result));
            conn.Close();
            return result;
        }
        public async static Task<int> GetCount2()
        {
            DbConnection conn = SqlHelper.GetConnection(name);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"waitfor delay '0:0:02';select count(*) from KMDoc";
            cmd.Connection = conn as SqlConnection;
            conn.Open();
            int result = await cmd.ExecuteScalarAsync().ContinueWith(t => Convert.ToInt32(t.Result));
            conn.Close();
            return result;
        }
        static void WithResultProc()
        {
            DbConnection conn = SqlHelper.GetConnection(name);
            SqlCommand cmd = new SqlCommand("TestTableInsert", conn as SqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50, "Name"));
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 500, "Description"));
            cmd.Parameters.Add(new SqlParameter("@ID",
                SqlDbType.Int,
                0,
                ParameterDirection.Output,
                false,
                0,
                0, "ID", DataRowVersion.Default, null));
            cmd.UpdatedRowSource = UpdateRowSource.OutputParameters;
            cmd.Parameters["@Name"].Value = "刘亦菲";
            cmd.Parameters["@Description"].Value = "神仙姐姐";
            conn.Open();
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            //adapter.Fill(table);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("ID:" + reader["ID"]);
            conn.Close();
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine("ID:" + row["ID"] + "Name:" + row["Name"] + "Description:" + row["Description"]);
            }
        }
        static void NoResultProc()
        {
            DbConnection conn = SqlHelper.GetConnection(name);
            SqlCommand cmd = new SqlCommand("TestTableUpdate", conn as SqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", 4);
            cmd.Parameters.AddWithValue("@Name", "songtaojie");
            cmd.Parameters.AddWithValue("@Description", "程序员");
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result);
            conn.Close();
        }
        static void ConnectionDemo()
        {
            string key = ConfigurationManager.AppSettings["TableName"];
            //string sql = @"select count(*) from "+ key;
            string sql = @"select * from " + key + " for xml auto";
            DbConnection conn = SqlHelper.GetConnection(name);
            //DbCommand cmd = conn.CreateCommand();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn as SqlConnection;
            conn.Open();
            cmd.CommandText = sql;
            //object o = cmd.ExecuteScalar();
            XmlReader reader = cmd.ExecuteXmlReader();
            reader.Read();
            string data = string.Empty;
            do
            {
                data = reader.ReadOuterXml();
                if (!string.IsNullOrEmpty(data))
                    Console.WriteLine(data);
            } while (!string.IsNullOrEmpty(data));
            conn.Close();
        }
    }
}

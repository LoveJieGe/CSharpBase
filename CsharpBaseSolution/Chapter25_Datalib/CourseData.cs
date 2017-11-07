using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter25_Datalib
{
    [Transaction(TransactionOption.Required)]
    public class CourseData
    {
        [AutoComplete]
        public void AddCOurse(Courses course)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"insert into Courses(Name,Title) values(@Name,@Title)";
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Name", course.Name);
                cmd.Parameters.AddWithValue("@Title", course.Title);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Clone();
                conn.Close();
            }
        }
    }
    /********************使用传统的ADO.NET*********************
    public class CourseData
    {
        public async Task AddCourseAsync(Courses course)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = conn.BeginTransaction();
            cmd.CommandText = @"insert into Courses(Name,Title) values(@Name,@Title)";
            try
            {
                await conn.OpenAsync();
                
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@Name", course.Name);
                cmd.Parameters.AddWithValue("@Title", course.Title);
                await cmd.ExecuteNonQueryAsync();
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("插入数据失败!");
                transaction.Rollback();
            }
            finally
            {
                cmd.Clone();
                conn.Close();
            }
        }
    }
    ***********************************************************/
}

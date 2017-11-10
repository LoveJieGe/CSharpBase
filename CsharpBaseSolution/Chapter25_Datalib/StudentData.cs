using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter25_Datalib
{
    public class StudentData:ServicedComponent
    {
        public async Task AddStudentAsync(Students student)
        {
            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            await connection.OpenAsync();
            try
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into students(Name,Age,Company) values(@Name,@Age,@Company)";
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Company", student.Company);
                await cmd.ExecuteNonQueryAsync();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

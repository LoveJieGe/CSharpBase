using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Chapter28_CustomResource
{
    public class DataBaseResourceReader : IResourceReader
    {
        private string connectionString;
        private string language;
        public DataBaseResourceReader(string connectionString, CultureInfo culture)
        {
            this.connectionString = connectionString;
            this.language = culture.Name;
        }
        public DataBaseResourceReader(string connectionString) : this(connectionString, CultureInfo.CurrentCulture)
        { }

        public void Close()
        {
            
        }

        public void Dispose()
        {
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var conn = new SqlConnection(connectionString);
            if (string.IsNullOrEmpty(language))
                language = "Default";
            string sqlText = @"select [key],[" + language + "] from Messages";
            SqlCommand cmd = conn.CreateCommand();
            try
            {
                conn.Open();
                cmd.CommandText = sqlText;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetValue(1) != null)
                    {
                        dict.Add(reader.GetValue(0).ToString(), reader.GetValue(1).ToString());
                    }
                }
            }
            catch(SqlException e)
            {
                if (e.Number != 207)
                    throw;
            }
            finally
            {
                cmd.Clone();
                conn.Close();
            }
            return dict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

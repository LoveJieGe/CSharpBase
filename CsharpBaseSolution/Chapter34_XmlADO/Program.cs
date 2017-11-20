using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chapter34_XmlADO
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.;Database=bookshop;Integrated Security=SSPI";
            XmlDocument doc = new XmlDocument();
            DataSet set = new DataSet("XmlBooks");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select top 50 * from books";
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(set, "Book");
                }
            }
            if(set.Tables.Count>0)
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                StreamReader reader = new StreamReader(stream);
                //IgnoreSchema代表不希望把内联架构写入到Xml的开头
                set.WriteXml(writer, XmlWriteMode.IgnoreSchema);
                stream.Seek(0, SeekOrigin.Begin);
                doc.Load(reader);
                doc.Save("book.xml");
                XmlNodeList nodeList = doc.SelectNodes("//XmlBooks/Book");
                
                foreach (XmlNode node in nodeList )
                {
                    Console.WriteLine(node.Name + "\r\n");
                }
            }
            Console.Read();
        }


    }
}

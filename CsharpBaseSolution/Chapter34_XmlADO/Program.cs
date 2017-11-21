using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Chapter34_XmlADO
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReadXmlToAdo();
            //Serializer();
            //DeSerializer();
            //SerializerArray();
            DescrializerArray();
            Console.Read();
        }
        static void ReadAdoToXml()
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
            if (set.Tables.Count > 0)
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

                foreach (XmlNode node in nodeList)
                {
                    Console.WriteLine(node.Name + "\r\n");
                }
            }
        }

        static void ReadXmlToAdo()
        {
            DataSet ds = new DataSet("XmlBook");
            ds.ReadXml("booksEdit.xml");
            foreach(DataTable table in ds.Tables)
            {
                Console.WriteLine(table.TableName);
                Console.WriteLine("-------------------------");
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn col in table.Columns)
                    {
                        Console.WriteLine(col.ColumnName + ":" +row[col]+","+ col.DataType.FullName);
                    }
                    Console.WriteLine("-------------------------");
                }
            }
        }

        static void Serializer()
        {
            Product p2 = new Product();
            p2.ProductID = 200;
            p2.CategoryID = 100;
            p2.Discontinued = true;
            p2.DisCount = 5;
            p2.ProductName = "茶杯";
            p2.QuantityPerUnit = "6";
            p2.UnitPrice = 20;
            p2.UnitsInStock = 5;

            TextWriter tw = new StreamWriter("serilProd.xml");
            XmlSerializer sr = new XmlSerializer(typeof(Product));
            sr.Serialize(tw, p2);
            tw.Close();
        }
        static void DeSerializer()
        {
            FileStream stream = new FileStream("serilProd.xml", FileMode.Open);
            XmlSerializer sr = new XmlSerializer(typeof(Product));
            Product p2 = sr.Deserialize(stream) as Product;
            Console.WriteLine(p2.ToString());
        }
        static void SerializerArray()
        {
            XmlAttributes attr = new XmlAttributes();
            attr.XmlElements.Add(new XmlElementAttribute("Product", typeof(Product)));
            attr.XmlElements.Add(new XmlElementAttribute("Book", typeof(BookProduct)));
            XmlAttributeOverrides attrOver = new XmlAttributeOverrides();
            attrOver.Add(typeof(Inventory), "InventoryItem", attr);
            Product p1 = new Product();
            p1.ProductID = 200;
            p1.CategoryID = 100;
            p1.Discontinued = true;
            p1.DisCount = 5;
            p1.ProductName = "茶杯";
            p1.QuantityPerUnit = "6";
            p1.UnitPrice = 20;
            p1.UnitsInStock = 5;

            BookProduct p2 = new BookProduct();
            p2.ISBN = "1234567890";
            p2.ProductID = 200;
            p2.CategoryID = 100;
            p2.Discontinued = true;
            p2.DisCount = 5;
            p2.ProductName = "茶杯2";
            p2.QuantityPerUnit = "6";
            p2.UnitPrice = 20;
            p2.UnitsInStock = 5;

           
            Inventory stuff = new Inventory();
            stuff.InventoryItem = new Product[] { p1, p2 };

            TextWriter tw = new StreamWriter("Inventory.xml");
            XmlSerializer sr = new XmlSerializer(typeof(Inventory),attrOver);
            sr.Serialize(tw, stuff);
            tw.Close();
        }
        static void DescrializerArray()
        {
            FileStream stream = new FileStream("Inventory.xml", FileMode.Open);
            XmlAttributes attrs = new XmlAttributes();
            attrs.XmlElements.Add(new XmlElementAttribute("Product", typeof(Product)));
            attrs.XmlElements.Add(new XmlElementAttribute("Book", typeof(BookProduct)));
            XmlAttributeOverrides attrOver = new XmlAttributeOverrides();
            attrOver.Add(typeof(Inventory), "InventoryItem", attrs);
            XmlSerializer sr = new XmlSerializer(typeof(Inventory), attrOver);
            Inventory inv = sr.Deserialize(stream) as Inventory;
            Console.WriteLine(inv.ToString());
        }

    }
}

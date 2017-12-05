using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chapter34_XDocument
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoadXml();
            //XElementTest();
            //XCommentTest();
            QueryData();
            Console.Read();
        }
        static void LoadXml()
        {
            XDocument doc = XDocument.Load("Inventory.xml");
            doc.Save("newInventory.xml");
            Console.WriteLine(doc.Root.Name);
            Console.WriteLine(doc.Root.HasAttributes.ToString());
        }
        static void XElementTest()
        {
            XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
            XDocument doc = new XDocument(); ;
            XElement xe = new XElement(ns+"Company",
                new XElement("Name", "普实"),
                new XElement("City", "苏州"),
                new XElement("Country", "中国"));
            doc.Add(xe);
            doc.Save("newInventory.xml");
            Console.WriteLine(xe.ToString());
        }
        static void XCommentTest()
        {
            XDocument xdoc = new XDocument();
            XComment xcom = new XComment("这是一个测试");
            xdoc.Add(xcom);
            XElement xe = new XElement( "Company",
                new XAttribute("Type","计算机软件"),
                new XElement("Name", "普实"),
                new XElement("City", "苏州"),
                new XComment("内部注释"),
                new XElement("Country", "中国"));
            xdoc.Add(xe);
            xdoc.Save(Console.Out);
            //Console.WriteLine(xdoc.ToString());
        }

        static void QueryData()
        {
            XDocument doc = XDocument.Load("Inventory.xml");
            var names = from n in doc.Descendants("ProductName")
                       select n.Value;
            Console.WriteLine("共有{0}组数据", names.Count());
            foreach(string name in names)
            {
                Console.WriteLine("ProductName：" + name);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace Chapter34_XmlReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReadXml();
            WriteXml();
            //XMLDocument();
            //WriteXmlDocument();
            //XPathRead();
            //XPathEvaluate();
            //AddElement();
            Console.Read();
        }
        static void ReadXml()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            XmlReader reader = XmlReader.Create("config.xml");
            
            //while(reader.Read())
            //{
            //    if (reader.NodeType == XmlNodeType.Text)
            //    {
            //        sb.Append(reader.Value + "\r\n");
            //    }
            //    if (reader.NodeType == XmlNodeType.Element)
            //    {
            //        if (reader.HasAttributes)
            //        {
            //            sb2.Append(reader.Name + "\r\n");
            //        }
            //    }
            //}

            //ReadElementString
            while(!reader.EOF)
            {
                if(reader.MoveToContent()==XmlNodeType.Element&&reader.Name.ToLower()=="item")
                {
                    sb.Append(reader.ReadElementContentAsString() + "\r\n");
                }
                else
                {
                    reader.Read();
                }
            }
            Console.Write(sb.ToString());
           // Console.ForegroundColor = ConsoleColor.Red;
          //  Console.WriteLine(sb2.ToString());
        }

        static void WriteXml()
        {
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            //设置缩进字符，使用空格、制表符、回车符或换行符。 默认值为两个空格。
            setting.IndentChars = "\t";
            setting.NewLineOnAttributes = true;
            XmlWriter writer = XmlWriter.Create("newbook.xml", setting);
            writer.WriteStartDocument();
            writer.WriteComment("这是一本书的Xml文档 ");
            writer.WriteStartElement("book");
            writer.WriteAttributeString("genre", "Mystery");
            writer.WriteAttributeString("publicationdate", "2001");
            writer.WriteAttributeString("ISBN", "1234567890");
            writer.WriteElementString("title", "书的名字");
            writer.WriteStartElement("author");
            writer.WriteElementString("name", "我的");
            writer.WriteEndElement();
            writer.WriteElementString("price", "21");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        static void XMLDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("newbook.xml");
            XmlNodeList nodeList = doc.GetElementsByTagName("author");
            foreach(XmlNode node in nodeList)
            {
                Console.WriteLine(node.InnerText + "\r\n");
            }
        }

        static void WriteXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            //XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", null, null);
            //doc.AppendChild(declaration);
            //XmlElement rootElement = doc.CreateElement("bookstore");
            //doc.AppendChild(rootElement);
            doc.Load("booksEdit.xml");
            XmlElement newbook = doc.CreateElement("book2");
            newbook.SetAttribute("genre2", "Mystery");
            newbook.SetAttribute("publicationdate2", "2001");
            newbook.SetAttribute("ISBN2", "1234567890");
            XmlElement title = doc.CreateElement("title");
            title.InnerText = "书的名字2";
            newbook.AppendChild(title);
            XmlElement author = doc.CreateElement("author");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = "我的2";
            author.AppendChild(name);
            newbook.AppendChild(author);
            XmlElement price = doc.CreateElement("price");
            price.InnerText = "21";
            newbook.AppendChild(price);
            doc.DocumentElement.AppendChild(newbook);

            XmlTextWriter writer = new XmlTextWriter("booksEdit.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.WriteContentTo(writer);
            writer.Close();

            XmlNodeList nodeList = doc.GetElementsByTagName("author");
            foreach (XmlNode node in nodeList)
            {
                Console.WriteLine(node.InnerText + "\r\n");
            }
        }

        static void XPathRead()
        {
            XPathDocument doc = new XPathDocument("booksEdit.xml");
            //创建XPath navigator
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator iter =  nav.Select("/bookstore/book2[@genre2='Mystery']");
            while(iter.MoveNext())
            {
                XPathNodeIterator newIter = iter.Current.SelectDescendants(XPathNodeType.Element, false);
                while(newIter.MoveNext())
                {
                    Console.WriteLine(newIter.Current.Name + "\r\n");
                }
            }
        }

        static void XPathEvaluate()
        {
            XPathDocument xPath = new XPathDocument("booksEdit.xml");
            XPathNavigator navigator = xPath.CreateNavigator();
            XPathNodeIterator iter = navigator.Select("/bookstore/book2");
            while(iter.MoveNext())
            {
                XPathNodeIterator newIter = iter.Current.SelectDescendants(XPathNodeType.Element, false);
                while(newIter.MoveNext())
                {
                    if(!newIter.Current.MoveToChild(XPathNodeType.Element))
                    Console.WriteLine(newIter.Current.Name + ":" + newIter.Current.Value + "\r\n");    
                }
            }
            Console.WriteLine("--------------------");
            Console.WriteLine("总金额：" + navigator.Evaluate("sum(/bookstore/book2/price)"));
        }
        /// <summary>
        /// 添加折扣节点  
        /// </summary>
        static void AddElement()
        {
            XmlDocument doc=  new XmlDocument();
            doc.Load("booksEdit.xml");
            XPathNavigator nav = doc.CreateNavigator();
            if(nav.CanEdit)
            {
                XPathNodeIterator iter = nav.Select("/bookstore/book2/price");
                while (iter.MoveNext())
                {
                    iter.Current.InsertAfter("<disc>0.5</disc>");
                }
            }
            doc.Save("newbooksEdit.xml");
        }
    }
}

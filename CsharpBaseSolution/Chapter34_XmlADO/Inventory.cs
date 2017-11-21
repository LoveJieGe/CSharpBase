using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chapter34_XmlADO
{
    public class Inventory
    {
        private Product[] stuff;
        [XmlArrayItem("prod",typeof(Product)),XmlArrayItem("book",typeof(BookProduct))]
        public Product[] InventoryItem
        {
            get { return this.stuff; }
            set { this.stuff = value; }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Product item in stuff)
            {
                sb.AppendFormat("ProductName:{0}\r\n", item.ProductName);
            }
            return sb.ToString();
        }
    }
}

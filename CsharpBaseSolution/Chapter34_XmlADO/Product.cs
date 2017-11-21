using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chapter34_XmlADO
{
    [XmlRoot]
    public class Product
    {
        private int prodId;
        private string prodName;
        private int catId;
        private string qtyPerUnit;
        private decimal unitPrice;
        private short unitsInStock;
        private bool discont;
        private int disc;
        [XmlAttribute(AttributeName ="DisCount")]
        public int DisCount
        {
            get { return this.disc; }
            set { this.disc = value; }
        }
        [XmlElement()]
        public int ProductID
        {
            get{ return this.prodId; }
            set { this.prodId = value; }
        }
        [XmlElement()]
        public string ProductName
        {
            get { return this.prodName; }
            set { this.prodName = value; }
        }
        [XmlElement()]
        public string QuantityPerUnit
        {
            get { return this.qtyPerUnit; }
            set { this.qtyPerUnit = value; }
        }
        [XmlElement()]
        public int CategoryID
        {
            get { return this.catId; }
            set { this.catId = value; }
        }
        [XmlElement()]
        public decimal UnitPrice
        {
            get { return this.unitPrice; }
            set { this.unitPrice = value; }
        }
        [XmlElement()]
        public short UnitsInStock
        {
            get { return this.unitsInStock; }
            set { this.unitsInStock = value; }
        }
        [XmlElement()]
        public bool Discontinued
        {
            get { return this.discont; }
            set { this.discont = value; }
        }

        public override string ToString()
        {
            return "ProductID:" + ProductID + "\nProductName:" + ProductName + "\nDisCount:"
                + DisCount + "\nUnitPrice:" + UnitPrice;
        }
    }
}

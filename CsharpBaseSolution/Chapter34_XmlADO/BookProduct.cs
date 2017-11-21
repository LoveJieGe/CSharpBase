using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter34_XmlADO
{
    public class BookProduct:Product
    {
        private string isbn;
        public BookProduct() { }
        public string ISBN
        {
            get { return this.isbn; }
            set { this.isbn = value; }
        }
    }
}

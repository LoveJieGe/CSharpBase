using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10_Collection.Collection
{
    public class Document
    {
        private string title;
        private string content;
        public Document(string title, string content)
        {
            this.title = title;
            this.content = content;
        }
        public string Title
        {
            get {return title;} 
            private set{this.title = value;}
        }
        public string Content
        {
            get {return this.content;}
            private set { this.content = value; }
        }
    }
}

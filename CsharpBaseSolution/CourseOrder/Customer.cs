using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCSharp.Messaging
{
    public class Customer:BindableBase
    {
        private string company;
        public string Company
        {
            get { return this.company; }
            set { SetProperty<string>(ref this.company, value, "Company"); }
        }
        private string contact;
        public string Contact
        {
            get { return this.contact; }
            set { SetProperty<string>(ref this.contact, value, "Contact"); }
        }
    }
}

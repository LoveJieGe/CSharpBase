using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCSharp.Messaging
{
    public class Course:BindableBase
    {
        private string title;
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                SetProperty<string>(ref this.title, value,"Title");
            }
        }
    }
}

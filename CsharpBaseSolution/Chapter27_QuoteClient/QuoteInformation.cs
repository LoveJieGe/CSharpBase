using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chapter27_QuoteClient
{
    public class QuoteInformation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public QuoteInformation()
        {
            this.enableRequest = true;
        }
        private string quote;
        public string Quote
        {
            get
            {
                return this.quote;
            }
            internal set
            {
                SetProperty(ref quote, value);
            }
        }
        private bool enableRequest;
        public bool EnableRequest
        {
            get
            {
                return this.enableRequest;
            }
            internal set
            {
                SetProperty(ref enableRequest, value);
            }
        }
        private void SetProperty<T>(ref T field,T value,[CallerMemberName]string propertyName="")
        {
            if(!EqualityComparer<T>.Default.Equals(field,value))
            {
                field = value;
                var hanlder = PropertyChanged;
                if(hanlder!=null)
                {
                    hanlder(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}

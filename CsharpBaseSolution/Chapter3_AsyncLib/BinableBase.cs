using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chapter13_AsyncLib
{
    /// <summary>
    /// 实现了通知机制
    /// </summary>
    public class BinableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChange(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void SetProperty<T>(ref T prop,T value,[CallerMemberName] string memberName="")
        {
            if (!EqualityComparer<T>.Default.Equals(prop, value))
            {
                prop = value;
                OnPropertyChange(memberName);
            }
        }
    }
}

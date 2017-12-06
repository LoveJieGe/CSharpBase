using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chapter43_RoomReservationContracts
{
    [DataContract]
    public class RoomReservaton:INotifyPropertyChanged
    {
        private int id;
        [DataMember]
        public int ID
        {
            get { return this.id; }
            set { SetProperty(ref id, value); }
        }
        private string roomName;
        [DataMember]
        [StringLength(30)]
        public string RoomName
        {
            get { return this.roomName; }
            set { SetProperty(ref roomName, value); }
        }
        private DateTime startTime;
        [DataMember]
        public DateTime StartTime
        {
            get { return this.startTime; }
            set { SetProperty(ref startTime, value); }
        }
        private DateTime endTime;
        [DataMember]
        public DateTime EndTime
        {
            get { return this.endTime; }
            set { SetProperty(ref endTime, value); }
        }
        private string contact;
        [DataMember]
        [StringLength(30)]
        public string Contact
        {
            get { return this.contact; }
            set { SetProperty(ref contact, value); }
        }
        private string text;
        [DataMember]
        [StringLength(50)]
        public string Text
        {
            get { return this.text; }
            set { SetProperty(ref text, value); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SetProperty<T>(ref T item,T value,[CallerMemberName] string propertyName=null)
        {
            if(!EqualityComparer<T>.Default.Equals(item,value))
            {
                item = value;
                OnNotifyPropertyChanged(propertyName);
            }
        }
        protected virtual void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using ProCSharp.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseOrderReceiver
{
    public class CourseOrderInfo:BindableBase
    {
        public CourseOrderInfo()
        {
            Clear();
        }
        private MessageInfo messageInfo;
        public MessageInfo MessageInfo
        {
            get { return this.messageInfo; }
            set { SetProperty(ref this.messageInfo, value, "MessageInfo"); }
        }
        private string course;
        public string Course
        {
            get { return this.course; }
            set { SetProperty(ref this.course, value); }
        }
        private string company;
        public string Company
        {
            get { return this.company; }
            set { SetProperty(ref company, value); }
        }
        private string contract;
        public string Contract
        {
            get { return this.contract; }
            set { SetProperty(ref this.contract, value); }
        }
        private bool enableProcess;
        public bool EnableProcess
        {
            get { return this.enableProcess; }
            set { SetProperty(ref this.enableProcess, value); }
        }
        private bool highVisibility;
        public bool HighVisibility
        {
            get { return this.highVisibility; }
            set { SetProperty(ref highVisibility, value); }
        }
        public void Clear()
        {
            Course = string.Empty;
            Company = string.Empty;
            Contract = string.Empty;
            EnableProcess = false;
            HighVisibility = false;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Messaging;

namespace ProCSharp.Messaging
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CourseOrder courseOrder = new CourseOrder();
        private readonly ObservableCollection<string> courseList = new ObservableCollection<string>();
        private readonly MessageConfiguration message = new MessageConfiguration();
        public MainWindow()
        {
            InitializeComponent();
            FillCourses();
            this.DataContext = this;
        }
        public CourseOrder CourseOrder
        {
            get { return this.courseOrder; }
        }
        public IEnumerable<string> Courses
        {
            get { return this.courseList; }
        }
        public MessageConfiguration MessageConfiguration
        {
            get { return this.message; }
        }
        private void FillCourses()
        {
            courseList.Add("C#高级编程");
            courseList.Add("Asp.Net本质论");
            courseList.Add("21天学会C#");
            courseList.Add("悲惨世界");
            courseList.Add("丰乳肥臀");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageQueue queue = null;
                if (!MessageQueue.Exists(CourseOrder.CourseOrderQueueName))
                {
                    queue = MessageQueue.Create(CourseOrder.CourseOrderQueueName);
                }
                using (queue = new MessageQueue(CourseOrder.CourseOrderQueueName))
                {
                    using (Message message = new Message(courseOrder)
                    {
                        Recoverable = true,
                        Priority = MessageConfiguration.HighPriority == true ? MessagePriority.High : MessagePriority.Normal
                    })
                    {
                        queue.Send(message, string.Format("Course Order {{{0}}}", CourseOrder.Customer.Company));
                    }
                    MessageBox.Show("提交成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

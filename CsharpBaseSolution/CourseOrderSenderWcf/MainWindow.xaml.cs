using CourseOrderServiceContract;
using CourseOrderWcf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
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

namespace CourseOrderSenderWcf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> coursesList = new ObservableCollection<string>();
        private CourseOrder courseOrder = new CourseOrder();
        public MainWindow()
        {
            InitializeComponent();
            FillCourse();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var factory = new ChannelFactory<ICourseOrderService>("queueEndpoint");
                ICourseOrderService proxy = factory.CreateChannel();
                proxy.AddCourseOrder(CourseOrder);
                factory.Close();
                MessageBox.Show("成功提交", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public IEnumerable<string> Courses
        {
            get { return this.coursesList; }
        }
        public CourseOrder CourseOrder
        {
            get { return this.courseOrder; }
        }
        private void FillCourse()
        {
            coursesList.Add("C#高级编程");
            coursesList.Add("Java编程思想");
            coursesList.Add("Asp.Net本质论");
            coursesList.Add("Jquery技术内幕");
            coursesList.Add("javascript高级编程");
        }
    }
}

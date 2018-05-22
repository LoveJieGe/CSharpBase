using CourseOrderServiceContract;
using CourseOrderWcf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace CourseOrderReceiverWcf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<CourseOrder> courseOrders = new ObservableCollection<CourseOrder>();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = courseOrders;
            CourseOrderService.CourseOrderAdd += CourseOrderService_CourseOrderAdd;
            try
            {
                ServiceHost host = new ServiceHost(typeof(CourseOrderService));
                host.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CourseOrderService_CourseOrderAdd(object sender, CourseOrderEventArgs e)
        {
            courseOrders.Add(e.CourseOrder);
            buttonProcessOrder.IsEnabled = true;
        }


        private void buttonProcessOrder_Click(object sender, RoutedEventArgs e)
        {
            var courseOrder = listOrders.SelectedItem as CourseOrder;
            courseOrders.Remove(courseOrder);
            listOrders.SelectedIndex = -1;
            buttonProcessOrder.IsEnabled = false;

            MessageBox.Show("Course order processed", "Course Order Receiver",
                  MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

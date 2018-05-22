using ProCSharp.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Messaging;
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

namespace CourseOrderReceiver
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MessageQueue orderQueue = null;
        private readonly ObservableCollection<MessageInfo> orderList = new ObservableCollection<MessageInfo>();
        private object syncOrderList = new object();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            BindingOperations.EnableCollectionSynchronization(orderList, syncOrderList);
            orderQueue = new MessageQueue(CourseOrder.CourseOrderQueueName);
            orderQueue.Formatter = new XmlMessageFormatter(new Type[]
                {
                    typeof(Course),
                    typeof(CourseOrder),
                    typeof(Customer)
                });
            Task.Factory.StartNew(PeekMessages);
        }
        public ObservableCollection<MessageInfo> OrderList
        {
            get { return this.orderList; }
        }
        private void PeekMessages()
        {
            try
            {
                using (MessageEnumerator messages = orderQueue.GetMessageEnumerator2())
                {
                    while (messages.MoveNext(TimeSpan.FromHours(3)))
                    {
                        Message message = messages.Current;
                        MessageInfo item = new MessageInfo()
                        {
                            ID = message.Id,
                            Label = message.Label
                        };
                        orderList.Add(item);
                    }
                    MessageBox.Show("在过去的3小时内没有订单。退出线程", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageInfo item = (sender as ListBox).SelectedItem as MessageInfo;
            if (item == null) return;
            orderQueue.MessageReadPropertyFilter.Priority = true;
            Message message = orderQueue.PeekById(item.ID);
            var order = message.Body as CourseOrder;
            if (order != null)
            {
                selectedCourseOrder.MessageInfo = item;
                selectedCourseOrder.Course = order.Course.Title;
                selectedCourseOrder.Company = order.Customer.Company;
                selectedCourseOrder.Contract = order.Customer.Contact;
                selectedCourseOrder.EnableProcess = true;
                if (message.Priority > MessagePriority.Normal)
                {
                    selectedCourseOrder.HighVisibility = true;
                }
                else
                {
                    selectedCourseOrder.HighVisibility = false;
                }
            }
            else
            {
                MessageBox.Show("所选项目不是课程订单", "课程订单", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private readonly CourseOrderInfo selectedCourseOrder = new CourseOrderInfo();
        public CourseOrderInfo SelectedCourseInfo
        {
            get { return this.selectedCourseOrder; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Message message = orderQueue.ReceiveById(SelectedCourseInfo.MessageInfo.ID);
            orderList.Remove(selectedCourseOrder.MessageInfo);
            listOrders.SelectedIndex = -1;
            selectedCourseOrder.Clear();
            MessageBox.Show("课程订单处理完成", "课程订单",
           MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

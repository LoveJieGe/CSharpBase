using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace Chapter27_QuoteClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private QuoteInformation quoteInformation = new QuoteInformation();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = quoteInformation;
        }
        protected async void OnGetQuote(object sender,RoutedEventArgs e)
        {
            const int bufferSize = 1024;
            Cursor cursor = this.Cursor;
            this.Cursor = Cursors.Wait;
            quoteInformation.EnableRequest = false;
            string serverName = Properties.Settings.Default.ServerName;
            int port = Properties.Settings.Default.Port;
            var client = new TcpClient();
            NetworkStream stream = null;
            try
            {
                await client.ConnectAsync(serverName, port);
                stream = client.GetStream();
                byte[] buffer = new byte[bufferSize];
                int received = await stream.ReadAsync(buffer, 0, bufferSize);
                if(received<=0)
                {
                    return;
                }
                quoteInformation.Quote = Encoding.Unicode.GetString(buffer).Trim('\0');
            }
            catch(SocketException ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if(stream!=null)
                {
                    stream.Close();
                }
                if(client.Connected)
                {
                    client.Close();
                }
            }
            this.Cursor = cursor;
            quoteInformation.EnableRequest = true;
        }
    }
}

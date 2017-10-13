using Chapter13_AsyncLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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

namespace Chapter13_AsyncPatterns
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private SearchInfo searchInfo;
        private object lockList = new object();
        private CancellationTokenSource cts = new CancellationTokenSource();
        public MainWindow()
        {
            InitializeComponent();
            searchInfo = new SearchInfo();
            this.DataContext = searchInfo;
        }

        private void OnSeachSync(object sender, RoutedEventArgs e)
        {
            foreach (var req in GetSearchRequests())
            {
                var client = new WebClient();
                client.Credentials = req.Credentials;
                string resp = client.DownloadString(req.Url);
                IEnumerable<SearchItemResult> images = req.Parse(resp);
                foreach (var image in images)
                {
                    searchInfo.List.Add(image);
                }
            }
        }

        private void OnSeachAsyncPattern(object sender, RoutedEventArgs e)
        {
            Func<string, ICredentials, string> downloadString = (address, cred) =>
            {
                var client = new WebClient();
                client.Credentials = cred;
                return client.DownloadString(address);
            };

            Action<SearchItemResult> addItem = item => searchInfo.List.Add(item);

            foreach (var req in GetSearchRequests())
            {
                downloadString.BeginInvoke(req.Url, req.Credentials, ar =>
                {
                    string resp = downloadString.EndInvoke(ar);
                    var images = req.Parse(resp);
                    foreach (var image in images)
                    {
                        this.Dispatcher.Invoke(addItem, image);
                    }
                }, null);
            }
        }

        private void OnAsyncEventPattern(object sender, RoutedEventArgs e)
        {
            foreach (var req in GetSearchRequests())
            {
                var client = new WebClient();
                client.Credentials = req.Credentials;
                client.DownloadStringCompleted += (sender1, e1) =>
                {
                    string resp = e1.Result;
                    var images = req.Parse(resp);
                    foreach (var image in images)
                    {
                        searchInfo.List.Add(image);
                    }
                };
                client.DownloadStringAsync(new Uri(req.Url));
            }
        }

        private async void OnTaskBasedAsyncPattern(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            try
            {
                foreach (var req in GetSearchRequests())
                {
                    var clientHandler = new HttpClientHandler
                    {
                        Credentials = req.Credentials
                    };
                    var client = new HttpClient(clientHandler);

                    var response = await client.GetAsync(req.Url, cts.Token);
                    string resp = await response.Content.ReadAsStringAsync();

                    await Task.Run(() =>
                    {
                        var images = req.Parse(resp);
                        foreach (var image in images)
                        {
                            cts.Token.ThrowIfCancellationRequested();
                            searchInfo.List.Add(image);
                        }
                    }, cts.Token);
                }
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void OnTaskBasedAsyncPattern1(object sender, RoutedEventArgs e)
        {
            foreach (var req in GetSearchRequests())
            {
                var client = new WebClient();
                client.Credentials = req.Credentials;
                string resp = await client.DownloadStringTaskAsync(req.Url);

                var images = req.Parse(resp);
                foreach (var image in images)
                {
                    searchInfo.List.Add(image);
                }
            }
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            if (cts != null)
                cts.Cancel();
        }

        private void OnClear(object sender, RoutedEventArgs e)
        {
            searchInfo.List.Clear();
        }


        private IEnumerable<IImageRequest> GetSearchRequests()
        {
            return new List<IImageRequest> { 
                new BingRequest{SearchTerm=searchInfo.SearchTerm},
                new FlickrRequest{SearchTerm = searchInfo.SearchTerm}
            };
        }

    }
}

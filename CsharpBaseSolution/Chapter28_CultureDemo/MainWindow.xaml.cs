using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Chapter28_CultureDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupCultures();
        }
        
        private void SetupCultures()
        {
            var cultureDataDict = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .OrderBy(c => c.Name)
                .Select(c => new CultureData
                {
                    CultureInfo = c,
                    SubCultures = new List<CultureData>()
                }).ToDictionary(c => c.CultureInfo.Name);
            var rootCultureData = new List<CultureData>();
            foreach (CultureData item in cultureDataDict.Values)
            {
                if (item.CultureInfo.Parent.LCID == 127)
                {
                    rootCultureData.Add(item);
                }
                else
                {
                    CultureData parentCultureData;
                    if (cultureDataDict.TryGetValue(item.CultureInfo.Parent.Name, out parentCultureData))
                    {
                        parentCultureData.SubCultures.Add(item);
                    }
                    else
                    {
                        throw new Exception("unexception-error:parent culture not found");
                    }
                }
            }
            this.DataContext = rootCultureData.OrderBy(c => c.CultureInfo.Name);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CultureData data = e.NewValue as CultureData;
            if(data!=null)
            {
                itemGrid.DataContext = data;
            }
        }
    }
}

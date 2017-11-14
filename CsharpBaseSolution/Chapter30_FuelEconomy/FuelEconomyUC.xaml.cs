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

namespace Chapter30_FuelEconomy
{
    /// <summary>
    /// FuelEconomyUC.xaml 的交互逻辑
    /// </summary>
    public partial class FuelEconomyUC : UserControl
    {
        private FuelEconomyViewModel viewModel = new FuelEconomyViewModel();
        public FuelEconomyUC()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        private void OnCalculator(object sender,RoutedEventArgs e)
        {
            double fuel = double.Parse(viewModel.Fuel);
            double distance = double.Parse(viewModel.Distance);
            FuelEconomyType ecoType = viewModel.SelectFuelEconomyType;
            double result = 0;
            switch(ecoType.Id)
            {
                case "lpk":
                    result = fuel / (distance / 100);
                    break;
                case "mpg":
                    result = distance / fuel;
                    break;
            }
            viewModel.Result = result.ToString();
        }
    }
}

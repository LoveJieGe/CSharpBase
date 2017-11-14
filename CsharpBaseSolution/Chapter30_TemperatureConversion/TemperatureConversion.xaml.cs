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

namespace Chapter30_TemperatureConversion
{
    /// <summary>
    /// TemperatureConversion.xaml 的交互逻辑
    /// </summary>
    public partial class TemperatureConversion : UserControl
    {
        public enum TempConversionType
        {
            Celsius,
            Fahrenheit,
            Kelvin
        }
        public TemperatureConversion()
        {
            InitializeComponent();
            this.DataContext = Enum.GetNames(typeof(TempConversionType));
        }
        private double ToCelsiusFrom(double t,TempConversionType conv)
        {
            switch(conv)
            {
                case TempConversionType.Celsius:
                    return t;
                case TempConversionType.Fahrenheit:
                    return (t - 32) / 1.8;
                case TempConversionType.Kelvin:
                    return (t - 273.15);
                default:
                    throw new ArgumentException("找不到要转换的类型!");
            }
        }

        private double FromCelsiusTo(double t,TempConversionType conv)
        {
            switch(conv)
            {
                case TempConversionType.Celsius:
                    return t;
                case TempConversionType.Fahrenheit:
                    return (t * 1.8 + 32);
                case TempConversionType.Kelvin:
                    return t + 273.15;
                default:
                    throw new ArgumentException("找不到要转换的类型!");
            }
        }

        private void OnCalculator(object sender,RoutedEventArgs e)
        {
            try
            {
                TempConversionType from;
                TempConversionType to;
                if(Enum.TryParse(comboForm.SelectedValue.ToString(),out from)&&
                    Enum.TryParse(comboTo.SelectedValue.ToString(),out to))
                {
                    double result = FromCelsiusTo(ToCelsiusFrom(double.Parse(textInput.Text), from), to);
                    textOutput.Text = result.ToString();
                }
            }
            catch(FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

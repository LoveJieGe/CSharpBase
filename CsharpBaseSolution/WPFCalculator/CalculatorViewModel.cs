using Chapter30_CalculatorUtils;
using Chapter30Lib_CalculatorContract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalculator
{
    public class CalculatorViewModel:BindableBase
    {
        private string status;

        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        private string input;
        public string Input
        {
            get { return input; }
            set { SetProperty(ref input, value); }
        }

        private string result;
        public string Result
        {
            get { return result; }
            set { SetProperty(ref result, value); }
        }

        private string fullInputText;

        public string FullInputText
        {
            get { return fullInputText; }
            set { fullInputText = value; }
        }

        private ObservableCollection<IOperation> calcAddInOperators = new ObservableCollection<IOperation>();
        public object syncCalcAddInOperator = new object();
        public ObservableCollection<IOperation> CalcAddInOperator
        {
            get
            {
                return calcAddInOperators;
            }
        }
        private readonly ObservableCollection<Lazy<ICalculatorExtension>> calcExtensions = new ObservableCollection<Lazy<ICalculatorExtension>>();
        public ObservableCollection<Lazy<ICalculatorExtension>> CalcExtensions
        {
            get
            {
                return calcExtensions;
            }
        }

        private readonly ObservableCollection<Lazy<ICalculatorExtension>> activatedExtensions = new ObservableCollection<Lazy<ICalculatorExtension>>();
        public object syncActivatedExtensions = new object();
        public ObservableCollection<Lazy<ICalculatorExtension>> ActivatedExtensions
        {
            get
            {
                return activatedExtensions;
            }
        }
    }
}

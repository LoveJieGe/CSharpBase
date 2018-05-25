using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesignPattern
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void BtnStrategyClick(object sender,EventArgs e)
        {
            DesignPattern.Strategy.Strategy strategy = new DesignPattern.Strategy.Strategy();

            strategy.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesignPattern.Strategy
{
    public partial class Strategy : Form
    {
        double total = 0;
        public Strategy()
        {
            InitializeComponent();
            Reset();
        }

        private void BtnSubmitClick(object sender, EventArgs e)
        {
            Submit1();
        }
        private void BtnResetClick(object sender, EventArgs e)
        {
            Reset();
        }
        #region 第一版本
        /// <summary>
        /// 简单工厂：需要工厂类和抽象类
        /// </summary>
        private void Submit1()
        {
            string price = this.txt_price.Text,
                count = this.txt_count.Text;
            if (string.IsNullOrEmpty(price) || string.IsNullOrEmpty(count)) return;
            total+= Convert.ToDouble(price) * Convert.ToDouble(count);
            ListItem select = this.comb_discount.SelectedItem as ListItem;
            BaseCash cash = CashFactory.GetCash((DiscountEnum)select.Value);
            double discountTotal = cash.GetResult(total);
            this.txt_list.Text = "商品描述：" +
                Environment.NewLine + "单价：" + price +
                Environment.NewLine + "数量：" + count +
                Environment.NewLine+"打折情况:"+select.Key+
                Environment.NewLine + "总价：" + total+
                Environment.NewLine + "打折后:" + discountTotal;
            this.txt_list.Select(0, 5);
            this.txt_list.SelectionColor = Color.FromArgb(200,105,200,85);
            this.txt_result.Text = discountTotal.ToString();
        }
        /// <summary>
        /// 策略模式。只需要一个CashContext上下文类即可
        /// </summary>
        private void Submit2()
        {
            string price = this.txt_price.Text,
                count = this.txt_count.Text;
            if (string.IsNullOrEmpty(price) || string.IsNullOrEmpty(count)) return;
            total += Convert.ToDouble(price) * Convert.ToDouble(count);
            ListItem select = this.comb_discount.SelectedItem as ListItem;
            CashContext context = new CashContext((DiscountEnum)select.Value);
            //BaseCash cash = CashFactory.GetCash((DiscountEnum)select.Value);
            //double discountTotal = cash.GetResult(total);
            double discountTotal = context.GetResult(total);
            this.txt_list.Text = "商品描述：" +
                Environment.NewLine + "单价：" + price +
                Environment.NewLine + "数量：" + count +
                Environment.NewLine + "打折情况:" + select.Key +
                Environment.NewLine + "总价：" + total +
                Environment.NewLine + "打折后:" + discountTotal;
            this.txt_list.Select(0, 5);
            this.txt_list.SelectionColor = Color.FromArgb(200, 105, 200, 85);
            this.txt_result.Text = discountTotal.ToString();
        }
        private void Reset()
        {
            this.txt_price.Text =
               this.txt_count.Text =
               this.txt_list.Text =
               this.txt_result.Text = string.Empty;
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem( "正常", DiscountEnum.Normal));
            list.Add(new ListItem( "打八折", DiscountEnum.Discount8));
            list.Add(new ListItem( "满300返50", DiscountEnum.Return));
            this.comb_discount.DataSource = list;
        }
        #endregion


        /// <summary>
        /// 输入框只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符
                }
            }
        }
    }
}

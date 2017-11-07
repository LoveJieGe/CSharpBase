using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chapter24_ReadAndWriterFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected void OnReadFileClick(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(textBoxFile.Text))
            //{
            //    MessageBox.Show("请填写要读取的文件路!", "提示");
            //    return;
            //}
            //if (File.Exists(textBoxFile.Text))
            //{
            //    textBoxContent.Text =  File.ReadAllText(textBoxFile.Text,Encoding.ASCII);
            //}
            //else
            //{
            //    MessageBox.Show("文件路径解析出错!");
            //}
            //写文件
            if (string.IsNullOrEmpty(textBoxFile.Text))
            {
                MessageBox.Show("请填写要写入的文件路!", "提示");
                return;
            }
            if (File.Exists(textBoxFile.Text))
            {

                File.WriteAllText(textBoxFile.Text, textBoxContent.Text);
            }
        }
    }
}

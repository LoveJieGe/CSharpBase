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

namespace Chapter24_FilePropertiesSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string currentForderPath;
        /// <summary>
        /// 清除所有的文本框信息
        /// </summary>
        private void ClearAllFields()
        {
            this.textBoxForder.Text = "";
            this.textBoxFile.Text = "";
            this.textBoxFileSize.Text = "";
            this.listBoxForders.Items.Clear();
            this.listBoxFiles.Items.Clear();
            this.textBoxFileName.Text = "";
            this.textBoxCreateTime.Text = "";
            this.textBoxLastWriteTime.Text = "";
            this.textBoxLastAccessTime.Text = "";
        }
        //
        protected void DisplayFileInfo(string fileFullPath)
        {
            FileInfo theFile = new FileInfo(fileFullPath);
            if (theFile.Exists)
            {
                textBoxFile.Text = fileFullPath;
                textBoxFileName.Text = theFile.Name;
                textBoxCreateTime.Text = theFile.CreationTime.ToString("yyyy-MM-dd HH:ss");
                textBoxFileSize.Text = ByteToGbMbKb(theFile.Length);
                textBoxLastWriteTime.Text = theFile.LastWriteTime.ToString("yyyy-MM-dd HH:ss");
                textBoxLastAccessTime.Text = theFile.LastAccessTime.ToString("yyyy-MM-dd HH:ss");
                btnCopy.Enabled = true;
                btnMove.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                MessageBox.Show(string.Format("找不到文件路径[{0}]", fileFullPath));
            }
        }
        protected void DisplayFolderList(string folderFullName)
        {
            DirectoryInfo theFolder = new DirectoryInfo(folderFullName);
            if (!theFolder.Exists)
            {
                MessageBox.Show(string.Format("找不到文件夹[{0}]", folderFullName));
                return;
            }
            ClearAllFields();
            DisableMoveFeatures();
            textBoxForder.Text = folderFullName;
            currentForderPath = folderFullName;
            foreach (DirectoryInfo folder in theFolder.GetDirectories())
            {
                listBoxForders.Items.Add(folder.Name);
            }
            foreach (FileInfo file in theFolder.GetFiles())
            {
                listBoxFiles.Items.Add(file.Name);
            }
        }
        protected void OnDisplayButtonClick(object sender, EventArgs e)
        {
            string folderPath = textBoxForder.Text;
            if (string.IsNullOrEmpty(folderPath))
            {
                MessageBox.Show("请输入要查看的文件路径!");
                return;
            }
            DirectoryInfo theFolder = new DirectoryInfo(folderPath);
            if (theFolder.Exists)
            {
                DisplayFolderList(folderPath);
                return;
            }
            FileInfo theFile = new FileInfo(folderPath);
            if (theFile.Exists)
            {
                DisplayFolderList(theFile.Directory.FullName);
                int index = listBoxFiles.Items.IndexOf(theFile.Name);
                listBoxFiles.SetSelected(index, true);
                DisplayFileInfo(folderPath);
                return;
            }
            MessageBox.Show("找不到你要查看的路径!");
        }
        protected void OnListBoxFilesSelect(object sender, EventArgs e)
        {
            int index = this.listBoxFiles.SelectedIndex;
            if (index >= 0)
            {
                string selectFile = this.listBoxFiles.SelectedItem.ToString();
                string fileFullName = this.currentForderPath + "\\" + selectFile;
                FileInfo theFile = new FileInfo(fileFullName);
                if (theFile.Exists)
                {
                    DisplayFileInfo(fileFullName);
                }
            }
        }
        protected void OnListBoxFoldersSelect(object sender, EventArgs e)
        {
            int index = this.listBoxForders.SelectedIndex;

            if (index >= 0)
            {
                string selectFolder = this.listBoxForders.SelectedItem.ToString();

                string folderFullName = this.currentForderPath + "\\" + selectFolder;
                DirectoryInfo theFolder = new DirectoryInfo(folderFullName);
                if (theFolder.Exists)
                {
                    DisplayFolderList(folderFullName);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxForder.Text))
            {
                MessageBox.Show("请输入你要查看的路径!");
                return;
            }
            string originFolderFullName = textBoxForder.Text;
            DirectoryInfo theOriginFolder = new DirectoryInfo(originFolderFullName);
            if (theOriginFolder.Exists)
            {
                DisplayFolderList(theOriginFolder.Parent.FullName);
            }
            else
            {
                MessageBox.Show("找不到要查看的路径!");
            }
        }
        private string ByteToGbMbKb(long size)
        {
            const long GB = 1024 * 1024 * 1024;
            const int MB = 1024 * 1024;
            const int KB = 1024;
            if (size / GB >= 1)
                return (Math.Round(size / (float)GB, 2)).ToString() + "GB";
            if (size / MB >= 1)
                return Math.Round(size / (float)MB, 2).ToString() + "MB";
            if (size / KB >= 1)
                return Math.Round(size / (float)KB, 2).ToString() + "KB";
            else
                return size.ToString() + "Byte";
        }

        private void DisableMoveFeatures()
        {
            textBoxNewPath.Text = "";
            btnCopy.Enabled = false;
            btnMove.Enabled = false;
            btnDelete.Enabled = false;
        }
        protected void OnBtnCopyClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNewPath.Text))
            {
                MessageBox.Show("请填写要复制到的文件路径!");
                return;
            }
            string query = "确定复制文件" + textBoxFile.Text + "到\n"
                + textBoxNewPath.Text;
            if (MessageBox.Show(query, "复制", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                File.Copy(textBoxFile.Text, textBoxNewPath.Text);
                DisplayFolderList(currentForderPath);
            }
        }


    }
}

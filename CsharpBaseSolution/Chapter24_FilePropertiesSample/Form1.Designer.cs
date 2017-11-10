using System;
using System.IO;
using System.Windows.Forms;

namespace Chapter24_FilePropertiesSample
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxForder = new System.Windows.Forms.TextBox();
            this.Display_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxFileSize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCreateTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxLastWriteTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxLastAccessTime = new System.Windows.Forms.TextBox();
            this.listBoxForders = new System.Windows.Forms.ListBox();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnMove = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxNewPath = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxForder
            // 
            this.textBoxForder.Location = new System.Drawing.Point(27, 28);
            this.textBoxForder.Name = "textBoxForder";
            this.textBoxForder.Size = new System.Drawing.Size(413, 25);
            this.textBoxForder.TabIndex = 0;
            // 
            // Display_Button
            // 
            this.Display_Button.Location = new System.Drawing.Point(477, 28);
            this.Display_Button.Name = "Display_Button";
            this.Display_Button.Size = new System.Drawing.Size(75, 23);
            this.Display_Button.TabIndex = 1;
            this.Display_Button.Text = "查看";
            this.Display_Button.UseVisualStyleBackColor = true;
            this.Display_Button.Click += new System.EventHandler(this.OnDisplayButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "输入你要查看的路径:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "当前所选文件路径:";
            // 
            // textBoxFile
            // 
            this.textBoxFile.Enabled = false;
            this.textBoxFile.Location = new System.Drawing.Point(27, 92);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(413, 25);
            this.textBoxFile.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(477, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "上一级";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 383);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "详细信息:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 411);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "文件名:";
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.CausesValidation = false;
            this.textBoxFileName.Enabled = false;
            this.textBoxFileName.Location = new System.Drawing.Point(96, 408);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.ReadOnly = true;
            this.textBoxFileName.Size = new System.Drawing.Size(456, 25);
            this.textBoxFileName.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 447);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "文件大小:";
            // 
            // textBoxFileSize
            // 
            this.textBoxFileSize.Enabled = false;
            this.textBoxFileSize.Location = new System.Drawing.Point(96, 444);
            this.textBoxFileSize.Name = "textBoxFileSize";
            this.textBoxFileSize.ReadOnly = true;
            this.textBoxFileSize.Size = new System.Drawing.Size(159, 25);
            this.textBoxFileSize.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(277, 447);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "创建日期:";
            // 
            // textBoxCreateTime
            // 
            this.textBoxCreateTime.Enabled = false;
            this.textBoxCreateTime.Location = new System.Drawing.Point(358, 444);
            this.textBoxCreateTime.Name = "textBoxCreateTime";
            this.textBoxCreateTime.ReadOnly = true;
            this.textBoxCreateTime.Size = new System.Drawing.Size(194, 25);
            this.textBoxCreateTime.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 480);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "最后一次修改日期:";
            // 
            // textBoxLastWriteTime
            // 
            this.textBoxLastWriteTime.Enabled = false;
            this.textBoxLastWriteTime.Location = new System.Drawing.Point(27, 498);
            this.textBoxLastWriteTime.Name = "textBoxLastWriteTime";
            this.textBoxLastWriteTime.ReadOnly = true;
            this.textBoxLastWriteTime.Size = new System.Drawing.Size(222, 25);
            this.textBoxLastWriteTime.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(277, 480);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "最后一次访问日期:";
            // 
            // textBoxLastAccessTime
            // 
            this.textBoxLastAccessTime.Enabled = false;
            this.textBoxLastAccessTime.Location = new System.Drawing.Point(280, 498);
            this.textBoxLastAccessTime.Name = "textBoxLastAccessTime";
            this.textBoxLastAccessTime.ReadOnly = true;
            this.textBoxLastAccessTime.Size = new System.Drawing.Size(272, 25);
            this.textBoxLastAccessTime.TabIndex = 18;
            // 
            // listBoxForders
            // 
            this.listBoxForders.FormattingEnabled = true;
            this.listBoxForders.ItemHeight = 15;
            this.listBoxForders.Location = new System.Drawing.Point(27, 153);
            this.listBoxForders.Name = "listBoxForders";
            this.listBoxForders.Size = new System.Drawing.Size(228, 214);
            this.listBoxForders.TabIndex = 19;
            this.listBoxForders.Click += new System.EventHandler(this.OnListBoxFoldersSelect);
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.HorizontalScrollbar = true;
            this.listBoxFiles.ItemHeight = 15;
            this.listBoxFiles.Location = new System.Drawing.Point(280, 153);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.ScrollAlwaysVisible = true;
            this.listBoxFiles.Size = new System.Drawing.Size(272, 214);
            this.listBoxFiles.TabIndex = 20;
            this.listBoxFiles.Click += new System.EventHandler(this.OnListBoxFilesSelect);
            // 
            // btnCopy
            // 
            this.btnCopy.Enabled = false;
            this.btnCopy.Location = new System.Drawing.Point(27, 567);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 21;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.OnBtnCopyClick);
            // 
            // btnMove
            // 
            this.btnMove.Enabled = false;
            this.btnMove.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMove.Location = new System.Drawing.Point(124, 567);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(75, 23);
            this.btnMove.TabIndex = 22;
            this.btnMove.Text = "移动";
            this.btnMove.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(225, 567);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 23;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 546);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 15);
            this.label9.TabIndex = 24;
            this.label9.Text = "操作";
            // 
            // textBoxNewPath
            // 
            this.textBoxNewPath.Location = new System.Drawing.Point(139, 611);
            this.textBoxNewPath.Name = "textBoxNewPath";
            this.textBoxNewPath.Size = new System.Drawing.Size(413, 25);
            this.textBoxNewPath.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 614);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 15);
            this.label10.TabIndex = 26;
            this.label10.Text = "新的文件路径:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 132);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 15);
            this.label11.TabIndex = 27;
            this.label11.Text = "文件夹列表";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(277, 132);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 15);
            this.label12.TabIndex = 28;
            this.label12.Text = "文件列表";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(575, 655);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxNewPath);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.listBoxForders);
            this.Controls.Add(this.textBoxLastAccessTime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxLastWriteTime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxCreateTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxFileSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Display_Button);
            this.Controls.Add(this.textBoxForder);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxForder;
        private System.Windows.Forms.Button Display_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxFileSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCreateTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxLastWriteTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxLastAccessTime;
        private ListBox listBoxForders;
        private ListBox listBoxFiles;
        private Button btnCopy;
        private Button btnMove;
        private Button btnDelete;
        private Label label9;
        private TextBox textBoxNewPath;
        private Label label10;
        private Label label11;
        private Label label12;
    }
}


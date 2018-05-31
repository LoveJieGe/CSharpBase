namespace DesignPattern
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
            this.btn_strategy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_strategy
            // 
            this.btn_strategy.Location = new System.Drawing.Point(37, 32);
            this.btn_strategy.Name = "btn_strategy";
            this.btn_strategy.Size = new System.Drawing.Size(75, 23);
            this.btn_strategy.TabIndex = 0;
            this.btn_strategy.Text = "策略模式";
            this.btn_strategy.UseVisualStyleBackColor = true;
            this.btn_strategy.Click += new System.EventHandler(this.BtnStrategyClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(457, 376);
            this.Controls.Add(this.btn_strategy);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设计模式";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_strategy;
    }
}


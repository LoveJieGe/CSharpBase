namespace DesignPattern.Strategy
{
    partial class Strategy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lab_price = new System.Windows.Forms.Label();
            this.lab_count = new System.Windows.Forms.Label();
            this.txt_price = new System.Windows.Forms.TextBox();
            this.txt_count = new System.Windows.Forms.TextBox();
            this.lab_list = new System.Windows.Forms.Label();
            this.btn_submit = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.lab_total = new System.Windows.Forms.Label();
            this.txt_result = new System.Windows.Forms.Label();
            this.txt_list = new System.Windows.Forms.RichTextBox();
            this.lab_discount = new System.Windows.Forms.Label();
            this.comb_discount = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lab_price
            // 
            this.lab_price.AutoSize = true;
            this.lab_price.Location = new System.Drawing.Point(27, 44);
            this.lab_price.Name = "lab_price";
            this.lab_price.Size = new System.Drawing.Size(75, 15);
            this.lab_price.TabIndex = 0;
            this.lab_price.Text = "商品单价:";
            // 
            // lab_count
            // 
            this.lab_count.AutoSize = true;
            this.lab_count.Location = new System.Drawing.Point(28, 87);
            this.lab_count.Name = "lab_count";
            this.lab_count.Size = new System.Drawing.Size(75, 15);
            this.lab_count.TabIndex = 1;
            this.lab_count.Text = "商品数量:";
            // 
            // txt_price
            // 
            this.txt_price.Location = new System.Drawing.Point(108, 34);
            this.txt_price.Name = "txt_price";
            this.txt_price.Size = new System.Drawing.Size(274, 25);
            this.txt_price.TabIndex = 2;
            this.txt_price.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBoxKeyPress);
            // 
            // txt_count
            // 
            this.txt_count.Location = new System.Drawing.Point(109, 77);
            this.txt_count.Name = "txt_count";
            this.txt_count.Size = new System.Drawing.Size(273, 25);
            this.txt_count.TabIndex = 3;
            this.txt_count.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBoxKeyPress);
            // 
            // lab_list
            // 
            this.lab_list.AutoSize = true;
            this.lab_list.Location = new System.Drawing.Point(23, 277);
            this.lab_list.Name = "lab_list";
            this.lab_list.Size = new System.Drawing.Size(75, 15);
            this.lab_list.TabIndex = 5;
            this.lab_list.Text = "详细清单:";
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(31, 360);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(145, 25);
            this.btn_submit.TabIndex = 6;
            this.btn_submit.Text = "确定";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.BtnSubmitClick);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(237, 360);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(145, 25);
            this.btn_reset.TabIndex = 7;
            this.btn_reset.Text = "重置";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.BtnResetClick);
            // 
            // lab_total
            // 
            this.lab_total.AutoSize = true;
            this.lab_total.Location = new System.Drawing.Point(24, 323);
            this.lab_total.Name = "lab_total";
            this.lab_total.Size = new System.Drawing.Size(75, 15);
            this.lab_total.TabIndex = 8;
            this.lab_total.Text = "总共消费:";
            // 
            // txt_result
            // 
            this.txt_result.AutoSize = true;
            this.txt_result.Location = new System.Drawing.Point(106, 322);
            this.txt_result.Name = "txt_result";
            this.txt_result.Size = new System.Drawing.Size(15, 15);
            this.txt_result.TabIndex = 9;
            this.txt_result.Text = "0";
            // 
            // txt_list
            // 
            this.txt_list.Location = new System.Drawing.Point(109, 174);
            this.txt_list.Name = "txt_list";
            this.txt_list.Size = new System.Drawing.Size(273, 118);
            this.txt_list.TabIndex = 10;
            this.txt_list.Text = "";
            // 
            // lab_discount
            // 
            this.lab_discount.AutoSize = true;
            this.lab_discount.Location = new System.Drawing.Point(53, 133);
            this.lab_discount.Name = "lab_discount";
            this.lab_discount.Size = new System.Drawing.Size(45, 15);
            this.lab_discount.TabIndex = 11;
            this.lab_discount.Text = "折扣:";
            // 
            // comb_discount
            // 
            this.comb_discount.FormattingEnabled = true;
            this.comb_discount.Location = new System.Drawing.Point(109, 130);
            this.comb_discount.Name = "comb_discount";
            this.comb_discount.Size = new System.Drawing.Size(273, 23);
            this.comb_discount.TabIndex = 12;
            // 
            // Strategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 443);
            this.Controls.Add(this.comb_discount);
            this.Controls.Add(this.lab_discount);
            this.Controls.Add(this.txt_list);
            this.Controls.Add(this.txt_result);
            this.Controls.Add(this.lab_total);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.lab_list);
            this.Controls.Add(this.txt_count);
            this.Controls.Add(this.txt_price);
            this.Controls.Add(this.lab_count);
            this.Controls.Add(this.lab_price);
            this.Name = "Strategy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "策略模式-商场促销";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_price;
        private System.Windows.Forms.Label lab_count;
        private System.Windows.Forms.TextBox txt_price;
        private System.Windows.Forms.TextBox txt_count;
        private System.Windows.Forms.Label lab_list;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Label lab_total;
        private System.Windows.Forms.Label txt_result;
        private System.Windows.Forms.RichTextBox txt_list;
        private System.Windows.Forms.Label lab_discount;
        private System.Windows.Forms.ComboBox comb_discount;
    }
}
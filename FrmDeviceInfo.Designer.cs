namespace AlarmMapClient
{
    partial class FrmDeviceInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_tel = new System.Windows.Forms.TextBox();
            this.txt_address = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "电话：";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(147, 161);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 3;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(124, 32);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(212, 21);
            this.txt_username.TabIndex = 4;
            // 
            // txt_tel
            // 
            this.txt_tel.Location = new System.Drawing.Point(124, 110);
            this.txt_tel.Name = "txt_tel";
            this.txt_tel.Size = new System.Drawing.Size(212, 21);
            this.txt_tel.TabIndex = 5;
            // 
            // txt_address
            // 
            this.txt_address.Location = new System.Drawing.Point(124, 67);
            this.txt_address.Name = "txt_address";
            this.txt_address.Size = new System.Drawing.Size(212, 21);
            this.txt_address.TabIndex = 6;
            // 
            // FrmDeviceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 215);
            this.Controls.Add(this.txt_address);
            this.Controls.Add(this.txt_tel);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmDeviceInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "用户信息配置";
            this.Load += new System.EventHandler(this.FrmChannelInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_tel;
        private System.Windows.Forms.TextBox txt_address;
    }
}
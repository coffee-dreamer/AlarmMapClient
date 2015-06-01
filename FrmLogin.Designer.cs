namespace AlarmMapClient
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.skinUI = new DotNetSkin.SkinUI();
            this.splitContainer_login = new System.Windows.Forms.SplitContainer();
            this.cb_opruser = new System.Windows.Forms.ComboBox();
            this.cb_serverset = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_userpwd = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.txt_oprpwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_server_set = new System.Windows.Forms.Button();
            this.txt_pwd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_userid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_server = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_login)).BeginInit();
            this.splitContainer_login.Panel1.SuspendLayout();
            this.splitContainer_login.Panel2.SuspendLayout();
            this.splitContainer_login.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // skinUI
            // 
            this.skinUI.Active = true;
            this.skinUI.Button = true;
            this.skinUI.Caption = true;
            this.skinUI.CheckBox = false;
            this.skinUI.ComboBox = false;
            this.skinUI.ContextMenu = true;
            this.skinUI.DisableTag = 999;
            this.skinUI.Edit = true;
            this.skinUI.GroupBox = true;
            this.skinUI.ImageList = null;
            this.skinUI.MaiMenu = true;
            this.skinUI.Panel = true;
            this.skinUI.Progress = true;
            this.skinUI.RadioButton = true;
            this.skinUI.ScrollBar = true;
            this.skinUI.SkinFile = "WE Ergo-BLUEC.skn";
            this.skinUI.SkinSteam = null;
            this.skinUI.Spin = true;
            this.skinUI.StatusBar = true;
            this.skinUI.SystemMenu = true;
            this.skinUI.TabControl = true;
            this.skinUI.Text = "Mycontrol1=edit\r\nMycontrol2=edit\r\n";
            this.skinUI.ToolBar = true;
            this.skinUI.TrackBar = true;
            // 
            // splitContainer_login
            // 
            this.splitContainer_login.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer_login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_login.IsSplitterFixed = true;
            this.splitContainer_login.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_login.Name = "splitContainer_login";
            this.splitContainer_login.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_login.Panel1
            // 
            this.splitContainer_login.Panel1.Controls.Add(this.cb_opruser);
            this.splitContainer_login.Panel1.Controls.Add(this.cb_serverset);
            this.splitContainer_login.Panel1.Controls.Add(this.label1);
            this.splitContainer_login.Panel1.Controls.Add(this.label4);
            this.splitContainer_login.Panel1.Controls.Add(this.cb_userpwd);
            this.splitContainer_login.Panel1.Controls.Add(this.label3);
            this.splitContainer_login.Panel1.Controls.Add(this.btn_login);
            this.splitContainer_login.Panel1.Controls.Add(this.txt_oprpwd);
            this.splitContainer_login.Panel1.Controls.Add(this.label2);
            this.splitContainer_login.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer_login.Panel2
            // 
            this.splitContainer_login.Panel2.Controls.Add(this.btn_server_set);
            this.splitContainer_login.Panel2.Controls.Add(this.txt_pwd);
            this.splitContainer_login.Panel2.Controls.Add(this.label5);
            this.splitContainer_login.Panel2.Controls.Add(this.txt_userid);
            this.splitContainer_login.Panel2.Controls.Add(this.label6);
            this.splitContainer_login.Panel2.Controls.Add(this.txt_server);
            this.splitContainer_login.Panel2.Controls.Add(this.label7);
            this.splitContainer_login.Size = new System.Drawing.Size(310, 267);
            this.splitContainer_login.SplitterDistance = 165;
            this.splitContainer_login.SplitterWidth = 1;
            this.splitContainer_login.TabIndex = 22;
            // 
            // cb_opruser
            // 
            this.cb_opruser.DisplayMember = "username";
            this.cb_opruser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_opruser.FormattingEnabled = true;
            this.cb_opruser.Location = new System.Drawing.Point(93, 77);
            this.cb_opruser.Name = "cb_opruser";
            this.cb_opruser.Size = new System.Drawing.Size(116, 20);
            this.cb_opruser.TabIndex = 34;
            this.cb_opruser.ValueMember = "username";
            // 
            // cb_serverset
            // 
            this.cb_serverset.AutoSize = true;
            this.cb_serverset.Location = new System.Drawing.Point(192, 136);
            this.cb_serverset.Name = "cb_serverset";
            this.cb_serverset.Size = new System.Drawing.Size(15, 14);
            this.cb_serverset.TabIndex = 33;
            this.cb_serverset.UseVisualStyleBackColor = true;
            this.cb_serverset.CheckedChanged += new System.EventHandler(this.cb_serverset_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(126, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "设  置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(25, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 14);
            this.label4.TabIndex = 31;
            this.label4.Text = "操作员";
            // 
            // cb_userpwd
            // 
            this.cb_userpwd.AutoSize = true;
            this.cb_userpwd.Location = new System.Drawing.Point(93, 134);
            this.cb_userpwd.Name = "cb_userpwd";
            this.cb_userpwd.Size = new System.Drawing.Size(15, 14);
            this.cb_userpwd.TabIndex = 30;
            this.cb_userpwd.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(27, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "记住我";
            // 
            // btn_login
            // 
            this.btn_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_login.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_login.Location = new System.Drawing.Point(228, 78);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(70, 68);
            this.btn_login.TabIndex = 29;
            this.btn_login.Text = "登录";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // txt_oprpwd
            // 
            this.txt_oprpwd.Location = new System.Drawing.Point(93, 105);
            this.txt_oprpwd.Name = "txt_oprpwd";
            this.txt_oprpwd.PasswordChar = '*';
            this.txt_oprpwd.Size = new System.Drawing.Size(116, 21);
            this.txt_oprpwd.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(26, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 14);
            this.label2.TabIndex = 24;
            this.label2.Text = "密  码";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(308, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // btn_server_set
            // 
            this.btn_server_set.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_server_set.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_server_set.Location = new System.Drawing.Point(228, 13);
            this.btn_server_set.Name = "btn_server_set";
            this.btn_server_set.Size = new System.Drawing.Size(70, 68);
            this.btn_server_set.TabIndex = 38;
            this.btn_server_set.Text = "设置";
            this.btn_server_set.UseVisualStyleBackColor = true;
            this.btn_server_set.Click += new System.EventHandler(this.btn_server_set_Click);
            // 
            // txt_pwd
            // 
            this.txt_pwd.Location = new System.Drawing.Point(93, 67);
            this.txt_pwd.Name = "txt_pwd";
            this.txt_pwd.PasswordChar = '*';
            this.txt_pwd.Size = new System.Drawing.Size(116, 21);
            this.txt_pwd.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(25, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 14);
            this.label5.TabIndex = 37;
            this.label5.Text = "平台密码";
            // 
            // txt_userid
            // 
            this.txt_userid.Location = new System.Drawing.Point(93, 38);
            this.txt_userid.Name = "txt_userid";
            this.txt_userid.Size = new System.Drawing.Size(116, 21);
            this.txt_userid.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(26, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 33;
            this.label6.Text = "平台用户";
            // 
            // txt_server
            // 
            this.txt_server.Location = new System.Drawing.Point(93, 8);
            this.txt_server.Name = "txt_server";
            this.txt_server.Size = new System.Drawing.Size(116, 21);
            this.txt_server.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(25, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 14);
            this.label7.TabIndex = 32;
            this.label7.Text = "平台地址";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 267);
            this.Controls.Add(this.splitContainer_login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlarmMap 登录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLogin_FormClosing);
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.splitContainer_login.Panel1.ResumeLayout(false);
            this.splitContainer_login.Panel1.PerformLayout();
            this.splitContainer_login.Panel2.ResumeLayout(false);
            this.splitContainer_login.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_login)).EndInit();
            this.splitContainer_login.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DotNetSkin.SkinUI skinUI;
        private System.Windows.Forms.SplitContainer splitContainer_login;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cb_userpwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.TextBox txt_oprpwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txt_pwd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_userid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_server;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_server_set;
        private System.Windows.Forms.CheckBox cb_serverset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_opruser;
    }
}
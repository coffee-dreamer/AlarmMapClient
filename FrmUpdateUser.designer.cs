namespace AlarmMapClient
{
    partial class FrmUpdateUser
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
            this.user_tabControl = new System.Windows.Forms.TabControl();
            this.add_tabPage = new System.Windows.Forms.TabPage();
            this.cancel_button = new System.Windows.Forms.Button();
            this.certain_button = new System.Windows.Forms.Button();
            this.password2_label = new System.Windows.Forms.Label();
            this.password_label = new System.Windows.Forms.Label();
            this.username_label = new System.Windows.Forms.Label();
            this.addpw2_textBox = new System.Windows.Forms.TextBox();
            this.addpw_textBox = new System.Windows.Forms.TextBox();
            this.adduser_textBox = new System.Windows.Forms.TextBox();
            this.mod_tabPage = new System.Windows.Forms.TabPage();
            this.modpw2_textBox = new System.Windows.Forms.TextBox();
            this.modpw_textBox = new System.Windows.Forms.TextBox();
            this.mod_comboBox = new System.Windows.Forms.ComboBox();
            this.pw2_lable = new System.Windows.Forms.Label();
            this.pw_label = new System.Windows.Forms.Label();
            this.user_label = new System.Windows.Forms.Label();
            this.cel_button = new System.Windows.Forms.Button();
            this.cert_button = new System.Windows.Forms.Button();
            this.del_tabPage = new System.Windows.Forms.TabPage();
            this.del_cel_button = new System.Windows.Forms.Button();
            this.del_button = new System.Windows.Forms.Button();
            this.del_comboBox = new System.Windows.Forms.ComboBox();
            this.del_label = new System.Windows.Forms.Label();
            this.user_tabControl.SuspendLayout();
            this.add_tabPage.SuspendLayout();
            this.mod_tabPage.SuspendLayout();
            this.del_tabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // user_tabControl
            // 
            this.user_tabControl.Controls.Add(this.add_tabPage);
            this.user_tabControl.Controls.Add(this.mod_tabPage);
            this.user_tabControl.Controls.Add(this.del_tabPage);
            this.user_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.user_tabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.user_tabControl.Location = new System.Drawing.Point(0, 0);
            this.user_tabControl.Name = "user_tabControl";
            this.user_tabControl.SelectedIndex = 0;
            this.user_tabControl.Size = new System.Drawing.Size(341, 282);
            this.user_tabControl.TabIndex = 0;
            // 
            // add_tabPage
            // 
            this.add_tabPage.Controls.Add(this.cancel_button);
            this.add_tabPage.Controls.Add(this.certain_button);
            this.add_tabPage.Controls.Add(this.password2_label);
            this.add_tabPage.Controls.Add(this.password_label);
            this.add_tabPage.Controls.Add(this.username_label);
            this.add_tabPage.Controls.Add(this.addpw2_textBox);
            this.add_tabPage.Controls.Add(this.addpw_textBox);
            this.add_tabPage.Controls.Add(this.adduser_textBox);
            this.add_tabPage.Location = new System.Drawing.Point(4, 22);
            this.add_tabPage.Name = "add_tabPage";
            this.add_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.add_tabPage.Size = new System.Drawing.Size(333, 256);
            this.add_tabPage.TabIndex = 0;
            this.add_tabPage.Text = "增加用户";
            this.add_tabPage.UseVisualStyleBackColor = true;
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(193, 204);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 7;
            this.cancel_button.Text = "取消";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // certain_button
            // 
            this.certain_button.Location = new System.Drawing.Point(46, 204);
            this.certain_button.Name = "certain_button";
            this.certain_button.Size = new System.Drawing.Size(75, 23);
            this.certain_button.TabIndex = 6;
            this.certain_button.Text = "确定";
            this.certain_button.UseVisualStyleBackColor = true;
            // 
            // password2_label
            // 
            this.password2_label.AutoSize = true;
            this.password2_label.Location = new System.Drawing.Point(20, 141);
            this.password2_label.Name = "password2_label";
            this.password2_label.Size = new System.Drawing.Size(59, 12);
            this.password2_label.TabIndex = 5;
            this.password2_label.Text = "确认密码:";
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(44, 82);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(35, 12);
            this.password_label.TabIndex = 4;
            this.password_label.Text = "密码:";
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Location = new System.Drawing.Point(32, 24);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(47, 12);
            this.username_label.TabIndex = 3;
            this.username_label.Text = "用户名:";
            // 
            // addpw2_textBox
            // 
            this.addpw2_textBox.Location = new System.Drawing.Point(127, 138);
            this.addpw2_textBox.MaxLength = 16;
            this.addpw2_textBox.Name = "addpw2_textBox";
            this.addpw2_textBox.PasswordChar = '*';
            this.addpw2_textBox.Size = new System.Drawing.Size(100, 21);
            this.addpw2_textBox.TabIndex = 2;
            // 
            // addpw_textBox
            // 
            this.addpw_textBox.Location = new System.Drawing.Point(127, 79);
            this.addpw_textBox.MaxLength = 16;
            this.addpw_textBox.Name = "addpw_textBox";
            this.addpw_textBox.PasswordChar = '*';
            this.addpw_textBox.Size = new System.Drawing.Size(100, 21);
            this.addpw_textBox.TabIndex = 1;
            // 
            // adduser_textBox
            // 
            this.adduser_textBox.Location = new System.Drawing.Point(127, 21);
            this.adduser_textBox.MaxLength = 16;
            this.adduser_textBox.Name = "adduser_textBox";
            this.adduser_textBox.Size = new System.Drawing.Size(100, 21);
            this.adduser_textBox.TabIndex = 0;
            // 
            // mod_tabPage
            // 
            this.mod_tabPage.Controls.Add(this.modpw2_textBox);
            this.mod_tabPage.Controls.Add(this.modpw_textBox);
            this.mod_tabPage.Controls.Add(this.mod_comboBox);
            this.mod_tabPage.Controls.Add(this.pw2_lable);
            this.mod_tabPage.Controls.Add(this.pw_label);
            this.mod_tabPage.Controls.Add(this.user_label);
            this.mod_tabPage.Controls.Add(this.cel_button);
            this.mod_tabPage.Controls.Add(this.cert_button);
            this.mod_tabPage.Location = new System.Drawing.Point(4, 22);
            this.mod_tabPage.Name = "mod_tabPage";
            this.mod_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mod_tabPage.Size = new System.Drawing.Size(333, 256);
            this.mod_tabPage.TabIndex = 1;
            this.mod_tabPage.Text = "修改密码";
            this.mod_tabPage.UseVisualStyleBackColor = true;
            // 
            // modpw2_textBox
            // 
            this.modpw2_textBox.Location = new System.Drawing.Point(133, 138);
            this.modpw2_textBox.Name = "modpw2_textBox";
            this.modpw2_textBox.PasswordChar = '*';
            this.modpw2_textBox.Size = new System.Drawing.Size(121, 21);
            this.modpw2_textBox.TabIndex = 7;
            // 
            // modpw_textBox
            // 
            this.modpw_textBox.Location = new System.Drawing.Point(133, 79);
            this.modpw_textBox.Name = "modpw_textBox";
            this.modpw_textBox.PasswordChar = '*';
            this.modpw_textBox.Size = new System.Drawing.Size(121, 21);
            this.modpw_textBox.TabIndex = 6;
            // 
            // mod_comboBox
            // 
            this.mod_comboBox.DisplayMember = "username";
            this.mod_comboBox.FormattingEnabled = true;
            this.mod_comboBox.Location = new System.Drawing.Point(133, 21);
            this.mod_comboBox.Name = "mod_comboBox";
            this.mod_comboBox.Size = new System.Drawing.Size(121, 20);
            this.mod_comboBox.TabIndex = 5;
            this.mod_comboBox.ValueMember = "username";
            // 
            // pw2_lable
            // 
            this.pw2_lable.AutoSize = true;
            this.pw2_lable.Location = new System.Drawing.Point(20, 141);
            this.pw2_lable.Name = "pw2_lable";
            this.pw2_lable.Size = new System.Drawing.Size(59, 12);
            this.pw2_lable.TabIndex = 4;
            this.pw2_lable.Text = "确认密码:";
            // 
            // pw_label
            // 
            this.pw_label.AutoSize = true;
            this.pw_label.Location = new System.Drawing.Point(44, 82);
            this.pw_label.Name = "pw_label";
            this.pw_label.Size = new System.Drawing.Size(35, 12);
            this.pw_label.TabIndex = 3;
            this.pw_label.Text = "密码:";
            // 
            // user_label
            // 
            this.user_label.AutoSize = true;
            this.user_label.Location = new System.Drawing.Point(32, 24);
            this.user_label.Name = "user_label";
            this.user_label.Size = new System.Drawing.Size(47, 12);
            this.user_label.TabIndex = 2;
            this.user_label.Text = "用户名:";
            // 
            // cel_button
            // 
            this.cel_button.Location = new System.Drawing.Point(193, 204);
            this.cel_button.Name = "cel_button";
            this.cel_button.Size = new System.Drawing.Size(75, 23);
            this.cel_button.TabIndex = 1;
            this.cel_button.Text = "取消";
            this.cel_button.UseVisualStyleBackColor = true;
            // 
            // cert_button
            // 
            this.cert_button.Location = new System.Drawing.Point(46, 204);
            this.cert_button.Name = "cert_button";
            this.cert_button.Size = new System.Drawing.Size(75, 23);
            this.cert_button.TabIndex = 0;
            this.cert_button.Text = "确定";
            this.cert_button.UseVisualStyleBackColor = true;
            // 
            // del_tabPage
            // 
            this.del_tabPage.Controls.Add(this.del_cel_button);
            this.del_tabPage.Controls.Add(this.del_button);
            this.del_tabPage.Controls.Add(this.del_comboBox);
            this.del_tabPage.Controls.Add(this.del_label);
            this.del_tabPage.Location = new System.Drawing.Point(4, 22);
            this.del_tabPage.Name = "del_tabPage";
            this.del_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.del_tabPage.Size = new System.Drawing.Size(333, 256);
            this.del_tabPage.TabIndex = 2;
            this.del_tabPage.Text = "删除用户";
            this.del_tabPage.UseVisualStyleBackColor = true;
            // 
            // del_cel_button
            // 
            this.del_cel_button.Location = new System.Drawing.Point(191, 179);
            this.del_cel_button.Name = "del_cel_button";
            this.del_cel_button.Size = new System.Drawing.Size(75, 23);
            this.del_cel_button.TabIndex = 9;
            this.del_cel_button.Text = "取消";
            this.del_cel_button.UseVisualStyleBackColor = true;
            // 
            // del_button
            // 
            this.del_button.Location = new System.Drawing.Point(45, 179);
            this.del_button.Name = "del_button";
            this.del_button.Size = new System.Drawing.Size(75, 23);
            this.del_button.TabIndex = 8;
            this.del_button.Text = "删除";
            this.del_button.UseVisualStyleBackColor = true;
            // 
            // del_comboBox
            // 
            this.del_comboBox.DisplayMember = "username";
            this.del_comboBox.FormattingEnabled = true;
            this.del_comboBox.Location = new System.Drawing.Point(156, 64);
            this.del_comboBox.Name = "del_comboBox";
            this.del_comboBox.Size = new System.Drawing.Size(121, 20);
            this.del_comboBox.TabIndex = 7;
            this.del_comboBox.ValueMember = "username";
            // 
            // del_label
            // 
            this.del_label.AutoSize = true;
            this.del_label.Location = new System.Drawing.Point(55, 67);
            this.del_label.Name = "del_label";
            this.del_label.Size = new System.Drawing.Size(47, 12);
            this.del_label.TabIndex = 6;
            this.del_label.Text = "用户名:";
            // 
            // FrmUpdateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 282);
            this.Controls.Add(this.user_tabControl);
            this.Name = "FrmUpdateUser";
            this.Text = "用户";
            this.Load += new System.EventHandler(this.FrmUpdateUser_Load);
            this.user_tabControl.ResumeLayout(false);
            this.add_tabPage.ResumeLayout(false);
            this.add_tabPage.PerformLayout();
            this.mod_tabPage.ResumeLayout(false);
            this.mod_tabPage.PerformLayout();
            this.del_tabPage.ResumeLayout(false);
            this.del_tabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl user_tabControl;
        private System.Windows.Forms.TabPage add_tabPage;
        private System.Windows.Forms.TabPage mod_tabPage;
        private System.Windows.Forms.Label password2_label;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.TextBox addpw2_textBox;
        private System.Windows.Forms.TextBox addpw_textBox;
        private System.Windows.Forms.TextBox adduser_textBox;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button certain_button;
        private System.Windows.Forms.TextBox modpw2_textBox;
        private System.Windows.Forms.TextBox modpw_textBox;
        private System.Windows.Forms.ComboBox mod_comboBox;
        private System.Windows.Forms.Label pw2_lable;
        private System.Windows.Forms.Label pw_label;
        private System.Windows.Forms.Label user_label;
        private System.Windows.Forms.Button cel_button;
        private System.Windows.Forms.Button cert_button;
        private System.Windows.Forms.TabPage del_tabPage;
        private System.Windows.Forms.Button del_cel_button;
        private System.Windows.Forms.Button del_button;
        private System.Windows.Forms.ComboBox del_comboBox;
        private System.Windows.Forms.Label del_label;

    }
}
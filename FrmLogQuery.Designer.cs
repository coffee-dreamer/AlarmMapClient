namespace AlarmMapClient
{
    partial class FrmLogQuery
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
            this.querylog_dataGridView = new System.Windows.Forms.DataGridView();
            this.query_button = new System.Windows.Forms.Button();
            this.deviceid_label = new System.Windows.Forms.Label();
            this.channelname_label = new System.Windows.Forms.Label();
            this.alarmstarttime_label = new System.Windows.Forms.Label();
            this.alarmstarttime_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_del = new System.Windows.Forms.Button();
            this.alarmendtime_label = new System.Windows.Forms.Label();
            this.alarmendtime_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.channelname_textBox = new System.Windows.Forms.TextBox();
            this.deviceid_textBox = new System.Windows.Forms.TextBox();
            this.device_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channel_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarm_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarm_deal_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zblog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.querylog_dataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // querylog_dataGridView
            // 
            this.querylog_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.querylog_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.device_id,
            this.channel_name,
            this.alarm_time,
            this.alarm_deal_time,
            this.username,
            this.tel,
            this.address,
            this.zblog});
            this.querylog_dataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.querylog_dataGridView.Location = new System.Drawing.Point(0, 60);
            this.querylog_dataGridView.Name = "querylog_dataGridView";
            this.querylog_dataGridView.RowTemplate.Height = 23;
            this.querylog_dataGridView.Size = new System.Drawing.Size(914, 262);
            this.querylog_dataGridView.TabIndex = 0;
            // 
            // query_button
            // 
            this.query_button.Location = new System.Drawing.Point(787, 14);
            this.query_button.Name = "query_button";
            this.query_button.Size = new System.Drawing.Size(51, 23);
            this.query_button.TabIndex = 1;
            this.query_button.Text = "查询";
            this.query_button.UseVisualStyleBackColor = true;
            this.query_button.Click += new System.EventHandler(this.query_button_Click);
            // 
            // deviceid_label
            // 
            this.deviceid_label.AutoSize = true;
            this.deviceid_label.Location = new System.Drawing.Point(15, 22);
            this.deviceid_label.Name = "deviceid_label";
            this.deviceid_label.Size = new System.Drawing.Size(47, 12);
            this.deviceid_label.TabIndex = 2;
            this.deviceid_label.Text = "设备ID:";
            // 
            // channelname_label
            // 
            this.channelname_label.AutoSize = true;
            this.channelname_label.Location = new System.Drawing.Point(195, 22);
            this.channelname_label.Name = "channelname_label";
            this.channelname_label.Size = new System.Drawing.Size(59, 12);
            this.channelname_label.TabIndex = 3;
            this.channelname_label.Text = "通道名称:";
            // 
            // alarmstarttime_label
            // 
            this.alarmstarttime_label.AutoSize = true;
            this.alarmstarttime_label.Location = new System.Drawing.Point(399, 21);
            this.alarmstarttime_label.Name = "alarmstarttime_label";
            this.alarmstarttime_label.Size = new System.Drawing.Size(59, 12);
            this.alarmstarttime_label.TabIndex = 4;
            this.alarmstarttime_label.Text = "开始时间:";
            // 
            // alarmstarttime_dateTimePicker
            // 
            this.alarmstarttime_dateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm";
            this.alarmstarttime_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.alarmstarttime_dateTimePicker.Location = new System.Drawing.Point(464, 16);
            this.alarmstarttime_dateTimePicker.Name = "alarmstarttime_dateTimePicker";
            this.alarmstarttime_dateTimePicker.ShowUpDown = true;
            this.alarmstarttime_dateTimePicker.Size = new System.Drawing.Size(123, 21);
            this.alarmstarttime_dateTimePicker.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_del);
            this.panel1.Controls.Add(this.alarmendtime_label);
            this.panel1.Controls.Add(this.alarmendtime_dateTimePicker);
            this.panel1.Controls.Add(this.channelname_textBox);
            this.panel1.Controls.Add(this.deviceid_textBox);
            this.panel1.Controls.Add(this.alarmstarttime_label);
            this.panel1.Controls.Add(this.query_button);
            this.panel1.Controls.Add(this.deviceid_label);
            this.panel1.Controls.Add(this.alarmstarttime_dateTimePicker);
            this.panel1.Controls.Add(this.channelname_label);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(914, 54);
            this.panel1.TabIndex = 8;
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(844, 14);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(45, 23);
            this.btn_del.TabIndex = 12;
            this.btn_del.Text = "删除";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // alarmendtime_label
            // 
            this.alarmendtime_label.AutoSize = true;
            this.alarmendtime_label.Location = new System.Drawing.Point(593, 20);
            this.alarmendtime_label.Name = "alarmendtime_label";
            this.alarmendtime_label.Size = new System.Drawing.Size(59, 12);
            this.alarmendtime_label.TabIndex = 11;
            this.alarmendtime_label.Text = "结束时间:";
            // 
            // alarmendtime_dateTimePicker
            // 
            this.alarmendtime_dateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm";
            this.alarmendtime_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.alarmendtime_dateTimePicker.Location = new System.Drawing.Point(658, 14);
            this.alarmendtime_dateTimePicker.Name = "alarmendtime_dateTimePicker";
            this.alarmendtime_dateTimePicker.ShowUpDown = true;
            this.alarmendtime_dateTimePicker.Size = new System.Drawing.Size(123, 21);
            this.alarmendtime_dateTimePicker.TabIndex = 10;
            // 
            // channelname_textBox
            // 
            this.channelname_textBox.Location = new System.Drawing.Point(261, 17);
            this.channelname_textBox.MaxLength = 50;
            this.channelname_textBox.Name = "channelname_textBox";
            this.channelname_textBox.Size = new System.Drawing.Size(121, 21);
            this.channelname_textBox.TabIndex = 9;
            // 
            // deviceid_textBox
            // 
            this.deviceid_textBox.Location = new System.Drawing.Point(68, 16);
            this.deviceid_textBox.MaxLength = 20;
            this.deviceid_textBox.Name = "deviceid_textBox";
            this.deviceid_textBox.Size = new System.Drawing.Size(121, 21);
            this.deviceid_textBox.TabIndex = 8;
            // 
            // device_id
            // 
            this.device_id.DataPropertyName = "DeviceId";
            this.device_id.HeaderText = "设备ID";
            this.device_id.Name = "device_id";
            this.device_id.ReadOnly = true;
            // 
            // channel_name
            // 
            this.channel_name.DataPropertyName = "ChannelName";
            this.channel_name.HeaderText = "通道名称";
            this.channel_name.Name = "channel_name";
            this.channel_name.ReadOnly = true;
            this.channel_name.Width = 180;
            // 
            // alarm_time
            // 
            this.alarm_time.DataPropertyName = "AlarmTime";
            this.alarm_time.HeaderText = "报警时间";
            this.alarm_time.Name = "alarm_time";
            this.alarm_time.ReadOnly = true;
            this.alarm_time.Width = 120;
            // 
            // alarm_deal_time
            // 
            this.alarm_deal_time.DataPropertyName = "AlarmDealTime";
            this.alarm_deal_time.HeaderText = "报警处理时间";
            this.alarm_deal_time.Name = "alarm_deal_time";
            this.alarm_deal_time.ReadOnly = true;
            // 
            // username
            // 
            this.username.DataPropertyName = "UserName";
            this.username.HeaderText = "报警人";
            this.username.Name = "username";
            this.username.ReadOnly = true;
            // 
            // tel
            // 
            this.tel.DataPropertyName = "Tel";
            this.tel.HeaderText = "电话";
            this.tel.Name = "tel";
            this.tel.ReadOnly = true;
            // 
            // address
            // 
            this.address.DataPropertyName = "Adress";
            this.address.HeaderText = "地址";
            this.address.Name = "address";
            this.address.ReadOnly = true;
            // 
            // zblog
            // 
            this.zblog.DataPropertyName = "ZbLog";
            this.zblog.HeaderText = "值班日志";
            this.zblog.Name = "zblog";
            // 
            // FrmLogQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 322);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.querylog_dataGridView);
            this.Name = "FrmLogQuery";
            this.Text = "日志综合查询";
            this.Load += new System.EventHandler(this.FrmLogQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.querylog_dataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView querylog_dataGridView;
        private System.Windows.Forms.Button query_button;
        private System.Windows.Forms.Label deviceid_label;
        private System.Windows.Forms.Label channelname_label;
        private System.Windows.Forms.Label alarmstarttime_label;
        private System.Windows.Forms.DateTimePicker alarmstarttime_dateTimePicker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox channelname_textBox;
        private System.Windows.Forms.TextBox deviceid_textBox;
        private System.Windows.Forms.Label alarmendtime_label;
        private System.Windows.Forms.DateTimePicker alarmendtime_dateTimePicker;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.DataGridViewTextBoxColumn device_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn channel_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarm_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarm_deal_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.DataGridViewTextBoxColumn zblog;
    }
}
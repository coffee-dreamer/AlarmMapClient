namespace AlarmMapClient
{
    partial class FrmPlayBack
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
            this.components = new System.ComponentModel.Container();
            this.playback_tabControl = new System.Windows.Forms.TabControl();
            this.network_tabPage = new System.Windows.Forms.TabPage();
            this.top_panel = new System.Windows.Forms.Panel();
            this.download_progressBar = new System.Windows.Forms.ProgressBar();
            this.network_trackBar = new System.Windows.Forms.TrackBar();
            this.networkplay_panel = new System.Windows.Forms.Panel();
            this.time_checkBox = new System.Windows.Forms.CheckBox();
            this.query_button = new System.Windows.Forms.Button();
            this.networkpause_button = new System.Windows.Forms.Button();
            this.data_dataGridView = new System.Windows.Forms.DataGridView();
            this.序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.文件名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.networkquickplay_button = new System.Windows.Forms.Button();
            this.networkopenvoice_button = new System.Windows.Forms.Button();
            this.download_button = new System.Windows.Forms.Button();
            this.networkslow_button = new System.Windows.Forms.Button();
            this.file_checkBox = new System.Windows.Forms.CheckBox();
            this.networkstop_button = new System.Windows.Forms.Button();
            this.pre_button = new System.Windows.Forms.Button();
            this.networkplay_button = new System.Windows.Forms.Button();
            this.channel_label = new System.Windows.Forms.Label();
            this.next_button = new System.Windows.Forms.Button();
            this.playbacktype_label = new System.Windows.Forms.Label();
            this.channel_comboBox = new System.Windows.Forms.ComboBox();
            this.endtime_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.type_label = new System.Windows.Forms.Label();
            this.starttime_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.type_comboBox = new System.Windows.Forms.ComboBox();
            this.endtime_label = new System.Windows.Forms.Label();
            this.starttime_label = new System.Windows.Forms.Label();
            this.local_tabPage = new System.Windows.Forms.TabPage();
            this.local_trackBar = new System.Windows.Forms.TrackBar();
            this.slowplay_button = new System.Windows.Forms.Button();
            this.openvoice_button = new System.Windows.Forms.Button();
            this.quickplay_button = new System.Windows.Forms.Button();
            this.pause_button = new System.Windows.Forms.Button();
            this.stop_button = new System.Windows.Forms.Button();
            this.play_button = new System.Windows.Forms.Button();
            this.open_button = new System.Windows.Forms.Button();
            this.localplay_panel = new System.Windows.Forms.Panel();
            this.download_timer = new System.Windows.Forms.Timer(this.components);
            this.play_timer = new System.Windows.Forms.Timer(this.components);
            this.playback_tabControl.SuspendLayout();
            this.network_tabPage.SuspendLayout();
            this.top_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.network_trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_dataGridView)).BeginInit();
            this.local_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.local_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // playback_tabControl
            // 
            this.playback_tabControl.Controls.Add(this.network_tabPage);
            this.playback_tabControl.Controls.Add(this.local_tabPage);
            this.playback_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playback_tabControl.Location = new System.Drawing.Point(0, 0);
            this.playback_tabControl.Name = "playback_tabControl";
            this.playback_tabControl.SelectedIndex = 0;
            this.playback_tabControl.Size = new System.Drawing.Size(1031, 712);
            this.playback_tabControl.TabIndex = 0;
            this.playback_tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // network_tabPage
            // 
            this.network_tabPage.Controls.Add(this.top_panel);
            this.network_tabPage.Location = new System.Drawing.Point(4, 22);
            this.network_tabPage.Name = "network_tabPage";
            this.network_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.network_tabPage.Size = new System.Drawing.Size(1023, 686);
            this.network_tabPage.TabIndex = 0;
            this.network_tabPage.Text = "网络回放";
            this.network_tabPage.UseVisualStyleBackColor = true;
            // 
            // top_panel
            // 
            this.top_panel.Controls.Add(this.download_progressBar);
            this.top_panel.Controls.Add(this.network_trackBar);
            this.top_panel.Controls.Add(this.networkplay_panel);
            this.top_panel.Controls.Add(this.time_checkBox);
            this.top_panel.Controls.Add(this.query_button);
            this.top_panel.Controls.Add(this.networkpause_button);
            this.top_panel.Controls.Add(this.data_dataGridView);
            this.top_panel.Controls.Add(this.networkquickplay_button);
            this.top_panel.Controls.Add(this.networkopenvoice_button);
            this.top_panel.Controls.Add(this.download_button);
            this.top_panel.Controls.Add(this.networkslow_button);
            this.top_panel.Controls.Add(this.file_checkBox);
            this.top_panel.Controls.Add(this.networkstop_button);
            this.top_panel.Controls.Add(this.pre_button);
            this.top_panel.Controls.Add(this.networkplay_button);
            this.top_panel.Controls.Add(this.channel_label);
            this.top_panel.Controls.Add(this.next_button);
            this.top_panel.Controls.Add(this.playbacktype_label);
            this.top_panel.Controls.Add(this.channel_comboBox);
            this.top_panel.Controls.Add(this.endtime_dateTimePicker);
            this.top_panel.Controls.Add(this.type_label);
            this.top_panel.Controls.Add(this.starttime_dateTimePicker);
            this.top_panel.Controls.Add(this.type_comboBox);
            this.top_panel.Controls.Add(this.endtime_label);
            this.top_panel.Controls.Add(this.starttime_label);
            this.top_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.top_panel.Location = new System.Drawing.Point(3, 3);
            this.top_panel.Name = "top_panel";
            this.top_panel.Size = new System.Drawing.Size(1017, 680);
            this.top_panel.TabIndex = 13;
            // 
            // download_progressBar
            // 
            this.download_progressBar.Location = new System.Drawing.Point(5, 640);
            this.download_progressBar.Name = "download_progressBar";
            this.download_progressBar.Size = new System.Drawing.Size(374, 23);
            this.download_progressBar.TabIndex = 14;
            this.download_progressBar.Visible = false;
            // 
            // network_trackBar
            // 
            this.network_trackBar.Location = new System.Drawing.Point(385, 589);
            this.network_trackBar.Maximum = 100;
            this.network_trackBar.Name = "network_trackBar";
            this.network_trackBar.Size = new System.Drawing.Size(627, 45);
            this.network_trackBar.TabIndex = 13;
            this.network_trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // networkplay_panel
            // 
            this.networkplay_panel.BackColor = System.Drawing.Color.Black;
            this.networkplay_panel.Location = new System.Drawing.Point(385, 13);
            this.networkplay_panel.Name = "networkplay_panel";
            this.networkplay_panel.Size = new System.Drawing.Size(627, 570);
            this.networkplay_panel.TabIndex = 2;
            // 
            // time_checkBox
            // 
            this.time_checkBox.AutoSize = true;
            this.time_checkBox.Location = new System.Drawing.Point(184, 138);
            this.time_checkBox.Name = "time_checkBox";
            this.time_checkBox.Size = new System.Drawing.Size(72, 16);
            this.time_checkBox.TabIndex = 12;
            this.time_checkBox.Text = "按时间段";
            this.time_checkBox.UseVisualStyleBackColor = true;
            this.time_checkBox.Click += new System.EventHandler(this.time_checkBox_Click);
            // 
            // query_button
            // 
            this.query_button.Location = new System.Drawing.Point(194, 611);
            this.query_button.Name = "query_button";
            this.query_button.Size = new System.Drawing.Size(75, 23);
            this.query_button.TabIndex = 2;
            this.query_button.Text = "录像查询";
            this.query_button.UseVisualStyleBackColor = true;
            this.query_button.Click += new System.EventHandler(this.query_button_Click);
            // 
            // networkpause_button
            // 
            this.networkpause_button.Location = new System.Drawing.Point(615, 647);
            this.networkpause_button.Name = "networkpause_button";
            this.networkpause_button.Size = new System.Drawing.Size(42, 23);
            this.networkpause_button.TabIndex = 6;
            this.networkpause_button.Text = "暂停";
            this.networkpause_button.UseVisualStyleBackColor = true;
            this.networkpause_button.Click += new System.EventHandler(this.networkpause_button_Click);
            // 
            // data_dataGridView
            // 
            this.data_dataGridView.AllowUserToAddRows = false;
            this.data_dataGridView.AllowUserToDeleteRows = false;
            this.data_dataGridView.AllowUserToOrderColumns = true;
            this.data_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序号,
            this.文件名});
            this.data_dataGridView.Location = new System.Drawing.Point(5, 170);
            this.data_dataGridView.MultiSelect = false;
            this.data_dataGridView.Name = "data_dataGridView";
            this.data_dataGridView.ReadOnly = true;
            this.data_dataGridView.RowTemplate.Height = 23;
            this.data_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.data_dataGridView.Size = new System.Drawing.Size(374, 429);
            this.data_dataGridView.TabIndex = 0;
            // 
            // 序号
            // 
            this.序号.DataPropertyName = "序号";
            this.序号.HeaderText = "序号";
            this.序号.Name = "序号";
            this.序号.ReadOnly = true;
            this.序号.Width = 60;
            // 
            // 文件名
            // 
            this.文件名.DataPropertyName = "文件名";
            this.文件名.HeaderText = "文件名";
            this.文件名.Name = "文件名";
            this.文件名.ReadOnly = true;
            this.文件名.Width = 280;
            // 
            // networkquickplay_button
            // 
            this.networkquickplay_button.Location = new System.Drawing.Point(692, 647);
            this.networkquickplay_button.Name = "networkquickplay_button";
            this.networkquickplay_button.Size = new System.Drawing.Size(39, 23);
            this.networkquickplay_button.TabIndex = 7;
            this.networkquickplay_button.Text = "快放";
            this.networkquickplay_button.UseVisualStyleBackColor = true;
            this.networkquickplay_button.Click += new System.EventHandler(this.networkquickplay_button_Click);
            // 
            // networkopenvoice_button
            // 
            this.networkopenvoice_button.Location = new System.Drawing.Point(859, 647);
            this.networkopenvoice_button.Name = "networkopenvoice_button";
            this.networkopenvoice_button.Size = new System.Drawing.Size(75, 23);
            this.networkopenvoice_button.TabIndex = 9;
            this.networkopenvoice_button.Text = "打开声音";
            this.networkopenvoice_button.UseVisualStyleBackColor = true;
            this.networkopenvoice_button.Click += new System.EventHandler(this.networkopenvoice_button_Click);
            // 
            // download_button
            // 
            this.download_button.Location = new System.Drawing.Point(285, 611);
            this.download_button.Name = "download_button";
            this.download_button.Size = new System.Drawing.Size(75, 23);
            this.download_button.TabIndex = 3;
            this.download_button.Text = "下载";
            this.download_button.UseVisualStyleBackColor = true;
            this.download_button.Click += new System.EventHandler(this.download_button_Click);
            // 
            // networkslow_button
            // 
            this.networkslow_button.Location = new System.Drawing.Point(766, 647);
            this.networkslow_button.Name = "networkslow_button";
            this.networkslow_button.Size = new System.Drawing.Size(42, 23);
            this.networkslow_button.TabIndex = 8;
            this.networkslow_button.Text = "慢放";
            this.networkslow_button.UseVisualStyleBackColor = true;
            this.networkslow_button.Click += new System.EventHandler(this.networkslow_button_Click);
            // 
            // file_checkBox
            // 
            this.file_checkBox.AutoSize = true;
            this.file_checkBox.Checked = true;
            this.file_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.file_checkBox.Location = new System.Drawing.Point(91, 138);
            this.file_checkBox.Name = "file_checkBox";
            this.file_checkBox.Size = new System.Drawing.Size(72, 16);
            this.file_checkBox.TabIndex = 11;
            this.file_checkBox.Text = "按文件名";
            this.file_checkBox.UseVisualStyleBackColor = true;
            this.file_checkBox.Click += new System.EventHandler(this.file_checkBox_Click);
            // 
            // networkstop_button
            // 
            this.networkstop_button.Location = new System.Drawing.Point(539, 647);
            this.networkstop_button.Name = "networkstop_button";
            this.networkstop_button.Size = new System.Drawing.Size(39, 23);
            this.networkstop_button.TabIndex = 5;
            this.networkstop_button.Text = "停止";
            this.networkstop_button.UseVisualStyleBackColor = true;
            this.networkstop_button.Click += new System.EventHandler(this.networkstop_button_Click);
            // 
            // pre_button
            // 
            this.pre_button.Location = new System.Drawing.Point(18, 611);
            this.pre_button.Name = "pre_button";
            this.pre_button.Size = new System.Drawing.Size(75, 23);
            this.pre_button.TabIndex = 0;
            this.pre_button.Text = "上一页";
            this.pre_button.UseVisualStyleBackColor = true;
            this.pre_button.Click += new System.EventHandler(this.pre_button_Click);
            // 
            // networkplay_button
            // 
            this.networkplay_button.Location = new System.Drawing.Point(452, 647);
            this.networkplay_button.Name = "networkplay_button";
            this.networkplay_button.Size = new System.Drawing.Size(48, 23);
            this.networkplay_button.TabIndex = 4;
            this.networkplay_button.Text = "播放";
            this.networkplay_button.UseVisualStyleBackColor = true;
            this.networkplay_button.Click += new System.EventHandler(this.networkplay_button_Click);
            // 
            // channel_label
            // 
            this.channel_label.AutoSize = true;
            this.channel_label.Location = new System.Drawing.Point(24, 13);
            this.channel_label.Name = "channel_label";
            this.channel_label.Size = new System.Drawing.Size(29, 12);
            this.channel_label.TabIndex = 2;
            this.channel_label.Text = "通道";
            // 
            // next_button
            // 
            this.next_button.Location = new System.Drawing.Point(100, 611);
            this.next_button.Name = "next_button";
            this.next_button.Size = new System.Drawing.Size(75, 23);
            this.next_button.TabIndex = 1;
            this.next_button.Text = "下一页";
            this.next_button.UseVisualStyleBackColor = true;
            this.next_button.Click += new System.EventHandler(this.next_button_Click);
            // 
            // playbacktype_label
            // 
            this.playbacktype_label.AutoSize = true;
            this.playbacktype_label.Location = new System.Drawing.Point(20, 139);
            this.playbacktype_label.Name = "playbacktype_label";
            this.playbacktype_label.Size = new System.Drawing.Size(53, 12);
            this.playbacktype_label.TabIndex = 10;
            this.playbacktype_label.Text = "回放方式";
            // 
            // channel_comboBox
            // 
            this.channel_comboBox.FormattingEnabled = true;
            this.channel_comboBox.Location = new System.Drawing.Point(59, 10);
            this.channel_comboBox.Name = "channel_comboBox";
            this.channel_comboBox.Size = new System.Drawing.Size(64, 20);
            this.channel_comboBox.TabIndex = 3;
            // 
            // endtime_dateTimePicker
            // 
            this.endtime_dateTimePicker.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.endtime_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endtime_dateTimePicker.Location = new System.Drawing.Point(105, 91);
            this.endtime_dateTimePicker.Name = "endtime_dateTimePicker";
            this.endtime_dateTimePicker.ShowUpDown = true;
            this.endtime_dateTimePicker.Size = new System.Drawing.Size(144, 21);
            this.endtime_dateTimePicker.TabIndex = 9;
            // 
            // type_label
            // 
            this.type_label.AutoSize = true;
            this.type_label.Location = new System.Drawing.Point(163, 13);
            this.type_label.Name = "type_label";
            this.type_label.Size = new System.Drawing.Size(29, 12);
            this.type_label.TabIndex = 4;
            this.type_label.Text = "类型";
            // 
            // starttime_dateTimePicker
            // 
            this.starttime_dateTimePicker.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.starttime_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.starttime_dateTimePicker.Location = new System.Drawing.Point(75, 49);
            this.starttime_dateTimePicker.Name = "starttime_dateTimePicker";
            this.starttime_dateTimePicker.ShowUpDown = true;
            this.starttime_dateTimePicker.Size = new System.Drawing.Size(146, 21);
            this.starttime_dateTimePicker.TabIndex = 8;
            // 
            // type_comboBox
            // 
            this.type_comboBox.FormattingEnabled = true;
            this.type_comboBox.Location = new System.Drawing.Point(198, 10);
            this.type_comboBox.Name = "type_comboBox";
            this.type_comboBox.Size = new System.Drawing.Size(80, 20);
            this.type_comboBox.TabIndex = 5;
            // 
            // endtime_label
            // 
            this.endtime_label.AutoSize = true;
            this.endtime_label.Location = new System.Drawing.Point(44, 97);
            this.endtime_label.Name = "endtime_label";
            this.endtime_label.Size = new System.Drawing.Size(53, 12);
            this.endtime_label.TabIndex = 7;
            this.endtime_label.Text = "结束时间";
            // 
            // starttime_label
            // 
            this.starttime_label.AutoSize = true;
            this.starttime_label.Location = new System.Drawing.Point(16, 53);
            this.starttime_label.Name = "starttime_label";
            this.starttime_label.Size = new System.Drawing.Size(53, 12);
            this.starttime_label.TabIndex = 6;
            this.starttime_label.Text = "开始时间";
            // 
            // local_tabPage
            // 
            this.local_tabPage.Controls.Add(this.local_trackBar);
            this.local_tabPage.Controls.Add(this.slowplay_button);
            this.local_tabPage.Controls.Add(this.openvoice_button);
            this.local_tabPage.Controls.Add(this.quickplay_button);
            this.local_tabPage.Controls.Add(this.pause_button);
            this.local_tabPage.Controls.Add(this.stop_button);
            this.local_tabPage.Controls.Add(this.play_button);
            this.local_tabPage.Controls.Add(this.open_button);
            this.local_tabPage.Controls.Add(this.localplay_panel);
            this.local_tabPage.Location = new System.Drawing.Point(4, 22);
            this.local_tabPage.Name = "local_tabPage";
            this.local_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.local_tabPage.Size = new System.Drawing.Size(1023, 686);
            this.local_tabPage.TabIndex = 1;
            this.local_tabPage.Text = "本地回放";
            this.local_tabPage.UseVisualStyleBackColor = true;
            // 
            // local_trackBar
            // 
            this.local_trackBar.AllowDrop = true;
            this.local_trackBar.Location = new System.Drawing.Point(94, 583);
            this.local_trackBar.Maximum = 100;
            this.local_trackBar.Name = "local_trackBar";
            this.local_trackBar.Size = new System.Drawing.Size(851, 45);
            this.local_trackBar.TabIndex = 8;
            this.local_trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.local_trackBar.Scroll += new System.EventHandler(this.local_trackBar_Scroll);
            this.local_trackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.local_trackBar_MouseUp);
            // 
            // slowplay_button
            // 
            this.slowplay_button.Location = new System.Drawing.Point(712, 640);
            this.slowplay_button.Name = "slowplay_button";
            this.slowplay_button.Size = new System.Drawing.Size(75, 23);
            this.slowplay_button.TabIndex = 7;
            this.slowplay_button.Text = "慢放";
            this.slowplay_button.UseVisualStyleBackColor = true;
            this.slowplay_button.Click += new System.EventHandler(this.slowplay_button_Click);
            // 
            // openvoice_button
            // 
            this.openvoice_button.Location = new System.Drawing.Point(828, 640);
            this.openvoice_button.Name = "openvoice_button";
            this.openvoice_button.Size = new System.Drawing.Size(75, 23);
            this.openvoice_button.TabIndex = 6;
            this.openvoice_button.Text = "打开声音";
            this.openvoice_button.UseVisualStyleBackColor = true;
            this.openvoice_button.Click += new System.EventHandler(this.openvoice_button_Click);
            // 
            // quickplay_button
            // 
            this.quickplay_button.Location = new System.Drawing.Point(589, 640);
            this.quickplay_button.Name = "quickplay_button";
            this.quickplay_button.Size = new System.Drawing.Size(75, 23);
            this.quickplay_button.TabIndex = 5;
            this.quickplay_button.Text = "快放";
            this.quickplay_button.UseVisualStyleBackColor = true;
            this.quickplay_button.Click += new System.EventHandler(this.quickplay_button_Click);
            // 
            // pause_button
            // 
            this.pause_button.Location = new System.Drawing.Point(474, 640);
            this.pause_button.Name = "pause_button";
            this.pause_button.Size = new System.Drawing.Size(75, 23);
            this.pause_button.TabIndex = 4;
            this.pause_button.Text = "暂停";
            this.pause_button.UseVisualStyleBackColor = true;
            this.pause_button.Click += new System.EventHandler(this.pause_button_Click);
            // 
            // stop_button
            // 
            this.stop_button.Location = new System.Drawing.Point(361, 640);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(75, 23);
            this.stop_button.TabIndex = 3;
            this.stop_button.Text = "停止";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // play_button
            // 
            this.play_button.Location = new System.Drawing.Point(247, 640);
            this.play_button.Name = "play_button";
            this.play_button.Size = new System.Drawing.Size(75, 23);
            this.play_button.TabIndex = 2;
            this.play_button.Text = "播放";
            this.play_button.UseVisualStyleBackColor = true;
            this.play_button.Click += new System.EventHandler(this.play_button_Click);
            // 
            // open_button
            // 
            this.open_button.Location = new System.Drawing.Point(134, 640);
            this.open_button.Name = "open_button";
            this.open_button.Size = new System.Drawing.Size(75, 23);
            this.open_button.TabIndex = 1;
            this.open_button.Text = "打开文件";
            this.open_button.UseVisualStyleBackColor = true;
            this.open_button.Click += new System.EventHandler(this.open_button_Click);
            // 
            // localplay_panel
            // 
            this.localplay_panel.BackColor = System.Drawing.Color.Black;
            this.localplay_panel.Location = new System.Drawing.Point(94, 6);
            this.localplay_panel.Name = "localplay_panel";
            this.localplay_panel.Size = new System.Drawing.Size(851, 571);
            this.localplay_panel.TabIndex = 0;
            // 
            // download_timer
            // 
            this.download_timer.Tick += new System.EventHandler(this.download_timer_Tick);
            // 
            // play_timer
            // 
            this.play_timer.Tick += new System.EventHandler(this.play_timer_Tick);
            // 
            // FrmPlayBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 712);
            this.Controls.Add(this.playback_tabControl);
            this.MaximizeBox = false;
            this.Name = "FrmPlayBack";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "录像回放";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPlayBack_Closed);
            this.Load += new System.EventHandler(this.FrmPlayBack_Load);
            this.playback_tabControl.ResumeLayout(false);
            this.network_tabPage.ResumeLayout(false);
            this.top_panel.ResumeLayout(false);
            this.top_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.network_trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_dataGridView)).EndInit();
            this.local_tabPage.ResumeLayout(false);
            this.local_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.local_trackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl playback_tabControl;
        private System.Windows.Forms.TabPage network_tabPage;
        private System.Windows.Forms.ComboBox type_comboBox;
        private System.Windows.Forms.Label type_label;
        private System.Windows.Forms.ComboBox channel_comboBox;
        private System.Windows.Forms.Label channel_label;
        private System.Windows.Forms.TabPage local_tabPage;
        private System.Windows.Forms.DateTimePicker endtime_dateTimePicker;
        private System.Windows.Forms.DateTimePicker starttime_dateTimePicker;
        private System.Windows.Forms.Label endtime_label;
        private System.Windows.Forms.Label starttime_label;
        private System.Windows.Forms.CheckBox time_checkBox;
        private System.Windows.Forms.CheckBox file_checkBox;
        private System.Windows.Forms.Label playbacktype_label;
        private System.Windows.Forms.Panel top_panel;
        private System.Windows.Forms.Button slowplay_button;
        private System.Windows.Forms.Button openvoice_button;
        private System.Windows.Forms.Button quickplay_button;
        private System.Windows.Forms.Button pause_button;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.Button play_button;
        private System.Windows.Forms.Button open_button;
        private System.Windows.Forms.Panel localplay_panel;
        private System.Windows.Forms.TrackBar local_trackBar;
        private System.Windows.Forms.Button download_button;
        private System.Windows.Forms.Button query_button;
        private System.Windows.Forms.Button next_button;
        private System.Windows.Forms.Button pre_button;
        private System.Windows.Forms.DataGridView data_dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文件名;
        private System.Windows.Forms.Panel networkplay_panel;
        private System.Windows.Forms.Button networkpause_button;
        private System.Windows.Forms.Button networkquickplay_button;
        private System.Windows.Forms.Button networkopenvoice_button;
        private System.Windows.Forms.Button networkslow_button;
        private System.Windows.Forms.Button networkstop_button;
        private System.Windows.Forms.Button networkplay_button;
        private System.Windows.Forms.TrackBar network_trackBar;
        private System.Windows.Forms.ProgressBar download_progressBar;
        private System.Windows.Forms.Timer download_timer;
        private System.Windows.Forms.Timer play_timer;
    }
}
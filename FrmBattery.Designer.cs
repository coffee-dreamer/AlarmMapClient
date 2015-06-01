namespace AlarmMapClient
{
    partial class FrmBattery
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridViewBattery = new System.Windows.Forms.DataGridView();
            this.LogTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VolPv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurPv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VolBat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurLoad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempBat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Soc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AhPv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AhLoad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VolFloat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VolCut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VolReconnect = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chartBattery = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_exporter = new DevExpress.XtraEditors.SimpleButton();
            this.cb_line3 = new System.Windows.Forms.CheckBox();
            this.cb_line2 = new System.Windows.Forms.CheckBox();
            this.cb_line1 = new System.Windows.Forms.CheckBox();
            this.btn_search = new DevExpress.XtraEditors.SimpleButton();
            this.endtime_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.starttime_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.endtime_label = new System.Windows.Forms.Label();
            this.starttime_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBattery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBattery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewBattery
            // 
            this.dataGridViewBattery.AllowUserToAddRows = false;
            this.dataGridViewBattery.AllowUserToDeleteRows = false;
            this.dataGridViewBattery.AllowUserToOrderColumns = true;
            this.dataGridViewBattery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBattery.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LogTime,
            this.VolPv,
            this.CurPv,
            this.VolBat,
            this.CurLoad,
            this.TempBat,
            this.Soc,
            this.AhPv,
            this.AhLoad,
            this.VolFloat,
            this.VolCut,
            this.VolReconnect});
            this.dataGridViewBattery.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewBattery.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewBattery.Name = "dataGridViewBattery";
            this.dataGridViewBattery.ReadOnly = true;
            this.dataGridViewBattery.RowTemplate.Height = 23;
            this.dataGridViewBattery.Size = new System.Drawing.Size(904, 249);
            this.dataGridViewBattery.TabIndex = 0;
            // 
            // LogTime
            // 
            this.LogTime.DataPropertyName = "LogTime";
            this.LogTime.HeaderText = "时间";
            this.LogTime.Name = "LogTime";
            this.LogTime.ReadOnly = true;
            // 
            // VolPv
            // 
            this.VolPv.DataPropertyName = "VolPv";
            this.VolPv.HeaderText = "PV电压";
            this.VolPv.Name = "VolPv";
            this.VolPv.ReadOnly = true;
            // 
            // CurPv
            // 
            this.CurPv.DataPropertyName = "CurPv";
            this.CurPv.HeaderText = "发电电流";
            this.CurPv.Name = "CurPv";
            this.CurPv.ReadOnly = true;
            // 
            // VolBat
            // 
            this.VolBat.DataPropertyName = "VolBat";
            this.VolBat.HeaderText = "电池电压";
            this.VolBat.Name = "VolBat";
            this.VolBat.ReadOnly = true;
            // 
            // CurLoad
            // 
            this.CurLoad.DataPropertyName = "CurLoad";
            this.CurLoad.HeaderText = "负载电流";
            this.CurLoad.Name = "CurLoad";
            this.CurLoad.ReadOnly = true;
            // 
            // TempBat
            // 
            this.TempBat.DataPropertyName = "TempBat";
            this.TempBat.HeaderText = "电池温度";
            this.TempBat.Name = "TempBat";
            this.TempBat.ReadOnly = true;
            // 
            // Soc
            // 
            this.Soc.DataPropertyName = "Soc";
            this.Soc.HeaderText = "电池负荷量";
            this.Soc.Name = "Soc";
            this.Soc.ReadOnly = true;
            // 
            // AhPv
            // 
            this.AhPv.DataPropertyName = "AhPv";
            this.AhPv.HeaderText = "发电安时数";
            this.AhPv.Name = "AhPv";
            this.AhPv.ReadOnly = true;
            // 
            // AhLoad
            // 
            this.AhLoad.DataPropertyName = "AhLoad";
            this.AhLoad.HeaderText = "累计用电安时";
            this.AhLoad.Name = "AhLoad";
            this.AhLoad.ReadOnly = true;
            // 
            // VolFloat
            // 
            this.VolFloat.DataPropertyName = "VolFloat";
            this.VolFloat.HeaderText = "浮充电压";
            this.VolFloat.Name = "VolFloat";
            this.VolFloat.ReadOnly = true;
            // 
            // VolCut
            // 
            this.VolCut.DataPropertyName = "VolCut";
            this.VolCut.HeaderText = "低电压保护电压";
            this.VolCut.Name = "VolCut";
            this.VolCut.ReadOnly = true;
            // 
            // VolReconnect
            // 
            this.VolReconnect.DataPropertyName = "VolReconnect";
            this.VolReconnect.HeaderText = "低压恢复连接电压";
            this.VolReconnect.Name = "VolReconnect";
            this.VolReconnect.ReadOnly = true;
            // 
            // chartBattery
            // 
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.Name = "ChartArea1";
            this.chartBattery.ChartAreas.Add(chartArea2);
            this.chartBattery.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chartBattery.Location = new System.Drawing.Point(0, 300);
            this.chartBattery.Name = "chartBattery";
            this.chartBattery.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Name = "Series1";
            this.chartBattery.Series.Add(series2);
            this.chartBattery.Size = new System.Drawing.Size(904, 217);
            this.chartBattery.TabIndex = 1;
            this.chartBattery.Text = "chart1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_exporter);
            this.panelControl1.Controls.Add(this.cb_line3);
            this.panelControl1.Controls.Add(this.cb_line2);
            this.panelControl1.Controls.Add(this.cb_line1);
            this.panelControl1.Controls.Add(this.btn_search);
            this.panelControl1.Controls.Add(this.endtime_dateTimePicker);
            this.panelControl1.Controls.Add(this.starttime_dateTimePicker);
            this.panelControl1.Controls.Add(this.endtime_label);
            this.panelControl1.Controls.Add(this.starttime_label);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 249);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(904, 51);
            this.panelControl1.TabIndex = 2;
            // 
            // btn_exporter
            // 
            this.btn_exporter.Location = new System.Drawing.Point(528, 10);
            this.btn_exporter.Name = "btn_exporter";
            this.btn_exporter.Size = new System.Drawing.Size(77, 35);
            this.btn_exporter.TabIndex = 18;
            this.btn_exporter.Text = "导出Excel";
            this.btn_exporter.Click += new System.EventHandler(this.btn_exporter_Click);
            // 
            // cb_line3
            // 
            this.cb_line3.AutoSize = true;
            this.cb_line3.Location = new System.Drawing.Point(791, 19);
            this.cb_line3.Name = "cb_line3";
            this.cb_line3.Size = new System.Drawing.Size(108, 16);
            this.cb_line3.TabIndex = 17;
            this.cb_line3.Text = "蓄电池电压曲线";
            this.cb_line3.UseVisualStyleBackColor = true;
            this.cb_line3.CheckedChanged += new System.EventHandler(this.cb_line3_CheckedChanged);
            // 
            // cb_line2
            // 
            this.cb_line2.AutoSize = true;
            this.cb_line2.Location = new System.Drawing.Point(696, 19);
            this.cb_line2.Name = "cb_line2";
            this.cb_line2.Size = new System.Drawing.Size(96, 16);
            this.cb_line2.TabIndex = 16;
            this.cb_line2.Text = "负载电流曲线";
            this.cb_line2.UseVisualStyleBackColor = true;
            this.cb_line2.CheckedChanged += new System.EventHandler(this.cb_line2_CheckedChanged);
            // 
            // cb_line1
            // 
            this.cb_line1.AutoSize = true;
            this.cb_line1.Checked = true;
            this.cb_line1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_line1.Location = new System.Drawing.Point(612, 19);
            this.cb_line1.Name = "cb_line1";
            this.cb_line1.Size = new System.Drawing.Size(84, 16);
            this.cb_line1.TabIndex = 15;
            this.cb_line1.Text = "PV电流曲线";
            this.cb_line1.UseVisualStyleBackColor = true;
            this.cb_line1.CheckedChanged += new System.EventHandler(this.cb_line1_CheckedChanged);
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(445, 10);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(77, 35);
            this.btn_search.TabIndex = 14;
            this.btn_search.Text = "查询";
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // endtime_dateTimePicker
            // 
            this.endtime_dateTimePicker.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.endtime_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endtime_dateTimePicker.Location = new System.Drawing.Point(285, 17);
            this.endtime_dateTimePicker.Name = "endtime_dateTimePicker";
            this.endtime_dateTimePicker.ShowUpDown = true;
            this.endtime_dateTimePicker.Size = new System.Drawing.Size(144, 21);
            this.endtime_dateTimePicker.TabIndex = 13;
            // 
            // starttime_dateTimePicker
            // 
            this.starttime_dateTimePicker.Checked = false;
            this.starttime_dateTimePicker.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.starttime_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.starttime_dateTimePicker.Location = new System.Drawing.Point(70, 17);
            this.starttime_dateTimePicker.Name = "starttime_dateTimePicker";
            this.starttime_dateTimePicker.ShowUpDown = true;
            this.starttime_dateTimePicker.Size = new System.Drawing.Size(146, 21);
            this.starttime_dateTimePicker.TabIndex = 12;
            // 
            // endtime_label
            // 
            this.endtime_label.AutoSize = true;
            this.endtime_label.Location = new System.Drawing.Point(224, 23);
            this.endtime_label.Name = "endtime_label";
            this.endtime_label.Size = new System.Drawing.Size(53, 12);
            this.endtime_label.TabIndex = 11;
            this.endtime_label.Text = "结束时间";
            // 
            // starttime_label
            // 
            this.starttime_label.AutoSize = true;
            this.starttime_label.Location = new System.Drawing.Point(11, 21);
            this.starttime_label.Name = "starttime_label";
            this.starttime_label.Size = new System.Drawing.Size(53, 12);
            this.starttime_label.TabIndex = 10;
            this.starttime_label.Text = "开始时间";
            // 
            // FrmBattery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 517);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.chartBattery);
            this.Controls.Add(this.dataGridViewBattery);
            this.MaximizeBox = false;
            this.Name = "FrmBattery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "历史数据查询";
            this.Load += new System.EventHandler(this.FrmBattery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBattery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBattery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewBattery;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBattery;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_search;
        private System.Windows.Forms.DateTimePicker endtime_dateTimePicker;
        private System.Windows.Forms.DateTimePicker starttime_dateTimePicker;
        private System.Windows.Forms.Label endtime_label;
        private System.Windows.Forms.Label starttime_label;
        private System.Windows.Forms.CheckBox cb_line3;
        private System.Windows.Forms.CheckBox cb_line2;
        private System.Windows.Forms.CheckBox cb_line1;
        private DevExpress.XtraEditors.SimpleButton btn_exporter;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn VolPv;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurPv;
        private System.Windows.Forms.DataGridViewTextBoxColumn VolBat;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempBat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Soc;
        private System.Windows.Forms.DataGridViewTextBoxColumn AhPv;
        private System.Windows.Forms.DataGridViewTextBoxColumn AhLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn VolFloat;
        private System.Windows.Forms.DataGridViewTextBoxColumn VolCut;
        private System.Windows.Forms.DataGridViewTextBoxColumn VolReconnect;
    }
}
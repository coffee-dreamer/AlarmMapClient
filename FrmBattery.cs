using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDS.CLIENT;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms.DataVisualization.Charting;

namespace AlarmMapClient
{
    public partial class FrmBattery : Form
    {
        private List<MDS.CLIENT.Domain.BatteryLog> logs = null;
        private int time = -1;//间隔天数
        private string deviceId;
        public FrmBattery(string deviceId)
        {
            InitializeComponent();
            this.deviceId = deviceId;
        }

        private void FrmBattery_Load(object sender, EventArgs e)
        {
            //当搜索时间小于一天x轴间隔为30分钟,小于5天x轴间隔为2个小时,小于10天x轴间隔为12小时,小于365天x轴间隔为24小时
            //每30分钟取最开始的一条记录
            /*
            select a.* from batterylog a, ( 
                select min(log_time) log_time from batterylog  where log_time>'2014-05-26' and log_time<'2014-05-27' 
                group by CONCAT(date_format(log_time,'%Y%m%d %H'),floor(date_format(log_time,'%i')/30)) 
            ) b where a.log_time=b.log_time order by a.log_time desc 
             * */
            //string stime = DateTime.Now.ToString("yyyy-MM-dd");
            //string etime = DateTime.Now.ToString("yyyy-MM-dd");
            //List<MDS.CLIENT.Domain.BatteryLog> logs = MDSUtils.GetBatteryLogs(stime, etime);
            dataGridViewBattery.AutoGenerateColumns = false;
            init();
        }

        private void init() {
            starttime_dateTimePicker.Value = DateTime.Now.AddDays(-1);
            endtime_dateTimePicker.Value = DateTime.Now;
            TimeSpan ts = endtime_dateTimePicker.Value - starttime_dateTimePicker.Value;
            time = ts.Days;
            logs = MDSUtils.GetBatteryLogs(starttime_dateTimePicker.Value.ToString(), endtime_dateTimePicker.Value.ToString(), deviceId);
            dataGridViewBattery.DataSource = logs;
            if (cb_line1.Checked)
            {
                showChart(cb_line1.Name);
            }
            else if (cb_line2.Checked)
            {
                showChart(cb_line2.Name);
            }
            else if (cb_line3.Checked)
            {
                showChart(cb_line3.Name);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            DateTime startTimeD = this.starttime_dateTimePicker.Value;
            DateTime endTimeD = this.endtime_dateTimePicker.Value;
            if (startTimeD.CompareTo(endTimeD) >= 0)
            {
                MessageBox.Show("开始时间不能大于或等于结束时间");
                return;
            }
            string startTime = startTimeD.ToString();
            string endTime = endTimeD.ToString();
            logs = MDSUtils.GetBatteryLogs(startTime, endTime, deviceId);
            dataGridViewBattery.DataSource = logs;

            if (logs == null || logs.Count == 0)
            {
                MessageBox.Show("未查询数据");
                return;
            }

            TimeSpan ts = endTimeD - startTimeD;
            time = ts.Days;//间隔天数
            if (cb_line1.Checked)
            {
                showChart(cb_line1.Name);
            }
            else if (cb_line2.Checked)
            {
                showChart(cb_line2.Name);
            }
            else if (cb_line3.Checked)
            {
                showChart(cb_line3.Name);
            }

        }

        private void btn_exporter_Click(object sender, EventArgs e)
        {
            if (logs == null || logs.Count == 0) {
                MessageBox.Show("没有数据可以进行导出操作!");
                return;
            }
            bool ret = ExportDataGridview(dataGridViewBattery);

            if (ret)
            {
                MessageBox.Show("导出成功!");
            }
            else {
                MessageBox.Show("导出失败!");
            }
        }
        private void showChart(string lineName)
        {
            try
            {
                chartBattery.ChartAreas.Clear();
                chartBattery.Series.Clear();
                chartBattery.Annotations.Clear();
                chartBattery.Legends.Clear();
                chartBattery.Titles.Clear();
                ChartArea c1 = new ChartArea("C1");
                //c1.AxisX.IntervalType = DateTimeIntervalType.Minutes;//X轴以天数为间隔 
                //c1.AxisX.Interval = 30; //X轴刻度的间隔为1 
                //c1.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                if (time < 1)
                {
                    c1.AxisX.LabelStyle.Format = "dd HH:mm";
                    c1.AxisX.IntervalType = DateTimeIntervalType.Minutes;
                    c1.AxisX.Interval = 30;
                }
                else if (time >= 1 && time <= 5) {
                    c1.AxisX.LabelStyle.Format = "MM-dd HH";
                    c1.AxisX.IntervalType = DateTimeIntervalType.Hours;
                    c1.AxisX.Interval = 2;
                }
                else if (time > 5 && time <= 12){
                    c1.AxisX.LabelStyle.Format = "MM-dd HH";
                    c1.AxisX.IntervalType = DateTimeIntervalType.Hours;
                    c1.AxisX.Interval = 12;
                }
                else if (time > 12 && time < 365)
                {
                    c1.AxisX.LabelStyle.Format = "MM-dd HH";
                    c1.AxisX.IntervalType = DateTimeIntervalType.Hours;
                    c1.AxisX.Interval = 24;
                }
                else {
                    c1.AxisX.LabelStyle.Format = "yyyy-MM-dd";
                    c1.AxisX.IntervalType = DateTimeIntervalType.Months;
                    c1.AxisX.Interval = 1;
                }
                
                chartBattery.ChartAreas.Add(c1);
                Series s1 = new Series("S1");
                s1.ChartType = SeriesChartType.Spline; //画表类型 
                s1.XValueType = ChartValueType.DateTime;

                
                if (cb_line1.Name.Equals(lineName))
                {
                    for (int i = 0; i < logs.Count; i++)
                    {
                        DateTime date = logs[i].LogTime;
                        DataPoint d = new DataPoint(); //要显示的数据
                        d.XValue = date.ToOADate(); //X轴的值，DataPoint只接收double类型
                        d.YValues[0] = logs[i].CurPv; //Y轴的值 
                        s1.Points.Add(d);
                    }
                }
                else if (cb_line2.Name.Equals(lineName))
                {
                    for (int i = 0; i < logs.Count; i++)
                    {
                        DateTime date = logs[i].LogTime;
                        DataPoint d = new DataPoint(); //要显示的数据
                        d.XValue = date.ToOADate(); //X轴的值，DataPoint只接收double类型
                        d.YValues[0] = logs[i].CurLoad; //Y轴的值 
                        s1.Points.Add(d);
                    }
                }
                else if (cb_line3.Name.Equals(lineName))
                {
                    for (int i = 0; i < logs.Count; i++)
                    {
                        DateTime date = logs[i].LogTime;
                        DataPoint d = new DataPoint(); //要显示的数据
                        d.XValue = date.ToOADate(); //X轴的值，DataPoint只接收double类型
                        d.YValues[0] = logs[i].VolBat; //Y轴的值 
                        s1.Points.Add(d);
                    }
                }
                chartBattery.Series.Add(s1);
            }catch(Exception ex){
                Console.WriteLine(ex.ToString());
            }
            
        }
        private void cb_line1_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_line1.Checked)
            {
                cb_line2.Checked = false;
                cb_line3.Checked = false;
                showChart(cb_line1.Name);
            }
            else {
                if (!cb_line2.Checked && !cb_line3.Checked)
                {
                    cb_line1.Checked = true;
                }
            }
        }

        private void cb_line2_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_line2.Checked)
            {
                cb_line1.Checked = false;
                cb_line3.Checked = false;
                showChart(cb_line2.Name);
            }
            else
            {
                if (!cb_line1.Checked && !cb_line3.Checked)
                {
                    cb_line2.Checked = true;
                }
            }
        }

        

        private void cb_line3_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_line3.Checked)
            {
                cb_line2.Checked = false;
                cb_line1.Checked = false;

                showChart(cb_line3.Name);
            }
            else
            {
                if (!cb_line2.Checked && !cb_line1.Checked)
                {
                    cb_line3.Checked = true;
                }
            }
        }

        #region 将DataGridView控件中数据导出到Excel
        /// <summary>
        /// 将DataGridView控件中数据导出到Excel
        /// </summary>
        /// <param name="gridView">DataGridView对象</param>
        /// <param name="isShowExcle">是否显示Excel界面</param>
        /// <returns></returns>
        public bool ExportDataGridview(DataGridView gridView)
        {

            //if (gridView.Rows.Count == 0)
            //    return false;

            //建立Excel对象
            //Excel.Application excel = new Excel.Application();
            //excel.Application.Workbooks.Add(true);
            //excel.Visible = isShowExcle;
            ////生成字段名称
            //for (int i = 0; i < gridView.ColumnCount; i++)
            //{
            //    excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;
            //}
            ////填充数据
            //for (int i = 0; i < gridView.RowCount; i++)
            //{
            //    for (int j = 0; j < gridView.ColumnCount; j++)
            //    {
            //        excel.Cells[i + 2, j + 1] = gridView[j, i].Value.ToString();
            //    }
            //}

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl files (*.xls)|*.xls";
            dlg.DefaultExt = "xls";
            dlg.CreatePrompt = true;
            dlg.Title = "保存为Excel文件";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream fileStream = null;
                fileStream = dlg.OpenFile();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileStream, System.Text.Encoding.GetEncoding(-0));
                string columnTitle = "";
                try
                {
                    //写入列标题
                    for (int i = 0; i < gridView.ColumnCount; i++)
                    {
                        if (i > 0)
                        {
                            columnTitle += "\t";
                        }
                        columnTitle += gridView.Columns[i].HeaderText;
                    }
                    sw.WriteLine(columnTitle);
                    //写入列内容
                    for (int j = 0; j < gridView.Rows.Count; j++)
                    {
                        string columnValue = "";
                        for (int k = 0; k < gridView.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                columnValue += "\t";
                            }
                            if (gridView.Rows[j].Cells[k].Value == null)
                                columnValue += "";
                            else
                                columnValue += gridView.Rows[j].Cells[k].Value.ToString().Trim();
                        }
                        sw.WriteLine(columnValue);
                    }
                    sw.Close();
                    fileStream.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return false;
                }
                finally
                {
                    sw.Close();
                    fileStream.Close();
                }
            }

            return true;
        }
        #endregion

    }
}

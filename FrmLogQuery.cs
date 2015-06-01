using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDS.Bll;
using System.Threading;

namespace AlarmMapClient
{
    public partial class FrmLogQuery : Form
    {
        public FrmLogQuery()
        {
            InitializeComponent();
        }

        private void FrmLogQuery_Load(object sender, EventArgs e)
        {
            //Thread t = new Thread(this.LoadAlarmThread);
            //t.Start();
            this.querylog_dataGridView.AutoGenerateColumns = false;
            LoadAlarm();
        }

        private void LoadAlarmThread() {
            LoadAlarmLog ual = new LoadAlarmLog(this.LoadAlarm);
            this.BeginInvoke(ual);
        }
        private delegate void LoadAlarmLog();
        private void LoadAlarm() {
            List<MDS.Domain.AlarmLog> logs = AlarmLogBll.QueryLogs();
            this.querylog_dataGridView.DataSource = null;
            this.querylog_dataGridView.DataSource = logs;
        }

        private void query_button_Click(object sender, EventArgs e)
        {
            string deviceId = this.deviceid_textBox.Text;
            string channelName = this.channelname_textBox.Text;
            DateTime alarmstartTime = this.alarmstarttime_dateTimePicker.Value;
            DateTime alarmendTime = this.alarmendtime_dateTimePicker.Value;
            List<MDS.Domain.AlarmLog> logs = AlarmLogBll.QueryLogsByDeviceIdAndChannelName(deviceId, channelName, alarmstartTime, alarmendTime);
            this.querylog_dataGridView.DataSource = null;
            this.querylog_dataGridView.DataSource = logs;
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            string deviceId = this.deviceid_textBox.Text;
            string channelName = this.channelname_textBox.Text;
            DateTime alarmstartTime = this.alarmstarttime_dateTimePicker.Value;
            DateTime alarmendTime = this.alarmendtime_dateTimePicker.Value;
            AlarmLogBll.DelLogs(deviceId, channelName, alarmstartTime, alarmendTime);
            List<MDS.Domain.AlarmLog> logs = AlarmLogBll.QueryLogsByDeviceIdAndChannelName(deviceId, channelName, alarmstartTime, alarmendTime);
            this.querylog_dataGridView.DataSource = null;
            this.querylog_dataGridView.DataSource = logs;
        }

    }
}

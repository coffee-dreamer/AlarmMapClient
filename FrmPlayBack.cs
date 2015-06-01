using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;

namespace AlarmMapClient
{
    public partial class FrmPlayBack : Form
    {
        private Int32 port;
        private int quicktime = 0;
        private int slowtime = 0;
        private String fileName = null;//获取的文件名 保存或者打开文件
        private Int32 fileHandle = 0;
        private Int32 handle = 0;
        private Int32 localHandle = 0;
        private Int32 networkHandle = 0;
        private Int32 fileSize = 0;
        private NetSDK.fPlayDrawCallBack fdcb;
        private NetSDK.fLocalPlayFileCallBack fpfc;
        private NetSDK.InfoFramCallBack ifc;
        private NetSDK.fRealDataCallBack rdcb;
        private NetSDK.fDownLoadPosCallBack dlpcb;
        private NetSDK.fRealDataCallBack_V2 rdcbv2;
        private Int32 user = 0;
        private bool isPlay = false;
        private Int32 loginId = 0;
        private int channelNum = 0;
        private bool isPause = false;
        private List<NetSDK.H264_DVR_FILE_DATA> fileDataList = new List<NetSDK.H264_DVR_FILE_DATA>();
        private int currPage = 1;//当前页
        private int pageSize = 20;//每页行数
        public FrmPlayBack(int channelId,Int32 loginId,int channelNum)
        {
            InitializeComponent();
            this.port = channelId;
            this.loginId = loginId;
            this.channelNum = channelNum;
        }

        private void FrmPlayBack_Load(object sender, EventArgs e)
        {
            this.type_comboBox.DisplayMember = "Text";
            this.type_comboBox.ValueMember = "Value";
            ComboBoxItem item = new ComboBoxItem("全", NetSDK.SDK_File_Type.SDK_RECORD_ALL);
            this.type_comboBox.Items.Add(item);
            item = new ComboBoxItem("外部报警", NetSDK.SDK_File_Type.SDK_RECORD_ALARM);
            this.type_comboBox.Items.Add(item);
            item = new ComboBoxItem("视频侦测", NetSDK.SDK_File_Type.SDK_RECORD_DETECT);
            this.type_comboBox.Items.Add(item);
            item = new ComboBoxItem("普通录像", NetSDK.SDK_File_Type.SDK_RECORD_REGULAR);
            this.type_comboBox.Items.Add(item);
            item = new ComboBoxItem("手动录像", NetSDK.SDK_File_Type.SDK_RECORD_MANUAL);
            this.type_comboBox.Items.Add(item);
            this.type_comboBox.SelectedIndex = 0;
            this.local_trackBar.Enabled = false;
            this.pre_button.Enabled = false;
            this.next_button.Enabled = false;
            this.download_button.Enabled = false;
            this.download_timer.Enabled = false;

            starttime_dateTimePicker.Value =DateTime.Now.AddDays(-1);

            for (int i = 0; i < this.channelNum; i++)
            {
                this.channel_comboBox.Items.Add(i + 1);
            }
            this.channel_comboBox.SelectedIndex = 0;
        }

        private void file_checkBox_Click(object sender, EventArgs e)
        {
            bool check = this.time_checkBox.Checked;
            if (check)
            {
                this.time_checkBox.Checked = false;
            }
        }

        private void time_checkBox_Click(object sender, EventArgs e)
        {
            bool check = this.file_checkBox.Checked;
            if (check)
            {
                this.file_checkBox.Checked = false;
            }
            
        }

        private void open_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".h264";
            ofd.Filter = "H264 Files | *.h264";
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK) {
                try
                {
                    this.fileName = ofd.FileName;
                }
                catch (Exception ex) {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);

                }
            }
        }

        private void fPlayDrawCallBack(Int32 lPlayHand, IntPtr hDc, Int32 nUser)
        {
        }

        private void fLocalPlayFileCallBack(Int32 lPlayHand, Int32 nUser) 
        {
   
        }

        private void InfoFramCallBack(Int32 lPlayHand, Int32 nType, String pBuf, Int32 nSize, Int32 nUser) 
        {
           
        }

        public void fRealDataCallBack_V2(Int32 lRealHandle, NetSDK.PACKET_INFO_EX pFrame, UInt32 dwUser){ 
        }
        private void play_button_Click(object sender, EventArgs e)
        {
            this.local_trackBar.Enabled = true;
            this.quicktime = 0;
            this.slowtime = 0;
            if (!this.isPlay)
            {
                this.isPlay = true;
                fdcb = new NetSDK.fPlayDrawCallBack(fPlayDrawCallBack);
                this.localHandle = NetSDK.H264_DVR_StartLocalPlay(this.fileName, this.localplay_panel.Handle, fdcb, this.user);
                if (this.localHandle > 0)
                {
                    fpfc = new NetSDK.fLocalPlayFileCallBack(fLocalPlayFileCallBack);
                    ifc = new NetSDK.InfoFramCallBack(InfoFramCallBack);
                    bool ret = NetSDK.H264_DVR_SetFileEndCallBack(this.localHandle, fpfc, this.user);
                    ret = NetSDK.H264_DVR_SetInfoFrameCallBack(this.localHandle, ifc, this.user);
                    this.play_timer.Enabled = true;
                    this.play_timer.Interval = 500;
                    this.play_timer.Start();
                }
                else
                {
                    MessageBox.Show("播放失败!");
                }
            }
            else{
                if(this.isPause){
                    NetSDK.H264_DVR_LocalPlayCtrl(this.localHandle, NetSDK.SDK_LoalPlayAction.SDK_Local_PLAY_CONTINUE, 0);
                    this.isPause = false;
                }

                NetSDK.H264_DVR_LocalPlayCtrl(this.localHandle, NetSDK.SDK_LoalPlayAction.SDK_Local_PLAY_FAST, 0);
                this.quicktime = 0;
                this.slowtime = 0;
            }
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            bool ret = NetSDK.H264_DVR_StopLocalPlay(this.localHandle);
            this.isPlay = false;
            this.quicktime = 0;
            this.slowtime = 0;
            this.isPause = false;
            this.localHandle = 0;
            this.local_trackBar.Value = 0;
        }

        private void pause_button_Click(object sender, EventArgs e)
        {
            bool ret = NetSDK.H264_DVR_LocalPlayCtrl(this.localHandle, NetSDK.SDK_LoalPlayAction.SDK_Local_PLAY_PAUSE, 0);
            this.isPause = true;
        }

        private void quickplay_button_Click(object sender, EventArgs e)
        {
            bool ret = false;
            if (this.quicktime < 4)
            {//最多调用四次
                this.quicktime++;
                ret = NetSDK.H264_DVR_LocalPlayCtrl(this.localHandle, NetSDK.SDK_LoalPlayAction.SDK_Local_PLAY_FAST, this.quicktime);
            }else{
                ret = NetSDK.H264_DVR_LocalPlayCtrl(this.localHandle, NetSDK.SDK_LoalPlayAction.SDK_Local_PLAY_FAST, 0);
                this.quicktime = 0;
            }
        }

        private void slowplay_button_Click(object sender, EventArgs e)
        {
            bool ret = false;
            if (this.slowtime < 4)
            {//最多调用四次
                this.slowtime++;
                ret = NetSDK.H264_DVR_LocalPlayCtrl(this.localHandle, NetSDK.SDK_LoalPlayAction.SDK_Local_PLAY_SLOW, this.slowtime);
            }else {//恢复正常播放
                ret = NetSDK.H264_DVR_LocalPlayCtrl(this.localHandle, NetSDK.SDK_LoalPlayAction.SDK_Local_PLAY_SLOW, 0);
                this.slowtime = 0;
            }
        }

        private void query_button_Click(object sender, EventArgs e)
        {
            DateTime starttime = this.starttime_dateTimePicker.Value;
            DateTime endtime = this.endtime_dateTimePicker.Value;

            if (endtime < starttime)
            {
                MessageBox.Show("开始时间不能大于结束时间!");
                return;
            }

            if (this.channel_comboBox.Text == null || "".Equals(this.channel_comboBox.Text))
            {
                MessageBox.Show("请选择通道!");
                return;
            }

            if(this.type_comboBox.Text == null || "".Equals(this.type_comboBox.Text))
            {
                MessageBox.Show("请选择类型!");
                return;
            }

            NetSDK.H264_DVR_FINDINFO findinfo = new NetSDK.H264_DVR_FINDINFO();
            try
            {
                this.currPage = 1;
                findinfo.nChannelN0 = this.channel_comboBox.SelectedIndex;
                NetSDK.SDK_File_Type typeValue = ((ComboBoxItem)this.type_comboBox.SelectedItem).Value;
                findinfo.nFileType = (Int32)typeValue;
                
                NetSDK.H264_DVR_TIME dvrstartime = new NetSDK.H264_DVR_TIME();
                dvrstartime.dwYear = starttime.Year;
                dvrstartime.dwMonth = starttime.Month;
                dvrstartime.dwDay = starttime.Day;
                dvrstartime.dwHour = starttime.Hour;
                dvrstartime.dwMinute = starttime.Minute;
                dvrstartime.dwSecond = starttime.Second;

                NetSDK.H264_DVR_TIME dvrendtime = new NetSDK.H264_DVR_TIME();
                dvrendtime.dwYear = endtime.Year;
                dvrendtime.dwMonth = endtime.Month;
                dvrendtime.dwDay = endtime.Day;
                dvrendtime.dwHour = endtime.Hour;
                dvrendtime.dwMinute = endtime.Minute;
                dvrendtime.dwSecond = endtime.Second;

                findinfo.startTime = dvrstartime;
                findinfo.endTime = dvrendtime;
                findinfo.hWnd = this.localplay_panel.Handle;
                int findcount = 100;
                Int32 ret = 0;
                fileDataList.Clear();//清除元原先的数据

                int size = Marshal.SizeOf(typeof(NetSDK.H264_DVR_FILE_DATA)) * 100;
                IntPtr fileDatasPtr = Marshal.AllocHGlobal(size);

                ret = NetSDK.H264_DVR_FindFile(this.loginId, ref findinfo, fileDatasPtr, size, ref findcount, 4000);
                if (ret > 0 && findcount > 0)//查询到有文件
                {
                    NetSDK.H264_DVR_FILE_DATA[] fileDatas = new NetSDK.H264_DVR_FILE_DATA[findcount];
                        
                    for (int i = 0; i < findcount; i++)
                    {
                        IntPtr ptr = new IntPtr(fileDatasPtr.ToInt64() + Marshal.SizeOf(typeof(NetSDK.H264_DVR_FILE_DATA)) * i);
                        fileDatas[i] = (NetSDK.H264_DVR_FILE_DATA)Marshal.PtrToStructure(ptr, typeof(NetSDK.H264_DVR_FILE_DATA));
                        fileDataList.Add(fileDatas[i]);
                    }

                    if (fileDataList.Count > this.pageSize)
                    {
                        this.next_button.Enabled = true;
                        this.download_button.Enabled = true;
                    }

                    AddFileListInfo(this.pageSize, this.currPage);
                }
                else//没有查询到文件
                {
                    Marshal.FreeHGlobal(fileDatasPtr); // 释放内存
                    MessageBox.Show("未查询到数据!");
                    return;
                }

                Marshal.FreeHGlobal(fileDatasPtr); // 释放内存
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally {
                
            }
            
        }
        //将文件名添加到列表中
        private void AddFileListInfo(int pageSize,int currPage) 
        {
            int start = (currPage - 1) * pageSize;
            int end = start + pageSize > fileDataList.Count ? fileDataList.Count : start + pageSize;
            string[,] dataArray = new string[end - start, 2];

            int i = 0;
            for (int k = start;k < end;k++)
            {
                NetSDK.H264_DVR_FILE_DATA filedata = fileDataList[k];
                dataArray[i,0] = (k + 1) + "";
                NetSDK.SDK_SYSTEM_TIME startime = filedata.stBeginTime;
                NetSDK.SDK_SYSTEM_TIME endtime = filedata.stEndTime;
                string month = startime.month < 10 ? ("0" + startime.month) : startime.month.ToString();
                string day = startime.day < 10 ? ("0" + startime.day) : startime.day.ToString();
                string hour = startime.hour < 10 ? ("0" + startime.hour) : startime.hour.ToString();
                string minute = startime.minute < 10 ? ("0" + startime.minute) : startime.minute.ToString();
                string second = startime.second < 10 ? ("0" + startime.second) : startime.second.ToString();
                string fileStart = String.Format("{0}-{1}-{2}-{3}:{4}:{5}", startime.year, month, day,hour,minute, second);
                month = endtime.month < 10 ? ("0" + endtime.month) : endtime.month.ToString();
                day = endtime.day < 10 ? ("0" + endtime.day) : endtime.day.ToString();
                hour = endtime.hour < 10 ? ("0" + endtime.hour) : endtime.hour.ToString();
                minute = endtime.minute < 10 ? ("0" + endtime.minute) : endtime.minute.ToString();
                second = endtime.second < 10 ? ("0" + endtime.second) : endtime.second.ToString();
                string fileEnd = String.Format("{0}-{1}-{2}-{3}:{4}:{5}", endtime.year, month, day, hour, minute, second);
                string fileName = String.Format("{0}-{1}({2}KB)", fileStart, fileEnd, filedata.size);
                dataArray[i, 1] = fileName;
                i++;
            }
            

            DataTable dt = new DataTable();
            string[] ColumnNames = {"序号","文件名"};
            foreach (string ColumnName in ColumnNames)
            {
                dt.Columns.Add(ColumnName, typeof(string));
            }

            for (int i1 = 0; i1 < dataArray.GetLength(0); i1++)
            {
                DataRow dr = dt.NewRow();
                for (int m = 0; m < ColumnNames.Length; m++)
                {
                    dr[m] = dataArray[i1, m];
                }
                dt.Rows.Add(dr);
            }

            this.data_dataGridView.DataSource = dt;
        }
        private void openvoice_button_Click(object sender, EventArgs e)
        {
            if (this.localHandle > 0)
            {
                bool ret = NetSDK.H264_DVR_OpenSound(this.localHandle);
                if (!ret) {
                    MessageBox.Show("打开声音失败!");
                }
            }
        }

        private void fDownLoadPosCallBack(Int32 lPlayHandle, Int32 lTotalSize, Int32 lDownLoadSize, Int32 dwUser) 
        {
            //结束回调
            if(this.isPlay){//结束远程播放回调
                this.handle = 0;
                this.isPlay = false;
                this.isPause = false;
                this.quicktime = 0;
                this.slowtime = 0;
            }
            Int32 ret = NetSDK.H264_DVR_GetDownloadPos(this.fileHandle);
            if (ret == 100)//结束下载回调
            {
                MessageBox.Show("下载完成!");
                this.fileHandle = 0;
                this.download_button.Text = "下载";
            }
            
        }
        private static void fRealDataCallBack(Int32 lRealHandle, Int32 dwDataType, IntPtr pBuffer, uint lbufsize, Int32 dwUser) 
        { 
        }
        private void networkplay_button_Click(object sender, EventArgs e)
        {
            int index = this.data_dataGridView.SelectedRows.Count;
            if (index <= 0)
            {
                MessageBox.Show("请选择数据!");
                return;
            }

            if(!this.time_checkBox.Checked && !this.file_checkBox.Checked){
                MessageBox.Show("请选择回放方式!");
                return;
            }

            if (this.time_checkBox.Checked)
            {
                OnPlayByTime();
            }
            else{
                OnPlayByName();
            }
        }

        private void networkstop_button_Click(object sender, EventArgs e)
        {
            bool ret = NetSDK.H264_DVR_StopPlayBack(this.networkHandle);
            this.isPlay = false;
            this.isPause = false;
            this.quicktime = 0;
            this.slowtime = 0;
        }

        private void networkpause_button_Click(object sender, EventArgs e)
        {
            this.isPause = true;
            NetWorkPlayCtrl(NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_PAUSE,0);
        }

        private void networkquickplay_button_Click(object sender, EventArgs e)
        {
            if (this.isPlay)
            {
                if (this.quicktime < 4)
                {//最多调用四次
                    this.quicktime++;
                    NetWorkPlayCtrl(NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_FAST, this.quicktime);
                }
                else
                {
                    NetWorkPlayCtrl(NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_FAST, 0);
                    this.quicktime = 0;
                }
            }
        }

        private void networkslow_button_Click(object sender, EventArgs e)
        {
            if (this.isPlay)
            {
                if (this.slowtime < 4)
                {//最多调用四次
                    this.slowtime++;
                    NetWorkPlayCtrl(NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_SLOW, this.slowtime);
                }
                else
                {
                    NetWorkPlayCtrl(NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_SLOW, 0);
                    this.slowtime = 0;
                }
            }
        }

        private void networkopenvoice_button_Click(object sender, EventArgs e)
        {
            if (this.networkHandle > 0)
            {
                bool ret = NetSDK.H264_DVR_OpenSound(this.networkHandle);
                if (!ret)
                {
                    MessageBox.Show("打开声音失败!");
                }
            }
        }

        private bool NetWorkPlayCtrl(NetSDK.SEDK_PlayBackAction action,Int32 step) 
        {
            bool ret = false;
            switch (action) {
                case NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_CONTINUE:
                case NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_PAUSE:
                case NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_SEEK:
                case NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_SEEK_PERCENT:
                case NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_FAST:
                case NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_SLOW:
                    ret = NetSDK.H264_DVR_PlayBackControl(this.networkHandle, action, step);
                    break;
                default:
                    break;

            }

            return ret;
        }
        

        private void local_trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            int barValue = this.local_trackBar.Value;
            float porcess = (float)barValue / this.local_trackBar.Maximum;
            if (this.localHandle > 0)
            {
                bool ret = NetSDK.H264_DVR_SetPlayPos(this.localHandle, porcess);
            }
        }

        private void local_trackBar_Scroll(object sender, EventArgs e)
        {

        }

        private void download_button_Click(object sender, EventArgs e)
        {
            if("停止下载".Equals(this.download_button.Text)){
                StopDownload();
            }else{//开始下载
                int index = this.data_dataGridView.SelectedRows.Count;
                if (index <= 0)
                {
                    MessageBox.Show("请选择数据!");
                }

                if (!this.time_checkBox.Checked && !this.file_checkBox.Checked)
                {
                    MessageBox.Show("请选择回放方式!");
                }

                if (this.file_checkBox.Checked)
                {
                    OnDownloadByName();
                }
                else {
                    OnDownloadByTime();
                }
            }
            
        }

        private void StopDownload()
        {
           bool ret = NetSDK.H264_DVR_StopGetFile(this.fileHandle);
           if (!ret)
           {
               MessageBox.Show("停止下载失败!");
           }
           
           this.fileHandle = 0;
           this.download_button.Text = "下载";
           this.download_progressBar.Visible = false;
        }
        private void OnDownloadByTime() {
            
            DateTime starttime = this.starttime_dateTimePicker.Value;
            DateTime endtime = this.endtime_dateTimePicker.Value;
            NetSDK.H264_DVR_FINDINFO findInfo = new NetSDK.H264_DVR_FINDINFO();
            findInfo.nChannelN0 = this.channel_comboBox.SelectedIndex;
            NetSDK.SDK_File_Type typeValue = ((ComboBoxItem)this.type_comboBox.SelectedItem).Value;
            findInfo.nFileType =
                ((Int32)typeValue <= (Int32)NetSDK.SDK_File_Type.SDK_RECORD_MANUAL) ? (Int32)typeValue :
                ((Int32)NetSDK.SDK_File_Type.SDK_PIC_ALL + (Int32)typeValue - (Int32)NetSDK.SDK_File_Type.SDK_RECORD_MANUAL - 1);
                
            NetSDK.H264_DVR_TIME dvrstartime = new NetSDK.H264_DVR_TIME();
            dvrstartime.dwYear = starttime.Year;
            dvrstartime.dwMonth = starttime.Month;
            dvrstartime.dwDay = starttime.Day;
            dvrstartime.dwHour = starttime.Hour;
            dvrstartime.dwMinute = starttime.Minute;
            dvrstartime.dwSecond = starttime.Second;

            NetSDK.H264_DVR_TIME dvrendtime = new NetSDK.H264_DVR_TIME();
            dvrendtime.dwYear = endtime.Year;
            dvrendtime.dwMonth = endtime.Month;
            dvrendtime.dwDay = endtime.Day;
            dvrendtime.dwHour = endtime.Hour;
            dvrendtime.dwMinute = endtime.Minute;
            dvrendtime.dwSecond = endtime.Second;

            findInfo.startTime = dvrstartime;
            findInfo.endTime = dvrendtime;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "H264 Files | *.h264";
            saveFileDialog.DefaultExt = "h264";
            saveFileDialog.FileName = String.Format("{0:00}_{1}{2:00}{3:00}_{4:00}{5:00}{6:00}.h264",
                                            this.channel_comboBox.Text,
                                            starttime.Year,
                                            starttime.Month,
                                            starttime.Day,
                                            starttime.Hour,
                                            starttime.Minute,
                                            starttime.Second);
            dlpcb = new NetSDK.fDownLoadPosCallBack(fDownLoadPosCallBack);
            Int32 dwDataUser = 1;
            DialogResult ret = saveFileDialog.ShowDialog();
            if (ret == DialogResult.OK)
            {
                this.fileName = saveFileDialog.FileName;
                string sSavedFileDIR = saveFileDialog.FileName.Substring(0, this.fileName.LastIndexOf("\\"));
                this.fileHandle = NetSDK.H264_DVR_GetFileByTime(this.loginId, ref findInfo, sSavedFileDIR, false, dlpcb, dwDataUser);
                if (this.fileHandle > 0)
                {
                    this.download_button.Text = "停止下载";
                    this.download_progressBar.Visible = true;//显示进度条
                    this.download_timer.Enabled = true;
                    this.download_timer.Interval = 1000;
                    this.download_timer.Start();
                }
                else
                {
                    MessageBox.Show("下载失败!");
                }
            }
        }

        private void OnDownloadByName()
        {
            int index = this.data_dataGridView.CurrentRow.Index;
            index = this.currPage > 1 ? (this.currPage - 1) * this.pageSize + index : index;
            NetSDK.H264_DVR_FILE_DATA fileData = this.fileDataList[index];
            this.fileSize = fileData.size;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "H264 Files | *.h264";
            saveFileDialog.DefaultExt = "h264";
            int flag = fileData.sFileName.IndexOf(".h264");
            string fileType = "";
            if (flag > -1)
            {
                fileType = "h264";
            }
            else {
                fileType = "jpg";
            }
            string ip = ConfigurationManager.AppSettings["VideoServerIP"].ToString();
            saveFileDialog.FileName = String.Format("{0}_{1:00}_{2}{3:00}{4:00}_{5:00}{6:00}{7:00}.{8}",                                                                       ip, this.channel_comboBox.Text,                                                                                                   fileData.stBeginTime.year,
                                             fileData.stBeginTime.month,                                                                                                       fileData.stBeginTime.day,
                                             fileData.stBeginTime.hour,                                                                                                        fileData.stBeginTime.minute,
                                             fileData.stBeginTime.second,
                                             fileType);
            DialogResult ret = saveFileDialog.ShowDialog();
            if (ret == DialogResult.OK)
            {
                this.fileName = saveFileDialog.FileName;

                try
                {
                    IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NetSDK.H264_DVR_FILE_DATA)));
                    Marshal.StructureToPtr(fileData, pnt, true);
                    dlpcb = new NetSDK.fDownLoadPosCallBack(fDownLoadPosCallBack);
                    Int32 dwDataUser = 1;
                    this.fileHandle = NetSDK.H264_DVR_GetFileByName(this.loginId, pnt, this.fileName, dlpcb, dwDataUser);

                    if (this.fileHandle > 0)//设置进度条
                    {
                        this.download_button.Text = "停止下载";
                        this.download_progressBar.Visible = true;//显示进度条
                        this.download_timer.Enabled = true;
                        this.download_timer.Interval = 1000;
                        this.download_timer.Start();

                    }
                    else
                    {
                        MessageBox.Show("下载失败!");
                    }

                    Marshal.FreeHGlobal(pnt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else{ 
            
            }
            
        }

        private void ClearResult() 
        {
            this.data_dataGridView.DataSource = null;
        }

        private void pre_button_Click(object sender, EventArgs e)
        {
            this.currPage--;
            this.next_button.Enabled = true;
            if (this.currPage == 1)
            {
                this.pre_button.Enabled = false;
            }
            AddFileListInfo(this.pageSize,this.currPage);
        }

        private void next_button_Click(object sender, EventArgs e)
        {
            this.pre_button.Enabled = true;
            this.currPage++;
            int pages = fileDataList.Count  / this.pageSize;
            if (this.currPage > pages)
            {
                this.next_button.Enabled = false;
            }
            AddFileListInfo(this.pageSize, this.currPage);

        }

        private void network_trackBar_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void OnPlayByTime() {

            if (!this.isPlay)
            {
                DateTime starttime = this.starttime_dateTimePicker.Value;
                DateTime endtime = this.endtime_dateTimePicker.Value;

                rdcb = new NetSDK.fRealDataCallBack(fRealDataCallBack);
                dlpcb = new NetSDK.fDownLoadPosCallBack(fDownLoadPosCallBack);
                NetSDK.H264_DVR_FINDINFO findinfo = new NetSDK.H264_DVR_FINDINFO();

                findinfo.nChannelN0 = this.channel_comboBox.SelectedIndex;
                NetSDK.SDK_File_Type typeValue = ((ComboBoxItem)this.type_comboBox.SelectedItem).Value;
                findinfo.nFileType = (Int32)typeValue;

                NetSDK.H264_DVR_TIME dvrstartime = new NetSDK.H264_DVR_TIME();
                dvrstartime.dwYear = starttime.Year;
                dvrstartime.dwMonth = starttime.Month;
                dvrstartime.dwDay = starttime.Day;
                dvrstartime.dwHour = starttime.Hour;
                dvrstartime.dwMinute = starttime.Minute;
                dvrstartime.dwSecond = starttime.Second;

                NetSDK.H264_DVR_TIME dvrendtime = new NetSDK.H264_DVR_TIME();
                dvrendtime.dwYear = endtime.Year;
                dvrendtime.dwMonth = endtime.Month;
                dvrendtime.dwDay = endtime.Day;
                dvrendtime.dwHour = endtime.Hour;
                dvrendtime.dwMinute = endtime.Minute;
                dvrendtime.dwSecond = endtime.Second;

                findinfo.startTime = dvrstartime;
                findinfo.endTime = dvrendtime;
                findinfo.hWnd = this.networkplay_panel.Handle;

                Int32 dwDataUser = 1;
                Int32 dwPosUser = 1;
                IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NetSDK.H264_DVR_FINDINFO)));
                Marshal.StructureToPtr(findinfo, pnt, true);
                try
                {
                    this.networkHandle = NetSDK.H264_DVR_PlayBackByTimeEx(this.loginId, pnt, rdcb, dwDataUser, dlpcb, dwPosUser);
                    if (this.networkHandle <= 0)
                    {
                        MessageBox.Show("回放失败!");
                    }
                    else
                    {
                        this.isPlay = true;
                    }
                }
                catch(Exception ex) {
                    Console.WriteLine(ex.ToString());
                }finally{
                    Marshal.FreeHGlobal(pnt);
                }

                
            }else{
                if (this.isPause)
                {
                    NetWorkPlayCtrl(NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_CONTINUE, 0);
                    this.isPause = false;
                }

                NetWorkPlayCtrl(NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_FAST, 0);
                this.quicktime = 0;
                this.slowtime = 0;
            }
        
        }

        private void OnPlayByName()
        {
            if(!this.isPlay){
                int index = this.data_dataGridView.SelectedRows.Count;
                if (index <= 0)
                {
                    MessageBox.Show("请选择数据!");
                    return;
                }
                index = this.data_dataGridView.CurrentRow.Index;
                index = this.currPage > 1 ? (this.currPage - 1) * this.pageSize + index : index;
                NetSDK.H264_DVR_FILE_DATA fileData = this.fileDataList[index];
                fileData.hWnd = this.networkplay_panel.Handle;
                IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NetSDK.H264_DVR_FILE_DATA)));
                Marshal.StructureToPtr(fileData, pnt, true);
                try
                {
                    dlpcb = new NetSDK.fDownLoadPosCallBack(fDownLoadPosCallBack);
                    rdcbv2 = new NetSDK.fRealDataCallBack_V2(fRealDataCallBack_V2);
                    Int32 dwDataUser = 1;

                    this.networkHandle = NetSDK.H264_DVR_PlayBackByName_V2(this.loginId, pnt, dlpcb, rdcbv2, dwDataUser);

                    if (this.networkHandle > 0)
                    {
                        this.isPlay = true;
                    }
                    else {
                        MessageBox.Show("回放失败!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally {
                    Marshal.FreeHGlobal(pnt); // 释放内存
                }
                
            }
            else
            {
                if (this.isPause)
                {
                    NetWorkPlayCtrl(NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_CONTINUE, 0);
                    this.isPause = false;
                }

                NetWorkPlayCtrl(NetSDK.SEDK_PlayBackAction.SDK_PLAY_BACK_FAST, 0);
                this.quicktime = 0;
                this.slowtime = 0;
            }
        }

        class ComboBoxItem
        {
            private string _text = null;
            private NetSDK.SDK_File_Type _value = 0;
            public ComboBoxItem(string text, NetSDK.SDK_File_Type _value)
            {
                this._text = text;
                this._value = _value;

            }
            public string Text { get { return this._text; } set { this._text = value; } }
            public NetSDK.SDK_File_Type Value { get { return this._value; } set { this._value = value; } }
            public override string ToString()
            {
                return this._text;
            }
        }

        private void download_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.fileHandle > 0)
                {
                    Int32 ret = NetSDK.H264_DVR_GetDownloadPos(this.fileHandle);
                    //System.IO.FileInfo file = new System.IO.FileInfo(this.fileName);
                    //long retx = (long)this.fileSize / file.Length;
                    if (ret == 100)
                    {
                        NetSDK.H264_DVR_StopGetFile(this.fileHandle);
                        this.download_button.Text = "下载";
                        this.download_progressBar.Visible = false;
                        this.isPlay = false;
                        this.isPause = false;
                        this.quicktime = 0;
                        this.slowtime = 0;
                        this.fileHandle = 0;
                        MessageBox.Show("下载完成!");
                        this.download_timer.Stop();
                    }
                    else if (ret < 0)
                    {

                    }
                    else if (ret > 100)
                    {
                    }
                    else
                    {
                        this.download_progressBar.Value = ret;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void play_timer_Tick(object sender, EventArgs e)
        {
            try {
                if (this.localHandle > 0)
                {
                    Single porce = NetSDK.H264_DVR_GetPlayPos(this.localHandle);
                    int pos = (int)(porce * 100);
                    if (pos == 100)
                    {
                        this.local_trackBar.Value = 0;
                        this.play_timer.Stop();
                    }
                    else {
                        this.local_trackBar.Value = pos;
                    }
                }

                if(this.networkHandle > 0){
                    Single porce = NetSDK.H264_DVR_GetPlayPos(this.networkHandle);
                    int pos = (int)(porce * 100);
                    if (pos == 100)
                    {
                        this.network_trackBar.Value = 0;
                        this.play_timer.Stop();
                    }
                    else
                    {
                        this.local_trackBar.Value = pos;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void FrmPlayBack_Closed(object sender, FormClosedEventArgs e)
        {
            this.play_timer.Stop();
            this.download_timer.Stop();
            NetSDK.H264_DVR_StopPlayBack(this.networkHandle);
            NetSDK.H264_DVR_StopLocalPlay(this.localHandle);
            StopDownload();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.playback_tabControl.SelectedTab == this.playback_tabControl.TabPages[0])//网络回放
            {
                this.play_timer.Stop();
                this.isPlay = false;
                this.isPause = false;
                this.quicktime = 0;
                this.slowtime = 0;
                this.localHandle = 0;
            }
            else if (this.playback_tabControl.SelectedTab == this.playback_tabControl.TabPages[1])//本地回放
            {
                this.play_timer.Stop();
                this.isPlay = false;
                this.isPause = false;
                this.quicktime = 0;
                this.slowtime = 0;
                this.networkHandle = 0;
            }
        }
    }
}

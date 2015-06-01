using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using DevExpress.XtraEditors;

using MDS.CLIENT;
using MDS.CLIENT.Domain;
using MDS.ConstTag;
using MDS.Bll;
using System.Globalization;

namespace AlarmMapClient
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmPlayer : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private FrmMain frm;

        private string deviceId;
        private string channelId;
        private int nchannelId;
        private string oprName;
        private Device dev;

        public int tabind;
        private int nStream;
        public MDS.Domain.AlarmLog log;

        private TreeNode curAlarmNode;
        private TreeNode curCameraNode;

        private List<TreeNode> cameraNodes;
        private List<TreeNode> alarmNodes;

        private DeviceF df;
        private NetSDK.LPH264_DVR_DEVICEINFO lpDeviceInfo;
        private bool isLogin;
        private bool isTransportOpen;

        private Panel curPanel;
        private int handelGla;
        private Int32 speakHandel;
        private bool loaded;
        private OpaqueCommand cmd;

        private NetSDK.fTransComCallBack fCallBack;

        int sysStatus;//系统状态01 是布防
        int alarmSound;//01是报警声开启
        int lowVolInput;//00是低电平有效输入
        int wireAreaNum;//04是有线防区的个数
        int[] wireAreaStatus = new int[] { 0, 0, 0, 0 };//0F 是4个有线防区都是开着的意思
        int wirelessStudyNum;//无线防区当前学习到的个数
        int[] wirelessAreaStatus = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//无线16个的状态
        int[] kglStatus = new int[] { 0, 0, 0, 0, 0, 0, 0, 0};//继电器8个的状态

        BatteryLog batterylog;

        public FrmPlayer(FrmMain frm, string deviceId, string channelId, int nchannelId, string oprName)
        {
            InitializeComponent();

            this.frm = frm;

            this.deviceId = deviceId;
            this.channelId = channelId;
            this.nchannelId = nchannelId;
            this.oprName = oprName;

            this.tabind = 1;
            this.nStream = 1;//Client 2为子码,1 为主码;NetSDK 0表示主码流,为1表示子码流
            this.cb_preset.SelectedIndex = 0;
            this.cb_tour.SelectedIndex = 0;
            this.navBarControl.NavigationPaneMaxVisibleGroups = -1;
            this.navBarControl.Tag = 999;

            this.cmd = new OpaqueCommand();

            //this.frm.node_timer.Stop();
            //this.frm.isStopNodeTimer = true;
        }

        private void FrmPlayer_Load(object sender, EventArgs e)
        {
            try
            {
                logger.Error("播放窗口加载中...");

                if(this.frm.isInDeviceOpr){
                    //logger.Error("wait for main stop status thread");
                    System.Threading.Thread.Sleep(100);

                    //df = DeviceF.Instance;
                    //df.LogOut();
                }
                //this.splitContainer_video.Panel2Collapsed = true;
                //设置视频分割
                this.panel_video.Controls.Clear();
                SplitPanel(2, 2);

                //登录设备
                loginDev();
                
                //分别加载不同TAB
                if (this.tabind == 1)
                {
                    if (isLogin)
                    {
                        isTransportOpen = openTransportRs232(this.deviceId);

                        this.curPanel = (Panel)this.panel_video.Controls.Find("VideoPanel0", false)[0];
                        playVideo("VideoPanel0");
                    }
                    else
                    {
                        MessageBox.Show("设备登录失败！", "播放异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                    tabControl1.SelectTab(this.tabind - 1);

                //如果log为空
                if (this.log == null)
                {
                    Device dev = MDSUtils.GetDeviceInfo(this.deviceId);

                    this.log = new MDS.Domain.AlarmLog();
                    this.log.MapPic = dev.MapPic;
                    this.log.Address = (dev.Address == null ? "无" : dev.Address);
                    this.log.Tel = (dev.Tel == null ? "无" : dev.Tel);
                    this.log.UserName = (dev.TrueName == null ? "无" : dev.TrueName);
                    this.log.IsAlarmed = -1;

                    this.btn_del_alarm.Enabled = false;
                }

                cameraNodes = new List<TreeNode>();
                alarmNodes = new List<TreeNode>();

                this.Text = ("当前设备：" + this.deviceId);
                //loadChannels();
                //创建加载通道相关信息线程,线程中不能更新UI
                //loaded = true;
                cmd.ShowOpaqueLayer(this.navBarControl, 125, true);
                Thread t = new Thread(new ThreadStart(this.loadChannels));
                t.Start();

                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 1000*5;
                timer.Elapsed += timer_Elapsed;
                timer.Enabled = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                loaded = true;
            }
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //加载电池信息
            this.loadBatteryInfo(this.deviceId);

            UpdateUI uui = new UpdateUI(this.UpdateBatteryUI);
            this.BeginInvoke(uui);
        }

        private delegate void UpdateUI();

        private void UpdateBatteryUI()
        {
            if (batterylog != null)
            {
                this.label_battery_time.Text = "更新时间：" + batterylog.LogTime.AddHours(8).ToString();

                this.txt_Vol_PV.Text = string.Format("{0:F}", batterylog.VolPv);
                this.txt_Cur_PV.Text = batterylog.CurPv.ToString();
                this.txt_P_VOL.Text = (batterylog.VolPv * batterylog.CurPv).ToString();
                this.txt_Vol_Bat.Text = batterylog.VolBat.ToString();
                this.txt_Cur_Load.Text = batterylog.CurLoad.ToString();
                this.txt_P_Load.Text = (batterylog.CurLoad*batterylog.VolBat).ToString();
                this.txt_Temp_Bat.Text = batterylog.TempBat.ToString();
                this.txt_SOC.Text = batterylog.Soc.ToString();

                this.txt_Ah_PV.Text = batterylog.AhPv.ToString();
                this.txt_Ah_Load.Text = batterylog.AhLoad.ToString();
                this.txt_Vol_Float.Text = batterylog.VolFloat.ToString();
                this.txt_Vol_Cut.Text = batterylog.VolCut.ToString();
                this.txt_Vol_Reconnect.Text = batterylog.VolReconnect.ToString();
            }
            else
            {
                this.label_battery_time.Text = "更新时间：无";
            }
        }

        private void UpdateChUI()
        {
            loaded = true;
            //不生效?
            this.Text  = ("当前设备：" + this.dev.DeviceName);
            if (this.log != null && this.log.IsAlarmed == 0)
                this.btn_del_alarm.Enabled = true;
            else
                this.btn_del_alarm.Enabled = false;

            this.treeView_cameras.Nodes.AddRange(this.cameraNodes.ToArray());
            this.treeView_alarms.Nodes.AddRange(this.alarmNodes.ToArray());

            cmd.HideOpaqueLayer();
        }

        private bool loginDev()
        {
            //加载摄像头
            this.dev = MDSUtils.GetDeviceInfo(this.deviceId);

            //登录设备
            /**
             *  3.0的平台 P2P登录设备是
                IP:服务器IP
                端口：9500
                用户名：设备的序列号
                密码：没
             * */
            df = DeviceF.Instance;
            lpDeviceInfo = new NetSDK.LPH264_DVR_DEVICEINFO();
            //isLogin = df.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), (ushort)this.dev.Port, this.dev.UserName, this.dev.PassWord, ref lpDeviceInfo);
            isLogin = df.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), 9500, this.dev.SerialNum, "", ref lpDeviceInfo);

            return isLogin;
        }

        private delegate void loadLogs();
        private void loadAlarmLogs()
        {
            List<MDS.Domain.AlarmLog> logs = AlarmLogBll.QueryLogsByDeviceId(this.deviceId);
            this.log_dataview.DataSource = null;
            this.log_dataview.DataSource = logs;
        }

        private void loadChannels()
        {
            //return;
            try
            {
                //login dev
                List<Channel> channels = MDSUtils.GetChannelInfoByDevice(this.deviceId);
                bool iserr = false;

                foreach (Channel c in channels)
                {
                    TreeNode node = new TreeNode(c.ChannelName);
                    node.Tag = c;
                    //获取摄像头布防状态：移动侦测开启状态
                    if (isLogin && !iserr)
                    {
                        int bsuccess = df.GetMotionCfg(int.Parse(c.ChannelNum) - 1);
                        if (bsuccess > 0)
                        {
                            if (df.motionCfg.bEnable > 0)
                                node.ImageIndex = 2;
                            else
                                node.ImageIndex = 3;
                        }
                        else
                        {
                            node.ImageIndex = 0;
                            iserr = true;
                        }
                    }

                    this.cameraNodes.Add(node);
                    //this.treeView_cameras.Nodes.Add(node);
                }

                //加载报警器
                for (int i = 0; i < lpDeviceInfo.byAlarmInPortNum; i++)
                {
                    TreeNode node = new TreeNode("报警通道" + (i + 1));
                    node.Tag = i;
                    //获取报警通道布防状态;
                    if (isLogin && !iserr)
                    {
                        int bsuccess = df.GetAlarmInputCfg(i);
                        if (bsuccess > 0)
                        {
                            if (df.alarmInputCfg.bEnable > 0)
                                node.ImageIndex = 2;
                            else
                                node.ImageIndex = 3;
                        }
                        else
                        {
                            iserr = true;
                        }
                    }

                    this.alarmNodes.Add(node);
                    //this.treeView_alarms.Nodes.Add(node);

                }

                //加载传感器
                searchFqStatus();

                //更新UI
                UpdateUI uui = new UpdateUI(this.UpdateChUI);
                loadLogs lam = new loadLogs(this.loadAlarmLogs);
                this.log_dataview.AutoGenerateColumns = false;
                this.BeginInvoke(uui);
                this.BeginInvoke(lam);

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void playVideo(string panelId)
        {
            //return;
            if (this.channelId == null||this.channelId.Equals("null"))
            {
                //MessageBox.Show("无法播放该通道视频：通道数据为空！请选择通道播放！", "播放异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //logger.Error("无法播放该通道视频：通道数据为空！");
                return;
            }

            try
            {
                Panel p = (Panel)this.panel_video.Controls.Find(panelId, false)[0];
                PictureBox pic = (PictureBox)p.Controls.Find(panelId.Replace("VideoPanel", "VideoPic"),false)[0];
                pic.Visible = true;
                /*
                TransSDK.P_Client_RealInfo pRealInfo = new TransSDK.P_Client_RealInfo();
                pRealInfo.szChannelID = this.channelId;
                pRealInfo.nStream = this.nStream;
                pRealInfo.hWnd = p.Handle;
                pRealInfo.realDataFunc = null;//null,pfRealDataCallBack
                pRealInfo.dwUser = 0;//this.Handle 

                //TransSDK.P_Client_SetMediaProtocol(1);
                int handle = TransSDK.P_Client_ConnectRealPlay(ref pRealInfo);//调用后马上返回?
                 * */
                int handle = df.Play(this.nchannelId, p.Handle, this.nStream);
                this.handelGla = handle;
                if (handle <= 0)
                {
                    MessageBox.Show("无法播放该通道视频,请重新播放!(" + handle+")", "播放异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Error("无法播放该通道视频：" + handle);
                }
                else
                {
                    //MessageBox.Show("成功调用播放:handle=" + this.videoHandle, "播放", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string[] paras = p.Tag.ToString().Split(',');
                    p.Tag = paras[0] + "," + handle;
                }
                pic.Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "播放异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void stopVideo(string panelId)
        {
            try
            {
                Panel p = (Panel)this.panel_video.Controls.Find(panelId, false)[0];
                string[] paras = p.Tag.ToString().Split(',');
                int handle = int.Parse(paras[1]);

                if (handle > 0)
                {
                    //报-111错误<没有发现当前视频>
                    //int ret = TransSDK.P_Client_StopRealPlayByID(handle);
                    int ret = df.Stop(handle)?0:1;

                    if (ret != 0)
                    {
                        MessageBox.Show("无法停止该通道视频：" + handle + "," + ret, "停止异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        p.Tag = paras[0] + ",0";

                        //p.Controls.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "停播异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showUserInfo()
        {
            if (this.log == null) return;

            this.txt_username.Text = this.log.UserName;
            this.txt_addr.Text = this.log.Address;
            this.txt_tel.Text = this.log.Tel;
        }

        private void showMap()
        {
            if (this.log == null)
            {
                MessageBox.Show("无警情日志状态，暂时不提供二级地图浏览！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // WebBrowser控件显示的网页路径
            //this.webBrowser.Refresh();
            this.webBrowser.Url = new Uri(ConfigurationManager.AppSettings["WebMapServerURL2"].ToString());
            // 将当前类设置为可由脚本访问
            this.webBrowser.ObjectForScripting = this;
            this.webBrowser.ScriptErrorsSuppressed = true;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (this.log == null)
            {
                MessageBox.Show("无警情日志状态，暂时不提供二级地图浏览！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            object[] param0 = new object[1];
            param0[0] = this.log.MapPic.Substring(0, this.log.MapPic.LastIndexOf("/"));
            webBrowser.Document.InvokeScript("loadMap", param0);
            //加载所有通道点,调用js显示
            foreach (TreeNode node in treeView_cameras.Nodes)
            {
                Channel c = (Channel)node.Tag;

                if (c.Latitude != 0 && c.Longitude != 0)
                {
                    object[] param = new object[7];
                    param[0] = this.channelId.Equals(c.ChannelID)?true:false;
                    param[1] = c.Latitude;
                    param[2] = c.Longitude;
                    param[3] = this.dev.DeviceName + "->" + c.ChannelName;
                    param[4] = this.log.Address;
                    param[5] = this.log.Tel;
                    param[6] = this.log.UserName;

                    webBrowser.Document.InvokeScript("addPoint", param);
                }
            }            
        }

        private void pfRealDataCallBack(Int32 lRealHandle, Int32 dwDataType, byte[] pBuffer, Int32 dwBufSize, Int32 dwUser)
        {
            //MessageBox.Show("rev size:"+dwBufSize);
        }

        private void stopAllVideo()
        {
            foreach (Object obj in this.panel_video.Controls)
            {
                if (!(obj is Panel)) continue;

                Panel p = (Panel)obj;

                stopVideo(p.Name);
            }
        }

        //尝试读取或写入受保护的内存。这通常指示其他内存已损坏
        [HandleProcessCorruptedStateExceptions]
        private void FrmPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!loaded)
            {
                e.Cancel = true;
                MessageBox.Show("加载中,请稍候关闭！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                try
                {
                    if (isLogin) stopTransportRs232();
                    stopAllVideo();
                    if (df != null) df.LogOut();

                    //this.frm.isStopNodeTimer = false;
                    //this.frm.node_timer.Start();
            
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void SplitPanel(int rowCount,int colCount)
        {
            Rectangle[] rects = GetSplittedRectangles(this.panel_video.DisplayRectangle,rowCount,colCount);

            for (int i = 0; i < rowCount * colCount; i++)
            {
                Panel myPanel = new Panel();
                myPanel.Name = "VideoPanel" + i.ToString();
                Point mypoint = new Point(rects[i].X, rects[i].Y);
                myPanel.Location = mypoint;
                myPanel.Height = rects[i].Height;
                myPanel.Width = rects[i].Width;
                myPanel.BackColor = Color.Gray;
                myPanel.BorderStyle = BorderStyle.FixedSingle;
                myPanel.Tag = i+",0";
                myPanel.ContextMenuStrip = this.contextMenuStrip_video;
                myPanel.Click += myPanel_Click;
                myPanel.DoubleClick += myPanel_DoubleClick;

                PictureBox pic = new PictureBox();
                pic.Name = "VideoPic" + i.ToString();
                pic.Image = Properties.Resources.loading;
                pic.Visible = false;
                pic.Location = new Point(myPanel.Width / 2-24, myPanel.Height/2-24); ;
                pic.Width = 48;
                pic.Height = 48;
                pic.SizeMode = PictureBoxSizeMode.AutoSize;
                myPanel.Controls.Add(pic);

                this.panel_video.Controls.Add(myPanel);
            }
        }

        void myPanel_DoubleClick(object sender, EventArgs e)
        {
            //极大化或恢复极小化
        }

        void myPanel_Click(object sender, EventArgs e)
        {
            curPanel = (Panel)sender;
            foreach (Object obj in this.panel_video.Controls)
            {
                if (!(obj is Panel)) continue;

                Panel p = (Panel)obj;
                if(p==curPanel)
                    p.BackColor = Color.LightGray;
                else
                    p.BackColor = Color.Gray;
            }
        }
        /// <summary>
        /// 返回分割后的rectangle数组
        /// </summary>
        /// <param name="rt">待分割的矩形</param>
        /// <param name="rowCount">行数</param>
        /// <param name="colCount">列数</param>
        /// <returns></returns>
        private Rectangle[] GetSplittedRectangles(Rectangle rt, int rowCount,int colCount)
        {
            Rectangle[] rects = new Rectangle[rowCount * colCount];
            int width = rt.Width / colCount;
            int height = rt.Height / rowCount;
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < colCount; colIndex++)
                {
                    rects[rowIndex * colCount + colIndex] = new Rectangle(colIndex * width,rowIndex * height, width, height);
                }
            }
            return rects;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:{
                    //playVideo("VideoPanel0");
                    break;
                }
                case 1:{
                    showMap();
                    break;
                }
                case 2:{
                    showUserInfo();
                    break;
                }
            }
        }

        private void btn_del_alarm_Click(object sender, EventArgs e)
        {
            log.IsAlarmed = 1;
            log.AlarmDealTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            log.AlarmDealName = this.oprName;
            //值班日志
            log.ZbLog = this.txt_zblog.Text;
            AlarmLogBll.UpdateLog(log);

            this.btn_del_alarm.Enabled = false;
            //通道主界面重加载日志
            Thread temp = new Thread(new ThreadStart(delegate()
            {
                if (frm != null)
                    frm.BindLogs();
            }
            ));
            temp.Start();
        }

        private void treeView_cameras_DoubleClick(object sender, EventArgs e)
        {
            //切换通道播放
            this.tabControl1.SelectTab(0);

            Channel c = (Channel)this.treeView_cameras.SelectedNode.Tag;
            this.Text = this.dev.DeviceName + "->" + c.ChannelName;

            this.channelId = c.ChannelID;
            this.nchannelId = int.Parse(c.ChannelNum)-1;

            stopVideo(this.curPanel.Name);
            playVideo(this.curPanel.Name);
        }


        private void treeView_cameras_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.curCameraNode = e.Node;
        }

        private void ToolStripMenuItem_Paly1Video_Click(object sender, EventArgs e)
        {
            try
            {
                stopVideo(this.curPanel.Name);
                this.nStream = 0;//主码
                playVideo(this.curPanel.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "主码异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripMenuItem_Play2Video_Click(object sender, EventArgs e)
        {
            stopVideo(this.curPanel.Name);
            this.nStream = 1;//子码
            playVideo(this.curPanel.Name);
        }

        private void ToolStripMenuItem_stopVideo_Click(object sender, EventArgs e)
        {
            stopVideo(this.curPanel.Name);
        }

        private void treeView_alarms_AfterSelect(object sender, TreeViewEventArgs e)
        {
            curAlarmNode = e.Node;
            if (curAlarmNode.ImageIndex == 2)
            {
                ToolStripMenuItem_alarmSet.Enabled = false;
                ToolStripMenuItem_alarmNoSet.Enabled = true;
            }
            else if (curAlarmNode.ImageIndex == 3)
            {
                ToolStripMenuItem_alarmSet.Enabled = true;
                ToolStripMenuItem_alarmNoSet.Enabled = false;
            }
            else{
                ToolStripMenuItem_alarmSet.Enabled = false;
                ToolStripMenuItem_alarmNoSet.Enabled = false;
            }
        }

        private void ToolStripMenuItem_showOprBtn_Click(object sender, EventArgs e)
        {
            return;
            //this.splitContainer_video.Panel2Collapsed = !this.splitContainer_video.Panel2Collapsed;
        }

        private void btn_split_11_Click(object sender, EventArgs e)
        {
            stopAllVideo();
            //分割1*1
            this.panel_video.Controls.Clear();
            SplitPanel(1,1);
        }

        private void btn_split_22_Click(object sender, EventArgs e)
        {
            stopAllVideo();
            this.panel_video.Controls.Clear();
            SplitPanel(2, 2);
        }

        private void btn_split_33_Click(object sender, EventArgs e)
        {
            stopAllVideo();
            this.panel_video.Controls.Clear();
            SplitPanel(3, 3);
        }

        private void btn_split_44_Click(object sender, EventArgs e)
        {
            stopAllVideo();
            this.panel_video.Controls.Clear();
            SplitPanel(4, 4);
        }

        private void btn_split_55_Click(object sender, EventArgs e)
        {
            stopAllVideo();
            this.panel_video.Controls.Clear();
            SplitPanel(5, 5);
        }

        private void btn_rec_Click(object sender, EventArgs e)
        {
            //抓录
            this.btn_stop_rec.Visible = true;
            SaveFileDialog cl_saveFileDialog = new SaveFileDialog();
            CultureInfo ci = CultureInfo.InvariantCulture;
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss",ci);
            cl_saveFileDialog.Filter = "H264 Files | *.h264";
            cl_saveFileDialog.DefaultExt = "h264";
            cl_saveFileDialog.FileName = this.deviceId + "-" + this.channelId + "-" + nowTime + ".h264";
            DialogResult ret = cl_saveFileDialog.ShowDialog();
            if (ret == DialogResult.OK)
            {
                string saveFile = cl_saveFileDialog.FileName;
                bool result = NetSDK.H264_DVR_StartLocalRecord(this.handelGla, saveFile,0);
            }
            else if (ret == DialogResult.Cancel)
            {
                this.btn_stop_rec.Visible = false;
            }
        }

        private void btn_stop_rec_Click(object sender, EventArgs e)
        {
            //停止抓录
            bool ret = NetSDK.H264_DVR_StopLocalRecord(this.handelGla);
            this.btn_stop_rec.Visible = false;
        }

        private void btn_speak_Click(object sender, EventArgs e)
        {
            try
            {
                //对讲
                this.btn_stopspeak.Visible = true;
                bool ret = true;
                //ret = NetSDK.H264_DVR_OpenSound(this.handelGla);
                if (ret)
                {//打开PC音频成功
                    //打开对讲
                    this.speakHandel = NetSDK.H264_DVR_StartLocalVoiceCom(this.df.lhwd);
                    //收集PC语音
                    //ret = H264Play.H264_PLAY_StartAudioCapture();
                    //IntPtr voice = new IntPtr(1234567890);
                    //ret = NetSDK.H264_DVR_VoiceComSendData((long)this.speakHandel, voice, 10);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void btn_stopspeak_Click(object sender, EventArgs e)
        {
            ////停止PC音频
            //NetSDK.H264_DVR_CloseSound(this.handelGla);
            ////停止对讲
            //bool ret = NetSDK.H264_DVR_CloseSound(this.speakHandel);
            if(NetSDK.H264_DVR_StopVoiceCom(this.speakHandel))
                this.btn_stopspeak.Visible = false;
        }
        private void btn_rec_his_Click(object sender, EventArgs e)
        {
            //录像回放
            FrmPlayBack playBack = new FrmPlayBack(this.nchannelId, this.df.lhwd,this.cameraNodes.Count);

            if (this.df.lhwd == 0)
            {
                MessageBox.Show("设备登陆失败!");
            }
            else
            {
                if (this.cameraNodes.Count > 0)
                {
                    playBack.ShowDialog();
                }
            }
            
            
        }
        //云台
        //private void ptzCtrl(string szCameraId, TransSDK.P_CLIENT_PTZCommand nCmd, int nId,int nCruise,int nPreset,int nTime)
        //{
        //    int ret;

        //    switch(nCmd)
        //    {
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_UP: //上
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_DOWN:    //下
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_LEFT:                          //左
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_RIGHT:                         //右
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_LEFTUP:                        //左上
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_LEFTDOWN:                      //左下
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_RIGHTUP:						//右上
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_RIGHTDOWN:                     //右下
        //        {
        //            int nSpeed = 1;
        //            ret = TransSDK.P_Client_PTZControl(szCameraId,(int)nCmd,nSpeed,0,0,true);
        //            break;
        //        }
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_GO_PRESET://定位到
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_PRESET_ADD://预置点设置 增加
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_PRESET_DEL://预置点设置 删除
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_PANCRUISE://线扫
        //        {
        //            ret = TransSDK.P_Client_PTZControl(szCameraId, (int)nCmd, nId, 0, 0, false);
        //            break;
        //        }
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_ADD_ZOOM: //变倍+        //Camera operation 的枚举顺序勿改，关系其他代码
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_ADD_FOCUS:					//变焦+
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_ADD_APERTURE:                  //光圈+
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_REDUCE_ZOOM:                   //变倍-
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_REDUCE_FOCUS:                  //变焦-
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_REDUCE_APERTURE:               //光圈-
        //        {
        //            ret = TransSDK.P_Client_PTZControl(szCameraId, (int)nCmd, 0, 0, 0, true);
        //            break;
        //        }
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_TOUR_START://启用
        //        {
        //            ret = TransSDK.P_Client_PTZControl(szCameraId, (int)nCmd, nId, 0, 0, false);
        //            break;
        //        }
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_TOUR_STOP://设置
        //        {
        //            ret = TransSDK.P_Client_PTZControl(szCameraId, (int)nCmd, 0, 0, 0, true);
        //            break;
        //        }
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_TOUR_ADD://巡航设置 增加
        //        case TransSDK.P_CLIENT_PTZCommand.P_CLIENT_PTZ_TOUR_DEL:
        //        {
        //            ret = TransSDK.P_Client_PTZControl(szCameraId,(int)nCmd,nCruise,nPreset,nTime,false);
        //            break;
        //        }
        //        default:
        //             break;
        //    } 
            
        //}

        private void ptzCtrl(Int32 loginId, NetSDK.PTZ_ControlType nCmd, int nId, int nCruise, int nPreset, int nTime,int nChannelNo,bool bstop=false)
        {
            bool ret;
            switch (nCmd)
            {
                case NetSDK.PTZ_ControlType.TILT_UP:                            //上
                case NetSDK.PTZ_ControlType.TILT_DOWN:                          //下
                case NetSDK.PTZ_ControlType.PAN_LEFT:                           //左
                case NetSDK.PTZ_ControlType.PAN_RIGHT:                          //右
                case NetSDK.PTZ_ControlType.PAN_LEFTTOP:                        //左上
                case NetSDK.PTZ_ControlType.PAN_LEFTDOWN:                       //左下
                case NetSDK.PTZ_ControlType.PAN_RIGTHTOP:						//右上
                case NetSDK.PTZ_ControlType.PAN_RIGTHDOWN:                      //右下
                    {
                        ret = NetSDK.H264_DVR_PTZControl(loginId, nChannelNo, (Int32)nCmd, bstop);
                        break;
                    }
                case NetSDK.PTZ_ControlType.EXTPTZ_POINT_SET_CONTROL://定位到
                case NetSDK.PTZ_ControlType.EXTPTZ_POINT_MOVE_CONTROL://预置点设置 增加
                case NetSDK.PTZ_ControlType.EXTPTZ_POINT_DEL_CONTROL://预置点设置 删除
                    {
                        ret = NetSDK.H264_DVR_PTZControlEx(loginId, nChannelNo, (Int32)nCmd, nPreset, 2, 3, bstop);
                        break;
                    }
                case NetSDK.PTZ_ControlType.ZOOM_IN:                    //变倍+       
                case NetSDK.PTZ_ControlType.FOCUS_FAR:					//变焦+
                case NetSDK.PTZ_ControlType.IRIS_OPEN:                  //光圈+
                case NetSDK.PTZ_ControlType.ZOOM_OUT:                   //变倍-
                case NetSDK.PTZ_ControlType.FOCUS_NEAR:                 //变焦-
                case NetSDK.PTZ_ControlType.IRIS_CLOSE:                 //光圈-
                    {
                        ret = NetSDK.H264_DVR_PTZControl(loginId, nChannelNo, (Int32)nCmd, bstop);
                        break;
                    }
                case NetSDK.PTZ_ControlType.EXTPTZ_POINT_LOOP_CONTROL://开始巡航
                case NetSDK.PTZ_ControlType.EXTPTZ_POINT_STOP_LOOP_CONTROL://停止巡航
                    {
                        ret = NetSDK.H264_DVR_PTZControlEx(loginId, nChannelNo, (Int32)nCmd, nCruise, 2, 3, bstop);
                        break;
                    }
                default:
                    
                    break;
            }

        }
        //云台控制部分
        private void btn_ptz_leftup_Click(object sender, EventArgs e)
        {
            //int step = int.Parse(this.txt_steplen.Text);
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_LEFTTOP, step, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_up_Click(object sender, EventArgs e)
        {
            //int step = int.Parse(this.txt_steplen.Text);
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.TILT_UP, step, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_rightup_Click(object sender, EventArgs e)
        {
            //int step = int.Parse(this.txt_steplen.Text);
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_RIGTHTOP, step, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_left_Click(object sender, EventArgs e)
        {
            //int step = int.Parse(this.txt_steplen.Text);
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_LEFT, step, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_right_Click(object sender, EventArgs e)
        {
           
        }

        private void btn_ptz_leftdown_Click(object sender, EventArgs e)
        {
            //int step = int.Parse(this.txt_steplen.Text);
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_LEFTDOWN, step, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_down_Click(object sender, EventArgs e)
        {
            //int step = int.Parse(this.txt_steplen.Text);
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.TILT_DOWN, step, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_rightdown_Click(object sender, EventArgs e)
        {
            //int step = int.Parse(this.txt_steplen.Text);
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_RIGTHDOWN, step, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_za_Click(object sender, EventArgs e)
        {
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.ZOOM_IN, 0, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_zm_Click(object sender, EventArgs e)
        {
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.ZOOM_OUT, 0, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_fa_Click(object sender, EventArgs e)
        {
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.FOCUS_FAR, 0, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_fm_Click(object sender, EventArgs e)
        {
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.FOCUS_NEAR, 0, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_ra_Click(object sender, EventArgs e)
        {
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.IRIS_OPEN, 0, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_rm_Click(object sender, EventArgs e)
        {
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.IRIS_CLOSE, 0, 0, 0, 0, this.nchannelId);
        }

        private void btn_ptz_preset_add_Click(object sender, EventArgs e)
        {
            //int preset = int.Parse(this.cb_preset.Text);
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.EXTPTZ_POINT_MOVE_CONTROL, 0, 0, preset, 0, this.nchannelId);
        }

        private void btn_ptz_preset_del_Click(object sender, EventArgs e)
        {
            //int preset = int.Parse(this.cb_preset.Text);
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.EXTPTZ_POINT_DEL_CONTROL, 0, 0, preset, 0, this.nchannelId);
        }

        private void btn_ptz_preset_go_Click(object sender, EventArgs e)
        {
            //int preset = int.Parse(this.cb_preset.Text);
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.EXTPTZ_POINT_SET_CONTROL, 0, 0, preset, 0, this.nchannelId);
        }

        private void btn_points_start_Click(object sender, EventArgs e)
        {
            //int cruise = int.Parse(this.cb_tour.Text);
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.EXTPTZ_POINT_LOOP_CONTROL, 0, cruise, 0, 0, this.nchannelId);
        }

        private void btn_points_stop_Click(object sender, EventArgs e)
        {
            //int cruise = int.Parse(this.cb_tour.Text);
            //this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.EXTPTZ_POINT_STOP_LOOP_CONTROL, 0, cruise, 0, 0, this.nchannelId);
        }

        private void btn_points_edit_Click(object sender, EventArgs e)
        {
            string cruise = this.cb_tour.Text;
            FrmCruiseEdit fce = new FrmCruiseEdit(this.df.lhwd, this.nchannelId,cruise);
            fce.ShowDialog();
        }

        private void ToolStripMenuItem_camera_play_Click(object sender, EventArgs e)
        {
            if (this.curPanel == null)
            {
                MessageBox.Show("请选择播放窗口！", "播放异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Channel c = (Channel)this.treeView_cameras.SelectedNode.Tag;
            //this.Text = this.dev.DeviceName + "->" + c.ChannelName;

            this.channelId = c.ChannelID;
            this.nchannelId = int.Parse(c.ChannelNum)-1;

            //播放或停止当前通道
            stopVideo(this.curPanel.Name);
            this.nStream = 1;//子码
            playVideo(this.curPanel.Name);
        }

        /// <summary>
        /// 摄像头布防
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_camera_set_Click(object sender, EventArgs e)
        {
            if (curCameraNode.ImageIndex == 3)
            {
                //布防
                int num = int.Parse(((Channel)curCameraNode.Tag).ChannelNum)-1;
                int bSuccess = df.SetMotionCfg(num, 1, 1);
                if (bSuccess > 0)
                {
                    curCameraNode.ImageIndex = 2;
                    MessageBox.Show("视频通道" + (num+1) + "布防成功！", "布防", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show("视频通道" + (num+1) + "布防失败！", "布防", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 摄像头撤防
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_camera_noset_Click(object sender, EventArgs e)
        {
            if (curCameraNode.ImageIndex == 2)
            {
                //撤防
                int num = int.Parse(((Channel)curCameraNode.Tag).ChannelNum)-1;
                int bSuccess = df.SetMotionCfg(num, 0, 1);
                if (bSuccess > 0)
                {
                    curCameraNode.ImageIndex = 3;
                    MessageBox.Show("视频通道" + (num+1) + "撤防成功！", "布防", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show("视频通道" + (num+1) + "撤防失败！", "布防", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void ToolStripMenuItem_alarmSet_Click(object sender, EventArgs e)
        {
            if (curAlarmNode.ImageIndex == 3)
            {
                //布防
                int bSuccess = df.SetAlarmInputCfg((int)curAlarmNode.Tag, 1, 1);
                if (bSuccess > 0)
                {
                    curAlarmNode.ImageIndex = 2;
                    MessageBox.Show("报警通道" + ((int)curAlarmNode.Tag + 1) + "布防成功！", "布防", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show("报警通道" + ((int)curAlarmNode.Tag + 1) + "布防失败！", "布防", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripMenuItem_alarmNoSet_Click(object sender, EventArgs e)
        {
            if (curAlarmNode.ImageIndex == 2)
            {
                //撤防
                int bSuccess = df.SetAlarmInputCfg((int)curAlarmNode.Tag, 0, 1);
                if (bSuccess > 0)
                {
                    curAlarmNode.ImageIndex = 3;
                    MessageBox.Show("报警通道" + ((int)curAlarmNode.Tag + 1) + "撤防成功！", "布防", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show("报警通道" + ((int)curAlarmNode.Tag + 1) + "撤防失败！", "布防", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeView_sensors_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        unsafe private bool openTransportRs232(string szDeviceID)
        {
            fCallBack = new NetSDK.fTransComCallBack(this.transportDataCallBack);

            NetSDK.TransComChannel transComChannel = new NetSDK.TransComChannel();
            transComChannel.TransComType = NetSDK.SERIAL_TYPE.RS232;
            transComChannel.baudrate = 38400;
            transComChannel.databits = 8;
            transComChannel.parity = 0;
            transComChannel.stopbits = 1;

            IntPtr ptransComChannel = Marshal.AllocCoTaskMem(Marshal.SizeOf(transComChannel));
            Marshal.StructureToPtr(transComChannel, ptransComChannel, false);
            bool ret = false;
            int tryNum = 0;
            //-11405
            while (!ret&&tryNum < 5)
            {
                ret = NetSDK.H264_DVR_OpenTransComChannel(df.lhwd, ptransComChannel, fCallBack, 0);
                if (!ret)
                    logger.Error("H264_DVR_OpenTransComChannel error=" + NetSDK.H264_DVR_GetLastError());
                tryNum++;
            }

            return ret;
        }
        private bool stopTransportRs232()
        {
            return NetSDK.H264_DVR_CloseTransComChannel(df.lhwd, NetSDK.SERIAL_TYPE.RS232);
        }
        private void transportDataCallBack(Int32 lLoginID, int lTransComType, IntPtr pBuffer, UInt32 dwBufSize, UInt32 dwUser)
        {
            byte[] data = new byte[dwBufSize+1];
            Marshal.Copy(pBuffer, data, 0, (int)dwBufSize);
            //for (int i = 0; i < dwBufSize; i++)
            //{
            //    logger.Error("data[" + i + "]=" + data[i]);
            //}
            //解析数据
            int command = data[2];
            int datalen = data[3];

            int wireChannelId;
            int wirelessChannelId;
            logger.Error("透明串口消息 command=" + command);
            switch (command)
            {
                case 0X81://0X01 读取系统状态 回复
                    {
                        sysStatus = data[3 + 1];//1 布防 0 撤防
                        alarmSound = data[3 + 2];
                        lowVolInput = data[3 + 3];
                        wireAreaNum = data[3 + 4];

                        wireAreaStatus[0] = GetbitValue(data[3 + 5],0);
                        wireAreaStatus[1] = GetbitValue(data[3 + 5], 1);
                        wireAreaStatus[2] = GetbitValue(data[3 + 5], 2);
                        wireAreaStatus[3] = GetbitValue(data[3 + 5], 3);

                        wirelessStudyNum = data[3 + 6];

                        wirelessAreaStatus[0] = GetbitValue(data[3 + 7], 0);
                        wirelessAreaStatus[1] = GetbitValue(data[3 + 7], 1);
                        wirelessAreaStatus[2] = GetbitValue(data[3 + 7], 2);
                        wirelessAreaStatus[3] = GetbitValue(data[3 + 7], 3);
                        wirelessAreaStatus[4] = GetbitValue(data[3 + 7], 4);
                        wirelessAreaStatus[5] = GetbitValue(data[3 + 7], 5);
                        wirelessAreaStatus[6] = GetbitValue(data[3 + 7], 6);
                        wirelessAreaStatus[7] = GetbitValue(data[3 + 7], 7);
                        wirelessAreaStatus[8] = GetbitValue(data[3 + 8], 0);
                        wirelessAreaStatus[9] = GetbitValue(data[3 + 8], 1);
                        wirelessAreaStatus[10] = GetbitValue(data[3 + 8], 2);
                        wirelessAreaStatus[11] = GetbitValue(data[3 + 8], 3);
                        wirelessAreaStatus[12] = GetbitValue(data[3 + 8], 4);
                        wirelessAreaStatus[13] = GetbitValue(data[3 + 8], 5);
                        wirelessAreaStatus[14] = GetbitValue(data[3 + 8], 6);
                        wirelessAreaStatus[15] = GetbitValue(data[3 + 8], 7);

                        kglStatus[0] = GetbitValue(data[3 + 10], 0);
                        kglStatus[1] = GetbitValue(data[3 + 10], 1);
                        kglStatus[2] = GetbitValue(data[3 + 10], 2);
                        kglStatus[3] = GetbitValue(data[3 + 10], 3);
                        kglStatus[4] = GetbitValue(data[3 + 10], 4);
                        kglStatus[5] = GetbitValue(data[3 + 10], 5);
                        kglStatus[6] = GetbitValue(data[3 + 10], 6);
                        kglStatus[7] = GetbitValue(data[3 + 10], 7);

                        UpdateUI uui = new UpdateUI(this.UpdateStatusUI);
                        this.BeginInvoke(uui);
                        break;
                    }
                case 0X82://0X02 设置布防 撤防 回复
                    {
                        break;
                    }
                case 0X83://0X03 设置报警声开启 关闭 回复
                    {
                        break;
                    }
                case 0X84://0X04 设置有线防区有效电平高 低 回复
                    {
                        break;
                    }
                case 0X85://0X05 设置4路有线报警无效 有效 回复
                    {
                        break;
                    }
                case 0X86://0X06 设置16路无线防区报警无效 有效 回复 //0X07 继电器控制
                    {
                        break;
                    }
                case 0X40://遥控手动报警
                    {
                        break;
                    }
                case 0X41://遥控布防
                    {
                        //全部布防
                        break;
                    }
                case 0X42://遥控撤防
                    {
                        //全部撤防
                        break;
                    }
                case 0X43://有线线防区报警
                    {
                        wireChannelId = data[3 + 1];
                        break;
                    }
                case 0X44://无线防区报警
                    {
                        wirelessChannelId = data[3 + 1];
                        break;
                    }
                default: break;
            }
        }
        private bool searchFqStatus()
        {
            if (!isTransportOpen) isTransportOpen = openTransportRs232(this.deviceId);

            int nBufLen = 6;

            byte[] data = new byte[] { 0xFF, 0x55, 0x01, 0x00, 0x01 };

            return df.SendTransportData(data, nBufLen);
        }
        private void UpdateStatusUI()
        {
            //设置有线防区按钮状态
            for (int i = 0; i < 4; i++)
            {
                string cname = "btn_fq_wire0" + (i + 1);
                SimpleButton btn_fq_wire = (SimpleButton)groupControl_bcf.Controls.Find(cname, false)[0];
                if (wireAreaStatus[i] == 1)
                    btn_fq_wire.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
                else
                    btn_fq_wire.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            }
            //设置无线防区状态
            for (int i = 0; i < 16; i++)
            {
                string cname = "btn_fq_wireless" + String.Format("{0:D2}", i + 1);
                SimpleButton btn_fq_wireless = (SimpleButton)groupControl_bcf.Controls.Find(cname, false)[0];
                if (wirelessAreaStatus[i] == 1)
                    btn_fq_wireless.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
                else
                    btn_fq_wireless.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            }
            //设置继电器状态
            for (int i = 0; i < 8; i++)
            {
                string cname = "btn_kgl" + String.Format("{0:D2}", i + 1);
                SimpleButton btn_kgl = (SimpleButton)this.groupControl_kgl.Controls.Find(cname, false)[0];
                if (kglStatus[i] == 1)
                    btn_kgl.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
                else
                    btn_kgl.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            }
        }

        /// <summary>
        /// 获取数据中某一位的值
        /// </summary>
        /// <param name="input">传入的数据类型,可换成其它数据类型,比如Int</param>
        /// <param name="index">要获取的第几位的序号,从0开始</param>
        /// <returns>返回值为-1表示获取值失败</returns>
        private int GetbitValue(byte input, int index)
        {
            return ((input & (1 << index)) > 0) ? 1 : 0; 
        }

        private void logquery_button_Click(object sender, EventArgs e)
        {
            string channelName = this.channelname_textbox.Text;
            DateTime alarmstartTime = this.alarmstart_dateTimePicker.Value;
            DateTime alarmendTime = this.alarmend_dateTimePicker.Value;
            List<MDS.Domain.AlarmLog> logs = AlarmLogBll.QueryLogsByDeviceIdAndChannelName(deviceId, channelName, alarmstartTime, alarmendTime);
            this.log_dataview.DataSource = null;
            this.log_dataview.DataSource = logs;
        }

        private void btn_del_allalarm_Click(object sender, EventArgs e)
        {
            AlarmLogBll.UpdateAllLog(this.oprName);

            //通道主界面重加载日志
            Thread temp = new Thread(new ThreadStart(delegate()
            {
                if (frm != null)
                    frm.BindLogs();
            }
            ));
            temp.Start();
        }

        private void treeView_cameras_Leave(object sender, EventArgs e)
        {
            //失去焦点
            treeView_cameras.SelectedNode = null;
        }

        private void treeView_alarms_Leave(object sender, EventArgs e)
        {
            //失去焦点
            treeView_alarms.SelectedNode = null;
        }

        private void btn_channel_Click(object sender, EventArgs e)
        {
            SimpleButton btn = (SimpleButton)sender;
            if (btn.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            else
                btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;

            switch (btn.Name)
            {
                default:
                break;
            }
        }

        private void btn_alarm_search_Click(object sender, EventArgs e)
        {
            searchFqStatus();
        }

        private void btn_alarm_save_Click(object sender, EventArgs e)
        {
            //防区布撤防设置
            //有线防区
            int nBufLen = 6;
            byte status = 0x00;
            if (this.btn_fq_wire01.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status += (byte)System.Math.Pow(2, 0);
            if (this.btn_fq_wire02.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status += (byte)System.Math.Pow(2, 1);
            if (this.btn_fq_wire03.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status += (byte)System.Math.Pow(2, 2);
            if (this.btn_fq_wire04.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status += (byte)System.Math.Pow(2, 3);
            byte[] data = new byte[] { 0xFF, 0x55, 0x05, 0x01, status,(byte)(0x06+status) };
            df.SendTransportData(data, nBufLen);
            //无线防区
            nBufLen = 7;
            byte status1 = 0x00;
            byte status2 = 0x00;
            if (this.btn_fq_wireless01.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status1 += (byte)System.Math.Pow(2, 0);
            if (this.btn_fq_wireless02.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status1 += (byte)System.Math.Pow(2, 1);
            if (this.btn_fq_wireless03.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status1 += (byte)System.Math.Pow(2, 2);
            if (this.btn_fq_wireless04.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status1 += (byte)System.Math.Pow(2, 3);
            if (this.btn_fq_wireless05.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status1 += (byte)System.Math.Pow(2, 4);
            if (this.btn_fq_wireless06.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status1 += (byte)System.Math.Pow(2, 5);
            if (this.btn_fq_wireless07.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status1 += (byte)System.Math.Pow(2, 6);
            if (this.btn_fq_wireless08.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status1 += (byte)System.Math.Pow(2, 7);
            if (this.btn_fq_wireless09.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status2 += (byte)System.Math.Pow(2, 0);
            if (this.btn_fq_wireless10.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status2 += (byte)System.Math.Pow(2, 1);
            if (this.btn_fq_wireless11.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status2 += (byte)System.Math.Pow(2, 2);
            if (this.btn_fq_wireless12.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status2 += (byte)System.Math.Pow(2, 3);
            if (this.btn_fq_wireless13.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status2 += (byte)System.Math.Pow(2, 4);
            if (this.btn_fq_wireless14.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status2 += (byte)System.Math.Pow(2, 5);
            if (this.btn_fq_wireless15.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status2 += (byte)System.Math.Pow(2, 6);
            if (this.btn_fq_wireless16.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status2 += (byte)System.Math.Pow(2, 7);
            data = new byte[] { 0xFF, 0x55, 0x06, 0x02, status1, status2, (byte)(0x08 + status1 + status2) };
            df.SendTransportData(data, nBufLen);

            //日志
            SectorLog sectorLog = new SectorLog();
            sectorLog.ip = dev.DeviceIP;
            sectorLog.usr = dev.UserName;
            sectorLog.port = dev.Port;
            sectorLog.sn = lpDeviceInfo.sSerialNumber;
            sectorLog.remark = "有线防区、无线防区设置";
            MDSUtils.SaveSectorLog(sectorLog);
        }

        private void btn_kgl_search_Click(object sender, EventArgs e)
        {
            searchFqStatus();
        }

        private void btn_kgl_save_Click(object sender, EventArgs e)
        {
            //继电器开关量设置
            int nBufLen = 6;
            byte status = 0x00;
            if(btn_kgl01.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status += (byte)System.Math.Pow(2, 0);
            if (btn_kgl02.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status += (byte)System.Math.Pow(2, 1);
            if (btn_kgl03.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status += (byte)System.Math.Pow(2, 2);
            if (btn_kgl04.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status += (byte)System.Math.Pow(2, 3);
            if (btn_kgl05.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status += (byte)System.Math.Pow(2, 4);
            if (btn_kgl06.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status += (byte)System.Math.Pow(2, 5);
            if (btn_kgl07.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status += (byte)System.Math.Pow(2, 6);
            if (btn_kgl08.ButtonStyle == DevExpress.XtraEditors.Controls.BorderStyles.Office2003)
                status += (byte)System.Math.Pow(2, 7);
            byte[] data = new byte[] { 0xFF, 0x55, 0x07, 0x01, status, (byte)(0x08+status) };
            df.SendTransportData(data, nBufLen);

            //日志
            SectorLog sectorLog = new SectorLog();
            sectorLog.ip = dev.DeviceIP;
            sectorLog.usr = dev.UserName;
            sectorLog.port = dev.Port;
            sectorLog.sn = lpDeviceInfo.sSerialNumber;
            sectorLog.remark = "继电器设置";
            MDSUtils.SaveSectorLog(sectorLog);
        }

        private void btn_ptz_right_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_RIGHT, 0, 0, 0, 0, this.nchannelId,false);
        }

        private void btn_ptz_right_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_RIGHT, 0, 0, 0, 0, this.nchannelId, true);
        }

        private void btn_ptz_leftup_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_LEFTTOP, 0, 0, 0, 0, this.nchannelId,false);
        }

        private void btn_ptz_leftup_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_LEFTTOP, 0, 0, 0, 0, this.nchannelId, true);
        }

        private void btn_ptz_up_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.TILT_UP, 0, 0, 0, 0, this.nchannelId,false);
        }

        private void btn_ptz_up_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.TILT_UP, 0, 0, 0, 0, this.nchannelId,true);
        }

        private void btn_ptz_rightup_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_RIGTHTOP, 0, 0, 0, 0, this.nchannelId, false);
        }

        private void btn_ptz_rightup_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_RIGTHTOP, 0, 0, 0, 0, this.nchannelId, true);
        }

        private void btn_ptz_left_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_LEFT, 0, 0, 0, 0, this.nchannelId, false);
        }

        private void btn_ptz_left_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_LEFT, 0, 0, 0, 0, this.nchannelId, true);
        }

        private void btn_ptz_leftdown_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_LEFTDOWN, 0, 0, 0, 0, this.nchannelId, false);
        }

        private void btn_ptz_leftdown_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_LEFTDOWN, 0, 0, 0, 0, this.nchannelId, true);
        }

        private void btn_ptz_down_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.TILT_DOWN, 0, 0, 0, 0, this.nchannelId, false);
        }

        private void btn_ptz_down_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.TILT_DOWN, 0, 0, 0, 0, this.nchannelId, true);
        }

        private void btn_ptz_rightdown_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_RIGTHDOWN, 0, 0, 0, 0, this.nchannelId, false);
        }

        private void btn_ptz_rightdown_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.PAN_RIGTHDOWN, 0, 0, 0, 0, this.nchannelId, true);
        }

        private void btn_ptz_za_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.ZOOM_IN, 0, 0, 0, 0, this.nchannelId, false);
        }

        private void btn_ptz_za_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.ZOOM_IN, 0, 0, 0, 0, this.nchannelId, true);
        }

        private void btn_ptz_za_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void btn_ptz_za_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void btn_ptz_fa_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.FOCUS_FAR, 0, 0, 0, 0, this.nchannelId,false);
        }

        private void btn_ptz_fa_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.FOCUS_FAR, 0, 0, 0, 0, this.nchannelId,true);
        }

        private void btn_ptz_zm_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.ZOOM_OUT, 0, 0, 0, 0, this.nchannelId,false);
        }

        private void btn_ptz_zm_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.ZOOM_OUT, 0, 0, 0, 0, this.nchannelId,true);
        }

        private void btn_ptz_fm_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.FOCUS_NEAR, 0, 0, 0, 0, this.nchannelId,false);
        }

        private void btn_ptz_fm_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.FOCUS_NEAR, 0, 0, 0, 0, this.nchannelId,true);
        }

        private void btn_ptz_ra_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.IRIS_OPEN, 0, 0, 0, 0, this.nchannelId,false);
        }

        private void btn_ptz_ra_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.IRIS_OPEN, 0, 0, 0, 0, this.nchannelId,true);
        }

        private void btn_ptz_rm_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.IRIS_CLOSE, 0, 0, 0, 0, this.nchannelId,false);
        }

        private void btn_ptz_rm_MouseUp(object sender, MouseEventArgs e)
        {
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.IRIS_CLOSE, 0, 0, 0, 0, this.nchannelId,true);
        }

        private void btn_ptz_preset_add_MouseDown(object sender, MouseEventArgs e)
        {
            int preset = int.Parse(this.cb_preset.Text);
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.EXTPTZ_POINT_MOVE_CONTROL, 0, 0, preset, 0, this.nchannelId,false);
        }

        private void btn_ptz_preset_add_MouseUp(object sender, MouseEventArgs e)
        {
            int preset = int.Parse(this.cb_preset.Text);
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.EXTPTZ_POINT_MOVE_CONTROL, 0, 0, preset, 0, this.nchannelId,true);
        }

        private void btn_ptz_preset_del_MouseDown(object sender, MouseEventArgs e)
        {
            int preset = int.Parse(this.cb_preset.Text);
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.EXTPTZ_POINT_DEL_CONTROL, 0, 0, preset, 0, this.nchannelId,false);
        }

        private void btn_ptz_preset_del_MouseUp(object sender, MouseEventArgs e)
        {
            int preset = int.Parse(this.cb_preset.Text);
            this.ptzCtrl(this.df.lhwd, NetSDK.PTZ_ControlType.EXTPTZ_POINT_DEL_CONTROL, 0, 0, preset, 0, this.nchannelId,true);
        }

        private void loadBatteryInfo(string deviceId)
        {
            batterylog = MDSUtils.GetCurBatteryLog(deviceId);
        }

        private void btn_battery_search_Click(object sender, EventArgs e)
        {
            //历史记录查询
            FrmBattery fb = new FrmBattery(deviceId);
            fb.ShowDialog();
        }

    }
}
